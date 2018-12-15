using ColossalFramework;
using ColossalFramework.UI;
using Stats.Configuration;
using Stats.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats.Ui
{
    public class MainPanel : UIPanel
    {
        private UIDragHandle uiDragHandle;
        private string modSystemName;
        private bool mapHasSnowDumps;
        private ConfigurationModel configuration;
        private LanguageResourceModel languageResource;
        private float secondsSinceLastUpdate = 0; //used to defer updates

        private ItemPanel electricityPanel;
        private ItemPanel heatingPanel;
        private ItemPanel waterPanel;
        private ItemPanel sewageTreatmentPanel;
        private ItemPanel waterReserveTankPanel;
        private ItemPanel waterPumpingServiceStoragePanel;
        private ItemPanel waterPumpingServiceVehiclesPanel;
        private ItemPanel landfillPanel;
        private ItemPanel landfillVehiclesPanel;
        private ItemPanel garbageProcessingPanel;
        private ItemPanel garbageProcessingVehiclesPanel;
        private ItemPanel elementarySchoolPanel;
        private ItemPanel highSchoolPanel;
        private ItemPanel universityPanel;
        private ItemPanel healthcarePanel;
        private ItemPanel healthcareVehiclesPanel;
        private ItemPanel averageIllnessRatePanel;
        private ItemPanel cemeteryPanel;
        private ItemPanel cemeteryVehiclesPanel;
        private ItemPanel crematoriumPanel;
        private ItemPanel crematoriumVehiclesPanel;
        private ItemPanel groundPollutionPanel;
        private ItemPanel drinkingWaterPollutionPanel;
        private ItemPanel noisePollutionPanel;
        private ItemPanel fireHazardPanel;
        private ItemPanel fireDepartmentVehiclesPanel;
        private ItemPanel crimeRatePanel;
        private ItemPanel policeHoldingCellsPanel;
        private ItemPanel policeVehiclesPanel;
        private ItemPanel prisonCellsPanel;
        private ItemPanel prisonVehiclesPanel;
        private ItemPanel unemploymentPanel;
        private ItemPanel trafficJamPanel;
        private ItemPanel roadMaintenanceVehiclesPanel;
        private ItemPanel snowDumpPanel;
        private ItemPanel snowDumpVehiclesPanel;
        private ItemPanel parkMaintenanceVehiclesPanel;
        private ItemPanel cityUnattractivenessPanel;

        private List<ItemPanel> allItemPanels;

        public void Initialize(string modSystemName, bool mapHasSnowDumps, ConfigurationModel configuration, LanguageResourceModel languageResource)
        {
            this.modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            this.mapHasSnowDumps = mapHasSnowDumps;
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (this.configuration.MainPanelColumnCount < 1)
                throw new ArgumentOutOfRangeException($"'{nameof(this.configuration.MainPanelColumnCount)}' parameter must be bigger or equal to 1.");
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));

            this.color = configuration.MainPanelBackgroundColor;
            this.name = modSystemName + "MainPanel";
            this.backgroundSprite = "GenericPanelLight";
            this.isInteractive = false;

            this.CreateAndAddDragHandle();
            this.CreateAndAddAllUiItems();
            this.UpdateLocalizedTooltips();
            this.UpdateUi(true);

            this.configuration.PropertyChanged += this.UpdateUiAndForceLayout;
            this.languageResource.LanguageChanged += this.UpdateLocalizedTooltips;
        }

        public override void OnDestroy()
        {
            this.configuration.PropertyChanged -= this.UpdateUiAndForceLayout;
            this.languageResource.LanguageChanged -= this.UpdateLocalizedTooltips;

            base.OnDestroy();
        }

        private void CreateAndAddDragHandle()
        {
            var dragHandle = this.AddUIComponent<UIDragHandle>();
            dragHandle.name = this.modSystemName + "DragHandle";
            dragHandle.relativePosition = Vector2.zero;
            dragHandle.target = this;
            dragHandle.constrainToScreen = true;
            dragHandle.SendToBack();
            this.uiDragHandle = dragHandle;
        }

        private void CreateAndAddAllUiItems()
        {
            this.electricityPanel = this.CreateUiItemAndAddButtons(ItemData.Electricity, new ItemDisplayInfo(this.configuration.Electricity, this.configuration.ElectricityCriticalThreshold, this.configuration.ElectricitySortOrder));
            this.heatingPanel = this.CreateUiItemAndAddButtons(ItemData.Heating, new ItemDisplayInfo(this.configuration.Heating, this.configuration.HeatingCriticalThreshold, this.configuration.HeatingSortOrder));
            this.waterPanel = this.CreateUiItemAndAddButtons(ItemData.Water, new ItemDisplayInfo(this.configuration.Water, this.configuration.WaterCriticalThreshold, this.configuration.WaterSortOrder));
            this.sewageTreatmentPanel = this.CreateUiItemAndAddButtons(ItemData.SewageTreatment, new ItemDisplayInfo(this.configuration.SewageTreatment, this.configuration.SewageTreatmentCriticalThreshold, this.configuration.SewageTreatmentSortOrder));
            this.waterReserveTankPanel = this.CreateUiItemAndAddButtons(ItemData.WaterReserveTank, new ItemDisplayInfo(this.configuration.WaterReserveTank, this.configuration.WaterReserveTankCriticalThreshold, this.configuration.WaterReserveTankSortOrder));
            this.waterPumpingServiceStoragePanel = this.CreateUiItemAndAddButtons(ItemData.WaterPumpingServiceStorage, new ItemDisplayInfo(this.configuration.WaterPumpingServiceStorage, this.configuration.WaterPumpingServiceStorageCriticalThreshold, this.configuration.WaterPumpingServiceStorageSortOrder));
            this.waterPumpingServiceVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.WaterPumpingServiceVehicles, new ItemDisplayInfo(this.configuration.WaterPumpingServiceVehicles, this.configuration.WaterPumpingServiceVehiclesCriticalThreshold, this.configuration.WaterPumpingServiceVehiclesSortOrder));
            this.landfillPanel = this.CreateUiItemAndAddButtons(ItemData.Landfill, new ItemDisplayInfo(this.configuration.Landfill, this.configuration.LandfillCriticalThreshold, this.configuration.LandfillSortOrder));
            this.landfillVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.LandfillVehicles, new ItemDisplayInfo(this.configuration.LandfillVehicles, this.configuration.LandfillVehiclesCriticalThreshold, this.configuration.LandfillVehiclesSortOrder));
            this.garbageProcessingPanel = this.CreateUiItemAndAddButtons(ItemData.GarbageProcessing, new ItemDisplayInfo(this.configuration.GarbageProcessing, this.configuration.GarbageProcessingCriticalThreshold, this.configuration.GarbageProcessingSortOrder));
            this.garbageProcessingVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.GarbageProcessingVehicles, new ItemDisplayInfo(this.configuration.GarbageProcessingVehicles, this.configuration.GarbageProcessingVehiclesCriticalThreshold, this.configuration.GarbageProcessingVehiclesSortOrder));
            this.elementarySchoolPanel = this.CreateUiItemAndAddButtons(ItemData.ElementarySchool, new ItemDisplayInfo(this.configuration.ElementarySchool, this.configuration.ElementarySchoolCriticalThreshold, this.configuration.ElementarySchoolSortOrder));
            this.highSchoolPanel = this.CreateUiItemAndAddButtons(ItemData.HighSchool, new ItemDisplayInfo(this.configuration.HighSchool, this.configuration.HighSchoolCriticalThreshold, this.configuration.HighSchoolSortOrder));
            this.universityPanel = this.CreateUiItemAndAddButtons(ItemData.University, new ItemDisplayInfo(this.configuration.University, this.configuration.UniversityCriticalThreshold, this.configuration.UniversitySortOrder));
            this.healthcarePanel = this.CreateUiItemAndAddButtons(ItemData.Healthcare, new ItemDisplayInfo(this.configuration.Healthcare, this.configuration.HealthcareCriticalThreshold, this.configuration.HealthcareSortOrder));
            this.healthcareVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.HealthcareVehicles, new ItemDisplayInfo(this.configuration.HealthcareVehicles, this.configuration.HealthcareVehiclesCriticalThreshold, this.configuration.HealthcareVehiclesSortOrder));
            this.averageIllnessRatePanel = this.CreateUiItemAndAddButtons(ItemData.AverageIllnessRate, new ItemDisplayInfo(this.configuration.AverageIllnessRate, this.configuration.AverageIllnessRateCriticalThreshold, this.configuration.AverageIllnessRateSortOrder));
            this.cemeteryPanel = this.CreateUiItemAndAddButtons(ItemData.Cemetery, new ItemDisplayInfo(this.configuration.Cemetery, this.configuration.CemeteryCriticalThreshold, this.configuration.CemeterySortOrder));
            this.cemeteryVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.CemeteryVehicles, new ItemDisplayInfo(this.configuration.CemeteryVehicles, this.configuration.CemeteryVehiclesCriticalThreshold, this.configuration.CemeteryVehiclesSortOrder));
            this.crematoriumPanel = this.CreateUiItemAndAddButtons(ItemData.Crematorium, new ItemDisplayInfo(this.configuration.Crematorium, this.configuration.CrematoriumCriticalThreshold, this.configuration.CrematoriumSortOrder));
            this.crematoriumVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.CrematoriumVehicles, new ItemDisplayInfo(this.configuration.CrematoriumVehicles, this.configuration.CrematoriumVehiclesCriticalThreshold, this.configuration.CrematoriumVehiclesSortOrder));
            this.groundPollutionPanel = this.CreateUiItemAndAddButtons(ItemData.GroundPollution, new ItemDisplayInfo(this.configuration.GroundPollution, this.configuration.GroundPollutionCriticalThreshold, this.configuration.GroundPollutionSortOrder));
            this.drinkingWaterPollutionPanel = this.CreateUiItemAndAddButtons(ItemData.DrinkingWaterPollution, new ItemDisplayInfo(this.configuration.DrinkingWaterPollution, this.configuration.DrinkingWaterPollutionCriticalThreshold, this.configuration.DrinkingWaterPollutionSortOrder));
            this.noisePollutionPanel = this.CreateUiItemAndAddButtons(ItemData.NoisePollution, new ItemDisplayInfo(this.configuration.NoisePollution, this.configuration.NoisePollutionCriticalThreshold, this.configuration.NoisePollutionSortOrder));
            this.fireHazardPanel = this.CreateUiItemAndAddButtons(ItemData.FireHazard, new ItemDisplayInfo(this.configuration.FireHazard, this.configuration.FireHazardCriticalThreshold, this.configuration.FireHazardSortOrder));
            this.fireDepartmentVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.FireDepartmentVehicles, new ItemDisplayInfo(this.configuration.FireDepartmentVehicles, this.configuration.FireDepartmentVehiclesCriticalThreshold, this.configuration.FireDepartmentVehiclesSortOrder));
            this.crimeRatePanel = this.CreateUiItemAndAddButtons(ItemData.CrimeRate, new ItemDisplayInfo(this.configuration.CrimeRate, this.configuration.CrimeRateCriticalThreshold, this.configuration.CrimeRateSortOrder));
            this.policeHoldingCellsPanel = this.CreateUiItemAndAddButtons(ItemData.PoliceHoldingCells, new ItemDisplayInfo(this.configuration.PoliceHoldingCells, this.configuration.PoliceHoldingCellsCriticalThreshold, this.configuration.PoliceHoldingCellsSortOrder));
            this.policeVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.PoliceVehicles, new ItemDisplayInfo(this.configuration.PoliceVehicles, this.configuration.PoliceVehiclesCriticalThreshold, this.configuration.PoliceVehiclesSortOrder));
            this.prisonCellsPanel = this.CreateUiItemAndAddButtons(ItemData.PrisonCells, new ItemDisplayInfo(this.configuration.PrisonCells, this.configuration.PrisonCellsCriticalThreshold, this.configuration.PrisonCellsSortOrder));
            this.prisonVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.PrisonVehicles, new ItemDisplayInfo(this.configuration.PrisonVehicles, this.configuration.PrisonVehiclesCriticalThreshold, this.configuration.PrisonVehiclesSortOrder));
            this.unemploymentPanel = this.CreateUiItemAndAddButtons(ItemData.Unemployment, new ItemDisplayInfo(this.configuration.Unemployment, this.configuration.UnemploymentCriticalThreshold, this.configuration.UnemploymentSortOrder));
            this.trafficJamPanel = this.CreateUiItemAndAddButtons(ItemData.TrafficJam, new ItemDisplayInfo(this.configuration.TrafficJam, this.configuration.TrafficJamCriticalThreshold, this.configuration.TrafficJamSortOrder));
            this.roadMaintenanceVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.RoadMaintenanceVehicles, new ItemDisplayInfo(this.configuration.RoadMaintenanceVehicles, this.configuration.RoadMaintenanceVehiclesCriticalThreshold, this.configuration.RoadMaintenanceVehiclesSortOrder));
            this.snowDumpPanel = this.CreateUiItemAndAddButtons(ItemData.SnowDump, new ItemDisplayInfo(this.mapHasSnowDumps && this.configuration.SnowDump, this.configuration.SnowDumpCriticalThreshold, this.configuration.SnowDumpSortOrder));
            this.snowDumpVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.SnowDumpVehicles, new ItemDisplayInfo(this.mapHasSnowDumps && this.configuration.SnowDumpVehicles, this.configuration.SnowDumpVehiclesCriticalThreshold, this.configuration.SnowDumpVehiclesSortOrder));
            this.parkMaintenanceVehiclesPanel = this.CreateUiItemAndAddButtons(ItemData.ParkMaintenanceVehicles, new ItemDisplayInfo(this.configuration.ParkMaintenanceVehicles, this.configuration.ParkMaintenanceVehiclesCriticalThreshold, this.configuration.ParkMaintenanceVehiclesSortOrder));
            this.cityUnattractivenessPanel = this.CreateUiItemAndAddButtons(ItemData.CityUnattractiveness, new ItemDisplayInfo(this.configuration.CityUnattractiveness, this.configuration.CityUnattractivenessCriticalThreshold, this.configuration.CityUnattractivenessSortOrder));

            this.allItemPanels = new List<ItemPanel>()
            {
                this.electricityPanel,
                this.heatingPanel,
                this.waterPanel,
                this.sewageTreatmentPanel,
                this.waterReserveTankPanel,
                this.waterPumpingServiceStoragePanel,
                this.waterPumpingServiceVehiclesPanel,
                this.landfillPanel,
                this.landfillVehiclesPanel,
                this.garbageProcessingPanel,
                this.garbageProcessingVehiclesPanel,
                this.elementarySchoolPanel,
                this.highSchoolPanel,
                this.universityPanel,
                this.healthcarePanel,
                this.healthcareVehiclesPanel,
                this.averageIllnessRatePanel,
                this.cemeteryPanel,
                this.cemeteryVehiclesPanel,
                this.crematoriumPanel,
                this.crematoriumVehiclesPanel,
                this.groundPollutionPanel,
                this.drinkingWaterPollutionPanel,
                this.noisePollutionPanel,
                this.fireHazardPanel,
                this.fireDepartmentVehiclesPanel,
                this.crimeRatePanel,
                this.policeHoldingCellsPanel,
                this.policeVehiclesPanel,
                this.prisonCellsPanel,
                this.prisonVehiclesPanel,
                this.unemploymentPanel,
                this.trafficJamPanel,
                this.roadMaintenanceVehiclesPanel,
                this.snowDumpPanel,
                this.snowDumpVehiclesPanel,
                this.parkMaintenanceVehiclesPanel,
                this.cityUnattractivenessPanel
            };

            this.allItemPanels.Sort((x, y) => x.SortOrder.CompareTo(y.SortOrder));
            this.allItemPanels.Sort((x, y) => x.SortOrder.CompareTo(y.SortOrder));
            //TODO fix the strange bug that forces the list to be sorted twice
        }

        private ItemPanel CreateUiItemAndAddButtons(ItemData itemData, ItemDisplayInfo itemDisplayInfo)
        {
            var uiItem = this.CreateAndAddItemPanel();
            uiItem.Initialize(itemData, this.configuration);
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
            this.electricityPanel.UpdateLocalizedTooltips(this.languageResource.Electricity);
            this.heatingPanel.UpdateLocalizedTooltips(this.languageResource.Heating);
            this.waterPanel.UpdateLocalizedTooltips(this.languageResource.Water);
            this.sewageTreatmentPanel.UpdateLocalizedTooltips(this.languageResource.SewageTreatment);
            this.waterReserveTankPanel.UpdateLocalizedTooltips(this.languageResource.WaterReserveTank);
            this.waterPumpingServiceStoragePanel.UpdateLocalizedTooltips(this.languageResource.WaterPumpingServiceStorage);
            this.waterPumpingServiceVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.WaterPumpingServiceVehicles);
            this.landfillPanel.UpdateLocalizedTooltips(this.languageResource.Landfill);
            this.landfillVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.LandfillVehicles);
            this.garbageProcessingPanel.UpdateLocalizedTooltips(this.languageResource.GarbageProcessing);
            this.garbageProcessingVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.GarbageProcessingVehicles);
            this.elementarySchoolPanel.UpdateLocalizedTooltips(this.languageResource.ElementarySchool);
            this.highSchoolPanel.UpdateLocalizedTooltips(this.languageResource.HighSchool);
            this.universityPanel.UpdateLocalizedTooltips(this.languageResource.University);
            this.healthcarePanel.UpdateLocalizedTooltips(this.languageResource.Healthcare);
            this.healthcareVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.HealthcareVehicles);
            this.averageIllnessRatePanel.UpdateLocalizedTooltips(this.languageResource.AverageIllnessRate);
            this.cemeteryPanel.UpdateLocalizedTooltips(this.languageResource.Cemetery);
            this.cemeteryVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.CemeteryVehicles);
            this.crematoriumPanel.UpdateLocalizedTooltips(this.languageResource.Crematorium);
            this.crematoriumVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.CrematoriumVehicles);
            this.groundPollutionPanel.UpdateLocalizedTooltips(this.languageResource.GroundPollution);
            this.drinkingWaterPollutionPanel.UpdateLocalizedTooltips(this.languageResource.DrinkingWaterPollution);
            this.noisePollutionPanel.UpdateLocalizedTooltips(this.languageResource.NoisePollution);
            this.fireHazardPanel.UpdateLocalizedTooltips(this.languageResource.FireHazard);
            this.fireDepartmentVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.FireDepartmentVehicles);
            this.crimeRatePanel.UpdateLocalizedTooltips(this.languageResource.CrimeRate);
            this.policeHoldingCellsPanel.UpdateLocalizedTooltips(this.languageResource.PoliceHoldingCells);
            this.policeVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.PoliceVehicles);
            this.prisonCellsPanel.UpdateLocalizedTooltips(this.languageResource.PrisonCells);
            this.prisonVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.PrisonVehicles);
            this.unemploymentPanel.UpdateLocalizedTooltips(this.languageResource.Unemployment);
            this.trafficJamPanel.UpdateLocalizedTooltips(this.languageResource.TrafficJam);
            this.roadMaintenanceVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.RoadMaintenanceVehicles);
            this.snowDumpPanel.UpdateLocalizedTooltips(this.languageResource.SnowDump);
            this.snowDumpVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.SnowDumpVehicles);
            this.parkMaintenanceVehiclesPanel.UpdateLocalizedTooltips(this.languageResource.ParkMaintenanceVehicles);
            this.cityUnattractivenessPanel.UpdateLocalizedTooltips(this.languageResource.CityUnattractiveness);
        }

        private void UpdateUiAndForceLayout()
        {
            this.UpdateUi(true);
        }

        private void UpdateUi(bool forceLayout)
        {
            var itemVisibilityChanged = this.UpdateItemsDisplay();
            if (forceLayout || itemVisibilityChanged)
            {
                this.UpdateItemsLayout();
                this.UpdatePanelSize();
            }
        }

        private bool UpdateItemsDisplay()
        {
            var itemVisibilityChanged = false;

            itemVisibilityChanged |= this.UpdateItemDisplay(this.electricityPanel, this.configuration.Electricity, electricityPanel.Percent, this.configuration.ElectricityCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.heatingPanel, this.configuration.Heating, heatingPanel.Percent, this.configuration.HeatingCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.waterPanel, this.configuration.Water, waterPanel.Percent, this.configuration.WaterCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.sewageTreatmentPanel, this.configuration.SewageTreatment, sewageTreatmentPanel.Percent, this.configuration.SewageTreatmentCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.waterReserveTankPanel, this.configuration.WaterReserveTank, waterReserveTankPanel.Percent, this.configuration.WaterReserveTankCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.waterPumpingServiceStoragePanel, this.configuration.WaterPumpingServiceStorage, waterPumpingServiceStoragePanel.Percent, this.configuration.WaterPumpingServiceStorageCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.waterPumpingServiceVehiclesPanel, this.configuration.WaterPumpingServiceVehicles, waterPumpingServiceVehiclesPanel.Percent, this.configuration.WaterPumpingServiceVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.landfillPanel, this.configuration.Landfill, landfillPanel.Percent, this.configuration.LandfillCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.landfillVehiclesPanel, this.configuration.LandfillVehicles, landfillVehiclesPanel.Percent, this.configuration.LandfillVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.garbageProcessingPanel, this.configuration.GarbageProcessing, garbageProcessingPanel.Percent, this.configuration.GarbageProcessingCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.garbageProcessingVehiclesPanel, this.configuration.GarbageProcessingVehicles, garbageProcessingVehiclesPanel.Percent, this.configuration.GarbageProcessingVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.elementarySchoolPanel, this.configuration.ElementarySchool, elementarySchoolPanel.Percent, this.configuration.ElementarySchoolCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.highSchoolPanel, this.configuration.HighSchool, highSchoolPanel.Percent, this.configuration.HighSchoolCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.universityPanel, this.configuration.University, universityPanel.Percent, this.configuration.UniversityCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.averageIllnessRatePanel, this.configuration.AverageIllnessRate, averageIllnessRatePanel.Percent, this.configuration.AverageIllnessRateCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.healthcarePanel, this.configuration.Healthcare, healthcarePanel.Percent, this.configuration.HealthcareCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.healthcareVehiclesPanel, this.configuration.HealthcareVehicles, healthcareVehiclesPanel.Percent, this.configuration.HealthcareVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.cemeteryPanel, this.configuration.Cemetery, cemeteryPanel.Percent, this.configuration.CemeteryCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.cemeteryVehiclesPanel, this.configuration.CemeteryVehicles, cemeteryVehiclesPanel.Percent, this.configuration.CemeteryVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.crematoriumPanel, this.configuration.Crematorium, crematoriumPanel.Percent, this.configuration.CrematoriumCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.crematoriumVehiclesPanel, this.configuration.CrematoriumVehicles, crematoriumVehiclesPanel.Percent, this.configuration.CrematoriumVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.groundPollutionPanel, this.configuration.GroundPollution, groundPollutionPanel.Percent, this.configuration.GroundPollutionCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.drinkingWaterPollutionPanel, this.configuration.DrinkingWaterPollution, drinkingWaterPollutionPanel.Percent, this.configuration.DrinkingWaterPollutionCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.noisePollutionPanel, this.configuration.NoisePollution, noisePollutionPanel.Percent, this.configuration.NoisePollutionCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.fireHazardPanel, this.configuration.FireHazard, fireHazardPanel.Percent, this.configuration.FireHazardCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.fireDepartmentVehiclesPanel, this.configuration.FireDepartmentVehicles, fireDepartmentVehiclesPanel.Percent, this.configuration.FireDepartmentVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.crimeRatePanel, this.configuration.CrimeRate, crimeRatePanel.Percent, this.configuration.CrimeRateCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.policeHoldingCellsPanel, this.configuration.PoliceHoldingCells, policeHoldingCellsPanel.Percent, this.configuration.PoliceHoldingCellsCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.policeVehiclesPanel, this.configuration.PoliceVehicles, policeVehiclesPanel.Percent, this.configuration.PoliceVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.prisonCellsPanel, this.configuration.PrisonCells, prisonCellsPanel.Percent, this.configuration.PrisonCellsCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.prisonVehiclesPanel, this.configuration.PrisonVehicles, prisonVehiclesPanel.Percent, this.configuration.PrisonVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.unemploymentPanel, this.configuration.Unemployment, unemploymentPanel.Percent, this.configuration.UnemploymentCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.trafficJamPanel, this.configuration.TrafficJam, trafficJamPanel.Percent, this.configuration.TrafficJamCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.roadMaintenanceVehiclesPanel, this.configuration.RoadMaintenanceVehicles, roadMaintenanceVehiclesPanel.Percent, this.configuration.RoadMaintenanceVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.parkMaintenanceVehiclesPanel, this.configuration.ParkMaintenanceVehicles, parkMaintenanceVehiclesPanel.Percent, this.configuration.ParkMaintenanceVehiclesCriticalThreshold, this.configuration);
            itemVisibilityChanged |= this.UpdateItemDisplay(this.cityUnattractivenessPanel, this.configuration.CityUnattractiveness, cityUnattractivenessPanel.Percent, this.configuration.CityUnattractivenessCriticalThreshold, this.configuration);

            if (this.mapHasSnowDumps)
            {
                itemVisibilityChanged |= this.UpdateItemDisplay(snowDumpPanel, this.configuration.SnowDump, snowDumpPanel.Percent, this.configuration.SnowDumpCriticalThreshold, this.configuration);
                itemVisibilityChanged |= this.UpdateItemDisplay(snowDumpVehiclesPanel, this.configuration.SnowDumpVehicles, snowDumpVehiclesPanel.Percent, this.configuration.SnowDumpVehiclesCriticalThreshold, this.configuration);
            }
            else
            {
                snowDumpPanel.isVisible = false;
                snowDumpVehiclesPanel.isVisible = false;
            }

            return itemVisibilityChanged;
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

            if (this.configuration.MainPanelUpdateEveryXSeconds == 0)
            {
                return;
            }

            this.secondsSinceLastUpdate += Time.deltaTime;
            if (this.secondsSinceLastUpdate < this.configuration.MainPanelUpdateEveryXSeconds)
            {
                return;
            }
            this.secondsSinceLastUpdate = 0;

            if (!Singleton<DistrictManager>.exists)
            {
                return;
            }

            var allDistricts = Singleton<DistrictManager>.instance.m_districts.m_buffer[0];

            if (this.configuration.Electricity)
            {
                var electricityCapacity = allDistricts.GetElectricityCapacity();
                var electricityConsumption = allDistricts.GetElectricityConsumption();
                this.electricityPanel.Percent = GetUsagePercent(electricityCapacity, electricityConsumption);
            }

            if (this.configuration.Heating)
            {
                var heatingCapacity = allDistricts.GetHeatingCapacity();
                var heatingConsumption = allDistricts.GetHeatingConsumption();
                this.heatingPanel.Percent = GetUsagePercent(heatingCapacity, heatingConsumption);
            }

            if (this.configuration.Water)
            {
                var waterCapacity = allDistricts.GetWaterCapacity();
                var waterConsumption = allDistricts.GetWaterConsumption();
                this.waterPanel.Percent = GetUsagePercent(waterCapacity, waterConsumption);
            }

            if (this.configuration.SewageTreatment)
            {
                var sewageCapacity = allDistricts.GetSewageCapacity();
                var sewageAccumulation = allDistricts.GetSewageAccumulation();
                this.sewageTreatmentPanel.Percent = GetUsagePercent(sewageCapacity, sewageAccumulation);
            }

            if (this.configuration.WaterReserveTank)
            {
                var waterStorageTotal = allDistricts.GetWaterStorageCapacity();
                var waterStorageInUse = allDistricts.GetWaterStorageAmount();
                this.waterReserveTankPanel.Percent = GetAvailabilityPercent(waterStorageTotal, waterStorageInUse);
            }

            if (this.configuration.WaterPumpingServiceStorage || this.configuration.WaterPumpingServiceVehicles)
            {
                long waterSewageStorageTotal = 0;
                long waterSewageStorageInUse = 0;

                int pumpingVehiclesTotal = 0;
                int pumpingVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var waterBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Water);

                for (int i = 0; i < waterBuildingIds.m_size; i++)
                {
                    var buildingId = waterBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI() as WaterFacilityAI;
                    if (buildingAi == null)
                    {
                        continue;
                    }

                    //WaterFacilityAI.GetLocalizedStats
                    if (buildingAi.m_waterIntake != 0 && buildingAi.m_waterOutlet != 0 && buildingAi.m_waterStorage != 0)
                    {
                        continue;
                    }

                    if (buildingAi.m_sewageOutlet == 0 || buildingAi.m_sewageStorage == 0 || buildingAi.m_pumpingVehicles == 0)
                    {
                        continue;
                    }

                    waterSewageStorageInUse += (building.m_customBuffer2 * 1000 + building.m_sewageBuffer);
                    waterSewageStorageTotal += buildingAi.m_sewageStorage;

                    var budget = Singleton<EconomyManager>.instance.GetBudget(building.Info.m_class);
                    var productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                    var pumpingVehicles = (productionRate * buildingAi.m_pumpingVehicles + 99) / 100;
                    int count = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.FloodWater, ref count, ref cargo, ref capacity, ref outside);

                    pumpingVehiclesTotal += pumpingVehicles;
                    pumpingVehiclesInUse += count;
                }

                this.waterPumpingServiceVehiclesPanel.Percent = GetUsagePercent(pumpingVehiclesTotal, pumpingVehiclesInUse);
                this.waterPumpingServiceStoragePanel.Percent = GetUsagePercent(waterSewageStorageTotal, waterSewageStorageInUse);
            }

            if (this.configuration.Landfill)
            {
                var garbageCapacity = allDistricts.GetGarbageCapacity();
                var garbageAmout = allDistricts.GetGarbageAmount();

                this.landfillPanel.Percent = GetUsagePercent(garbageCapacity, garbageAmout);
            }

            if (this.configuration.GarbageProcessing)
            {
                var incineratorCapacity = allDistricts.GetIncinerationCapacity();
                var incineratorAccumulation = allDistricts.GetGarbageAccumulation();
                this.garbageProcessingPanel.Percent = GetUsagePercent(incineratorCapacity, incineratorAccumulation);
            }

            if (this.configuration.LandfillVehicles || this.configuration.GarbageProcessingVehicles)
            {
                var landfillVehiclesTotal = 0;
                var landfillVehiclesInUse = 0;

                var garbageProcessingVehiclesTotal = 0;
                var garbageProcessingVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var garbageBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Garbage);

                for (int i = 0; i < garbageBuildingIds.m_size; i++)
                {
                    var buildingId = garbageBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI() as LandfillSiteAI;
                    if (buildingAi == null)
                    {
                        continue;
                    }

                    int budget = Singleton<EconomyManager>.instance.GetBudget(buildingAi.m_info.m_class);
                    int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                    int garbageTruckVehicles = (productionRate * buildingAi.m_garbageTruckCount + 99) / 100;
                    int count = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
                    {
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Garbage, ref count, ref cargo, ref capacity, ref outside);
                    }
                    else
                    {
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.GarbageMove, ref count, ref cargo, ref capacity, ref outside);
                    }

                    if (buildingAi.m_garbageConsumption <= 0)
                    {
                        landfillVehiclesTotal += garbageTruckVehicles;
                        landfillVehiclesInUse += count;
                    }
                    else
                    {
                        garbageProcessingVehiclesTotal += garbageTruckVehicles;
                        garbageProcessingVehiclesInUse += count;
                    }
                }

                this.landfillVehiclesPanel.Percent = GetUsagePercent(landfillVehiclesTotal, landfillVehiclesInUse);
                this.garbageProcessingVehiclesPanel.Percent = GetUsagePercent(garbageProcessingVehiclesTotal, garbageProcessingVehiclesInUse);
            }

            if (this.configuration.ElementarySchool)
            {
                var elementrySchoolCapacity = allDistricts.GetEducation1Capacity();
                var elementrySchoolUsage = allDistricts.GetEducation1Need();
                this.elementarySchoolPanel.Percent = GetUsagePercent(elementrySchoolCapacity, elementrySchoolUsage);
            }

            if (this.configuration.HighSchool)
            {
                var highSchoolCapacity = allDistricts.GetEducation2Capacity();
                var highSchoolUsage = allDistricts.GetEducation2Need();
                this.highSchoolPanel.Percent = GetUsagePercent(highSchoolCapacity, highSchoolUsage);
            }

            if (this.configuration.University)
            {
                var universityCapacity = allDistricts.GetEducation3Capacity();
                var universityUsage = allDistricts.GetEducation3Need();
                this.universityPanel.Percent = GetUsagePercent(universityCapacity, universityUsage);
            }

            if (this.configuration.Healthcare)
            {
                var healthcareCapacity = allDistricts.GetHealCapacity();
                var healthcareUsage = allDistricts.GetSickCount();
                this.healthcarePanel.Percent = GetUsagePercent(healthcareCapacity, healthcareUsage);
            }

            if (this.configuration.AverageIllnessRate)
            {
                this.averageIllnessRatePanel.Percent = (int)(100 - (float)allDistricts.m_residentialData.m_finalHealth);
            }

            if (this.configuration.Cemetery)
            {
                var deadCapacity = allDistricts.GetDeadCapacity();
                var deadAmount = allDistricts.GetDeadAmount();
                this.cemeteryPanel.Percent = GetUsagePercent(deadCapacity, deadAmount);
            }

            if (this.configuration.Crematorium)
            {
                var cremateCapacity = allDistricts.GetCremateCapacity();
                var deadCount = allDistricts.GetDeadCount();
                this.crematoriumPanel.Percent = GetUsagePercent(cremateCapacity, deadCount);
            }

            if (this.configuration.HealthcareVehicles || this.configuration.CemeteryVehicles || this.configuration.CrematoriumVehicles)
            {
                var healthcareVehiclesTotal = 0;
                var healthcareVehiclesInUse = 0;

                var cemeteryVehiclesTotal = 0;
                var cemeteryVehiclesInUse = 0;

                var crematoriumVehiclesTotal = 0;
                var crematoriumVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var healthcareBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

                for (int i = 0; i < healthcareBuildingIds.m_size; i++)
                {
                    var buildingId = healthcareBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    if (buildingAi is HospitalAI hospitalAi)
                    {
                        int budget = Singleton<EconomyManager>.instance.GetBudget(hospitalAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                        int healthcareVehicles = (productionRate * hospitalAi.m_ambulanceCount + 99) / 100;
                        int count = 0;
                        int cargo = 0;
                        int capacity = 0;
                        int outside = 0;
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Sick, ref count, ref cargo, ref capacity, ref outside);

                        healthcareVehiclesTotal += healthcareVehicles;
                        healthcareVehiclesInUse += count;
                    }
                    else if (buildingAi is CemeteryAI cemeteryAi)
                    {
                        int budget = Singleton<EconomyManager>.instance.GetBudget(cemeteryAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

                        int cemeteryVehicles = (productionRate * cemeteryAi.m_hearseCount + 99) / 100;
                        int count = 0;
                        int cargo = 0;
                        int capacity = 0;
                        int outside = 0;
                        if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
                        {
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Dead, ref count, ref cargo, ref capacity, ref outside);
                        }
                        else
                        {
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.DeadMove, ref count, ref cargo, ref capacity, ref outside);
                        }

                        if (cemeteryAi.m_graveCount == 0) //crematory
                        {
                            crematoriumVehiclesTotal += cemeteryVehicles;
                            crematoriumVehiclesInUse += count;
                        }
                        else //cemetery
                        {
                            cemeteryVehiclesTotal += cemeteryVehicles;
                            cemeteryVehiclesInUse += count;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                this.healthcareVehiclesPanel.Percent = GetUsagePercent(healthcareVehiclesTotal, healthcareVehiclesInUse);
                this.cemeteryVehiclesPanel.Percent = GetUsagePercent(cemeteryVehiclesTotal, cemeteryVehiclesInUse);
                this.crematoriumVehiclesPanel.Percent = GetUsagePercent(crematoriumVehiclesTotal, crematoriumVehiclesInUse);
            }

            if (this.configuration.TrafficJam)
            {
                this.trafficJamPanel.Percent = (int)(100 - (float)Singleton<VehicleManager>.instance.m_lastTrafficFlow);
            }

            if (this.configuration.GroundPollution)
            {
                this.groundPollutionPanel.Percent = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetGroundPollution();
            }

            if (this.configuration.DrinkingWaterPollution)
            {
                this.drinkingWaterPollutionPanel.Percent = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetWaterPollution();
            }

            if (this.configuration.NoisePollution)
            {
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.NoisePollution, out int noisePollution);
                this.noisePollutionPanel.Percent = noisePollution;
            }

            if (this.configuration.FireHazard)
            {
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.FireHazard, out int fireHazard);
                this.fireHazardPanel.Percent = fireHazard;
            }

            if (this.configuration.FireDepartmentVehicles)
            {
                var fireDepartmentVehiclesTotal = 0;
                var fireDepartmentVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var fireDepartmentBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.FireDepartment);

                for (int i = 0; i < fireDepartmentBuildingIds.m_size; i++)
                {
                    var buildingId = fireDepartmentBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI() as FireStationAI;
                    if (buildingAi == null)
                    {
                        continue;
                    }

                    int budget = Singleton<EconomyManager>.instance.GetBudget(buildingAi.m_info.m_class);
                    int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                    int fireTrucks = (productionRate * buildingAi.m_fireTruckCount + 99) / 100;
                    int count = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Fire, ref count, ref cargo, ref capacity, ref outside);

                    fireDepartmentVehiclesTotal += fireTrucks;
                    fireDepartmentVehiclesInUse += count;
                }

                this.fireDepartmentVehiclesPanel.Percent = GetUsagePercent(fireDepartmentVehiclesTotal, fireDepartmentVehiclesInUse);
            }

            if (this.configuration.CrimeRate)
            {
                this.crimeRatePanel.Percent = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalCrimeRate;
            }

            if (this.configuration.PoliceHoldingCells || this.configuration.PoliceVehicles || this.configuration.PrisonCells || this.configuration.PrisonVehicles)
            {
                var policeHoldingCellsTotal = 0;
                var policeHoldingCellsInUse = 0;

                var policeVehiclesTotal = 0;
                var policeVehiclesInUse = 0;

                var prisonCellsTotal = 0;
                var prisonCellsInUse = 0;

                var prisonVehiclesTotal = 0;
                var prisonVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var policeBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

                for (int i = 0; i < policeBuildingIds.m_size; i++)
                {
                    var buildingId = policeBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI() as PoliceStationAI;
                    if (buildingAi == null)
                    {
                        continue;
                    }

                    //PoliceStationAI.GetLocalizedStats
                    var instance = Singleton<CitizenManager>.instance;
                    uint num = building.m_citizenUnits;
                    int num2 = 0;
                    int cellsInUse = 0;
                    while (num != 0u)
                    {
                        uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                        if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Visit) != 0)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(j);
                                if (citizen != 0u && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Visit)
                                {
                                    cellsInUse++;
                                }
                            }
                        }
                        num = nextUnit;
                        if (++num2 > 524288)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }

                    int budget = Singleton<EconomyManager>.instance.GetBudget(buildingAi.m_info.m_class);
                    int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                    int policeCars = (productionRate * buildingAi.m_policeCarCount + 99) / 100;
                    int count = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    if (buildingAi.m_info.m_class.m_level < ItemClass.Level.Level4)
                    {
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Crime, ref count, ref cargo, ref capacity, ref outside);

                        policeHoldingCellsInUse += cellsInUse;
                        policeHoldingCellsTotal += buildingAi.m_jailCapacity;

                        policeVehiclesTotal += policeCars;
                        policeVehiclesInUse += count;
                    }
                    else
                    {
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.CriminalMove, ref count, ref cargo, ref capacity, ref outside);

                        prisonCellsTotal += buildingAi.m_jailCapacity;
                        prisonCellsInUse += cellsInUse;

                        prisonVehiclesTotal += policeCars;
                        prisonVehiclesInUse += count;
                    }
                }

                this.policeHoldingCellsPanel.Percent = GetUsagePercent(policeHoldingCellsTotal, policeHoldingCellsInUse);
                this.policeVehiclesPanel.Percent = GetUsagePercent(policeVehiclesTotal, policeVehiclesInUse);
                this.prisonCellsPanel.Percent = GetUsagePercent(prisonCellsTotal, prisonCellsInUse);
                this.prisonVehiclesPanel.Percent = GetUsagePercent(prisonVehiclesTotal, prisonVehiclesInUse);
            }

            if (this.configuration.Unemployment)
            {
                this.unemploymentPanel.Percent = allDistricts.GetUnemployment();
            }

            if (
                this.configuration.RoadMaintenanceVehicles
                || (this.mapHasSnowDumps && this.configuration.SnowDump)
                || (this.mapHasSnowDumps && this.configuration.SnowDumpVehicles)
            )
            {
                var roadMaintenanceVehiclesTotal = 0;
                var roadMaintenanceVehiclesInUse = 0;

                var snowDumpStorageTotal = 0;
                var snowDumpStorageInUse = 0;

                var snowDumpVehiclesTotal = 0;
                var snowDumpVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var roadMaintenanceBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Road);

                for (int i = 0; i < roadMaintenanceBuildingIds.m_size; i++)
                {
                    var buildingId = roadMaintenanceBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    if (buildingAi is MaintenanceDepotAI maintenanceDepotAi)
                    {
                        int budget = Singleton<EconomyManager>.instance.GetBudget(maintenanceDepotAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                        int trucks = (productionRate * maintenanceDepotAi.m_maintenanceTruckCount + 99) / 100;
                        int truckCount = 0;
                        int cargo = 0;
                        int capacity = 0;
                        int outside = 0;
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.RoadMaintenance, ref truckCount, ref cargo, ref capacity, ref outside);

                        roadMaintenanceVehiclesTotal += trucks;
                        roadMaintenanceVehiclesInUse += truckCount;
                    }
                    else if (this.mapHasSnowDumps && buildingAi is SnowDumpAI snowDumpAi)
                    {
                        int budget = Singleton<EconomyManager>.instance.GetBudget(snowDumpAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                        int snowTrucks = (productionRate * snowDumpAi.m_snowTruckCount + 99) / 100;
                        int count = 0;
                        int cargo = 0;
                        int capacity = 0;
                        int outside = 0;
                        if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
                        {
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Snow, ref count, ref cargo, ref capacity, ref outside);
                        }
                        else
                        {
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.SnowMove, ref count, ref cargo, ref capacity, ref outside);
                        }

                        snowDumpStorageTotal += snowDumpAi.m_snowCapacity;
                        snowDumpStorageInUse += snowDumpAi.GetSnowAmount(buildingId, ref building);

                        snowDumpVehiclesTotal += snowTrucks;
                        snowDumpVehiclesInUse += count;
                    }
                    else
                    {
                        continue;
                    }
                }

                this.roadMaintenanceVehiclesPanel.Percent = GetUsagePercent(roadMaintenanceVehiclesTotal, roadMaintenanceVehiclesInUse);

                if (this.mapHasSnowDumps)
                {
                    this.snowDumpPanel.Percent = GetUsagePercent(snowDumpStorageTotal, snowDumpStorageInUse);
                    this.snowDumpVehiclesPanel.Percent = GetUsagePercent(snowDumpVehiclesTotal, snowDumpVehiclesInUse);
                }
            }

            if (this.configuration.ParkMaintenanceVehicles)
            {
                var parkMaintenanceVehiclesTotal = 0;
                var parkMaintenanceVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var beautificationBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Beautification);

                for (int i = 0; i < beautificationBuildingIds.m_size; i++)
                {
                    var buildingId = beautificationBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    if (buildingAi is MaintenanceDepotAI maintenanceDepotAi)
                    {
                        var transferReason = GameMethods.GetTransferReason(maintenanceDepotAi);
                        if (transferReason == TransferManager.TransferReason.None)
                        {
                            continue;
                        }

                        int budget = Singleton<EconomyManager>.instance.GetBudget(maintenanceDepotAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                        if (transferReason == TransferManager.TransferReason.ParkMaintenance)
                        {
                            DistrictManager instance = Singleton<DistrictManager>.instance;
                            byte district = instance.GetDistrict(building.m_position);
                            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
                            if ((servicePolicies & DistrictPolicies.Services.ParkMaintenanceBoost) != DistrictPolicies.Services.None)
                            {
                                productionRate *= 2;
                            }
                        }
                        int trucks = (productionRate * maintenanceDepotAi.m_maintenanceTruckCount + 99) / 100;
                        int truckCount = 0;
                        int cargo = 0;
                        int capacity = 0;
                        int outside = 0;
                        GameMethods.CalculateOwnVehicles(buildingId, ref building, transferReason, ref truckCount, ref cargo, ref capacity, ref outside);

                        parkMaintenanceVehiclesTotal += trucks;
                        parkMaintenanceVehiclesInUse += truckCount;
                    }
                }

                this.parkMaintenanceVehiclesPanel.Percent = GetUsagePercent(parkMaintenanceVehiclesTotal, parkMaintenanceVehiclesInUse);
            }

            if (this.configuration.CityUnattractiveness)
            {
                Singleton<ImmaterialResourceManager>.instance.CheckGlobalResource(ImmaterialResourceManager.Resource.Attractiveness, out int cityAttractivenessRaw);
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.LandValue, out int landValueRaw);
                var cityAttractivenessAndLandValue = cityAttractivenessRaw + landValueRaw;
                var cityAttractiveness = 100 * (cityAttractivenessAndLandValue) / Mathf.Max(cityAttractivenessAndLandValue + 200, 200);

                this.cityUnattractivenessPanel.Percent = (100 - cityAttractiveness);
            }

            this.UpdateUi(false);

            base.Update();
        }

        private bool UpdateItemDisplay(ItemPanel itemPanel, bool enabled, int? percent, int Threshold, ConfigurationModel configuration)
        {
            var oldItemVisible = itemPanel.isVisible;
            var newItemVisible = GetItemVisibility(enabled, percent, Threshold);

            itemPanel.isVisible = newItemVisible;

            if (newItemVisible)
            {
                itemPanel.Percent = percent;
                itemPanel.PercentButton.text = GetUsagePercentString(percent);
                itemPanel.PercentButton.textColor = GetItemTextColor(percent, Threshold);
                itemPanel.PercentButton.focusedColor = GetItemTextColor(percent, Threshold);
                itemPanel.PercentButton.hoveredTextColor = GetItemHoveredTextColor(percent, Threshold);
            }

            return oldItemVisible != newItemVisible;
        }

        private Color32 GetItemTextColor(int? percent, int Threshold)
        {
            if (!percent.HasValue || percent.Value >= Threshold)
            {
                return this.configuration.MainPanelAccentColor;
            }

            return this.configuration.MainPanelForegroundColor;
        }

        private Color32 GetItemHoveredTextColor(int? percent, int criticalThreshold)
        {
            if (!percent.HasValue || percent.Value >= criticalThreshold)
            {
                return this.configuration.MainPanelForegroundColor;
            }

            return this.configuration.MainPanelAccentColor;
        }

        private bool GetItemVisibility(bool enabled, int? percent, int criticalThreshold)
        {
            if (!enabled)
            {
                return false;
            }

            if (percent.HasValue)
            {
                if (this.configuration.MainPanelHideItemsBelowThreshold)
                {
                    return criticalThreshold < percent.Value;
                }

                return true;
            }
            else
            {
                return !this.configuration.MainPanelHideItemsNotAvailable;
            }
        }

        private int? GetAvailabilityPercent(long capacity, long need)
        {
            if (capacity == 0)
                return null;

            if (need == 0)
                return 0;

            return (int)((1 - need / (float)capacity) * 100);
        }

        private int? GetUsagePercent(long capacity, long usage)
        {
            if (capacity == 0)
                return null;

            if (usage == 0)
                return 0;

            return (int)((usage / (float)capacity) * 100);
        }

        private string GetUsagePercentString(int? percent)
        {
            if (percent.HasValue)
            {
                return percent.Value.ToString() + "%";
            }

            return "-%";
        }

        private void UpdateItemsLayout()
        {
            var lastLayoutPosition = Vector2.zero;
            int index = 0;

            for (int i = 0; i < this.allItemPanels.Count; i++)
            {
                var currentItem = this.allItemPanels[i];
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
            var visibleItemCount = this.allItemPanels.Where(x => x.isVisible).Count();
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

        private float CalculatePanelWidth(int visibleItemCount)
        {
            if (visibleItemCount < this.configuration.MainPanelColumnCount)
            {
                return (visibleItemCount + 1) * this.configuration.ItemPadding + visibleItemCount * this.configuration.ItemWidth;
            }
            else
            {
                return (this.configuration.MainPanelColumnCount + 1) * this.configuration.ItemPadding + this.configuration.MainPanelColumnCount * this.configuration.ItemWidth;
            }
        }

        private float CalculatePanelHeight(int visibleItemCount)
        {
            var rowCount = Mathf.CeilToInt(visibleItemCount / (float)this.configuration.MainPanelColumnCount);
            return (rowCount + 1) * this.configuration.ItemPadding + rowCount * this.configuration.ItemHeight;
        }
    }
}
