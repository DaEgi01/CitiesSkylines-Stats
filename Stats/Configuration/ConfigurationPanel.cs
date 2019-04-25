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
        private readonly LanguageResourceModel languageResource;

        private ConfigurationModel configuration;

        private UISlider updateEveryXSeconds;
        private UISlider columnCountSlider;
        private UISlider itemWidth;
        private UISlider itemHeight;
        private UISlider itemPadding;
        private UISlider itemTextScale;
        private UICheckBox autoHide;
        private UICheckBox hideItemsBelowThreshold;
        private UICheckBox hideItemsNotAvailable;

        private UICheckBox electricity;
        private UISlider electricityCriticalThreshold;

        private UICheckBox heating;
        private UISlider heatingCriticalThreshold;

        private UICheckBox water;
        private UISlider waterCriticalThreshold;

        private UICheckBox sewageTreatment;
        private UISlider sewageTreatmentCriticalThreshold;

        private UICheckBox waterReserveTank;
        private UISlider waterReserveTankCriticalThreshold;

        private UICheckBox waterPumpingServiceStorage;
        private UISlider waterPumpingServiceStorageCriticalThreshold;

        private UICheckBox waterPumpingServiceVehicles;
        private UISlider waterPumpingServiceVehiclesCriticalThreshold;

        private UICheckBox landfill;
        private UISlider landfillCriticalThreshold;

        private UICheckBox landfillVehicles;
        private UISlider landfillVehiclesCriticalThreshold;

        private UICheckBox garbageProcessing;
        private UISlider garbageProcessingCriticalThreshold;

        private UICheckBox garbageProcessingVehicles;
        private UISlider garbageProcessingVehiclesCriticalThreshold;

        private UICheckBox elementarySchool;
        private UISlider elementarySchoolCriticalThreshold;

        private UICheckBox highSchool;
        private UISlider highSchoolCriticalThreshold;

        private UICheckBox university;
        private UISlider universityCriticalThreshold;

        private UICheckBox healthcare;
        private UISlider healthcareCriticalThreshold;

        private UICheckBox healthcareVehicles;
        private UISlider healthcareVehiclesCriticalThreshold;

        private UICheckBox medicalHelicopters;
        private UISlider medicalHelicoptersCriticalThreshold;

        private UICheckBox averageIllnessRate;
        private UISlider averageIllnessRateCriticalThreshold;

        private UICheckBox cemetery;
        private UISlider cemeteryCriticalThreshold;

        private UICheckBox cemeteryVehicles;
        private UISlider cemeteryVehiclesCriticalThreshold;

        private UICheckBox crematorium;
        private UISlider crematoriumCriticalThreshold;

        private UICheckBox crematoriumVehicles;
        private UISlider crematoriumVehiclesCriticalThreshold;

        private UICheckBox groundPollution;
        private UISlider groundPollutionCriticalThreshold;

        private UICheckBox drinkingWaterPollution;
        private UISlider drinkingWaterPollutionCriticalThreshold;

        private UICheckBox noisePollution;
        private UISlider noisePollutionCriticalThreshold;

        private UICheckBox fireHazard;
        private UISlider fireHazardCriticalThreshold;

        private UICheckBox fireDepartmentVehicles;
        private UISlider fireDepartmentVehiclesCriticalThreshold;

        private UICheckBox fireHelicopters;
        private UISlider fireHelicoptersCriticalThreshold;

        private UICheckBox crimeRate;
        private UISlider crimeRateCriticalThreshold;

        private UICheckBox policeHoldingCells;
        private UISlider policeHoldingCellsCriticalThreshold;

        private UICheckBox policeVehicles;
        private UISlider policeVehiclesCriticalThreshold;

        private UICheckBox policeHelicopters;
        private UISlider policeHelicoptersCriticalThreshold;

        private UICheckBox prisonCells;
        private UISlider prisonCellsCriticalThreshold;

        private UICheckBox prisonVehicles;
        private UISlider prisonVehiclesCriticalThreshold;

        private UICheckBox unemployment;
        private UISlider unemploymentCriticalThreshold;

        private UICheckBox trafficJam;
        private UISlider trafficJamCriticalThreshold;

        private UICheckBox roadMaintenanceVehicles;
        private UISlider roadMaintenanceVehiclesCriticalThreshold;

        private UICheckBox snowDump;
        private UISlider snowDumpCriticalThreshold;

        private UICheckBox snowDumpVehicles;
        private UISlider snowDumpVehiclesCriticalThreshold;

        private UICheckBox parkMaintenanceVehicles;
        private UISlider parkMaintenanceVehiclesCriticalThreshold;

        private UICheckBox cityUnattractiveness;
        private UISlider cityUnattractivenessCriticalThreshold;

        private UICheckBox taxis;
        private UISlider taxisCriticalThreshold;

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
                this.configuration.Reset();
                this.UpdateUiFromModel();
            });

            mainGroupUiHelper.AddButton(this.languageResource.ResetPosition, () =>
            {
                this.configuration.ResetPosition();
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

            this.hideItemsBelowThreshold = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsBelowThreshold, this.configuration.MainPanelHideItemsBelowThreshold, _checked =>
            {
                this.configuration.MainPanelHideItemsBelowThreshold = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.hideItemsNotAvailable = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsNotAvailable, this.configuration.MainPanelHideItemsNotAvailable, _checked =>
            {
                this.configuration.MainPanelHideItemsNotAvailable = _checked;
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

            this.electricityCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.ElectricityCriticalThreshold, value =>
            {
                this.configuration.ElectricityCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.heating = itemGroupUiHelper.AddCheckbox(languageResource.Heating, this.configuration.Heating, _checked =>
            {
                this.configuration.Heating = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.heatingCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.HeatingCriticalThreshold, value =>
            {
                this.configuration.HeatingCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.water = itemGroupUiHelper.AddCheckbox(languageResource.Water, this.configuration.Water, _checked =>
            {
                this.configuration.Water = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.WaterCriticalThreshold, value =>
            {
                this.configuration.WaterCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.sewageTreatment = itemGroupUiHelper.AddCheckbox(languageResource.SewageTreatment, this.configuration.SewageTreatment, _checked =>
            {
                this.configuration.SewageTreatment = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.sewageTreatmentCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.SewageTreatmentCriticalThreshold, value =>
            {
                this.configuration.SewageTreatmentCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.waterReserveTank = itemGroupUiHelper.AddCheckbox(languageResource.WaterReserveTank, this.configuration.WaterReserveTank, _checked =>
            {
                this.configuration.WaterReserveTank = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterReserveTankCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.WaterReserveTankCriticalThreshold, value =>
            {
                this.configuration.WaterReserveTankCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.waterPumpingServiceStorage = itemGroupUiHelper.AddCheckbox(languageResource.WaterPumpingServiceStorage, this.configuration.WaterPumpingServiceStorage, _checked =>
            {
                this.configuration.WaterPumpingServiceStorage = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterPumpingServiceStorageCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.WaterPumpingServiceStorageCriticalThreshold, value =>
            {
                this.configuration.WaterPumpingServiceStorageCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.waterPumpingServiceVehicles = itemGroupUiHelper.AddCheckbox(languageResource.WaterPumpingServiceVehicles, this.configuration.WaterPumpingServiceVehicles, _checked =>
            {
                this.configuration.WaterPumpingServiceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.waterPumpingServiceVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.WaterPumpingServiceVehiclesCriticalThreshold, value =>
            {
                this.configuration.WaterPumpingServiceVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.landfill = itemGroupUiHelper.AddCheckbox(languageResource.Landfill, this.configuration.Landfill, _checked =>
            {
                this.configuration.Landfill = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.landfillCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.LandfillCriticalThreshold, value =>
            {
                this.configuration.LandfillCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.landfillVehicles = itemGroupUiHelper.AddCheckbox(languageResource.LandfillVehicles, this.configuration.LandfillVehicles, _checked =>
            {
                this.configuration.LandfillVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.landfillVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.LandfillVehiclesCriticalThreshold, value =>
            {
                this.configuration.LandfillVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.garbageProcessing = itemGroupUiHelper.AddCheckbox(languageResource.GarbageProcessing, this.configuration.GarbageProcessing, _checked =>
            {
                this.configuration.GarbageProcessing = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.garbageProcessingCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.GarbageProcessingCriticalThreshold, value =>
            {
                this.configuration.GarbageProcessingCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.garbageProcessingVehicles = itemGroupUiHelper.AddCheckbox(languageResource.GarbageProcessingVehicles, this.configuration.GarbageProcessingVehicles, _checked =>
            {
                this.configuration.GarbageProcessingVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.garbageProcessingVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.GarbageProcessingVehiclesCriticalThreshold, value =>
            {
                this.configuration.GarbageProcessingVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.elementarySchool = itemGroupUiHelper.AddCheckbox(languageResource.ElementarySchool, this.configuration.ElementarySchool, _checked =>
            {
                this.configuration.ElementarySchool = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.elementarySchoolCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.ElementarySchoolCriticalThreshold, value =>
            {
                this.configuration.ElementarySchoolCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.highSchool = itemGroupUiHelper.AddCheckbox(languageResource.HighSchool, this.configuration.HighSchool, _checked =>
            {
                this.configuration.HighSchool = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.highSchoolCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.HighSchoolCriticalThreshold, value =>
            {
                this.configuration.HighSchoolCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.university = itemGroupUiHelper.AddCheckbox(languageResource.University, this.configuration.University, _checked =>
            {
                this.configuration.University = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.universityCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.UniversityCriticalThreshold, value =>
            {
                this.configuration.UniversityCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.healthcare = itemGroupUiHelper.AddCheckbox(languageResource.Healthcare, this.configuration.Healthcare, _checked =>
            {
                this.configuration.Healthcare = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.healthcareCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.HealthcareCriticalThreshold, value =>
            {
                this.configuration.HealthcareCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.healthcareVehicles = itemGroupUiHelper.AddCheckbox(languageResource.HealthcareVehicles, this.configuration.HealthcareVehicles, _checked =>
            {
                this.configuration.HealthcareVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.healthcareVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.HealthcareVehiclesCriticalThreshold, value =>
            {
                this.configuration.HealthcareVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.medicalHelicopters = itemGroupUiHelper.AddCheckbox(languageResource.MedicalHelicopters, this.configuration.MedicalHelicopters, _checked =>
            {
                this.configuration.MedicalHelicopters = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.medicalHelicoptersCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.MedicalHelicoptersCriticalThreshold, value =>
            {
                this.configuration.MedicalHelicoptersCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.averageIllnessRate = itemGroupUiHelper.AddCheckbox(languageResource.AverageIllnessRate, this.configuration.AverageIllnessRate, _checked =>
            {
                this.configuration.AverageIllnessRate = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.averageIllnessRateCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.AverageIllnessRateCriticalThreshold, value =>
            {
                this.configuration.AverageIllnessRateCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.cemetery = itemGroupUiHelper.AddCheckbox(languageResource.Cemetery, this.configuration.Cemetery, _checked =>
            {
                this.configuration.Cemetery = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.cemeteryCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.CemeteryCriticalThreshold, value =>
            {
                this.configuration.CemeteryCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.cemeteryVehicles = itemGroupUiHelper.AddCheckbox(languageResource.CemeteryVehicles, this.configuration.CemeteryVehicles, _checked =>
            {
                this.configuration.CemeteryVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.cemeteryVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.CemeteryVehiclesCriticalThreshold, value =>
            {
                this.configuration.CemeteryVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.crematorium = itemGroupUiHelper.AddCheckbox(languageResource.Crematorium, this.configuration.Crematorium, _checked =>
            {
                this.configuration.Crematorium = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.crematoriumCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.CrematoriumCriticalThreshold, value =>
            {
                this.configuration.CrematoriumCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.crematoriumVehicles = itemGroupUiHelper.AddCheckbox(languageResource.CrematoriumVehicles, this.configuration.CrematoriumVehicles, _checked =>
            {
                this.configuration.CrematoriumVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.crematoriumVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.CrematoriumVehiclesCriticalThreshold, value =>
            {
                this.configuration.CrematoriumVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.groundPollution = itemGroupUiHelper.AddCheckbox(languageResource.GroundPollution, this.configuration.GroundPollution, _checked =>
            {
                this.configuration.GroundPollution = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.groundPollutionCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.GroundPollutionCriticalThreshold, value =>
            {
                this.configuration.GroundPollutionCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.drinkingWaterPollution = itemGroupUiHelper.AddCheckbox(languageResource.DrinkingWaterPollution, this.configuration.DrinkingWaterPollution, _checked =>
            {
                this.configuration.DrinkingWaterPollution = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.drinkingWaterPollutionCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.DrinkingWaterPollutionCriticalThreshold, value =>
            {
                this.configuration.DrinkingWaterPollutionCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.noisePollution = itemGroupUiHelper.AddCheckbox(languageResource.NoisePollution, this.configuration.NoisePollution, _checked =>
            {
                this.configuration.NoisePollution = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.noisePollutionCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.NoisePollutionCriticalThreshold, value =>
            {
                this.configuration.NoisePollutionCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.fireHazard = itemGroupUiHelper.AddCheckbox(languageResource.FireHazard, this.configuration.FireHazard, _checked =>
            {
                this.configuration.FireHazard = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.fireHazardCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.FireHazardCriticalThreshold, value =>
            {
                this.configuration.FireHazardCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.fireDepartmentVehicles = itemGroupUiHelper.AddCheckbox(languageResource.FireDepartmentVehicles, this.configuration.FireDepartmentVehicles, _checked =>
            {
                this.configuration.FireDepartmentVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.fireDepartmentVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.FireDepartmentVehiclesCriticalThreshold, value =>
            {
                this.configuration.FireDepartmentVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.fireHelicopters = itemGroupUiHelper.AddCheckbox(languageResource.FireHelicopters, this.configuration.FireHelicopters, _checked =>
            {
                this.configuration.FireHelicopters = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.fireHelicoptersCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.FireHelicoptersCriticalThreshold, value =>
            {
                this.configuration.FireHelicoptersCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.crimeRate = itemGroupUiHelper.AddCheckbox(languageResource.CrimeRate, this.configuration.CrimeRate, _checked =>
            {
                this.configuration.CrimeRate = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.crimeRateCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.CrimeRateCriticalThreshold, value =>
            {
                this.configuration.CrimeRateCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.policeHoldingCells = itemGroupUiHelper.AddCheckbox(languageResource.PoliceHoldingCells, this.configuration.PoliceHoldingCells, _checked =>
            {
                this.configuration.PoliceHoldingCells = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.policeHoldingCellsCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.PoliceHoldingCellsCriticalThreshold, value =>
            {
                this.configuration.PoliceHoldingCellsCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.policeVehicles = itemGroupUiHelper.AddCheckbox(languageResource.PoliceVehicles, this.configuration.PoliceVehicles, _checked =>
            {
                this.configuration.PoliceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.policeVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.PoliceVehiclesCriticalThreshold, value =>
            {
                this.configuration.PoliceVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.policeHelicopters = itemGroupUiHelper.AddCheckbox(languageResource.PoliceHelicopters, this.configuration.PoliceHelicopters, _checked =>
            {
                this.configuration.PoliceHelicopters = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.policeHelicoptersCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.PoliceHelicoptersCriticalThreshold, value =>
            {
                this.configuration.PoliceHelicoptersCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.prisonCells = itemGroupUiHelper.AddCheckbox(languageResource.PrisonCells, this.configuration.PrisonCells, _checked =>
            {
                this.configuration.PrisonCells = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.prisonCellsCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.PrisonCellsCriticalThreshold, value =>
            {
                this.configuration.PrisonCellsCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.prisonVehicles = itemGroupUiHelper.AddCheckbox(languageResource.PrisonVehicles, this.configuration.PrisonVehicles, _checked =>
            {
                this.configuration.PrisonVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.prisonVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.PrisonVehiclesCriticalThreshold, value =>
            {
                this.configuration.PrisonVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.unemployment = itemGroupUiHelper.AddCheckbox(languageResource.Unemployment, this.configuration.Unemployment, _checked =>
            {
                this.configuration.Unemployment = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.unemploymentCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.UnemploymentCriticalThreshold, value =>
            {
                this.configuration.UnemploymentCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.trafficJam = itemGroupUiHelper.AddCheckbox(languageResource.TrafficJam, this.configuration.TrafficJam, _checked =>
            {
                this.configuration.TrafficJam = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.trafficJamCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.TrafficJamCriticalThreshold, value =>
            {
                this.configuration.TrafficJamCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.roadMaintenanceVehicles = itemGroupUiHelper.AddCheckbox(languageResource.RoadMaintenanceVehicles, this.configuration.RoadMaintenanceVehicles, _checked =>
            {
                this.configuration.RoadMaintenanceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.roadMaintenanceVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.RoadMaintenanceVehiclesCriticalThreshold, value =>
            {
                this.configuration.RoadMaintenanceVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.parkMaintenanceVehicles = itemGroupUiHelper.AddCheckbox(languageResource.ParkMaintenanceVehicles, this.configuration.ParkMaintenanceVehicles, _checked =>
            {
                this.configuration.ParkMaintenanceVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.parkMaintenanceVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.ParkMaintenanceVehiclesCriticalThreshold, value =>
            {
                this.configuration.ParkMaintenanceVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.cityUnattractiveness = itemGroupUiHelper.AddCheckbox(languageResource.CityUnattractiveness, this.configuration.CityUnattractiveness, _checked =>
            {
                this.configuration.CityUnattractiveness = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.cityUnattractivenessCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.CityUnattractivenessCriticalThreshold, value =>
            {
                this.configuration.CityUnattractivenessCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.snowDump = itemGroupUiHelper.AddCheckbox(languageResource.SnowDump, this.configuration.SnowDump, _checked =>
            {
                this.configuration.SnowDump = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.snowDumpCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.SnowDumpCriticalThreshold, value =>
            {
                this.configuration.SnowDumpCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.snowDumpVehicles = itemGroupUiHelper.AddCheckbox(languageResource.SnowDumpVehicles, this.configuration.SnowDumpVehicles, _checked =>
            {
                this.configuration.SnowDumpVehicles = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.snowDumpVehiclesCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.SnowDumpVehiclesCriticalThreshold, value =>
            {
                this.configuration.SnowDumpVehiclesCriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.taxis = itemGroupUiHelper.AddCheckbox(languageResource.Taxis, this.configuration.Taxis, _checked =>
            {
                this.configuration.Taxis = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.taxisCriticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.configuration.TaxisCriticalThreshold, value =>
            {
                this.configuration.TaxisCriticalThreshold = (int)value;
                this.configuration.Save();
            });
        }

        private void UpdateUiFromModel()
        {
            this.updateEveryXSeconds.value = this.configuration.MainPanelUpdateEveryXSeconds;
            this.columnCountSlider.value = this.configuration.MainPanelColumnCount;
            this.itemWidth.value = this.configuration.ItemWidth;
            this.itemHeight.value = this.configuration.ItemHeight;
            this.itemPadding.value = this.configuration.ItemPadding;
            this.itemTextScale.value = this.configuration.ItemTextScale;
            this.autoHide.isChecked = this.configuration.MainPanelAutoHide;
            this.hideItemsBelowThreshold.isChecked = this.configuration.MainPanelHideItemsBelowThreshold;
            this.hideItemsNotAvailable.isChecked = this.configuration.MainPanelHideItemsNotAvailable;

            this.electricity.isChecked = this.configuration.Electricity;
            this.electricityCriticalThreshold.value = this.configuration.ElectricityCriticalThreshold;
            this.heating.isChecked = this.configuration.Heating;
            this.heatingCriticalThreshold.value = this.configuration.HeatingCriticalThreshold;
            this.water.isChecked = this.configuration.Water;
            this.waterCriticalThreshold.value = this.configuration.WaterCriticalThreshold;
            this.sewageTreatment.isChecked = this.configuration.SewageTreatment;
            this.sewageTreatmentCriticalThreshold.value = this.configuration.SewageTreatmentCriticalThreshold;
            this.waterReserveTank.isChecked = this.configuration.WaterReserveTank;
            this.waterReserveTankCriticalThreshold.value = this.configuration.WaterReserveTankCriticalThreshold;
            this.waterPumpingServiceStorage.isChecked = this.configuration.WaterPumpingServiceStorage;
            this.waterPumpingServiceStorageCriticalThreshold.value = this.configuration.WaterPumpingServiceStorageCriticalThreshold;
            this.waterPumpingServiceVehicles.isChecked = this.configuration.WaterPumpingServiceVehicles;
            this.waterPumpingServiceVehiclesCriticalThreshold.value = this.configuration.WaterPumpingServiceVehiclesCriticalThreshold;
            this.landfill.isChecked = this.configuration.Landfill;
            this.landfillCriticalThreshold.value = this.configuration.LandfillCriticalThreshold;
            this.landfillVehicles.isChecked = this.configuration.LandfillVehicles;
            this.landfillVehiclesCriticalThreshold.value = this.configuration.LandfillVehiclesCriticalThreshold;
            this.garbageProcessing.isChecked = this.configuration.GarbageProcessing;
            this.garbageProcessingCriticalThreshold.value = this.configuration.GarbageProcessingCriticalThreshold;
            this.garbageProcessingVehicles.isChecked = this.configuration.GarbageProcessingVehicles;
            this.garbageProcessingVehiclesCriticalThreshold.value = this.configuration.GarbageProcessingVehiclesCriticalThreshold;
            this.elementarySchool.isChecked = this.configuration.ElementarySchool;
            this.elementarySchoolCriticalThreshold.value = this.configuration.ElementarySchoolCriticalThreshold;
            this.highSchool.isChecked = this.configuration.HighSchool;
            this.highSchoolCriticalThreshold.value = this.configuration.HighSchoolCriticalThreshold;
            this.university.isChecked = this.configuration.University;
            this.universityCriticalThreshold.value = this.configuration.UniversityCriticalThreshold;
            this.healthcare.isChecked = this.configuration.Healthcare;
            this.healthcareCriticalThreshold.value = this.configuration.HealthcareCriticalThreshold;
            this.healthcareVehicles.isChecked = this.configuration.HealthcareVehicles;
            this.healthcareVehiclesCriticalThreshold.value = this.configuration.HealthcareVehiclesCriticalThreshold;
            this.medicalHelicopters.isChecked = this.configuration.MedicalHelicopters;
            this.medicalHelicoptersCriticalThreshold.value = this.configuration.MedicalHelicoptersCriticalThreshold;
            this.averageIllnessRate.isChecked = this.configuration.AverageIllnessRate;
            this.averageIllnessRateCriticalThreshold.value = this.configuration.AverageIllnessRateCriticalThreshold;
            this.cemetery.isChecked = this.configuration.Cemetery;
            this.cemeteryCriticalThreshold.value = this.configuration.CemeteryCriticalThreshold;
            this.cemeteryVehicles.isChecked = this.configuration.CemeteryVehicles;
            this.cemeteryVehiclesCriticalThreshold.value = this.configuration.CemeteryVehiclesCriticalThreshold;
            this.crematorium.isChecked = this.configuration.Crematorium;
            this.crematoriumCriticalThreshold.value = this.configuration.CrematoriumCriticalThreshold;
            this.crematoriumVehicles.isChecked = this.configuration.CrematoriumVehicles;
            this.crematoriumVehiclesCriticalThreshold.value = this.configuration.CrematoriumVehiclesCriticalThreshold;
            this.groundPollution.isChecked = this.configuration.GroundPollution;
            this.groundPollutionCriticalThreshold.value = this.configuration.GroundPollutionCriticalThreshold;
            this.drinkingWaterPollution.isChecked = this.configuration.DrinkingWaterPollution;
            this.drinkingWaterPollutionCriticalThreshold.value = this.configuration.DrinkingWaterPollutionCriticalThreshold;
            this.noisePollution.isChecked = this.configuration.NoisePollution;
            this.noisePollutionCriticalThreshold.value = this.configuration.NoisePollutionCriticalThreshold;
            this.fireHazard.isChecked = this.configuration.FireHazard;
            this.fireHazardCriticalThreshold.value = this.configuration.FireHazardCriticalThreshold;
            this.fireDepartmentVehicles.isChecked = this.configuration.FireDepartmentVehicles;
            this.fireDepartmentVehiclesCriticalThreshold.value = this.configuration.FireDepartmentVehiclesCriticalThreshold;
            this.fireHelicopters.isChecked = this.configuration.FireHelicopters;
            this.fireHelicoptersCriticalThreshold.value = this.configuration.FireHelicoptersCriticalThreshold;
            this.crimeRate.isChecked = this.configuration.CrimeRate;
            this.crimeRateCriticalThreshold.value = this.configuration.CrimeRateCriticalThreshold;
            this.policeHoldingCells.isChecked = this.configuration.PoliceHoldingCells;
            this.policeHoldingCellsCriticalThreshold.value = this.configuration.PoliceHoldingCellsCriticalThreshold;
            this.policeVehicles.isChecked = this.configuration.PoliceVehicles;
            this.policeVehiclesCriticalThreshold.value = this.configuration.PoliceVehiclesCriticalThreshold;
            this.policeHelicopters.isChecked = this.configuration.PoliceHelicopters;
            this.policeHelicoptersCriticalThreshold.value = this.configuration.PoliceHelicoptersCriticalThreshold;
            this.prisonCells.isChecked = this.configuration.PrisonCells;
            this.prisonCellsCriticalThreshold.value = this.configuration.PrisonCellsCriticalThreshold;
            this.prisonVehicles.isChecked = this.configuration.PrisonVehicles;
            this.prisonVehiclesCriticalThreshold.value = this.configuration.PrisonVehiclesCriticalThreshold;
            this.unemployment.isChecked = this.configuration.Unemployment;
            this.unemploymentCriticalThreshold.value = this.configuration.UnemploymentCriticalThreshold;
            this.trafficJam.isChecked = this.configuration.TrafficJam;
            this.trafficJamCriticalThreshold.value = this.configuration.TrafficJamCriticalThreshold;
            this.roadMaintenanceVehicles.isChecked = this.configuration.RoadMaintenanceVehicles;
            this.roadMaintenanceVehiclesCriticalThreshold.value = this.configuration.RoadMaintenanceVehiclesCriticalThreshold;
            this.snowDump.isChecked = this.configuration.SnowDump;
            this.snowDumpCriticalThreshold.value = this.configuration.SnowDumpCriticalThreshold;
            this.snowDumpVehicles.isChecked = this.configuration.SnowDumpVehicles;
            this.snowDumpVehiclesCriticalThreshold.value = this.configuration.SnowDumpVehiclesCriticalThreshold;
            this.parkMaintenanceVehicles.isChecked = this.configuration.ParkMaintenanceVehicles;
            this.parkMaintenanceVehiclesCriticalThreshold.value = this.configuration.ParkMaintenanceVehiclesCriticalThreshold;
            this.cityUnattractiveness.isChecked = this.configuration.CityUnattractiveness;
            this.cityUnattractivenessCriticalThreshold.value = this.configuration.CityUnattractivenessCriticalThreshold;
            this.taxis.isChecked = this.configuration.Taxis;
            this.taxisCriticalThreshold.value = this.configuration.TaxisCriticalThreshold;
        }
    }
}
