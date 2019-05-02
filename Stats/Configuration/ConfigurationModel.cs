using System;
using UnityEngine;

namespace Stats.Configuration
{
    public class ConfigurationModel
    {
        private readonly ConfigurationService<ConfigurationDto> configurationService;
        private ConfigurationDto dto;

        public ConfigurationModel(ConfigurationService<ConfigurationDto> configurationService, ConfigurationDto dto)
        {
            this.configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this.dto = dto ?? throw new ArgumentNullException(nameof(dto));
        }

        public float MainPanelPositionX
        {
            get => dto.MainPanelPositionX;
            set
            {
                dto.MainPanelPositionX = value;
                // don't OnPositionChanged(); here - that will cause issues with position saving
            }
        }

        public float MainPanelPositionY
        {
            get => dto.MainPanelPositionY;
            set
            {
                dto.MainPanelPositionY = value;
                // don't OnPositionChanged(); here - that will cause issues with position saving
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
                OnLayoutChanged();
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => dto.MainPanelHideItemsNotAvailable;
            set
            {
                dto.MainPanelHideItemsNotAvailable = value;
                OnLayoutChanged();
            }
        }

        public int MainPanelColumnCount
        {
            get => dto.MainPanelColumnCount;
            set
            {
                dto.MainPanelColumnCount = value;
                OnLayoutChanged();
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
                OnLayoutChanged();
            }
        }

        public float ItemHeight
        {
            get => dto.ItemHeight;
            set
            {
                dto.ItemHeight = value;
                OnLayoutChanged();
            }
        }

        public float ItemPadding
        {
            get => dto.ItemPadding;
            set
            {
                dto.ItemPadding = value;
                OnLayoutChanged();
            }
        }

        public float ItemTextScale
        {
            get => dto.ItemTextScale;
            set
            {
                dto.ItemTextScale = value;
                OnLayoutChanged();
            }
        }

        public bool Electricity
        {
            get => dto.Electricity;
            set
            {
                dto.Electricity = value;
                OnLayoutChanged();
            }
        }

        public int ElectricityCriticalThreshold
        {
            get => dto.ElectricityCriticalThreshold;
            set
            {
                dto.ElectricityCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int ElectricitySortOrder
        {
            get => dto.ElectricitySortOrder;
            set
            {
                dto.ElectricitySortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Heating
        {
            get => dto.Heating;
            set
            {
                dto.Heating = value;
                OnLayoutChanged();
            }
        }

        public int HeatingCriticalThreshold
        {
            get => dto.HeatingCriticalThreshold;
            set
            {
                dto.HeatingCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int HeatingSortOrder
        {
            get => dto.HeatingSortOrder;
            set
            {
                dto.HeatingSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Water
        {
            get => dto.Water;
            set
            {
                dto.Water = value;
                OnLayoutChanged();
            }
        }

        public int WaterCriticalThreshold
        {
            get => dto.WaterCriticalThreshold;
            set
            {
                dto.WaterCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int WaterSortOrder
        {
            get => dto.WaterSortOrder;
            set
            {
                dto.WaterSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool SewageTreatment
        {
            get => dto.SewageTreatment;
            set
            {
                dto.SewageTreatment = value;
                OnLayoutChanged();
            }
        }

        public int SewageTreatmentCriticalThreshold
        {
            get => dto.SewageTreatmentCriticalThreshold;
            set
            {
                dto.SewageTreatmentCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int SewageTreatmentSortOrder
        {
            get => dto.SewageTreatmentSortOrder;
            set
            {
                dto.SewageTreatmentSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool WaterReserveTank
        {
            get => dto.WaterReserveTank;
            set
            {
                dto.WaterReserveTank = value;
                OnLayoutChanged();
            }
        }

        public int WaterReserveTankCriticalThreshold
        {
            get => dto.WaterReserveTankCriticalThreshold;
            set
            {
                dto.WaterReserveTankCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int WaterReserveTankSortOrder
        {
            get => dto.WaterReserveTankSortOrder;
            set
            {
                dto.WaterReserveTankSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool WaterPumpingServiceStorage
        {
            get => dto.WaterPumpingServiceStorage;
            set
            {
                dto.WaterPumpingServiceStorage = value;
                OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceStorageCriticalThreshold
        {
            get => dto.WaterPumpingServiceStorageCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceStorageCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceStorageSortOrder
        {
            get => dto.WaterPumpingServiceStorageSortOrder;
            set
            {
                dto.WaterPumpingServiceStorageSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool WaterPumpingServiceVehicles
        {
            get => dto.WaterPumpingServiceVehicles;
            set
            {
                dto.WaterPumpingServiceVehicles = value;
                OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceVehiclesCriticalThreshold
        {
            get => dto.WaterPumpingServiceVehiclesCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int WaterPumpingServiceVehiclesSortOrder
        {
            get => dto.WaterPumpingServiceVehiclesSortOrder;
            set
            {
                dto.WaterPumpingServiceVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Landfill
        {
            get => dto.Landfill;
            set
            {
                dto.Landfill = value;
                OnLayoutChanged();
            }
        }

        public int LandfillCriticalThreshold
        {
            get => dto.LandfillCriticalThreshold;
            set
            {
                dto.LandfillCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int LandfillSortOrder
        {
            get => dto.LandfillSortOrder;
            set
            {
                dto.LandfillSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool LandfillVehicles
        {
            get => dto.LandfillVehicles;
            set
            {
                dto.LandfillVehicles = value;
                OnLayoutChanged();
            }
        }

        public int LandfillVehiclesCriticalThreshold
        {
            get => dto.LandfillVehiclesCriticalThreshold;
            set
            {
                dto.LandfillVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int LandfillVehiclesSortOrder
        {
            get => dto.LandfillVehiclesSortOrder;
            set
            {
                dto.LandfillVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool GarbageProcessing
        {
            get => dto.GarbageProcessing;
            set
            {
                dto.GarbageProcessing = value;
                OnLayoutChanged();
            }
        }

        public int GarbageProcessingCriticalThreshold
        {
            get => dto.GarbageProcessingCriticalThreshold;
            set
            {
                dto.GarbageProcessingCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int GarbageProcessingSortOrder
        {
            get => dto.GarbageProcessingSortOrder;
            set
            {
                dto.GarbageProcessingSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool GarbageProcessingVehicles
        {
            get => dto.GarbageProcessingVehicles;
            set
            {
                dto.GarbageProcessingVehicles = value;
                OnLayoutChanged();
            }
        }

        public int GarbageProcessingVehiclesCriticalThreshold
        {
            get => dto.GarbageProcessingVehiclesCriticalThreshold;
            set
            {
                dto.GarbageProcessingVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int GarbageProcessingVehiclesSortOrder
        {
            get => dto.GarbageProcessingVehiclesSortOrder;
            set
            {
                dto.GarbageProcessingVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool ElementarySchool
        {
            get => dto.ElementarySchool;
            set
            {
                dto.ElementarySchool = value;
                OnLayoutChanged();
            }
        }

        public int ElementarySchoolCriticalThreshold
        {
            get => dto.ElementarySchoolCriticalThreshold;
            set
            {
                dto.ElementarySchoolCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int ElementarySchoolSortOrder
        {
            get => dto.ElementarySchoolSortOrder;
            set
            {
                dto.ElementarySchoolSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool HighSchool
        {
            get => dto.HighSchool;
            set
            {
                dto.HighSchool = value;
                OnLayoutChanged();
            }
        }

        public int HighSchoolCriticalThreshold
        {
            get => dto.HighSchoolCriticalThreshold;
            set
            {
                dto.HighSchoolCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int HighSchoolSortOrder
        {
            get => dto.HighSchoolSortOrder;
            set
            {
                dto.HighSchoolSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool University
        {
            get => dto.University;
            set
            {
                dto.University = value;
                OnLayoutChanged();
            }
        }

        public int UniversityCriticalThreshold
        {
            get => dto.UniversityCriticalThreshold;
            set
            {
                dto.UniversityCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int UniversitySortOrder
        {
            get => dto.UniversitySortOrder;
            set
            {
                dto.UniversitySortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Healthcare
        {
            get => dto.Healthcare;
            set
            {
                dto.Healthcare = value;
                OnLayoutChanged();
            }
        }

        public int HealthcareCriticalThreshold
        {
            get => dto.HealthcareCriticalThreshold;
            set
            {
                dto.HealthcareCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int HealthcareSortOrder
        {
            get => dto.HealthcareSortOrder;
            set
            {
                dto.HealthcareSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool HealthcareVehicles
        {
            get => dto.HealthcareVehicles;
            set
            {
                dto.HealthcareVehicles = value;
                OnLayoutChanged();
            }
        }

        public int HealthcareVehiclesCriticalThreshold
        {
            get => dto.HealthcareVehiclesCriticalThreshold;
            set
            {
                dto.HealthcareVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int HealthcareVehiclesSortOrder
        {
            get => dto.HealthcareVehiclesSortOrder;
            set
            {
                dto.HealthcareVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool MedicalHelicopters
        {
            get => dto.MedicalHelicopters;
            set
            {
                dto.MedicalHelicopters = value;
                OnLayoutChanged();
            }
        }

        public int MedicalHelicoptersCriticalThreshold
        {
            get => dto.MedicalHelicoptersCriticalThreshold;
            set
            {
                dto.MedicalHelicoptersCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int MedicalHelicoptersSortOrder
        {
            get => dto.MedicalHelicoptersSortOrder;
            set
            {
                dto.MedicalHelicoptersSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool AverageIllnessRate
        {
            get => dto.AverageIllnessRate;
            set
            {
                dto.AverageIllnessRate = value;
                OnLayoutChanged();
            }
        }

        public int AverageIllnessRateCriticalThreshold
        {
            get => dto.AverageIllnessRateCriticalThreshold;
            set
            {
                dto.AverageIllnessRateCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int AverageIllnessRateSortOrder
        {
            get => dto.AverageIllnessRateSortOrder;
            set
            {
                dto.AverageIllnessRateSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Cemetery
        {
            get => dto.Cemetery;
            set
            {
                dto.Cemetery = value;
                OnLayoutChanged();
            }
        }

        public int CemeteryCriticalThreshold
        {
            get => dto.CemeteryCriticalThreshold;
            set
            {
                dto.CemeteryCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int CemeterySortOrder
        {
            get => dto.CemeterySortOrder;
            set
            {
                dto.CemeterySortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool CemeteryVehicles
        {
            get => dto.CemeteryVehicles;
            set
            {
                dto.CemeteryVehicles = value;
                OnLayoutChanged();
            }
        }

        public int CemeteryVehiclesCriticalThreshold
        {
            get => dto.CemeteryVehiclesCriticalThreshold;
            set
            {
                dto.CemeteryVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int CemeteryVehiclesSortOrder
        {
            get => dto.CemeteryVehiclesSortOrder;
            set
            {
                dto.CemeteryVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Crematorium
        {
            get => dto.Crematorium;
            set
            {
                dto.Crematorium = value;
                OnLayoutChanged();
            }
        }

        public int CrematoriumCriticalThreshold
        {
            get => dto.CrematoriumCriticalThreshold;
            set
            {
                dto.CrematoriumCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int CrematoriumSortOrder
        {
            get => dto.CrematoriumSortOrder;
            set
            {
                dto.CrematoriumSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool CrematoriumVehicles
        {
            get => dto.CrematoriumVehicles;
            set
            {
                dto.CrematoriumVehicles = value;
                OnLayoutChanged();
            }
        }

        public int CrematoriumVehiclesCriticalThreshold
        {
            get => dto.CrematoriumVehiclesCriticalThreshold;
            set
            {
                dto.CrematoriumVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int CrematoriumVehiclesSortOrder
        {
            get => dto.CrematoriumVehiclesSortOrder;
            set
            {
                dto.CrematoriumVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool GroundPollution
        {
            get => dto.GroundPollution;
            set
            {
                dto.GroundPollution = value;
                OnLayoutChanged();
            }
        }

        public int GroundPollutionCriticalThreshold
        {
            get => dto.GroundPollutionCriticalThreshold;
            set
            {
                dto.GroundPollutionCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int GroundPollutionSortOrder
        {
            get => dto.GroundPollutionSortOrder;
            set
            {
                dto.GroundPollutionSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool DrinkingWaterPollution
        {
            get => dto.DrinkingWaterPollution;
            set
            {
                dto.DrinkingWaterPollution = value;
                OnLayoutChanged();
            }
        }

        public int DrinkingWaterPollutionCriticalThreshold
        {
            get => dto.DrinkingWaterPollutionCriticalThreshold;
            set
            {
                dto.DrinkingWaterPollutionCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int DrinkingWaterPollutionSortOrder
        {
            get => dto.DrinkingWaterPollutionSortOrder;
            set
            {
                dto.DrinkingWaterPollutionSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool NoisePollution
        {
            get => dto.NoisePollution;
            set
            {
                dto.NoisePollution = value;
                OnLayoutChanged();
            }
        }

        public int NoisePollutionCriticalThreshold
        {
            get => dto.NoisePollutionCriticalThreshold;
            set
            {
                dto.NoisePollutionCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int NoisePollutionSortOrder
        {
            get => dto.NoisePollutionSortOrder;
            set
            {
                dto.NoisePollutionSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool FireHazard
        {
            get => dto.FireHazard;
            set
            {
                dto.FireHazard = value;
                OnLayoutChanged();
            }
        }

        public int FireHazardCriticalThreshold
        {
            get => dto.FireHazardCriticalThreshold;
            set
            {
                dto.FireHazardCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int FireHazardSortOrder
        {
            get => dto.FireHazardSortOrder;
            set
            {
                dto.FireHazardSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool FireDepartmentVehicles
        {
            get => dto.FireDepartmentVehicles;
            set
            {
                dto.FireDepartmentVehicles = value;
                OnLayoutChanged();
            }
        }

        public int FireDepartmentVehiclesCriticalThreshold
        {
            get => dto.FireDepartmentVehiclesCriticalThreshold;
            set
            {
                dto.FireDepartmentVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int FireDepartmentVehiclesSortOrder
        {
            get => dto.FireDepartmentVehiclesSortOrder;
            set
            {
                dto.FireDepartmentVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool FireHelicopters
        {
            get => dto.FireHelicopters;
            set
            {
                dto.FireHelicopters = value;
                OnLayoutChanged();
            }
        }

        public int FireHelicoptersCriticalThreshold
        {
            get => dto.FireHelicoptersCriticalThreshold;
            set
            {
                dto.FireHelicoptersCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int FireHelicoptersSortOrder
        {
            get => dto.FireHelicoptersSortOrder;
            set
            {
                dto.FireHelicoptersSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool CrimeRate
        {
            get => dto.CrimeRate;
            set
            {
                dto.CrimeRate = value;
                OnLayoutChanged();
            }
        }

        public int CrimeRateCriticalThreshold
        {
            get => dto.CrimeRateCriticalThreshold;
            set
            {
                dto.CrimeRateCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int CrimeRateSortOrder
        {
            get => dto.CrimeRateSortOrder;
            set
            {
                dto.CrimeRateSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PoliceHoldingCells
        {
            get => dto.PoliceHoldingCells;
            set
            {
                dto.PoliceHoldingCells = value;
                OnLayoutChanged();
            }
        }

        public int PoliceHoldingCellsCriticalThreshold
        {
            get => dto.PoliceHoldingCellsCriticalThreshold;
            set
            {
                dto.PoliceHoldingCellsCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PoliceHoldingCellsSortOrder
        {
            get => dto.PoliceHoldingCellsSortOrder;
            set
            {
                dto.PoliceHoldingCellsSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PoliceVehicles
        {
            get => dto.PoliceVehicles;
            set
            {
                dto.PoliceVehicles = value;
                OnLayoutChanged();
            }
        }

        public int PoliceVehiclesCriticalThreshold
        {
            get => dto.PoliceVehiclesCriticalThreshold;
            set
            {
                dto.PoliceVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PoliceVehiclesSortOrder
        {
            get => dto.PoliceVehiclesSortOrder;
            set
            {
                dto.PoliceVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PoliceHelicopters
        {
            get => dto.PoliceHelicopters;
            set
            {
                dto.PoliceHelicopters = value;
                OnLayoutChanged();
            }
        }

        public int PoliceHelicoptersCriticalThreshold
        {
            get => dto.PoliceHelicoptersCriticalThreshold;
            set
            {
                dto.PoliceHelicoptersCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PoliceHelicoptersSortOrder
        {
            get => dto.PoliceHelicoptersSortOrder;
            set
            {
                dto.PoliceHelicoptersSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PrisonCells
        {
            get => dto.PrisonCells;
            set
            {
                dto.PrisonCells = value;
                OnLayoutChanged();
            }
        }

        public int PrisonCellsCriticalThreshold
        {
            get => dto.PrisonCellsCriticalThreshold;
            set
            {
                dto.PrisonCellsCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PrisonCellsSortOrder
        {
            get => dto.PrisonCellsSortOrder;
            set
            {
                dto.PrisonCellsSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PrisonVehicles
        {
            get => dto.PrisonVehicles;
            set
            {
                dto.PrisonVehicles = value;
                OnLayoutChanged();
            }
        }

        public int PrisonVehiclesCriticalThreshold
        {
            get => dto.PrisonVehiclesCriticalThreshold;
            set
            {
                dto.PrisonVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PrisonVehiclesSortOrder
        {
            get => dto.PrisonVehiclesSortOrder;
            set
            {
                dto.PrisonVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Unemployment
        {
            get => dto.Unemployment;
            set
            {
                dto.Unemployment = value;
                OnLayoutChanged();
            }
        }

        public int UnemploymentCriticalThreshold
        {
            get => dto.UnemploymentCriticalThreshold;
            set
            {
                dto.UnemploymentCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int UnemploymentSortOrder
        {
            get => dto.UnemploymentSortOrder;
            set
            {
                dto.UnemploymentSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool TrafficJam
        {
            get => dto.TrafficJam;
            set
            {
                dto.TrafficJam = value;
                OnLayoutChanged();
            }
        }

        public int TrafficJamCriticalThreshold
        {
            get => dto.TrafficJamCriticalThreshold;
            set
            {
                dto.TrafficJamCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int TrafficJamSortOrder
        {
            get => dto.TrafficJamSortOrder;
            set
            {
                dto.TrafficJamSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool RoadMaintenanceVehicles
        {
            get => dto.RoadMaintenanceVehicles;
            set
            {
                dto.RoadMaintenanceVehicles = value;
                OnLayoutChanged();
            }
        }

        public int RoadMaintenanceVehiclesCriticalThreshold
        {
            get => dto.RoadMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.RoadMaintenanceVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int RoadMaintenanceVehiclesSortOrder
        {
            get => dto.RoadMaintenanceVehiclesSortOrder;
            set
            {
                dto.RoadMaintenanceVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool SnowDump
        {
            get => dto.SnowDump;
            set
            {
                dto.SnowDump = value;
                OnLayoutChanged();
            }
        }

        public int SnowDumpCriticalThreshold
        {
            get => dto.SnowDumpCriticalThreshold;
            set
            {
                dto.SnowDumpCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int SnowDumpSortOrder
        {
            get => dto.SnowDumpSortOrder;
            set
            {
                dto.SnowDumpSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool SnowDumpVehicles
        {
            get => dto.SnowDumpVehicles;
            set
            {
                dto.SnowDumpVehicles = value;
                OnLayoutChanged();
            }
        }

        public int SnowDumpVehiclesCriticalThreshold
        {
            get => dto.SnowDumpVehiclesCriticalThreshold;
            set
            {
                dto.SnowDumpVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int SnowDumpVehiclesSortOrder
        {
            get => dto.SnowDumpVehiclesSortOrder;
            set
            {
                dto.SnowDumpVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool ParkMaintenanceVehicles
        {
            get => dto.ParkMaintenanceVehicles;
            set
            {
                dto.ParkMaintenanceVehicles = value;
                OnLayoutChanged();
            }
        }

        public int ParkMaintenanceVehiclesCriticalThreshold
        {
            get => dto.ParkMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.ParkMaintenanceVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int ParkMaintenanceVehiclesSortOrder
        {
            get => dto.ParkMaintenanceVehiclesSortOrder;
            set
            {
                dto.ParkMaintenanceVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool CityUnattractiveness
        {
            get => dto.CityUnattractiveness;
            set
            {
                dto.CityUnattractiveness = value;
                OnLayoutChanged();
            }
        }

        public int CityUnattractivenessCriticalThreshold
        {
            get => dto.CityUnattractivenessCriticalThreshold;
            set
            {
                dto.CityUnattractivenessCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int CityUnattractivenessSortOrder
        {
            get => dto.CityUnattractivenessSortOrder;
            set
            {
                dto.CityUnattractivenessSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool Taxis
        {
            get => dto.Taxis;
            set
            {
                dto.Taxis = value;
                OnLayoutChanged();
            }
        }

        public int TaxisCriticalThreshold
        {
            get => dto.TaxisCriticalThreshold;
            set
            {
                dto.TaxisCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int TaxisSortOrder
        {
            get => dto.TaxisSortOrder;
            set
            {
                dto.TaxisSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PostVans
        {
            get => dto.PostVans;
            set
            {
                dto.PostVans = value;
                OnLayoutChanged();
            }
        }

        public int PostVansCriticalThreshold
        {
            get => dto.PostVansCriticalThreshold;
            set
            {
                dto.PostVansCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PostVansSortOrder
        {
            get => dto.PostVansSortOrder;
            set
            {
                dto.PostVansSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool PostTrucks
        {
            get => dto.PostTrucks;
            set
            {
                dto.PostTrucks = value;
                OnLayoutChanged();
            }
        }

        public int PostTrucksCriticalThreshold
        {
            get => dto.PostTrucksCriticalThreshold;
            set
            {
                dto.PostTrucksCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int PostTrucksSortOrder
        {
            get => dto.PostTrucksSortOrder;
            set
            {
                dto.PostTrucksSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool DisasterResponseVehicles
        {
            get => dto.DisasterResponseVehicles;
            set
            {
                dto.DisasterResponseVehicles = value;
                OnLayoutChanged();
            }
        }

        public int DisasterResponseVehiclesCriticalThreshold
        {
            get => dto.DisasterResponseVehiclesCriticalThreshold;
            set
            {
                dto.DisasterResponseVehiclesCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int DisasterResponseVehiclesSortOrder
        {
            get => dto.DisasterResponseVehiclesSortOrder;
            set
            {
                dto.DisasterResponseVehiclesSortOrder = value;
                OnLayoutChanged();
            }
        }

        public bool DisasterResponseHelicopters
        {
            get => dto.DisasterResponseHelicopters;
            set
            {
                dto.DisasterResponseHelicopters = value;
                OnLayoutChanged();
            }
        }

        public int DisasterResponseHelicoptersCriticalThreshold
        {
            get => dto.DisasterResponseHelicoptersCriticalThreshold;
            set
            {
                dto.DisasterResponseHelicoptersCriticalThreshold = value;
                OnLayoutChanged();
            }
        }

        public int DisasterResponseHelicoptersSortOrder
        {
            get => dto.DisasterResponseHelicoptersSortOrder;
            set
            {
                dto.DisasterResponseHelicoptersSortOrder = value;
                OnLayoutChanged();
            }
        }

        public void Save()
        {
            configurationService.Save(dto);
        }

        public void Reset()
        {
            dto = new ConfigurationDto();
            configurationService.Save(dto);
            OnLayoutChanged();
            OnPositionChanged();
        }

        public void ResetPosition()
        {
            dto.MainPanelPositionX = 0;
            dto.MainPanelPositionY = 0;
            configurationService.Save(dto);
            OnPositionChanged();
        }

        public event Action LayoutChanged;

        private void OnLayoutChanged()
        {
            LayoutChanged?.Invoke();
        }

        public event Action PositionChanged;

        private void OnPositionChanged()
        {
            PositionChanged?.Invoke();
        }
    }
}
