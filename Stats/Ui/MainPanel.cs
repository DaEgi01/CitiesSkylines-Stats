using ColossalFramework.UI;
using Stats.Config;
using Stats.Localization;
using Stats.Model;
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
        private Configuration configuration;
        private LanguageResource languageResource;
        private GameEngineService gameEngineService;

        private ItemPanel[] itemPanelsInIndexOrder;
        private ItemPanel[] itemPanelsInDisplayOrder;

        public void Initialize(
            string modSystemName,
            bool mapHasSnowDumps,
            Configuration configuration,
            LanguageResource languageResource,
            GameEngineService gameEngineService,
            ItemsInIndexOrder items)
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
            this.CreateAndAddAllUiItems(items);

            this.UpdateLayout();
            this.relativePosition = this.configuration.MainPanelPosition;
            this.UpdateLocalizedTooltips();

            this.configuration.LayoutPropertyChanged += this.UpdateLayout;
            this.configuration.PositionChanged += this.UpdatePosition;
            this.languageResource.LanguageChanged += this.UpdateLocalizedTooltips;
            this.uiDragHandle.eventMouseUp += UiDragHandle_eventMouseUp;

            this.StartCoroutine(KeepUpdatingUICoroutine());
        }

        public override void OnDestroy()
        {
            this.configuration.LayoutPropertyChanged -= this.UpdateLayout;
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

        private void CreateAndAddAllUiItems(ItemsInIndexOrder itemsInIndexOrder)
        {
            this.itemPanelsInIndexOrder = itemsInIndexOrder.Items
                .Select(i => this.CreateUiItemAndAddButtons(i))
                .ToArray();

            ValidateIndexes(this.itemPanelsInIndexOrder);

            if (!mapHasSnowDumps)
            {
                this.itemPanelsInIndexOrder[ItemData.SnowDump.Index].isVisible = false;
                this.itemPanelsInIndexOrder[ItemData.SnowDumpVehicles.Index].isVisible = false;
            }

            this.itemPanelsInDisplayOrder = this.itemPanelsInIndexOrder
                .OrderBy(x => x.Item.SortOrder)
                .ToArray();
        }

        private void ValidateIndexes(ItemPanel[] itemPanel)
        {
            for (int i = 0; i < itemPanel.Length; i++)
            {
                if (i != itemPanel[i].Item.ItemData.Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        private ItemPanel CreateUiItemAndAddButtons(Item item)
        {
            var uiItem = this.CreateAndAddItemPanel();
            uiItem.Initialize(this.configuration, item, this.languageResource);
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
            if (this.configuration.GetConfigurationItemData(ItemData.AverageIllnessRate).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.AverageIllnessRate.Index].Item.Percent =
                    this.gameEngineService.GetAverageIllnessRate();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Cemetery).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Cemetery.Index].Item.Percent =
                    this.gameEngineService.GetCemeteryPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CemeteryVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.CrematoriumVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.HealthcareVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.MedicalHelicopters).enabled)
            {
                var healthCareVehiclesPercent = this.gameEngineService.GetHealthCareVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.CemeteryVehicles.Index].Item.Percent =
                    healthCareVehiclesPercent.cemeteryVehiclesPercent;
                this.itemPanelsInIndexOrder[ItemData.CrematoriumVehicles.Index].Item.Percent =
                    healthCareVehiclesPercent.crematoriumVehiclesPercent;
                this.itemPanelsInIndexOrder[ItemData.HealthcareVehicles.Index].Item.Percent =
                    healthCareVehiclesPercent.healthCareVehiclesPercent;
                this.itemPanelsInIndexOrder[ItemData.MedicalHelicopters.Index].Item.Percent =
                    healthCareVehiclesPercent.medicalHelicoptersPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CityUnattractiveness).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.CityUnattractiveness.Index].Item.Percent =
                    this.gameEngineService.GetCityUnattractivenessPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Crematorium).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Crematorium.Index].Item.Percent =
                    this.gameEngineService.GetCrematoriumPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CrimeRate).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.CrimeRate.Index].Item.Percent =
                    this.gameEngineService.GetCrimePercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.DisasterResponseVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.DisasterResponseHelicopters).enabled)
            {
                var disasterReponseVehiclesPercent = this.gameEngineService.GetDisasterResponseVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.DisasterResponseVehicles.Index].Item.Percent =
                    disasterReponseVehiclesPercent.disasterResponseVehicles;
                this.itemPanelsInIndexOrder[ItemData.DisasterResponseHelicopters.Index].Item.Percent =
                    disasterReponseVehiclesPercent.disasterResponseHelicopters;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.DrinkingWaterPollution).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.DrinkingWaterPollution.Index].Item.Percent =
                    this.gameEngineService.GetDrinkingWaterPollutionPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Electricity).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Electricity.Index].Item.Percent =
                    this.gameEngineService.GetElectricityPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.ElementarySchool).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.ElementarySchool.Index].Item.Percent =
                    this.gameEngineService.GetElementarySchoolPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.FireDepartmentVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.FireHelicopters).enabled)
            {
                var fireDepartmentVehiclesPercent = this.gameEngineService.GetFireDepartmentVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.FireDepartmentVehicles.Index].Item.Percent =
                    fireDepartmentVehiclesPercent.fireDepartmentVehiclesPercent;
                this.itemPanelsInIndexOrder[ItemData.FireHelicopters.Index].Item.Percent =
                    fireDepartmentVehiclesPercent.fireHelicoptersPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.FireHazard).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.FireHazard.Index].Item.Percent =
                    this.gameEngineService.GetFireHazardPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.GarbageProcessing).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.GarbageProcessing.Index].Item.Percent =
                    this.gameEngineService.GetGarbageProcessingPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.GarbageProcessingVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.LandfillVehicles).enabled)
            {
                var garbageVehiclesPercent = this.gameEngineService.GetGarbageVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.GarbageProcessingVehicles.Index].Item.Percent =
                    garbageVehiclesPercent.garbageProcessingVehicles;
                this.itemPanelsInIndexOrder[ItemData.LandfillVehicles.Index].Item.Percent =
                    garbageVehiclesPercent.landfillVehicles;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.GroundPollution).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.GroundPollution.Index].Item.Percent =
                    this.gameEngineService.GetGroundPollutionPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Healthcare).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Healthcare.Index].Item.Percent =
                    this.gameEngineService.GetHealthCarePercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Heating).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Heating.Index].Item.Percent =
                    this.gameEngineService.GetHeatingPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.HighSchool).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.HighSchool.Index].Item.Percent =
                    this.gameEngineService.GetHighSchoolPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Landfill).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Landfill.Index].Item.Percent =
                    this.gameEngineService.GetLandfillPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.NoisePollution).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.NoisePollution.Index].Item.Percent =
                    this.gameEngineService.GetNoisePollutionPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.ParkMaintenanceVehicles).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.ParkMaintenanceVehicles.Index].Item.Percent =
                    this.gameEngineService.GetParkMaintenanceVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PoliceHoldingCells).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PoliceVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PrisonCells).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PrisonVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PoliceHelicopters).enabled)
            {
                var policeDepartmentVehiclesPercent = this.gameEngineService.GetPoliceDepartmentVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.PoliceHelicopters.Index].Item.Percent =
                    policeDepartmentVehiclesPercent.policeHelicoptersPercent;
                this.itemPanelsInIndexOrder[ItemData.PoliceHoldingCells.Index].Item.Percent =
                    policeDepartmentVehiclesPercent.policeHoldingCellsPercent;
                this.itemPanelsInIndexOrder[ItemData.PoliceVehicles.Index].Item.Percent =
                    policeDepartmentVehiclesPercent.policeVehiclesPercent;
                this.itemPanelsInIndexOrder[ItemData.PrisonCells.Index].Item.Percent =
                    policeDepartmentVehiclesPercent.prisonCellsPercent;
                this.itemPanelsInIndexOrder[ItemData.PrisonVehicles.Index].Item.Percent =
                    policeDepartmentVehiclesPercent.prisonVehiclesPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Taxis).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PostVans).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PostTrucks).enabled
                )
            {
                var postAndTaxiVehiclesPercent = this.gameEngineService.GetPostAndTaxiVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.Taxis.Index].Item.Percent =
                    postAndTaxiVehiclesPercent.taxisPercent;
                this.itemPanelsInIndexOrder[ItemData.PostVans.Index].Item.Percent =
                    postAndTaxiVehiclesPercent.postVansPercent;
                this.itemPanelsInIndexOrder[ItemData.PostTrucks.Index].Item.Percent =
                    postAndTaxiVehiclesPercent.postTrucksPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.RoadMaintenanceVehicles).enabled
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItemData(ItemData.SnowDump).enabled)
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItemData(ItemData.SnowDumpVehicles).enabled))
            {
                var roadMaintenanceAndSnowDumpVehiclesPercent =
                    this.gameEngineService.GetRoadMaintenanceAndSnowDumpVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.RoadMaintenanceVehicles.Index].Item.Percent =
                    roadMaintenanceAndSnowDumpVehiclesPercent.roadMaintenanceVehiclesPercent;

                if (this.mapHasSnowDumps)
                {
                    this.itemPanelsInIndexOrder[ItemData.SnowDump.Index].Item.Percent =
                        roadMaintenanceAndSnowDumpVehiclesPercent.snowDumpPercent;
                    this.itemPanelsInIndexOrder[ItemData.SnowDumpVehicles.Index].Item.Percent =
                        roadMaintenanceAndSnowDumpVehiclesPercent.snowDumpVehiclesPercent;
                }
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.SewageTreatment).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.SewageTreatment.Index].Item.Percent =
                    this.gameEngineService.GetSewageTreatmentPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.TrafficJam).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.TrafficJam.Index].Item.Percent =
                    this.gameEngineService.GetTrafficJamPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Unemployment).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Unemployment.Index].Item.Percent =
                    this.gameEngineService.GetUnemploymentPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.University).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.University.Index].Item.Percent =
                    this.gameEngineService.GetUniversityPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Water).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.Water.Index].Item.Percent =
                    this.gameEngineService.GetWaterPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.WaterPumpingServiceStorage).enabled
                || this.configuration.GetConfigurationItemData(ItemData.WaterPumpingServiceVehicles).enabled)
            {
                var waterPumpingServiceVehiclesPercent =
                    this.gameEngineService.GetWaterPumpingServiceVehiclesPercent();

                this.itemPanelsInIndexOrder[ItemData.WaterPumpingServiceVehicles.Index].Item.Percent =
                    waterPumpingServiceVehiclesPercent.waterPumpingServiceVehiclesPercent;
                this.itemPanelsInIndexOrder[ItemData.WaterPumpingServiceStorage.Index].Item.Percent =
                    waterPumpingServiceVehiclesPercent.waterPumpingServiceStoragePercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.WaterReserveTank).enabled)
            {
                this.itemPanelsInIndexOrder[ItemData.WaterReserveTank.Index].Item.Percent =
                    this.gameEngineService.GetWaterReservePercent();
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
