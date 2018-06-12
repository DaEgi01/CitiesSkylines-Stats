using Stats.Localization;
using ColossalFramework.UI;
using ICities;
using System;

namespace Stats.Configuration
{
    public class ConfigurationPanel
    {
        private readonly int space = 16;

        private readonly UIHelperBase uiHelperBase;
        private readonly ModFullTitle modFullTitle;
        private readonly ConfigurationModel configuration;
        private readonly LanguageResourceModel languageResource;

        private UICheckBox autoHide;
        private UICheckBox hideItemsBelowTreshold;
        private UISlider updateEveryXSeconds;
        private UISlider columnCountSlider;
        private UISlider itemWidth;
        private UISlider itemHeight;
        private UISlider itemPadding;
        private UISlider itemTextScale;

        private UICheckBox electricity;
        private UISlider electricityCriticalTreshold;
        private UICheckBox heating;
        private UISlider heatingCriticalTreshold;
        private UICheckBox water;
        private UISlider waterCriticalTreshold;
        private UICheckBox sewageTreatment;
        private UISlider sewageTreatmentCriticalTreshold;
        private UICheckBox waterReserveTank;
        private UISlider waterReserveTankCriticalTreshold;
        private UICheckBox waterPumpingServiceStorage;
        private UISlider waterPumpingServiceStorageCriticalTreshold;
        private UICheckBox waterPumpingServiceVehicles;
        private UISlider waterPumpingServiceVehiclesCriticalTreshold;
        private UICheckBox landfill;
        private UISlider landfillCriticalTreshold;
        private UICheckBox landfillVehicles;
        private UISlider landfillVehiclesCriticalTreshold;
        private UICheckBox garbageProcessing;
        private UISlider garbageProcessingCriticalTreshold;
        private UICheckBox garbageProcessingVehicles;
        private UISlider garbageProcessingVehiclesCriticalTreshold;
        private UICheckBox elementarySchool;
        private UISlider elementarySchoolCriticalTreshold;
        private UICheckBox highSchool;
        private UISlider highSchoolCriticalTreshold;
        private UICheckBox university;
        private UISlider universityCriticalTreshold;
        private UICheckBox healthcare;
        private UISlider healthcareCriticalTreshold;
        private UICheckBox healthcareVehicles;
        private UISlider healthcareVehiclesCriticalTreshold;
        private UICheckBox averageIllnessRate;
        private UISlider averageIllnessRateCriticalTreshold;
        private UICheckBox cemetery;
        private UISlider cemeteryCriticalTreshold;
        private UICheckBox cemeteryVehicles;
        private UISlider cemeteryVehiclesCriticalTreshold;
        private UICheckBox crematorium;
        private UISlider crematoriumCriticalTreshold;
        private UICheckBox crematoriumVehicles;
        private UISlider crematoriumVehiclesCriticalTreshold;
        private UICheckBox groundPollution;
        private UISlider groundPollutionCriticalTreshold;
        private UICheckBox drinkingWaterPollution;
        private UISlider drinkingWaterPollutionCriticalTreshold;
        private UICheckBox noisePollution;
        private UISlider noisePollutionCriticalTreshold;
        private UICheckBox fireHazard;
        private UISlider fireHazardCriticalTreshold;
        private UICheckBox fireDepartmentVehicles;
        private UISlider fireDepartmentVehiclesCriticalTreshold;
        private UICheckBox crimeRate;
        private UISlider crimeRateCriticalTreshold;
        private UICheckBox policeHoldingCells;
        private UISlider policeHoldingCellsCriticalTreshold;
        private UICheckBox policeVehicles;
        private UISlider policeVehiclesCriticalTreshold;
        private UICheckBox prisonCells;
        private UISlider prisonCellsCriticalTreshold;
        private UICheckBox prisonVehicles;
        private UISlider prisonVehiclesCriticalTreshold;
        private UICheckBox unemployment;
        private UISlider unemploymentCriticalTreshold;
        private UICheckBox trafficJam;
        private UISlider trafficJamCriticalTreshold;
        private UICheckBox roadMaintenanceVehicles;
        private UISlider roadMaintenanceVehiclesCriticalTreshold;
        private UICheckBox snowDump;
        private UISlider snowDumpCriticalTreshold;
        private UICheckBox snowDumpVehicles;
        private UISlider snowDumpVehiclesCriticalTreshold;
        private UICheckBox parkMaintenanceVehicles;
        private UISlider parkMaintenanceVehiclesCriticalTreshold;
        private UICheckBox cityUnattractiveness;
        private UISlider cityUnattractivenessCriticalTreshold;

        public ConfigurationPanel(UIHelperBase uiHelperBase, ModFullTitle modFullTitle, ConfigurationModel configuration, LanguageResourceModel languageResource)
        {
            this.uiHelperBase = uiHelperBase ?? throw new ArgumentNullException(nameof(uiHelperBase));
            this.modFullTitle = modFullTitle ?? throw new ArgumentNullException(nameof(modFullTitle));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
        }

        public void Initialize()
        {
            var mainGroupUiHelper = this.uiHelperBase.AddGroup(this.modFullTitle);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

            mainGroupUiHelper.AddButton(this.languageResource.Reset, () =>
            {
                this.configuration.ResetToDefault();
                this.configuration.Save();
                this.UpdateUi();
            });

            mainGroupUiHelper.AddSpace(space);

            var mainPanelGroupUiHelper = mainGroupUiHelper.AddGroup(languageResource.MainWindow);
            var mainPanelGroupContentPanel = (mainPanelGroupUiHelper as UIHelper).self as UIPanel;
            mainPanelGroupContentPanel.backgroundSprite = string.Empty;

            this.updateEveryXSeconds = mainPanelGroupUiHelper.AddSliderWithLabel(this.languageResource.UpdateEveryXSeconds, 0, 30, 1, this.configuration.MainPanelUpdateEveryXSeconds, value =>
            {
                this.configuration.MainPanelUpdateEveryXSeconds = (int)value;
                this.configuration.Save();
            });

            this.columnCountSlider = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ColumnCount, 1, 30, 1, this.configuration.MainPanelColumnCount, value =>
            {
                this.configuration.MainPanelColumnCount = (int)value;
                this.configuration.Save();
            });

            this.itemWidth = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemWidth, 10, 100, 1, this.configuration.ItemWidth, value =>
            {
                this.configuration.ItemWidth = value;
                this.configuration.Save();
            });

            this.itemHeight = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemHeight, 10, 100, 1, this.configuration.ItemHeight, value =>
            {
                this.configuration.ItemHeight = value;
                this.configuration.Save();
            });

            this.itemPadding = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemPadding, 0, 30, 1, this.configuration.ItemPadding, value =>
            {
                this.configuration.ItemPadding = value;
                this.configuration.Save();
            });

            this.itemTextScale = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemTextScale, 0, 4, 0.1f, this.configuration.ItemTextScale, value =>
            {
                this.configuration.ItemTextScale = value;
                this.configuration.Save();
            });

            this.autoHide = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.AutoHide, this.configuration.MainPanelAutoHide, _checked =>
            {
                this.configuration.MainPanelAutoHide = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.hideItemsBelowTreshold = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsBelowTreshold, this.configuration.MainPanelHideItemsBelowTreshold, _checked =>
            {
                this.configuration.MainPanelHideItemsBelowTreshold = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            var itemGroupUiHelper = mainGroupUiHelper.AddGroup(languageResource.Items);
            var itemGroupContentPanel = (itemGroupUiHelper as UIHelper).self as UIPanel;
            itemGroupContentPanel.backgroundSprite = string.Empty;

            this.electricity = itemGroupUiHelper.AddCheckbox(languageResource.Electricity, this.configuration.Electricity, _checked =>
            {
                this.configuration.Electricity = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.electricityCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.ElectricityCriticalTreshold, value =>
            {
                this.configuration.ElectricityCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.heating = itemGroupUiHelper.AddCheckbox(languageResource.Heating, this.configuration.Heating, _checked =>
            {
                this.configuration.Heating = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.heatingCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.HeatingCriticalTreshold, value =>
            {
                this.configuration.HeatingCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.water = itemGroupUiHelper.AddCheckbox(languageResource.Water, this.configuration.Water, _checked =>
            {
                this.configuration.Water = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.WaterCriticalTreshold, value =>
            {
                this.configuration.WaterCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.sewageTreatment = itemGroupUiHelper.AddCheckbox(languageResource.SewageTreatment, this.configuration.SewageTreatment, _checked =>
            {
                this.configuration.SewageTreatment = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.sewageTreatmentCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.SewageTreatmentCriticalTreshold, value =>
            {
                this.configuration.SewageTreatmentCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.waterReserveTank = itemGroupUiHelper.AddCheckbox(languageResource.WaterReserveTank, this.configuration.WaterReserveTank, _checked =>
            {
                this.configuration.WaterReserveTank = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterReserveTankCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.WaterReserveTankCriticalTreshold, value =>
            {
                this.configuration.WaterReserveTankCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.waterPumpingServiceStorage = itemGroupUiHelper.AddCheckbox(languageResource.WaterPumpingServiceStorage, this.configuration.WaterPumpingServiceStorage, _checked =>
            {
                this.configuration.WaterPumpingServiceStorage = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterPumpingServiceStorageCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.WaterPumpingServiceStorageCriticalTreshold, value =>
            {
                this.configuration.WaterPumpingServiceStorageCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.waterPumpingServiceVehicles = itemGroupUiHelper.AddCheckbox(languageResource.WaterPumpingServiceVehicles, this.configuration.WaterPumpingServiceVehicles, _checked =>
            {
                this.configuration.WaterPumpingServiceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterPumpingServiceVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.WaterPumpingServiceVehiclesCriticalTreshold, value =>
            {
                this.configuration.WaterPumpingServiceVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.landfill = itemGroupUiHelper.AddCheckbox(languageResource.Landfill, this.configuration.Landfill, _checked =>
            {
                this.configuration.Landfill = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.landfillCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.LandfillCriticalTreshold, value =>
            {
                this.configuration.LandfillCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.landfillVehicles = itemGroupUiHelper.AddCheckbox(languageResource.LandfillVehicles, this.configuration.LandfillVehicles, _checked =>
            {
                this.configuration.LandfillVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.landfillVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.LandfillVehiclesCriticalTreshold, value =>
            {
                this.configuration.LandfillVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.garbageProcessing = itemGroupUiHelper.AddCheckbox(languageResource.GarbageProcessing, this.configuration.GarbageProcessing, _checked =>
            {
                this.configuration.GarbageProcessing = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.garbageProcessingCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.GarbageProcessingCriticalTreshold, value =>
            {
                this.configuration.GarbageProcessingCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.garbageProcessingVehicles = itemGroupUiHelper.AddCheckbox(languageResource.GarbageProcessingVehicles, this.configuration.GarbageProcessingVehicles, _checked =>
            {
                this.configuration.GarbageProcessingVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.garbageProcessingVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.GarbageProcessingVehiclesCriticalTreshold, value =>
            {
                this.configuration.GarbageProcessingVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.elementarySchool = itemGroupUiHelper.AddCheckbox(languageResource.ElementarySchool, this.configuration.ElementarySchool, _checked =>
            {
                this.configuration.ElementarySchool = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.elementarySchoolCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.ElementarySchoolCriticalTreshold, value =>
            {
                this.configuration.ElementarySchoolCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.highSchool = itemGroupUiHelper.AddCheckbox(languageResource.HighSchool, this.configuration.HighSchool, _checked =>
            {
                this.configuration.HighSchool = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.highSchoolCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.HighSchoolCriticalTreshold, value =>
            {
                this.configuration.HighSchoolCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.university = itemGroupUiHelper.AddCheckbox(languageResource.University, this.configuration.University, _checked =>
            {
                this.configuration.University = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.universityCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.UniversityCriticalTreshold, value =>
            {
                this.configuration.UniversityCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.healthcare = itemGroupUiHelper.AddCheckbox(languageResource.Healthcare, this.configuration.Healthcare, _checked =>
            {
                this.configuration.Healthcare = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.healthcareCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.HealthcareCriticalTreshold, value =>
            {
                this.configuration.HealthcareCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.healthcareVehicles = itemGroupUiHelper.AddCheckbox(languageResource.HealthcareVehicles, this.configuration.HealthcareVehicles, _checked =>
            {
                this.configuration.HealthcareVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.healthcareVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.HealthcareVehiclesCriticalTreshold, value =>
            {
                this.configuration.HealthcareVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.averageIllnessRate = itemGroupUiHelper.AddCheckbox(languageResource.AverageIllnessRate, this.configuration.AverageIllnessRate, _checked =>
            {
                this.configuration.AverageIllnessRate = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.averageIllnessRateCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.AverageIllnessRateCriticalTreshold, value =>
            {
                this.configuration.AverageIllnessRateCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.cemetery = itemGroupUiHelper.AddCheckbox(languageResource.Cemetery, this.configuration.Cemetery, _checked =>
            {
                this.configuration.Cemetery = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.cemeteryCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.CemeteryCriticalTreshold, value =>
            {
                this.configuration.CemeteryCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.cemeteryVehicles = itemGroupUiHelper.AddCheckbox(languageResource.CemeteryVehicles, this.configuration.CemeteryVehicles, _checked =>
            {
                this.configuration.CemeteryVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.cemeteryVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.CemeteryVehiclesCriticalTreshold, value =>
            {
                this.configuration.CemeteryVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.crematorium = itemGroupUiHelper.AddCheckbox(languageResource.Crematorium, this.configuration.Crematorium, _checked =>
            {
                this.configuration.Crematorium = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.crematoriumCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.CrematoriumCriticalTreshold, value =>
            {
                this.configuration.CrematoriumCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.crematoriumVehicles = itemGroupUiHelper.AddCheckbox(languageResource.CrematoriumVehicles, this.configuration.Crematorium, _checked =>
            {
                this.configuration.CrematoriumVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.crematoriumVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.CrematoriumVehiclesCriticalTreshold, value =>
            {
                this.configuration.CrematoriumVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.groundPollution = itemGroupUiHelper.AddCheckbox(languageResource.GroundPollution, this.configuration.GroundPollution, _checked =>
            {
                this.configuration.GroundPollution = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.groundPollutionCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.GroundPollutionCriticalTreshold, value =>
            {
                this.configuration.GroundPollutionCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.drinkingWaterPollution = itemGroupUiHelper.AddCheckbox(languageResource.DrinkingWaterPollution, this.configuration.DrinkingWaterPollution, _checked =>
            {
                this.configuration.DrinkingWaterPollution = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.drinkingWaterPollutionCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.DrinkingWaterPollutionCriticalTreshold, value =>
            {
                this.configuration.DrinkingWaterPollutionCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.noisePollution = itemGroupUiHelper.AddCheckbox(languageResource.NoisePollution, this.configuration.NoisePollution, _checked =>
            {
                this.configuration.NoisePollution = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.noisePollutionCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.NoisePollutionCriticalTreshold, value =>
            {
                this.configuration.NoisePollutionCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.fireHazard = itemGroupUiHelper.AddCheckbox(languageResource.FireHazard, this.configuration.FireHazard, _checked =>
            {
                this.configuration.FireHazard = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.fireHazardCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.FireHazardCriticalTreshold, value =>
            {
                this.configuration.FireHazardCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.fireDepartmentVehicles = itemGroupUiHelper.AddCheckbox(languageResource.FireDepartmentVehicles, this.configuration.FireDepartmentVehicles, _checked =>
            {
                this.configuration.FireDepartmentVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.fireDepartmentVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.FireDepartmentVehiclesCriticalTreshold, value =>
            {
                this.configuration.FireDepartmentVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.crimeRate = itemGroupUiHelper.AddCheckbox(languageResource.CrimeRate, this.configuration.CrimeRate, _checked =>
            {
                this.configuration.CrimeRate = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.crimeRateCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.CrimeRateCriticalTreshold, value =>
            {
                this.configuration.CrimeRateCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.policeHoldingCells = itemGroupUiHelper.AddCheckbox(languageResource.PoliceHoldingCells, this.configuration.PoliceHoldingCells, _checked =>
            {
                this.configuration.PoliceHoldingCells = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.policeHoldingCellsCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.PoliceHoldingCellsCriticalTreshold, value =>
            {
                this.configuration.PoliceHoldingCellsCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.policeVehicles = itemGroupUiHelper.AddCheckbox(languageResource.PoliceVehicles, this.configuration.PoliceVehicles, _checked =>
            {
                this.configuration.PoliceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.policeVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.PoliceVehiclesCriticalTreshold, value =>
            {
                this.configuration.PoliceVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.prisonCells = itemGroupUiHelper.AddCheckbox(languageResource.PrisonCells, this.configuration.PrisonCells, _checked =>
            {
                this.configuration.PrisonCells = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.prisonCellsCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.PrisonCellsCriticalTreshold, value =>
            {
                this.configuration.PrisonCellsCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.prisonVehicles = itemGroupUiHelper.AddCheckbox(languageResource.PrisonVehicles, this.configuration.PrisonVehicles, _checked =>
            {
                this.configuration.PrisonVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.prisonVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.PrisonVehiclesCriticalTreshold, value =>
            {
                this.configuration.PrisonVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.unemployment = itemGroupUiHelper.AddCheckbox(languageResource.Unemployment, this.configuration.Unemployment, _checked =>
            {
                this.configuration.Unemployment = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.unemploymentCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.UnemploymentCriticalTreshold, value =>
            {
                this.configuration.UnemploymentCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.trafficJam = itemGroupUiHelper.AddCheckbox(languageResource.TrafficJam, this.configuration.TrafficJam, _checked =>
            {
                this.configuration.TrafficJam = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.trafficJamCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.TrafficJamCriticalTreshold, value =>
            {
                this.configuration.TrafficJamCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.roadMaintenanceVehicles = itemGroupUiHelper.AddCheckbox(languageResource.RoadMaintenanceVehicles, this.configuration.RoadMaintenanceVehicles, _checked =>
            {
                this.configuration.RoadMaintenanceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.roadMaintenanceVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.RoadMaintenanceVehiclesCriticalTreshold, value =>
            {
                this.configuration.RoadMaintenanceVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.snowDump = itemGroupUiHelper.AddCheckbox(languageResource.SnowDump, this.configuration.SnowDump, _checked =>
            {
                this.configuration.SnowDump = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.snowDumpCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.SnowDumpCriticalTreshold, value =>
            {
                this.configuration.SnowDumpCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.snowDumpVehicles = itemGroupUiHelper.AddCheckbox(languageResource.SnowDumpVehicles, this.configuration.SnowDumpVehicles, _checked =>
            {
                this.configuration.SnowDumpVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.snowDumpVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.SnowDumpVehiclesCriticalTreshold, value =>
            {
                this.configuration.SnowDumpVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.parkMaintenanceVehicles = itemGroupUiHelper.AddCheckbox(languageResource.ParkMaintenanceVehicles, this.configuration.ParkMaintenanceVehicles, _checked =>
            {
                this.configuration.ParkMaintenanceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.parkMaintenanceVehiclesCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.ParkMaintenanceVehiclesCriticalTreshold, value =>
            {
                this.configuration.ParkMaintenanceVehiclesCriticalTreshold = (int)value;
                this.configuration.Save();
            });

            this.cityUnattractiveness = itemGroupUiHelper.AddCheckbox(languageResource.CityUnattractiveness, this.configuration.CityUnattractiveness, _checked =>
            {
                this.configuration.CityUnattractiveness = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.cityUnattractivenessCriticalTreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalTreshold, 0, 100, 1, this.configuration.CityUnattractivenessCriticalTreshold, value =>
            {
                this.configuration.CityUnattractivenessCriticalTreshold = (int)value;
                this.configuration.Save();
            });
        }

        private void UpdateUi()
        {
            this.updateEveryXSeconds.value = this.configuration.MainPanelUpdateEveryXSeconds;
            this.autoHide.isChecked = this.configuration.MainPanelAutoHide;
            this.columnCountSlider.value = this.configuration.MainPanelColumnCount;
            this.itemWidth.value = this.configuration.ItemWidth;
            this.itemHeight.value = this.configuration.ItemHeight;
            this.itemPadding.value = this.configuration.ItemPadding;
            this.itemTextScale.value = this.configuration.ItemTextScale;
            this.electricity.isChecked = this.configuration.Electricity;
            this.heating.isChecked = this.configuration.Heating;
            this.water.isChecked = this.configuration.Water;
            this.sewageTreatment.isChecked = this.configuration.SewageTreatment;
            this.waterReserveTank.isChecked = this.configuration.WaterReserveTank;
            this.waterPumpingServiceStorage.isChecked = this.configuration.WaterPumpingServiceStorage;
            this.waterPumpingServiceVehicles.isChecked = this.configuration.WaterPumpingServiceVehicles;
            this.landfill.isChecked = this.configuration.Landfill;
            this.landfillVehicles.isChecked = this.configuration.LandfillVehicles;
            this.garbageProcessing.isChecked = this.configuration.GarbageProcessing;
            this.garbageProcessingVehicles.isChecked = this.configuration.GarbageProcessingVehicles;
            this.elementarySchool.isChecked = this.configuration.ElementarySchool;
            this.highSchool.isChecked = this.configuration.HighSchool;
            this.university.isChecked = this.configuration.University;
            this.healthcare.isChecked = this.configuration.Healthcare;
            this.healthcareVehicles.isChecked = this.configuration.HealthcareVehicles;
            this.averageIllnessRate.isChecked = this.configuration.AverageIllnessRate;
            this.cemetery.isChecked = this.configuration.Cemetery;
            this.cemeteryVehicles.isChecked = this.configuration.CemeteryVehicles;
            this.crematorium.isChecked = this.configuration.Crematorium;
            this.crematoriumVehicles.isChecked = this.configuration.CrematoriumVehicles;
            this.groundPollution.isChecked = this.configuration.GroundPollution;
            this.drinkingWaterPollution.isChecked = this.configuration.DrinkingWaterPollution;
            this.noisePollution.isChecked = this.configuration.NoisePollution;
            this.fireHazard.isChecked = this.configuration.FireHazard;
            this.fireDepartmentVehicles.isChecked = this.configuration.FireDepartmentVehicles;
            this.crimeRate.isChecked = this.configuration.CrimeRate;
            this.policeHoldingCells.isChecked = this.configuration.PoliceHoldingCells;
            this.policeVehicles.isChecked = this.configuration.PoliceVehicles;
            this.prisonCells.isChecked = this.configuration.PrisonCells;
            this.prisonVehicles.isChecked = this.configuration.PrisonVehicles;
            this.unemployment.isChecked = this.configuration.Unemployment;
            this.trafficJam.isChecked = this.configuration.TrafficJam;
            this.roadMaintenanceVehicles.isChecked = this.configuration.RoadMaintenanceVehicles;
            this.snowDump.isChecked = this.configuration.SnowDump;
            this.snowDumpVehicles.isChecked = this.configuration.SnowDumpVehicles;
            this.parkMaintenanceVehicles.isChecked = this.configuration.ParkMaintenanceVehicles;
            this.cityUnattractiveness.isChecked = this.configuration.CityUnattractiveness;
        }
    }
}
