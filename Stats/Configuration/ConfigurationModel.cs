using System;
using UnityEngine;

namespace Stats.Configuration
{
    public class ConfigurationModel
    {
        private readonly ConfigurationService configurationService;
        private readonly ConfigurationDto dto;

        public ConfigurationModel(ConfigurationService configurationService, ConfigurationDto configurationDto)
        {
            this.configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this.dto = configurationDto ?? throw new ArgumentNullException(nameof(configurationDto));

            this.UpdateFromDto(this.dto);
        }

        public float MainPanelPositionX
        {
            get => dto.MainPanelPositionX;
            set
            {
                dto.MainPanelPositionX = value;
                this.OnPropertyChanged();
            }
        }

        public float MainPanelPositionY
        {
            get => dto.MainPanelPositionY;
            set
            {
                dto.MainPanelPositionY = value;
                this.OnPropertyChanged();
            }
        }

        public int MainPanelUpdateEveryXSeconds
        {
            get => dto.MainPanelUpdateEveryXSeconds;
            set
            {
                dto.MainPanelUpdateEveryXSeconds = value;
                this.OnPropertyChanged();
            }
        }

        public bool MainPanelAutoHide
        {
            get => dto.MainPanelAutoHide;
            set
            {
                dto.MainPanelAutoHide = value;
                this.OnPropertyChanged();
            }
        }

        public bool MainPanelHideItemsBelowThreshold
        {
            get => dto.MainPanelHideItemsBelowThreshold;
            set
            {
                dto.MainPanelHideItemsBelowThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => dto.MainPanelHideItemsNotAvailable;
            set
            {
                dto.MainPanelHideItemsNotAvailable = value;
                this.OnPropertyChanged();
            }
        }

        public int MainPanelColumnCount
        {
            get => dto.MainPanelColumnCount;
            set
            {
                dto.MainPanelColumnCount = value;
                this.OnPropertyChanged();
            }
        }

        public Color32 MainPanelBackgroundColor
        {
            get => dto.MainPanelBackgroundColor.GetColor32();
            set
            {
                dto.MainPanelBackgroundColor = value.GetColorString();
                this.OnPropertyChanged();
            }
        }

        public Color32 MainPanelForegroundColor
        {
            get => dto.MainPanelForegroundColor.GetColor32();
            set
            {
                dto.MainPanelForegroundColor = value.GetColorString();
                this.OnPropertyChanged();
            }
        }

        public Color32 MainPanelAccentColor
        {
            get => dto.MainPanelAccentColor.GetColor32();
            set
            {
                dto.MainPanelAccentColor = value.GetColorString();
                this.OnPropertyChanged();
            }
        }

        public float ItemWidth
        {
            get => dto.ItemWidth;
            set
            {
                dto.ItemWidth = value;
                this.OnPropertyChanged();
            }
        }

        public float ItemHeight
        {
            get => dto.ItemHeight;
            set
            {
                dto.ItemHeight = value;
                this.OnPropertyChanged();
            }
        }

        public float ItemPadding
        {
            get => dto.ItemPadding;
            set
            {
                dto.ItemPadding = value;
                this.OnPropertyChanged();
            }
        }

        public float ItemTextScale
        {
            get => dto.ItemTextScale;
            set
            {
                dto.ItemTextScale = value;
                this.OnPropertyChanged();
            }
        }

        public bool Electricity
        {
            get => dto.Electricity;
            set
            {
                dto.Electricity = value;
                this.OnPropertyChanged();
            }
        }

        public int ElectricityCriticalThreshold
        {
            get => dto.ElectricityCriticalThreshold;
            set
            {
                dto.ElectricityCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int ElectricitySortOrder
        {
            get => dto.ElectricitySortOrder;
            set
            {
                dto.ElectricitySortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Heating
        {
            get => dto.Heating;
            set
            {
                dto.Heating = value;
                this.OnPropertyChanged();
            }
        }

        public int HeatingCriticalThreshold
        {
            get => dto.HeatingCriticalThreshold;
            set
            {
                dto.HeatingCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int HeatingSortOrder
        {
            get => dto.HeatingSortOrder;
            set
            {
                dto.HeatingSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Water
        {
            get => dto.Water;
            set
            {
                dto.Water = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterCriticalThreshold
        {
            get => dto.WaterCriticalThreshold;
            set
            {
                dto.WaterCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterSortOrder
        {
            get => dto.WaterSortOrder;
            set
            {
                dto.WaterSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool SewageTreatment
        {
            get => dto.SewageTreatment;
            set
            {
                dto.SewageTreatment = value;
                this.OnPropertyChanged();
            }
        }

        public int SewageTreatmentCriticalThreshold
        {
            get => dto.SewageTreatmentCriticalThreshold;
            set
            {
                dto.SewageTreatmentCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int SewageTreatmentSortOrder
        {
            get => dto.SewageTreatmentSortOrder;
            set
            {
                dto.SewageTreatmentSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool WaterReserveTank
        {
            get => dto.WaterReserveTank;
            set
            {
                dto.WaterReserveTank = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterReserveTankCriticalThreshold
        {
            get => dto.WaterReserveTankCriticalThreshold;
            set
            {
                dto.WaterReserveTankCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterReserveTankSortOrder
        {
            get => dto.WaterReserveTankSortOrder;
            set
            {
                dto.WaterReserveTankSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool WaterPumpingServiceStorage
        {
            get => dto.WaterPumpingServiceStorage;
            set
            {
                dto.WaterPumpingServiceStorage = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterPumpingServiceStorageCriticalThreshold
        {
            get => dto.WaterPumpingServiceStorageCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceStorageCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterPumpingServiceStorageSortOrder
        {
            get => dto.WaterPumpingServiceStorageSortOrder;
            set
            {
                dto.WaterPumpingServiceStorageSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool WaterPumpingServiceVehicles
        {
            get => dto.WaterPumpingServiceVehicles;
            set
            {
                dto.WaterPumpingServiceVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterPumpingServiceVehiclesCriticalThreshold
        {
            get => dto.WaterPumpingServiceVehiclesCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int WaterPumpingServiceVehiclesSortOrder
        {
            get => dto.WaterPumpingServiceVehiclesSortOrder;
            set
            {
                dto.WaterPumpingServiceVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Landfill
        {
            get => dto.Landfill;
            set
            {
                dto.Landfill = value;
                this.OnPropertyChanged();
            }
        }

        public int LandfillCriticalThreshold
        {
            get => dto.LandfillCriticalThreshold;
            set
            {
                dto.LandfillCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int LandfillSortOrder
        {
            get => dto.LandfillSortOrder;
            set
            {
                dto.LandfillSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool LandfillVehicles
        {
            get => dto.LandfillVehicles;
            set
            {
                dto.LandfillVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int LandfillVehiclesCriticalThreshold
        {
            get => dto.LandfillVehiclesCriticalThreshold;
            set
            {
                dto.LandfillVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int LandfillVehiclesSortOrder
        {
            get => dto.LandfillVehiclesSortOrder;
            set
            {
                dto.LandfillVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool GarbageProcessing
        {
            get => dto.GarbageProcessing;
            set
            {
                dto.GarbageProcessing = value;
                this.OnPropertyChanged();
            }
        }

        public int GarbageProcessingCriticalThreshold
        {
            get => dto.GarbageProcessingCriticalThreshold;
            set
            {
                dto.GarbageProcessingCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int GarbageProcessingSortOrder
        {
            get => dto.GarbageProcessingSortOrder;
            set
            {
                dto.GarbageProcessingSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool GarbageProcessingVehicles
        {
            get => dto.GarbageProcessingVehicles;
            set
            {
                dto.GarbageProcessingVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int GarbageProcessingVehiclesCriticalThreshold
        {
            get => dto.GarbageProcessingVehiclesCriticalThreshold;
            set
            {
                dto.GarbageProcessingVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int GarbageProcessingVehiclesSortOrder
        {
            get => dto.GarbageProcessingVehiclesSortOrder;
            set
            {
                dto.GarbageProcessingVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool ElementarySchool
        {
            get => dto.ElementarySchool;
            set
            {
                dto.ElementarySchool = value;
                this.OnPropertyChanged();
            }
        }

        public int ElementarySchoolCriticalThreshold
        {
            get => dto.ElementarySchoolCriticalThreshold;
            set
            {
                dto.ElementarySchoolCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int ElementarySchoolSortOrder
        {
            get => dto.ElementarySchoolSortOrder;
            set
            {
                dto.ElementarySchoolSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool HighSchool
        {
            get => dto.HighSchool;
            set
            {
                dto.HighSchool = value;
                this.OnPropertyChanged();
            }
        }

        public int HighSchoolCriticalThreshold
        {
            get => dto.HighSchoolCriticalThreshold;
            set
            {
                dto.HighSchoolCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int HighSchoolSortOrder
        {
            get => dto.HighSchoolSortOrder;
            set
            {
                dto.HighSchoolSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool University
        {
            get => dto.University;
            set
            {
                dto.University = value;
                this.OnPropertyChanged();
            }
        }

        public int UniversityCriticalThreshold
        {
            get => dto.UniversityCriticalThreshold;
            set
            {
                dto.UniversityCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int UniversitySortOrder
        {
            get => dto.UniversitySortOrder;
            set
            {
                dto.UniversitySortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Healthcare
        {
            get => dto.Healthcare;
            set
            {
                dto.Healthcare = value;
                this.OnPropertyChanged();
            }
        }

        public int HealthcareCriticalThreshold
        {
            get => dto.HealthcareCriticalThreshold;
            set
            {
                dto.HealthcareCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int HealthcareSortOrder
        {
            get => dto.HealthcareSortOrder;
            set
            {
                dto.HealthcareSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool HealthcareVehicles
        {
            get => dto.HealthcareVehicles;
            set
            {
                dto.HealthcareVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int HealthcareVehiclesCriticalThreshold
        {
            get => dto.HealthcareVehiclesCriticalThreshold;
            set
            {
                dto.HealthcareVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int HealthcareVehiclesSortOrder
        {
            get => dto.HealthcareVehiclesSortOrder;
            set
            {
                dto.HealthcareVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool AverageIllnessRate
        {
            get => dto.AverageIllnessRate;
            set
            {
                dto.AverageIllnessRate = value;
                this.OnPropertyChanged();
            }
        }

        public int AverageIllnessRateCriticalThreshold
        {
            get => dto.AverageIllnessRateCriticalThreshold;
            set
            {
                dto.AverageIllnessRateCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int AverageIllnessRateSortOrder
        {
            get => dto.AverageIllnessRateSortOrder;
            set
            {
                dto.AverageIllnessRateSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Cemetery
        {
            get => dto.Cemetery;
            set
            {
                dto.Cemetery = value;
                this.OnPropertyChanged();
            }
        }

        public int CemeteryCriticalThreshold
        {
            get => dto.CemeteryCriticalThreshold;
            set
            {
                dto.CemeteryCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int CemeterySortOrder
        {
            get => dto.CemeterySortOrder;
            set
            {
                dto.CemeterySortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool CemeteryVehicles
        {
            get => dto.CemeteryVehicles;
            set
            {
                dto.CemeteryVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int CemeteryVehiclesCriticalThreshold
        {
            get => dto.CemeteryVehiclesCriticalThreshold;
            set
            {
                dto.CemeteryVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int CemeteryVehiclesSortOrder
        {
            get => dto.CemeteryVehiclesSortOrder;
            set
            {
                dto.CemeteryVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Crematorium
        {
            get => dto.Crematorium;
            set
            {
                dto.Crematorium = value;
                this.OnPropertyChanged();
            }
        }

        public int CrematoriumCriticalThreshold
        {
            get => dto.CrematoriumCriticalThreshold;
            set
            {
                dto.CrematoriumCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int CrematoriumSortOrder
        {
            get => dto.CrematoriumSortOrder;
            set
            {
                dto.CrematoriumSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool CrematoriumVehicles
        {
            get => dto.CrematoriumVehicles;
            set
            {
                dto.CrematoriumVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int CrematoriumVehiclesCriticalThreshold
        {
            get => dto.CrematoriumVehiclesCriticalThreshold;
            set
            {
                dto.CrematoriumVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int CrematoriumVehiclesSortOrder
        {
            get => dto.CrematoriumVehiclesSortOrder;
            set
            {
                dto.CrematoriumVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool GroundPollution
        {
            get => dto.GroundPollution;
            set
            {
                dto.GroundPollution = value;
                this.OnPropertyChanged();
            }
        }

        public int GroundPollutionCriticalThreshold
        {
            get => dto.GroundPollutionCriticalThreshold;
            set
            {
                dto.GroundPollutionCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int GroundPollutionSortOrder
        {
            get => dto.GroundPollutionSortOrder;
            set
            {
                dto.GroundPollutionSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool DrinkingWaterPollution
        {
            get => dto.DrinkingWaterPollution;
            set
            {
                dto.DrinkingWaterPollution = value;
                this.OnPropertyChanged();
            }
        }

        public int DrinkingWaterPollutionCriticalThreshold
        {
            get => dto.DrinkingWaterPollutionCriticalThreshold;
            set
            {
                dto.DrinkingWaterPollutionCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int DrinkingWaterPollutionSortOrder
        {
            get => dto.DrinkingWaterPollutionSortOrder;
            set
            {
                dto.DrinkingWaterPollutionSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool NoisePollution
        {
            get => dto.NoisePollution;
            set
            {
                dto.NoisePollution = value;
                this.OnPropertyChanged();
            }
        }

        public int NoisePollutionCriticalThreshold
        {
            get => dto.NoisePollutionCriticalThreshold;
            set
            {
                dto.NoisePollutionCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int NoisePollutionSortOrder
        {
            get => dto.NoisePollutionSortOrder;
            set
            {
                dto.NoisePollutionSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool FireHazard
        {
            get => dto.FireHazard;
            set
            {
                dto.FireHazard = value;
                this.OnPropertyChanged();
            }
        }

        public int FireHazardCriticalThreshold
        {
            get => dto.FireHazardCriticalThreshold;
            set
            {
                dto.FireHazardCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int FireHazardSortOrder
        {
            get => dto.FireHazardSortOrder;
            set
            {
                dto.FireHazardSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool FireDepartmentVehicles
        {
            get => dto.FireDepartmentVehicles;
            set
            {
                dto.FireDepartmentVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int FireDepartmentVehiclesCriticalThreshold
        {
            get => dto.FireDepartmentVehiclesCriticalThreshold;
            set
            {
                dto.FireDepartmentVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int FireDepartmentVehiclesSortOrder
        {
            get => dto.FireDepartmentVehiclesSortOrder;
            set
            {
                dto.FireDepartmentVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool CrimeRate
        {
            get => dto.CrimeRate;
            set
            {
                dto.CrimeRate = value;
                this.OnPropertyChanged();
            }
        }

        public int CrimeRateCriticalThreshold
        {
            get => dto.CrimeRateCriticalThreshold;
            set
            {
                dto.CrimeRateCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int CrimeRateSortOrder
        {
            get => dto.CrimeRateSortOrder;
            set
            {
                dto.CrimeRateSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool PoliceHoldingCells
        {
            get => dto.PoliceHoldingCells;
            set
            {
                dto.PoliceHoldingCells = value;
                this.OnPropertyChanged();
            }
        }

        public int PoliceHoldingCellsCriticalThreshold
        {
            get => dto.PoliceHoldingCellsCriticalThreshold;
            set
            {
                dto.PoliceHoldingCellsCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int PoliceHoldingCellsSortOrder
        {
            get => dto.PoliceHoldingCellsSortOrder;
            set
            {
                dto.PoliceHoldingCellsSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool PoliceVehicles
        {
            get => dto.PoliceVehicles;
            set
            {
                dto.PoliceVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int PoliceVehiclesCriticalThreshold
        {
            get => dto.PoliceVehiclesCriticalThreshold;
            set
            {
                dto.PoliceVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int PoliceVehiclesSortOrder
        {
            get => dto.PoliceVehiclesSortOrder;
            set
            {
                dto.PoliceVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool PrisonCells
        {
            get => dto.PrisonCells;
            set
            {
                dto.PrisonCells = value;
                this.OnPropertyChanged();
            }
        }

        public int PrisonCellsCriticalThreshold
        {
            get => dto.PrisonCellsCriticalThreshold;
            set
            {
                dto.PrisonCellsCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int PrisonCellsSortOrder
        {
            get => dto.PrisonCellsSortOrder;
            set
            {
                dto.PrisonCellsSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool PrisonVehicles
        {
            get => dto.PrisonVehicles;
            set
            {
                dto.PrisonVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int PrisonVehiclesCriticalThreshold
        {
            get => dto.PrisonVehiclesCriticalThreshold;
            set
            {
                dto.PrisonVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int PrisonVehiclesSortOrder
        {
            get => dto.PrisonVehiclesSortOrder;
            set
            {
                dto.PrisonVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool Unemployment
        {
            get => dto.Unemployment;
            set
            {
                dto.Unemployment = value;
                this.OnPropertyChanged();
            }
        }

        public int UnemploymentCriticalThreshold
        {
            get => dto.UnemploymentCriticalThreshold;
            set
            {
                dto.UnemploymentCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int UnemploymentSortOrder
        {
            get => dto.UnemploymentSortOrder;
            set
            {
                dto.UnemploymentSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool TrafficJam
        {
            get => dto.TrafficJam;
            set
            {
                dto.TrafficJam = value;
                this.OnPropertyChanged();
            }
        }

        public int TrafficJamCriticalThreshold
        {
            get => dto.TrafficJamCriticalThreshold;
            set
            {
                dto.TrafficJamCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int TrafficJamSortOrder
        {
            get => dto.TrafficJamSortOrder;
            set
            {
                dto.TrafficJamSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool RoadMaintenanceVehicles
        {
            get => dto.RoadMaintenanceVehicles;
            set
            {
                dto.RoadMaintenanceVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int RoadMaintenanceVehiclesCriticalThreshold
        {
            get => dto.RoadMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.RoadMaintenanceVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int RoadMaintenanceVehiclesSortOrder
        {
            get => dto.RoadMaintenanceVehiclesSortOrder;
            set
            {
                dto.RoadMaintenanceVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool SnowDump
        {
            get => dto.SnowDump;
            set
            {
                dto.SnowDump = value;
                this.OnPropertyChanged();
            }
        }

        public int SnowDumpCriticalThreshold
        {
            get => dto.SnowDumpCriticalThreshold;
            set
            {
                dto.SnowDumpCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int SnowDumpSortOrder
        {
            get => dto.SnowDumpSortOrder;
            set
            {
                dto.SnowDumpSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool SnowDumpVehicles
        {
            get => dto.SnowDumpVehicles;
            set
            {
                dto.SnowDumpVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int SnowDumpVehiclesCriticalThreshold
        {
            get => dto.SnowDumpVehiclesCriticalThreshold;
            set
            {
                dto.SnowDumpVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int SnowDumpVehiclesSortOrder
        {
            get => dto.SnowDumpVehiclesSortOrder;
            set
            {
                dto.SnowDumpVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool ParkMaintenanceVehicles
        {
            get => dto.ParkMaintenanceVehicles;
            set
            {
                dto.ParkMaintenanceVehicles = value;
                this.OnPropertyChanged();
            }
        }

        public int ParkMaintenanceVehiclesCriticalThreshold
        {
            get => dto.ParkMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.ParkMaintenanceVehiclesCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int ParkMaintenanceVehiclesSortOrder
        {
            get => dto.ParkMaintenanceVehiclesSortOrder;
            set
            {
                dto.ParkMaintenanceVehiclesSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public bool CityUnattractiveness
        {
            get => dto.CityUnattractiveness;
            set
            {
                dto.CityUnattractiveness = value;
                this.OnPropertyChanged();
            }
        }

        public int CityUnattractivenessCriticalThreshold
        {
            get => dto.CityUnattractivenessCriticalThreshold;
            set
            {
                dto.CityUnattractivenessCriticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int CityUnattractivenessSortOrder
        {
            get => dto.CityUnattractivenessSortOrder;
            set
            {
                dto.CityUnattractivenessSortOrder = value;
                this.OnPropertyChanged();
            }
        }

        private void UpdateFromDto(ConfigurationDto dto)
        {
            this.dto.MainPanelPositionX = dto.MainPanelPositionX;
            this.dto.MainPanelPositionY = dto.MainPanelPositionY;
            this.dto.MainPanelUpdateEveryXSeconds = dto.MainPanelUpdateEveryXSeconds;
            this.dto.MainPanelAutoHide = dto.MainPanelAutoHide;
            this.dto.MainPanelColumnCount = dto.MainPanelColumnCount;
            this.dto.MainPanelBackgroundColor = dto.MainPanelBackgroundColor;
            this.dto.MainPanelForegroundColor = dto.MainPanelForegroundColor;
            this.dto.MainPanelAccentColor = dto.MainPanelAccentColor;
            this.dto.ItemWidth = dto.ItemWidth;
            this.dto.ItemHeight = dto.ItemHeight;
            this.dto.ItemPadding = dto.ItemPadding;
            this.dto.ItemTextScale = dto.ItemTextScale;
            this.dto.Electricity = dto.Electricity;
            this.dto.ElectricityCriticalThreshold = dto.ElectricityCriticalThreshold;
            this.dto.ElectricitySortOrder = dto.ElectricitySortOrder;
            this.dto.Heating = dto.Heating;
            this.dto.HeatingCriticalThreshold = dto.HeatingCriticalThreshold;
            this.dto.HeatingSortOrder = dto.HeatingSortOrder;
            this.dto.Water = dto.Water;
            this.dto.WaterCriticalThreshold = dto.WaterCriticalThreshold;
            this.dto.WaterSortOrder = dto.WaterSortOrder;
            this.dto.SewageTreatment = dto.SewageTreatment;
            this.dto.SewageTreatmentCriticalThreshold = dto.SewageTreatmentCriticalThreshold;
            this.dto.SewageTreatmentSortOrder = dto.SewageTreatmentSortOrder;
            this.dto.WaterReserveTank = dto.WaterReserveTank;
            this.dto.WaterReserveTankCriticalThreshold = dto.WaterReserveTankCriticalThreshold;
            this.dto.WaterReserveTankSortOrder = dto.WaterReserveTankSortOrder;
            this.dto.WaterPumpingServiceStorage = dto.WaterPumpingServiceStorage;
            this.dto.WaterPumpingServiceStorageCriticalThreshold = dto.WaterPumpingServiceStorageCriticalThreshold;
            this.dto.WaterPumpingServiceStorageSortOrder = dto.WaterPumpingServiceStorageSortOrder;
            this.dto.WaterPumpingServiceVehicles = dto.WaterPumpingServiceVehicles;
            this.dto.WaterPumpingServiceVehiclesCriticalThreshold = dto.WaterPumpingServiceVehiclesCriticalThreshold;
            this.dto.WaterPumpingServiceVehiclesSortOrder = dto.WaterPumpingServiceVehiclesSortOrder;
            this.dto.Landfill = dto.Landfill;
            this.dto.LandfillCriticalThreshold = dto.LandfillCriticalThreshold;
            this.dto.LandfillSortOrder = dto.LandfillSortOrder;
            this.dto.LandfillVehicles = dto.LandfillVehicles;
            this.dto.LandfillVehiclesCriticalThreshold = dto.LandfillVehiclesCriticalThreshold;
            this.dto.LandfillVehiclesSortOrder = dto.LandfillVehiclesSortOrder;
            this.dto.GarbageProcessing = dto.GarbageProcessing;
            this.dto.GarbageProcessingCriticalThreshold = dto.GarbageProcessingCriticalThreshold;
            this.dto.GarbageProcessingSortOrder = dto.GarbageProcessingSortOrder;
            this.dto.GarbageProcessingVehicles = dto.GarbageProcessingVehicles;
            this.dto.GarbageProcessingVehiclesCriticalThreshold = dto.GarbageProcessingVehiclesCriticalThreshold;
            this.dto.GarbageProcessingVehiclesSortOrder = dto.GarbageProcessingVehiclesSortOrder;
            this.dto.ElementarySchool = dto.ElementarySchool;
            this.dto.ElementarySchoolCriticalThreshold = dto.ElementarySchoolCriticalThreshold;
            this.dto.ElementarySchoolSortOrder = dto.ElementarySchoolSortOrder;
            this.dto.HighSchool = dto.HighSchool;
            this.dto.HighSchoolCriticalThreshold = dto.HighSchoolCriticalThreshold;
            this.dto.HighSchoolSortOrder = dto.HighSchoolSortOrder;
            this.dto.University = dto.University;
            this.dto.UniversityCriticalThreshold = dto.UniversityCriticalThreshold;
            this.dto.UniversitySortOrder = dto.UniversitySortOrder;
            this.dto.Healthcare = dto.Healthcare;
            this.dto.HealthcareCriticalThreshold = dto.HealthcareCriticalThreshold;
            this.dto.HealthcareSortOrder = dto.HealthcareSortOrder;
            this.dto.HealthcareVehicles = dto.HealthcareVehicles;
            this.dto.HealthcareVehiclesCriticalThreshold = dto.HealthcareVehiclesCriticalThreshold;
            this.dto.HealthcareVehiclesSortOrder = dto.HealthcareVehiclesSortOrder;
            this.dto.AverageIllnessRate = dto.AverageIllnessRate;
            this.dto.AverageIllnessRateCriticalThreshold = dto.AverageIllnessRateCriticalThreshold;
            this.dto.AverageIllnessRateSortOrder = dto.AverageIllnessRateSortOrder;
            this.dto.Cemetery = dto.Cemetery;
            this.dto.CemeteryCriticalThreshold = dto.CemeteryCriticalThreshold;
            this.dto.CemeterySortOrder = dto.CemeterySortOrder;
            this.dto.CemeteryVehicles = dto.CemeteryVehicles;
            this.dto.CemeteryVehiclesCriticalThreshold = dto.CemeteryVehiclesCriticalThreshold;
            this.dto.CemeteryVehiclesSortOrder = dto.CemeteryVehiclesSortOrder;
            this.dto.Crematorium = dto.Crematorium;
            this.dto.CrematoriumCriticalThreshold = dto.CrematoriumCriticalThreshold;
            this.dto.CrematoriumSortOrder = dto.CrematoriumSortOrder;
            this.dto.CrematoriumVehicles = dto.CrematoriumVehicles;
            this.dto.CrematoriumVehiclesCriticalThreshold = dto.CrematoriumVehiclesCriticalThreshold;
            this.dto.CrematoriumVehiclesSortOrder = dto.CrematoriumVehiclesSortOrder;
            this.dto.GroundPollution = dto.GroundPollution;
            this.dto.GroundPollutionCriticalThreshold = dto.GroundPollutionCriticalThreshold;
            this.dto.GroundPollutionSortOrder = dto.GroundPollutionSortOrder;
            this.dto.DrinkingWaterPollution = dto.DrinkingWaterPollution;
            this.dto.DrinkingWaterPollutionCriticalThreshold = dto.DrinkingWaterPollutionCriticalThreshold;
            this.dto.DrinkingWaterPollutionSortOrder = dto.DrinkingWaterPollutionSortOrder;
            this.dto.NoisePollution = dto.NoisePollution;
            this.dto.NoisePollutionCriticalThreshold = dto.NoisePollutionCriticalThreshold;
            this.dto.NoisePollutionSortOrder = dto.NoisePollutionSortOrder;
            this.dto.FireHazard = dto.FireHazard;
            this.dto.FireHazardCriticalThreshold = dto.FireHazardCriticalThreshold;
            this.dto.FireHazardSortOrder = dto.FireHazardSortOrder;
            this.dto.FireDepartmentVehicles = dto.FireDepartmentVehicles;
            this.dto.FireDepartmentVehiclesCriticalThreshold = dto.FireDepartmentVehiclesCriticalThreshold;
            this.dto.FireDepartmentVehiclesSortOrder = dto.FireDepartmentVehiclesSortOrder;
            this.dto.CrimeRate = dto.CrimeRate;
            this.dto.CrimeRateCriticalThreshold = dto.CrimeRateCriticalThreshold;
            this.dto.CrimeRateSortOrder = dto.CrimeRateSortOrder;
            this.dto.PoliceHoldingCells = dto.PoliceHoldingCells;
            this.dto.PoliceHoldingCellsCriticalThreshold = dto.PoliceHoldingCellsCriticalThreshold;
            this.dto.PoliceHoldingCellsSortOrder = dto.PoliceHoldingCellsSortOrder;
            this.dto.PoliceVehicles = dto.PoliceVehicles;
            this.dto.PoliceVehiclesCriticalThreshold = dto.PoliceVehiclesCriticalThreshold;
            this.dto.PoliceVehiclesSortOrder = dto.PoliceVehiclesSortOrder;
            this.dto.PrisonCells = dto.PrisonCells;
            this.dto.PrisonCellsCriticalThreshold = dto.PrisonCellsCriticalThreshold;
            this.dto.PrisonCellsSortOrder = dto.PrisonCellsSortOrder;
            this.dto.PrisonVehicles = dto.PrisonVehicles;
            this.dto.PrisonVehiclesCriticalThreshold = dto.PrisonVehiclesCriticalThreshold;
            this.dto.PrisonVehiclesSortOrder = dto.PrisonVehiclesSortOrder;
            this.dto.Unemployment = dto.Unemployment;
            this.dto.UnemploymentCriticalThreshold = dto.UnemploymentCriticalThreshold;
            this.dto.UnemploymentSortOrder = dto.UnemploymentSortOrder;
            this.dto.TrafficJam = dto.TrafficJam;
            this.dto.TrafficJamCriticalThreshold = dto.TrafficJamCriticalThreshold;
            this.dto.TrafficJamSortOrder = dto.TrafficJamSortOrder;
            this.dto.RoadMaintenanceVehicles = dto.RoadMaintenanceVehicles;
            this.dto.RoadMaintenanceVehiclesCriticalThreshold = dto.RoadMaintenanceVehiclesCriticalThreshold;
            this.dto.RoadMaintenanceVehiclesSortOrder = dto.RoadMaintenanceVehiclesSortOrder;
            this.dto.SnowDump = dto.SnowDump;
            this.dto.SnowDumpCriticalThreshold = dto.SnowDumpCriticalThreshold;
            this.dto.SnowDumpSortOrder = dto.SnowDumpSortOrder;
            this.dto.SnowDumpVehicles = dto.SnowDumpVehicles;
            this.dto.SnowDumpVehiclesCriticalThreshold = dto.SnowDumpVehiclesCriticalThreshold;
            this.dto.SnowDumpVehiclesSortOrder = dto.SnowDumpVehiclesSortOrder;
            this.dto.ParkMaintenanceVehicles = dto.ParkMaintenanceVehicles;
            this.dto.ParkMaintenanceVehiclesCriticalThreshold = dto.ParkMaintenanceVehiclesCriticalThreshold;
            this.dto.ParkMaintenanceVehiclesSortOrder = dto.ParkMaintenanceVehiclesSortOrder;
            this.dto.CityUnattractiveness = dto.CityUnattractiveness;
            this.dto.CityUnattractivenessCriticalThreshold = dto.CityUnattractivenessCriticalThreshold;
            this.dto.CityUnattractivenessSortOrder = dto.CityUnattractivenessSortOrder;

            this.OnPropertyChanged();
        }

        private ConfigurationDto GetDtoFromValues()
        {
            var dto = new ConfigurationDto()
            {
                MainPanelPositionX = this.MainPanelPositionX,
                MainPanelPositionY = this.MainPanelPositionY,
                MainPanelUpdateEveryXSeconds = this.MainPanelUpdateEveryXSeconds,
                MainPanelAutoHide = this.MainPanelAutoHide,
                MainPanelColumnCount = this.MainPanelColumnCount,
                MainPanelBackgroundColor = this.MainPanelBackgroundColor.GetColorString(),
                MainPanelForegroundColor = this.MainPanelForegroundColor.GetColorString(),
                MainPanelAccentColor = this.MainPanelAccentColor.GetColorString(),
                ItemWidth = this.ItemWidth,
                ItemHeight = this.ItemHeight,
                ItemPadding = this.ItemPadding,
                ItemTextScale = this.ItemTextScale,
                Electricity = this.Electricity,
                ElectricityCriticalThreshold = this.ElectricityCriticalThreshold,
                ElectricitySortOrder = this.ElectricitySortOrder,
                Heating = this.Heating,
                HeatingCriticalThreshold = this.HeatingCriticalThreshold,
                HeatingSortOrder = this.HeatingSortOrder,
                Water = this.Water,
                WaterCriticalThreshold = this.WaterCriticalThreshold,
                WaterSortOrder = this.WaterSortOrder,
                SewageTreatment = this.SewageTreatment,
                SewageTreatmentCriticalThreshold = this.SewageTreatmentCriticalThreshold,
                SewageTreatmentSortOrder = this.SewageTreatmentSortOrder,
                WaterReserveTank = this.WaterReserveTank,
                WaterReserveTankCriticalThreshold = this.WaterReserveTankCriticalThreshold,
                WaterReserveTankSortOrder = this.WaterReserveTankSortOrder,
                WaterPumpingServiceStorage = this.WaterPumpingServiceStorage,
                WaterPumpingServiceStorageCriticalThreshold = this.WaterPumpingServiceStorageCriticalThreshold,
                WaterPumpingServiceStorageSortOrder = this.WaterPumpingServiceStorageSortOrder,
                WaterPumpingServiceVehicles = this.WaterPumpingServiceVehicles,
                WaterPumpingServiceVehiclesCriticalThreshold = this.WaterPumpingServiceVehiclesCriticalThreshold,
                WaterPumpingServiceVehiclesSortOrder = this.WaterPumpingServiceVehiclesSortOrder,
                Landfill = this.Landfill,
                LandfillCriticalThreshold = this.LandfillCriticalThreshold,
                LandfillSortOrder = this.LandfillSortOrder,
                LandfillVehicles = this.LandfillVehicles,
                LandfillVehiclesCriticalThreshold = this.LandfillVehiclesCriticalThreshold,
                LandfillVehiclesSortOrder = this.LandfillVehiclesSortOrder,
                GarbageProcessing = this.GarbageProcessing,
                GarbageProcessingCriticalThreshold = this.GarbageProcessingCriticalThreshold,
                GarbageProcessingSortOrder = this.GarbageProcessingSortOrder,
                GarbageProcessingVehicles = this.GarbageProcessingVehicles,
                GarbageProcessingVehiclesCriticalThreshold = this.GarbageProcessingVehiclesCriticalThreshold,
                GarbageProcessingVehiclesSortOrder = this.GarbageProcessingVehiclesSortOrder,
                ElementarySchool = this.ElementarySchool,
                ElementarySchoolCriticalThreshold = this.ElementarySchoolCriticalThreshold,
                ElementarySchoolSortOrder = this.ElementarySchoolSortOrder,
                HighSchool = this.HighSchool,
                HighSchoolCriticalThreshold = this.HighSchoolCriticalThreshold,
                HighSchoolSortOrder = this.HighSchoolSortOrder,
                University = this.University,
                UniversityCriticalThreshold = this.UniversityCriticalThreshold,
                UniversitySortOrder = this.UniversitySortOrder,
                Healthcare = this.Healthcare,
                HealthcareCriticalThreshold = this.HealthcareCriticalThreshold,
                HealthcareSortOrder = this.HealthcareSortOrder,
                HealthcareVehicles = this.HealthcareVehicles,
                HealthcareVehiclesCriticalThreshold = this.HealthcareVehiclesCriticalThreshold,
                HealthcareVehiclesSortOrder = this.HealthcareVehiclesSortOrder,
                AverageIllnessRate = this.AverageIllnessRate,
                AverageIllnessRateCriticalThreshold = this.AverageIllnessRateCriticalThreshold,
                AverageIllnessRateSortOrder = this.AverageIllnessRateSortOrder,
                Cemetery = this.Cemetery,
                CemeteryCriticalThreshold = this.CemeteryCriticalThreshold,
                CemeterySortOrder = this.CemeterySortOrder,
                CemeteryVehicles = this.CemeteryVehicles,
                CemeteryVehiclesCriticalThreshold = this.CemeteryVehiclesCriticalThreshold,
                CemeteryVehiclesSortOrder = this.CemeteryVehiclesSortOrder,
                Crematorium = this.Crematorium,
                CrematoriumCriticalThreshold = this.CrematoriumCriticalThreshold,
                CrematoriumSortOrder = this.CrematoriumSortOrder,
                CrematoriumVehicles = this.CrematoriumVehicles,
                CrematoriumVehiclesCriticalThreshold = this.CrematoriumVehiclesCriticalThreshold,
                CrematoriumVehiclesSortOrder = this.CrematoriumVehiclesSortOrder,
                GroundPollution = this.GroundPollution,
                GroundPollutionCriticalThreshold = this.GroundPollutionCriticalThreshold,
                GroundPollutionSortOrder = this.GroundPollutionSortOrder,
                DrinkingWaterPollution = this.DrinkingWaterPollution,
                DrinkingWaterPollutionCriticalThreshold = this.DrinkingWaterPollutionCriticalThreshold,
                DrinkingWaterPollutionSortOrder = this.DrinkingWaterPollutionSortOrder,
                NoisePollution = this.NoisePollution,
                NoisePollutionCriticalThreshold = this.NoisePollutionCriticalThreshold,
                NoisePollutionSortOrder = this.NoisePollutionSortOrder,
                FireHazard = this.FireHazard,
                FireHazardCriticalThreshold = this.FireHazardCriticalThreshold,
                FireHazardSortOrder = this.FireHazardSortOrder,
                FireDepartmentVehicles = this.FireDepartmentVehicles,
                FireDepartmentVehiclesCriticalThreshold = this.FireDepartmentVehiclesCriticalThreshold,
                FireDepartmentVehiclesSortOrder = this.FireDepartmentVehiclesSortOrder,
                CrimeRate = this.CrimeRate,
                CrimeRateCriticalThreshold = this.CrimeRateCriticalThreshold,
                CrimeRateSortOrder = this.CrimeRateSortOrder,
                PoliceHoldingCells = this.PoliceHoldingCells,
                PoliceHoldingCellsCriticalThreshold = this.PoliceHoldingCellsCriticalThreshold,
                PoliceHoldingCellsSortOrder = this.PoliceHoldingCellsSortOrder,
                PoliceVehicles = this.PoliceVehicles,
                PoliceVehiclesCriticalThreshold = this.PoliceVehiclesCriticalThreshold,
                PoliceVehiclesSortOrder = this.PoliceVehiclesSortOrder,
                PrisonCells = this.PrisonCells,
                PrisonCellsCriticalThreshold = this.PrisonCellsCriticalThreshold,
                PrisonCellsSortOrder = this.PrisonCellsSortOrder,
                PrisonVehicles = this.PrisonVehicles,
                PrisonVehiclesCriticalThreshold = this.PrisonVehiclesCriticalThreshold,
                PrisonVehiclesSortOrder = this.PrisonVehiclesSortOrder,
                Unemployment = this.Unemployment,
                UnemploymentCriticalThreshold = this.UnemploymentCriticalThreshold,
                UnemploymentSortOrder = this.UnemploymentSortOrder,
                TrafficJam = this.TrafficJam,
                TrafficJamCriticalThreshold = this.TrafficJamCriticalThreshold,
                TrafficJamSortOrder = this.TrafficJamSortOrder,
                RoadMaintenanceVehicles = this.RoadMaintenanceVehicles,
                RoadMaintenanceVehiclesCriticalThreshold = this.RoadMaintenanceVehiclesCriticalThreshold,
                RoadMaintenanceVehiclesSortOrder = this.RoadMaintenanceVehiclesSortOrder,
                SnowDump = this.SnowDump,
                SnowDumpCriticalThreshold = this.SnowDumpCriticalThreshold,
                SnowDumpSortOrder = this.SnowDumpSortOrder,
                SnowDumpVehicles = this.SnowDumpVehicles,
                SnowDumpVehiclesCriticalThreshold = this.SnowDumpVehiclesCriticalThreshold,
                SnowDumpVehiclesSortOrder = this.SnowDumpVehiclesSortOrder,
                ParkMaintenanceVehicles = this.ParkMaintenanceVehicles,
                ParkMaintenanceVehiclesCriticalThreshold = this.ParkMaintenanceVehiclesCriticalThreshold,
                ParkMaintenanceVehiclesSortOrder = this.ParkMaintenanceVehiclesSortOrder,
                CityUnattractiveness = this.CityUnattractiveness,
                CityUnattractivenessCriticalThreshold = this.CityUnattractivenessCriticalThreshold,
                CityUnattractivenessSortOrder = this.CityUnattractivenessSortOrder
            };

            return dto;
        }

        public void Save()
        {
            this.configurationService.Save(this.dto);
        }

        public void ResetToDefault()
        {
            var dto = new ConfigurationDto();
            this.UpdateFromDto(dto);
            this.OnPropertyChanged();
        }

        public event Action PropertyChanged;

        private void OnPropertyChanged()
        {
            this.PropertyChanged?.Invoke();
        }
    }
}
