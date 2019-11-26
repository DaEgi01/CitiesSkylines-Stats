using ColossalFramework.UI;
using Stats.Configuration;
using Stats.Localization;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Stats.Ui
{
    public class MainPanel : UIPanel
    {
        private UIDragHandleWithDragState uiDragHandle;
        private string modSystemName;
        private bool mapHasSnowDumps;
        private ConfigurationModel configuration;
        private LanguageResourceModel languageResource;
        private GameEngineService gameEngineService;

        private ItemPanel[] itemPanelsInIndexOrder;
        private ItemPanel[] itemPanelsInDisplayOrder;

        public void Initialize(
            string modSystemName,
            bool mapHasSnowDumps,
            ConfigurationModel configuration,
            LanguageResourceModel languageResource,
            GameEngineService gameEngineService)
        {
            this.modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            this.mapHasSnowDumps = mapHasSnowDumps;
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (this.configuration.MainPanelColumnCount < 1)
            {
                throw new ArgumentOutOfRangeException($"'{nameof(this.configuration.MainPanelColumnCount)}' parameter must be bigger or equal to 1.");
            }
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
            this.gameEngineService = gameEngineService;

            this.color = configuration.MainPanelBackgroundColor;
            this.name = modSystemName + "MainPanel";
            this.backgroundSprite = "GenericPanelLight";
            this.isInteractive = false;

            this.CreateAndAddDragHandle();
            this.CreateAndAddAllUiItems();
            this.UpdateLocalizedTooltips();

            this.relativePosition = this.configuration.MainPanelPosition;

            this.UpdateLayout();

            this.configuration.LayoutPropertyChanged += this.UpdateLayoutIfDirty;
            this.configuration.PositionChanged += this.UpdatePosition;
            this.languageResource.LanguageChanged += this.UpdateLocalizedTooltips;
            this.uiDragHandle.eventMouseUp += UiDragHandle_eventMouseUp;

            this.StartCoroutine(KeepUpdatingUICoroutine());
        }

        public override void OnDestroy()
        {
            this.configuration.LayoutPropertyChanged -= this.UpdateLayoutIfDirty;
            this.configuration.PositionChanged -= this.UpdatePosition;
            this.languageResource.LanguageChanged -= this.UpdateLocalizedTooltips;
            this.uiDragHandle.eventMouseUp -= UiDragHandle_eventMouseUp;

            base.OnDestroy();
        }

        private void CreateAndAddDragHandle()
        {
            var dragHandle = this.AddUIComponent<UIDragHandleWithDragState>();
            dragHandle.name = this.modSystemName + "DragHandle";
            dragHandle.relativePosition = Vector2.zero;
            dragHandle.target = this;
            dragHandle.constrainToScreen = true;
            dragHandle.SendToBack();
            this.uiDragHandle = dragHandle;
        }

        private void CreateAndAddAllUiItems()
        {
            this.itemPanelsInIndexOrder = Item.AllItems
                .Select(i => this.configuration.GetConfigurationItem(i))
                .Select(ci => this.CreateUiItemAndAddButtons(ci))
                .OrderBy(ci => ci.ConfigurationItem.Item.Index)
                .ToArray();

            ValidateIndexes(this.itemPanelsInIndexOrder);

            if (!mapHasSnowDumps)
            {
                this.itemPanelsInIndexOrder[Item.SnowDump.Index].isVisible = false;
                this.itemPanelsInIndexOrder[Item.SnowDumpVehicles.Index].isVisible = false;
            }

            this.itemPanelsInDisplayOrder = this.itemPanelsInIndexOrder
                .OrderBy(x => x.ConfigurationItem.SortOrder)
                .ToArray();
        }

        private void ValidateIndexes(ItemPanel[] itemPanel)
        {
            for (int i = 0; i < itemPanel.Length; i++)
            {
                if (i != itemPanel[i].ConfigurationItem.Item.Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        private ItemPanel CreateUiItemAndAddButtons(ConfigurationItemModel configurationItem)
        {
            var uiItem = this.CreateAndAddItemPanel();
            uiItem.Initialize(this.configuration, configurationItem, this.languageResource);
            return uiItem;
        }

        private ItemPanel CreateAndAddItemPanel()
        {
            var itemPanel = this.AddUIComponent<ItemPanel>();
            itemPanel.width = this.configuration.ItemWidth;
            itemPanel.height = this.configuration.ItemHeight;
            itemPanel.zOrder = zOrder;
            return itemPanel;
        }

        private void UpdateLocalizedTooltips()
        {
            for (int i = 0; i < itemPanelsInIndexOrder.Length; i++)
            {
                itemPanelsInIndexOrder[i].UpdateLocalizedTooltips();
            }
        }

        private void UpdateLayoutIfDirty()
        {
            var anyItemPanelVisibilityChanged = false;
            for (int i = 0; i < this.itemPanelsInIndexOrder.Length; i++)
            {
                var itemPanel = this.itemPanelsInIndexOrder[i];
                if (itemPanel.ItemVisibility.isVisibleChanged)
                {
                    anyItemPanelVisibilityChanged = true;
                    break;
                }
            }

            if (anyItemPanelVisibilityChanged)
            {
                this.UpdateLayout();
            }
        }

        private void UpdateLayout()
        {
            this.UpdateItemsLayout();
            this.UpdatePanelSize();
        }

        public override void Update()
        {
            if (this.configuration.MainPanelAutoHide && !this.containsMouse)
            {
                this.opacity = 0;
            }
            else
            {
                this.opacity = 1;
            }
        }

        private IEnumerator KeepUpdatingUICoroutine()
        {
            while (true)
            {
                yield return StartCoroutine(UpdateUICoroutine());
            }
        }

        private IEnumerator UpdateUICoroutine()
        {
            if (this.configuration.GetConfigurationItem(Item.AverageIllnessRate).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.AverageIllnessRate.Index].Percent =
                    this.gameEngineService.GetAverageIllnessRate();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Cemetery).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Cemetery.Index].Percent =
                    this.gameEngineService.GetCemeteryPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.CemeteryVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.CrematoriumVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.HealthcareVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.MedicalHelicopters).Enabled)
            {
                var healthCareVehiclesPercent = this.gameEngineService.GetHealthCareVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.CemeteryVehicles.Index].Percent =
                    healthCareVehiclesPercent.cemeteryVehiclesPercent;
                this.itemPanelsInIndexOrder[Item.CrematoriumVehicles.Index].Percent =
                    healthCareVehiclesPercent.crematoriumVehiclesPercent;
                this.itemPanelsInIndexOrder[Item.HealthcareVehicles.Index].Percent =
                    healthCareVehiclesPercent.healthCareVehiclesPercent;
                this.itemPanelsInIndexOrder[Item.MedicalHelicopters.Index].Percent =
                    healthCareVehiclesPercent.medicalHelicoptersPercent;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.CityUnattractiveness).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.CityUnattractiveness.Index].Percent =
                    this.gameEngineService.GetCityUnattractivenessPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Crematorium).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Crematorium.Index].Percent =
                    this.gameEngineService.GetCrematoriumPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.CrimeRate).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.CrimeRate.Index].Percent =
                    this.gameEngineService.GetCrimePercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.DisasterResponseVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.DisasterResponseHelicopters).Enabled)
            {
                var disasterReponseVehiclesPercent = this.gameEngineService.GetDisasterResponseVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.DisasterResponseVehicles.Index].Percent =
                    disasterReponseVehiclesPercent.disasterResponseVehicles;
                this.itemPanelsInIndexOrder[Item.DisasterResponseHelicopters.Index].Percent =
                    disasterReponseVehiclesPercent.disasterResponseHelicopters;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.DrinkingWaterPollution).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.DrinkingWaterPollution.Index].Percent =
                    this.gameEngineService.GetDrinkingWaterPollutionPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Electricity).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Electricity.Index].Percent =
                    this.gameEngineService.GetElectricityPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.ElementarySchool).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.ElementarySchool.Index].Percent =
                    this.gameEngineService.GetElementarySchoolPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.FireDepartmentVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.FireHelicopters).Enabled)
            {
                var fireDepartmentVehiclesPercent = this.gameEngineService.GetFireDepartmentVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.FireDepartmentVehicles.Index].Percent =
                    fireDepartmentVehiclesPercent.fireDepartmentVehiclesPercent;
                this.itemPanelsInIndexOrder[Item.FireHelicopters.Index].Percent =
                    fireDepartmentVehiclesPercent.fireHelicoptersPercent;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.FireHazard).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.FireHazard.Index].Percent =
                    this.gameEngineService.GetFireHazardPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.GarbageProcessing).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.GarbageProcessing.Index].Percent =
                    this.gameEngineService.GetGarbageProcessingPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.GarbageProcessingVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.LandfillVehicles).Enabled)
            {
                var garbageVehiclesPercent = this.gameEngineService.GetGarbageVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.GarbageProcessingVehicles.Index].Percent =
                    garbageVehiclesPercent.garbageProcessingVehicles;
                this.itemPanelsInIndexOrder[Item.LandfillVehicles.Index].Percent =
                    garbageVehiclesPercent.landfillVehicles;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.GroundPollution).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.GroundPollution.Index].Percent =
                    this.gameEngineService.GetGroundPollutionPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Healthcare).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Healthcare.Index].Percent =
                    this.gameEngineService.GetHealthCarePercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Heating).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Heating.Index].Percent =
                    this.gameEngineService.GetHeatingPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.HighSchool).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.HighSchool.Index].Percent =
                    this.gameEngineService.GetHighSchoolPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Landfill).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Landfill.Index].Percent =
                    this.gameEngineService.GetLandfillPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.NoisePollution).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.NoisePollution.Index].Percent =
                    this.gameEngineService.GetNoisePollutionPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.ParkMaintenanceVehicles).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.ParkMaintenanceVehicles.Index].Percent =
                    this.gameEngineService.GetParkMaintenanceVehiclesPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.PoliceHoldingCells).Enabled
                || this.configuration.GetConfigurationItem(Item.PoliceVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.PrisonCells).Enabled
                || this.configuration.GetConfigurationItem(Item.PrisonVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.PoliceHelicopters).Enabled)
            {
                var policeDepartmentVehiclesPercent = this.gameEngineService.GetPoliceDepartmentVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.PoliceHelicopters.Index].Percent =
                    policeDepartmentVehiclesPercent.policeHelicoptersPercent;
                this.itemPanelsInIndexOrder[Item.PoliceHoldingCells.Index].Percent =
                    policeDepartmentVehiclesPercent.policeHoldingCellsPercent;
                this.itemPanelsInIndexOrder[Item.PoliceVehicles.Index].Percent =
                    policeDepartmentVehiclesPercent.policeVehiclesPercent;
                this.itemPanelsInIndexOrder[Item.PrisonCells.Index].Percent =
                    policeDepartmentVehiclesPercent.prisonCellsPercent;
                this.itemPanelsInIndexOrder[Item.PrisonVehicles.Index].Percent =
                    policeDepartmentVehiclesPercent.prisonVehiclesPercent;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Taxis).Enabled
                || this.configuration.GetConfigurationItem(Item.PostVans).Enabled
                || this.configuration.GetConfigurationItem(Item.PostTrucks).Enabled
                )
            {
                var postAndTaxiVehiclesPercent = this.gameEngineService.GetPostAndTaxiVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.Taxis.Index].Percent =
                    postAndTaxiVehiclesPercent.taxisPercent;
                this.itemPanelsInIndexOrder[Item.PostVans.Index].Percent =
                    postAndTaxiVehiclesPercent.postVansPercent;
                this.itemPanelsInIndexOrder[Item.PostTrucks.Index].Percent =
                    postAndTaxiVehiclesPercent.postTrucksPercent;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.RoadMaintenanceVehicles).Enabled
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItem(Item.SnowDump).Enabled)
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItem(Item.SnowDumpVehicles).Enabled))
            {
                var roadMaintenanceAndSnowDumpVehiclesPercent =
                    this.gameEngineService.GetRoadMaintenanceAndSnowDumpVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.RoadMaintenanceVehicles.Index].Percent =
                    roadMaintenanceAndSnowDumpVehiclesPercent.roadMaintenanceVehiclesPercent;

                if (this.mapHasSnowDumps)
                {
                    this.itemPanelsInIndexOrder[Item.SnowDump.Index].Percent =
                        roadMaintenanceAndSnowDumpVehiclesPercent.snowDumpPercent;
                    this.itemPanelsInIndexOrder[Item.SnowDumpVehicles.Index].Percent =
                        roadMaintenanceAndSnowDumpVehiclesPercent.snowDumpVehiclesPercent;
                }

                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.SewageTreatment).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.SewageTreatment.Index].Percent =
                    this.gameEngineService.GetSewageTreatmentPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.TrafficJam).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.TrafficJam.Index].Percent =
                    this.gameEngineService.GetTrafficJamPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Unemployment).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Unemployment.Index].Percent =
                    this.gameEngineService.GetUnemploymentPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.University).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.University.Index].Percent =
                    this.gameEngineService.GetUniversityPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.Water).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Water.Index].Percent =
                    this.gameEngineService.GetWaterPercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.WaterPumpingServiceStorage).Enabled
                || this.configuration.GetConfigurationItem(Item.WaterPumpingServiceVehicles).Enabled)
            {
                var waterPumpingServiceVehiclesPercent =
                    this.gameEngineService.GetWaterPumpingServiceVehiclesPercent();

                this.itemPanelsInIndexOrder[Item.WaterPumpingServiceVehicles.Index].Percent =
                    waterPumpingServiceVehiclesPercent.waterPumpingServiceVehiclesPercent;
                this.itemPanelsInIndexOrder[Item.WaterPumpingServiceStorage.Index].Percent =
                    waterPumpingServiceVehiclesPercent.waterPumpingServiceStoragePercent;
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItem(Item.WaterReserveTank).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.WaterReserveTank.Index].Percent =
                    this.gameEngineService.GetWaterReservePercent();
                UpdateLayoutIfDirty();
                yield return new WaitForEndOfFrame();
            }

            //wait at least one frame, even if all Items are off.
            yield return new WaitForEndOfFrame();
        }

        private void UpdateItemsLayout()
        {
            var lastLayoutPosition = Vector2.zero;
            int index = 0;

            for (int i = 0; i < this.itemPanelsInDisplayOrder.Length; i++)
            {
                var currentItem = this.itemPanelsInDisplayOrder[i];
                if (!currentItem.isVisible)
                {
                    continue;
                }

                var layoutPosition = new Vector2(index % this.configuration.MainPanelColumnCount, index / this.configuration.MainPanelColumnCount);

                currentItem.relativePosition = CalculateRelativePosition(layoutPosition);
                currentItem.AdjustButtonAndUiItemSize();

                lastLayoutPosition = CalculateNextLayoutPosition(lastLayoutPosition);
                index++;
            }
        }

        private Vector2 CalculateNextLayoutPosition(Vector2 position)
        {
            if (position.x < this.configuration.MainPanelColumnCount - 1)
            {
                return new Vector2(position.x + 1, position.y);
            }
            else
            {
                return new Vector2(0, position.y + 1);
            }
        }

        private Vector3 CalculateRelativePosition(Vector2 layoutPosition)
        {
            var posX = (layoutPosition.x + 1) * this.configuration.ItemPadding + layoutPosition.x * this.configuration.ItemWidth;
            var posY = (layoutPosition.y + 1) * this.configuration.ItemPadding + layoutPosition.y * this.configuration.ItemHeight;

            return new Vector3(posX, posY);
        }

        private void UpdatePanelSize()
        {
            var visibleItemCount = this.itemPanelsInIndexOrder.Where(x => x.isVisible)
                .Count();
            if (visibleItemCount > 0)
            {
                this.isVisible = true;
            }
            else
            {
                this.isVisible = false;
                return;
            }

            var newWidth = this.CalculatePanelWidth(visibleItemCount);
            var newHeight = this.CalculatePanelHeight(visibleItemCount);

            this.width = newWidth;
            this.height = newHeight;

            this.uiDragHandle.width = newWidth;
            this.uiDragHandle.height = newHeight;
        }

        private void UpdatePosition()
        {
            if (uiDragHandle.IsDragged)
            {
                return;
            }

            this.relativePosition = this.configuration.MainPanelPosition;
        }

        private float CalculatePanelWidth(int visibleItemCount)
        {
            if (visibleItemCount < this.configuration.MainPanelColumnCount)
            {
                return (visibleItemCount + 1) * this.configuration.ItemPadding
                    + visibleItemCount * this.configuration.ItemWidth;
            }
            else
            {
                return (this.configuration.MainPanelColumnCount + 1) * this.configuration.ItemPadding
                    + this.configuration.MainPanelColumnCount * this.configuration.ItemWidth;
            }
        }

        private float CalculatePanelHeight(int visibleItemCount)
        {
            var rowCount = Mathf.CeilToInt(visibleItemCount / (float)this.configuration.MainPanelColumnCount);
            return (rowCount + 1) * this.configuration.ItemPadding + rowCount * this.configuration.ItemHeight;
        }

        private void UiDragHandle_eventMouseUp(UIComponent component, UIMouseEventParameter eventParam)
        {
            SaveMainPanelPosition();
        }

        private void SaveMainPanelPosition()
        {
            this.configuration.MainPanelPosition = this.relativePosition;
            this.configuration.Save();
        }
    }
}
