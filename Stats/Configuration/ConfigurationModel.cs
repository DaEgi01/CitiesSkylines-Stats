using System;
using UnityEngine;

namespace Stats.Configuration
{
    public class ConfigurationModel
    {
        private readonly ConfigurationService<ConfigurationDto> configurationService;
        private ConfigurationDto dto;

        public ConfigurationModel(ConfigurationService<ConfigurationDto> configurationService, ConfigurationDto configurationDto)
        {
            this.configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this.dto = configurationDto ?? throw new ArgumentNullException(nameof(configurationDto));
        }

        public float MainPanelPositionX
        {
            get => dto.MainPanelPositionX;
            set
            {
                dto.MainPanelPositionX = value;
                this.OnPositionChanged();
            }
        }

        public float MainPanelPositionY
        {
            get => dto.MainPanelPositionY;
            set
            {
                dto.MainPanelPositionY = value;
                this.OnPositionChanged();
            }
        }

        public int MainPanelUpdateEveryXSeconds
        {
            get => dto.MainPanelUpdateEveryXSeconds;
            set
            {
                dto.MainPanelUpdateEveryXSeconds = value;
            }
        }

        public bool MainPanelAutoHide
        {
            get => dto.MainPanelAutoHide;
            set
            {
                dto.MainPanelAutoHide = value;
            }
        }

        public bool MainPanelHideItemsBelowThreshold
        {
            get => dto.MainPanelHideItemsBelowThreshold;
            set
            {
                dto.MainPanelHideItemsBelowThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => dto.MainPanelHideItemsNotAvailable;
            set
            {
                dto.MainPanelHideItemsNotAvailable = value;
                this.OnLayoutChanged();
            }
        }

        public int MainPanelColumnCount
        {
            get => dto.MainPanelColumnCount;
            set
            {
                dto.MainPanelColumnCount = value;
                this.OnLayoutChanged();
            }
        }

        public Color32 MainPanelBackgroundColor
        {
            get => dto.MainPanelBackgroundColor.GetColor32();
            set
            {
                dto.MainPanelBackgroundColor = value.GetColorString();
            }
        }

        public Color32 MainPanelForegroundColor
        {
            get => dto.MainPanelForegroundColor.GetColor32();
            set
            {
                dto.MainPanelForegroundColor = value.GetColorString();
            }
        }

        public Color32 MainPanelAccentColor
        {
            get => dto.MainPanelAccentColor.GetColor32();
            set
            {
                dto.MainPanelAccentColor = value.GetColorString();
            }
        }

        public float ItemWidth
        {
            get => dto.ItemWidth;
            set
            {
                dto.ItemWidth = value;
                this.OnLayoutChanged();
            }
        }

        public float ItemHeight
        {
            get => dto.ItemHeight;
            set
            {
                dto.ItemHeight = value;
                this.OnLayoutChanged();
            }
        }

        public float ItemPadding
        {
            get => dto.ItemPadding;
            set
            {
                dto.ItemPadding = value;
                this.OnLayoutChanged();
            }
        }

        public float ItemTextScale
        {
            get => dto.ItemTextScale;
            set
            {
                dto.ItemTextScale = value;
                this.OnLayoutChanged();
            }
        }

        public bool Electricity
        {
            get => dto.Electricity;
            set
            {
                dto.Electricity = value;
                this.OnLayoutChanged();
            }
        }

        public int ElectricityCriticalThreshold
        {
            get => dto.ElectricityCriticalThreshold;
            set
            {
                dto.ElectricityCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int ElectricitySortOrder
        {
            get => dto.ElectricitySortOrder;
            set
            {
                dto.ElectricitySortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Heating
        {
            get => dto.Heating;
            set
            {
                dto.Heating = value;
                this.OnLayoutChanged();
            }
        }

        public int HeatingCriticalThreshold
        {
            get => dto.HeatingCriticalThreshold;
            set
            {
                dto.HeatingCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int HeatingSortOrder
        {
            get => dto.HeatingSortOrder;
            set
            {
                dto.HeatingSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Water
        {
            get => dto.Water;
            set
            {
                dto.Water = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterCriticalThreshold
        {
            get => dto.WaterCriticalThreshold;
            set
            {
                dto.WaterCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterSortOrder
        {
            get => dto.WaterSortOrder;
            set
            {
                dto.WaterSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool SewageTreatment
        {
            get => dto.SewageTreatment;
            set
            {
                dto.SewageTreatment = value;
                this.OnLayoutChanged();
            }
        }

        public int SewageTreatmentCriticalThreshold
        {
            get => dto.SewageTreatmentCriticalThreshold;
            set
            {
                dto.SewageTreatmentCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int SewageTreatmentSortOrder
        {
            get => dto.SewageTreatmentSortOrder;
            set
            {
                dto.SewageTreatmentSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool WaterReserveTank
        {
            get => dto.WaterReserveTank;
            set
            {
                dto.WaterReserveTank = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterReserveTankCriticalThreshold
        {
            get => dto.WaterReserveTankCriticalThreshold;
            set
            {
                dto.WaterReserveTankCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterReserveTankSortOrder
        {
            get => dto.WaterReserveTankSortOrder;
            set
            {
                dto.WaterReserveTankSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool WaterPumpingServiceStorage
        {
            get => dto.WaterPumpingServiceStorage;
            set
            {
                dto.WaterPumpingServiceStorage = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceStorageCriticalThreshold
        {
            get => dto.WaterPumpingServiceStorageCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceStorageCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceStorageSortOrder
        {
            get => dto.WaterPumpingServiceStorageSortOrder;
            set
            {
                dto.WaterPumpingServiceStorageSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool WaterPumpingServiceVehicles
        {
            get => dto.WaterPumpingServiceVehicles;
            set
            {
                dto.WaterPumpingServiceVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceVehiclesCriticalThreshold
        {
            get => dto.WaterPumpingServiceVehiclesCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceVehiclesSortOrder
        {
            get => dto.WaterPumpingServiceVehiclesSortOrder;
            set
            {
                dto.WaterPumpingServiceVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Landfill
        {
            get => dto.Landfill;
            set
            {
                dto.Landfill = value;
                this.OnLayoutChanged();
            }
        }

        public int LandfillCriticalThreshold
        {
            get => dto.LandfillCriticalThreshold;
            set
            {
                dto.LandfillCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int LandfillSortOrder
        {
            get => dto.LandfillSortOrder;
            set
            {
                dto.LandfillSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool LandfillVehicles
        {
            get => dto.LandfillVehicles;
            set
            {
                dto.LandfillVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int LandfillVehiclesCriticalThreshold
        {
            get => dto.LandfillVehiclesCriticalThreshold;
            set
            {
                dto.LandfillVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int LandfillVehiclesSortOrder
        {
            get => dto.LandfillVehiclesSortOrder;
            set
            {
                dto.LandfillVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool GarbageProcessing
        {
            get => dto.GarbageProcessing;
            set
            {
                dto.GarbageProcessing = value;
                this.OnLayoutChanged();
            }
        }

        public int GarbageProcessingCriticalThreshold
        {
            get => dto.GarbageProcessingCriticalThreshold;
            set
            {
                dto.GarbageProcessingCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int GarbageProcessingSortOrder
        {
            get => dto.GarbageProcessingSortOrder;
            set
            {
                dto.GarbageProcessingSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool GarbageProcessingVehicles
        {
            get => dto.GarbageProcessingVehicles;
            set
            {
                dto.GarbageProcessingVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int GarbageProcessingVehiclesCriticalThreshold
        {
            get => dto.GarbageProcessingVehiclesCriticalThreshold;
            set
            {
                dto.GarbageProcessingVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int GarbageProcessingVehiclesSortOrder
        {
            get => dto.GarbageProcessingVehiclesSortOrder;
            set
            {
                dto.GarbageProcessingVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool ElementarySchool
        {
            get => dto.ElementarySchool;
            set
            {
                dto.ElementarySchool = value;
                this.OnLayoutChanged();
            }
        }

        public int ElementarySchoolCriticalThreshold
        {
            get => dto.ElementarySchoolCriticalThreshold;
            set
            {
                dto.ElementarySchoolCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int ElementarySchoolSortOrder
        {
            get => dto.ElementarySchoolSortOrder;
            set
            {
                dto.ElementarySchoolSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool HighSchool
        {
            get => dto.HighSchool;
            set
            {
                dto.HighSchool = value;
                this.OnLayoutChanged();
            }
        }

        public int HighSchoolCriticalThreshold
        {
            get => dto.HighSchoolCriticalThreshold;
            set
            {
                dto.HighSchoolCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int HighSchoolSortOrder
        {
            get => dto.HighSchoolSortOrder;
            set
            {
                dto.HighSchoolSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool University
        {
            get => dto.University;
            set
            {
                dto.University = value;
                this.OnLayoutChanged();
            }
        }

        public int UniversityCriticalThreshold
        {
            get => dto.UniversityCriticalThreshold;
            set
            {
                dto.UniversityCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int UniversitySortOrder
        {
            get => dto.UniversitySortOrder;
            set
            {
                dto.UniversitySortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Healthcare
        {
            get => dto.Healthcare;
            set
            {
                dto.Healthcare = value;
                this.OnLayoutChanged();
            }
        }

        public int HealthcareCriticalThreshold
        {
            get => dto.HealthcareCriticalThreshold;
            set
            {
                dto.HealthcareCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int HealthcareSortOrder
        {
            get => dto.HealthcareSortOrder;
            set
            {
                dto.HealthcareSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool HealthcareVehicles
        {
            get => dto.HealthcareVehicles;
            set
            {
                dto.HealthcareVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int HealthcareVehiclesCriticalThreshold
        {
            get => dto.HealthcareVehiclesCriticalThreshold;
            set
            {
                dto.HealthcareVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int HealthcareVehiclesSortOrder
        {
            get => dto.HealthcareVehiclesSortOrder;
            set
            {
                dto.HealthcareVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool MedicalHelicopters
        {
            get => dto.MedicalHelicopters;
            set
            {
                dto.MedicalHelicopters = value;
                this.OnLayoutChanged();
            }
        }

        public int MedicalHelicoptersCriticalThreshold
        {
            get => dto.MedicalHelicoptersCriticalThreshold;
            set
            {
                dto.MedicalHelicoptersCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int MedicalHelicoptersSortOrder
        {
            get => dto.MedicalHelicoptersSortOrder;
            set
            {
                dto.MedicalHelicoptersSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool AverageIllnessRate
        {
            get => dto.AverageIllnessRate;
            set
            {
                dto.AverageIllnessRate = value;
                this.OnLayoutChanged();
            }
        }

        public int AverageIllnessRateCriticalThreshold
        {
            get => dto.AverageIllnessRateCriticalThreshold;
            set
            {
                dto.AverageIllnessRateCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int AverageIllnessRateSortOrder
        {
            get => dto.AverageIllnessRateSortOrder;
            set
            {
                dto.AverageIllnessRateSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Cemetery
        {
            get => dto.Cemetery;
            set
            {
                dto.Cemetery = value;
                this.OnLayoutChanged();
            }
        }

        public int CemeteryCriticalThreshold
        {
            get => dto.CemeteryCriticalThreshold;
            set
            {
                dto.CemeteryCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int CemeterySortOrder
        {
            get => dto.CemeterySortOrder;
            set
            {
                dto.CemeterySortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool CemeteryVehicles
        {
            get => dto.CemeteryVehicles;
            set
            {
                dto.CemeteryVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int CemeteryVehiclesCriticalThreshold
        {
            get => dto.CemeteryVehiclesCriticalThreshold;
            set
            {
                dto.CemeteryVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int CemeteryVehiclesSortOrder
        {
            get => dto.CemeteryVehiclesSortOrder;
            set
            {
                dto.CemeteryVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Crematorium
        {
            get => dto.Crematorium;
            set
            {
                dto.Crematorium = value;
                this.OnLayoutChanged();
            }
        }

        public int CrematoriumCriticalThreshold
        {
            get => dto.CrematoriumCriticalThreshold;
            set
            {
                dto.CrematoriumCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int CrematoriumSortOrder
        {
            get => dto.CrematoriumSortOrder;
            set
            {
                dto.CrematoriumSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool CrematoriumVehicles
        {
            get => dto.CrematoriumVehicles;
            set
            {
                dto.CrematoriumVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int CrematoriumVehiclesCriticalThreshold
        {
            get => dto.CrematoriumVehiclesCriticalThreshold;
            set
            {
                dto.CrematoriumVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int CrematoriumVehiclesSortOrder
        {
            get => dto.CrematoriumVehiclesSortOrder;
            set
            {
                dto.CrematoriumVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool GroundPollution
        {
            get => dto.GroundPollution;
            set
            {
                dto.GroundPollution = value;
                this.OnLayoutChanged();
            }
        }

        public int GroundPollutionCriticalThreshold
        {
            get => dto.GroundPollutionCriticalThreshold;
            set
            {
                dto.GroundPollutionCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int GroundPollutionSortOrder
        {
            get => dto.GroundPollutionSortOrder;
            set
            {
                dto.GroundPollutionSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool DrinkingWaterPollution
        {
            get => dto.DrinkingWaterPollution;
            set
            {
                dto.DrinkingWaterPollution = value;
                this.OnLayoutChanged();
            }
        }

        public int DrinkingWaterPollutionCriticalThreshold
        {
            get => dto.DrinkingWaterPollutionCriticalThreshold;
            set
            {
                dto.DrinkingWaterPollutionCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int DrinkingWaterPollutionSortOrder
        {
            get => dto.DrinkingWaterPollutionSortOrder;
            set
            {
                dto.DrinkingWaterPollutionSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool NoisePollution
        {
            get => dto.NoisePollution;
            set
            {
                dto.NoisePollution = value;
                this.OnLayoutChanged();
            }
        }

        public int NoisePollutionCriticalThreshold
        {
            get => dto.NoisePollutionCriticalThreshold;
            set
            {
                dto.NoisePollutionCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int NoisePollutionSortOrder
        {
            get => dto.NoisePollutionSortOrder;
            set
            {
                dto.NoisePollutionSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool FireHazard
        {
            get => dto.FireHazard;
            set
            {
                dto.FireHazard = value;
                this.OnLayoutChanged();
            }
        }

        public int FireHazardCriticalThreshold
        {
            get => dto.FireHazardCriticalThreshold;
            set
            {
                dto.FireHazardCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int FireHazardSortOrder
        {
            get => dto.FireHazardSortOrder;
            set
            {
                dto.FireHazardSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool FireDepartmentVehicles
        {
            get => dto.FireDepartmentVehicles;
            set
            {
                dto.FireDepartmentVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int FireDepartmentVehiclesCriticalThreshold
        {
            get => dto.FireDepartmentVehiclesCriticalThreshold;
            set
            {
                dto.FireDepartmentVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int FireDepartmentVehiclesSortOrder
        {
            get => dto.FireDepartmentVehiclesSortOrder;
            set
            {
                dto.FireDepartmentVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool FireHelicopters
        {
            get => dto.FireHelicopters;
            set
            {
                dto.FireHelicopters = value;
                this.OnLayoutChanged();
            }
        }

        public int FireHelicoptersCriticalThreshold
        {
            get => dto.FireHelicoptersCriticalThreshold;
            set
            {
                dto.FireHelicoptersCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int FireHelicoptersSortOrder
        {
            get => dto.FireHelicoptersSortOrder;
            set
            {
                dto.FireHelicoptersSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool CrimeRate
        {
            get => dto.CrimeRate;
            set
            {
                dto.CrimeRate = value;
                this.OnLayoutChanged();
            }
        }

        public int CrimeRateCriticalThreshold
        {
            get => dto.CrimeRateCriticalThreshold;
            set
            {
                dto.CrimeRateCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int CrimeRateSortOrder
        {
            get => dto.CrimeRateSortOrder;
            set
            {
                dto.CrimeRateSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool PoliceHoldingCells
        {
            get => dto.PoliceHoldingCells;
            set
            {
                dto.PoliceHoldingCells = value;
                this.OnLayoutChanged();
            }
        }

        public int PoliceHoldingCellsCriticalThreshold
        {
            get => dto.PoliceHoldingCellsCriticalThreshold;
            set
            {
                dto.PoliceHoldingCellsCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int PoliceHoldingCellsSortOrder
        {
            get => dto.PoliceHoldingCellsSortOrder;
            set
            {
                dto.PoliceHoldingCellsSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool PoliceVehicles
        {
            get => dto.PoliceVehicles;
            set
            {
                dto.PoliceVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int PoliceVehiclesCriticalThreshold
        {
            get => dto.PoliceVehiclesCriticalThreshold;
            set
            {
                dto.PoliceVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int PoliceVehiclesSortOrder
        {
            get => dto.PoliceVehiclesSortOrder;
            set
            {
                dto.PoliceVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool PoliceHelicopters
        {
            get => dto.PoliceHelicopters;
            set
            {
                dto.PoliceHelicopters = value;
                this.OnLayoutChanged();
            }
        }

        public int PoliceHelicoptersCriticalThreshold
        {
            get => dto.PoliceHelicoptersCriticalThreshold;
            set
            {
                dto.PoliceHelicoptersCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int PoliceHelicoptersSortOrder
        {
            get => dto.PoliceHelicoptersSortOrder;
            set
            {
                dto.PoliceHelicoptersSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool PrisonCells
        {
            get => dto.PrisonCells;
            set
            {
                dto.PrisonCells = value;
                this.OnLayoutChanged();
            }
        }

        public int PrisonCellsCriticalThreshold
        {
            get => dto.PrisonCellsCriticalThreshold;
            set
            {
                dto.PrisonCellsCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int PrisonCellsSortOrder
        {
            get => dto.PrisonCellsSortOrder;
            set
            {
                dto.PrisonCellsSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool PrisonVehicles
        {
            get => dto.PrisonVehicles;
            set
            {
                dto.PrisonVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int PrisonVehiclesCriticalThreshold
        {
            get => dto.PrisonVehiclesCriticalThreshold;
            set
            {
                dto.PrisonVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int PrisonVehiclesSortOrder
        {
            get => dto.PrisonVehiclesSortOrder;
            set
            {
                dto.PrisonVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Unemployment
        {
            get => dto.Unemployment;
            set
            {
                dto.Unemployment = value;
                this.OnLayoutChanged();
            }
        }

        public int UnemploymentCriticalThreshold
        {
            get => dto.UnemploymentCriticalThreshold;
            set
            {
                dto.UnemploymentCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int UnemploymentSortOrder
        {
            get => dto.UnemploymentSortOrder;
            set
            {
                dto.UnemploymentSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool TrafficJam
        {
            get => dto.TrafficJam;
            set
            {
                dto.TrafficJam = value;
                this.OnLayoutChanged();
            }
        }

        public int TrafficJamCriticalThreshold
        {
            get => dto.TrafficJamCriticalThreshold;
            set
            {
                dto.TrafficJamCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int TrafficJamSortOrder
        {
            get => dto.TrafficJamSortOrder;
            set
            {
                dto.TrafficJamSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool RoadMaintenanceVehicles
        {
            get => dto.RoadMaintenanceVehicles;
            set
            {
                dto.RoadMaintenanceVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int RoadMaintenanceVehiclesCriticalThreshold
        {
            get => dto.RoadMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.RoadMaintenanceVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int RoadMaintenanceVehiclesSortOrder
        {
            get => dto.RoadMaintenanceVehiclesSortOrder;
            set
            {
                dto.RoadMaintenanceVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool SnowDump
        {
            get => dto.SnowDump;
            set
            {
                dto.SnowDump = value;
                this.OnLayoutChanged();
            }
        }

        public int SnowDumpCriticalThreshold
        {
            get => dto.SnowDumpCriticalThreshold;
            set
            {
                dto.SnowDumpCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int SnowDumpSortOrder
        {
            get => dto.SnowDumpSortOrder;
            set
            {
                dto.SnowDumpSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool SnowDumpVehicles
        {
            get => dto.SnowDumpVehicles;
            set
            {
                dto.SnowDumpVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int SnowDumpVehiclesCriticalThreshold
        {
            get => dto.SnowDumpVehiclesCriticalThreshold;
            set
            {
                dto.SnowDumpVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int SnowDumpVehiclesSortOrder
        {
            get => dto.SnowDumpVehiclesSortOrder;
            set
            {
                dto.SnowDumpVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool ParkMaintenanceVehicles
        {
            get => dto.ParkMaintenanceVehicles;
            set
            {
                dto.ParkMaintenanceVehicles = value;
                this.OnLayoutChanged();
            }
        }

        public int ParkMaintenanceVehiclesCriticalThreshold
        {
            get => dto.ParkMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.ParkMaintenanceVehiclesCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int ParkMaintenanceVehiclesSortOrder
        {
            get => dto.ParkMaintenanceVehiclesSortOrder;
            set
            {
                dto.ParkMaintenanceVehiclesSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool CityUnattractiveness
        {
            get => dto.CityUnattractiveness;
            set
            {
                dto.CityUnattractiveness = value;
                this.OnLayoutChanged();
            }
        }

        public int CityUnattractivenessCriticalThreshold
        {
            get => dto.CityUnattractivenessCriticalThreshold;
            set
            {
                dto.CityUnattractivenessCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int CityUnattractivenessSortOrder
        {
            get => dto.CityUnattractivenessSortOrder;
            set
            {
                dto.CityUnattractivenessSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public bool Taxis
        {
            get => dto.Taxis;
            set
            {
                dto.Taxis = value;
                this.OnLayoutChanged();
            }
        }

        public int TaxisCriticalThreshold
        {
            get => dto.TaxisCriticalThreshold;
            set
            {
                dto.TaxisCriticalThreshold = value;
                this.OnLayoutChanged();
            }
        }

        public int TaxisSortOrder
        {
            get => dto.TaxisSortOrder;
            set
            {
                dto.TaxisSortOrder = value;
                this.OnLayoutChanged();
            }
        }

        public void Save()
        {
            this.configurationService.Save(this.dto);
        }

        public void Reset()
        {
            this.dto = new ConfigurationDto();
            this.configurationService.Save(this.dto);
            this.OnLayoutChanged();
            this.OnPositionChanged();
        }

        public void ResetPosition()
        {
            this.dto.MainPanelPositionX = 0;
            this.dto.MainPanelPositionY = 0;
            this.configurationService.Save(this.dto);
            this.OnPositionChanged();
        }

        public event Action LayoutChanged;

        private void OnLayoutChanged()
        {
            this.LayoutChanged?.Invoke();
        }

        public event Action PositionChanged;

        private void OnPositionChanged()
        {
            this.PositionChanged?.Invoke();
        }
    }
}
