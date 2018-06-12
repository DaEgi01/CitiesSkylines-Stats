using System;
using UnityEngine;

namespace Stats.Configuration
{
    public class ConfigurationModel
    {
        private readonly ConfigurationService configurationService;

        private ConfigurationDto dto;

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

        public bool MainPanelHideItemsBelowTreshold
        {
            get => dto.MainPanelHideItemsBelowTreshold;
            set
            {
                dto.MainPanelHideItemsBelowTreshold = value;
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

        public int ElectricityCriticalTreshold
        {
            get => dto.ElectricityCriticalTreshold;
            set
            {
                dto.ElectricityCriticalTreshold = value;
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

        public int HeatingCriticalTreshold
        {
            get => dto.HeatingCriticalTreshold;
            set
            {
                dto.HeatingCriticalTreshold = value;
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

        public int WaterCriticalTreshold
        {
            get => dto.WaterCriticalTreshold;
            set
            {
                dto.WaterCriticalTreshold = value;
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

        public int SewageTreatmentCriticalTreshold
        {
            get => dto.SewageTreatmentCriticalTreshold;
            set
            {
                dto.SewageTreatmentCriticalTreshold = value;
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

        public int WaterReserveTankCriticalTreshold
        {
            get => dto.WaterReserveTankCriticalTreshold;
            set
            {
                dto.WaterReserveTankCriticalTreshold = value;
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

        public int WaterPumpingServiceStorageCriticalTreshold
        {
            get => dto.WaterPumpingServiceStorageCriticalTreshold;
            set
            {
                dto.WaterPumpingServiceStorageCriticalTreshold = value;
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

        public int WaterPumpingServiceVehiclesCriticalTreshold
        {
            get => dto.WaterPumpingServiceVehiclesCriticalTreshold;
            set
            {
                dto.WaterPumpingServiceVehiclesCriticalTreshold = value;
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

        public int LandfillCriticalTreshold
        {
            get => dto.LandfillCriticalTreshold;
            set
            {
                dto.LandfillCriticalTreshold = value;
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

        public int LandfillVehiclesCriticalTreshold
        {
            get => dto.LandfillVehiclesCriticalTreshold;
            set
            {
                dto.LandfillVehiclesCriticalTreshold = value;
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

        public int GarbageProcessingCriticalTreshold
        {
            get => dto.GarbageProcessingCriticalTreshold;
            set
            {
                dto.GarbageProcessingCriticalTreshold = value;
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

        public int GarbageProcessingVehiclesCriticalTreshold
        {
            get => dto.GarbageProcessingVehiclesCriticalTreshold;
            set
            {
                dto.GarbageProcessingVehiclesCriticalTreshold = value;
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

        public int ElementarySchoolCriticalTreshold
        {
            get => dto.ElementarySchoolCriticalTreshold;
            set
            {
                dto.ElementarySchoolCriticalTreshold = value;
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

        public int HighSchoolCriticalTreshold
        {
            get => dto.HighSchoolCriticalTreshold;
            set
            {
                dto.HighSchoolCriticalTreshold = value;
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

        public int UniversityCriticalTreshold
        {
            get => dto.UniversityCriticalTreshold;
            set
            {
                dto.UniversityCriticalTreshold = value;
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

        public int HealthcareCriticalTreshold
        {
            get => dto.HealthcareCriticalTreshold;
            set
            {
                dto.HealthcareCriticalTreshold = value;
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

        public int HealthcareVehiclesCriticalTreshold
        {
            get => dto.HealthcareVehiclesCriticalTreshold;
            set
            {
                dto.HealthcareVehiclesCriticalTreshold = value;
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

        public int AverageIllnessRateCriticalTreshold
        {
            get => dto.AverageIllnessRateCriticalTreshold;
            set
            {
                dto.AverageIllnessRateCriticalTreshold = value;
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

        public int CemeteryCriticalTreshold
        {
            get => dto.CemeteryCriticalTreshold;
            set
            {
                dto.CemeteryCriticalTreshold = value;
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

        public int CemeteryVehiclesCriticalTreshold
        {
            get => dto.CemeteryVehiclesCriticalTreshold;
            set
            {
                dto.CemeteryVehiclesCriticalTreshold = value;
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

        public int CrematoriumCriticalTreshold
        {
            get => dto.CrematoriumCriticalTreshold;
            set
            {
                dto.CrematoriumCriticalTreshold = value;
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

        public int CrematoriumVehiclesCriticalTreshold
        {
            get => dto.CrematoriumVehiclesCriticalTreshold;
            set
            {
                dto.CrematoriumVehiclesCriticalTreshold = value;
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

        public int GroundPollutionCriticalTreshold
        {
            get => dto.GroundPollutionCriticalTreshold;
            set
            {
                dto.GroundPollutionCriticalTreshold = value;
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

        public int DrinkingWaterPollutionCriticalTreshold
        {
            get => dto.DrinkingWaterPollutionCriticalTreshold;
            set
            {
                dto.DrinkingWaterPollutionCriticalTreshold = value;
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

        public int NoisePollutionCriticalTreshold
        {
            get => dto.NoisePollutionCriticalTreshold;
            set
            {
                dto.NoisePollutionCriticalTreshold = value;
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

        public int FireHazardCriticalTreshold
        {
            get => dto.FireHazardCriticalTreshold;
            set
            {
                dto.FireHazardCriticalTreshold = value;
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

        public int FireDepartmentVehiclesCriticalTreshold
        {
            get => dto.FireDepartmentVehiclesCriticalTreshold;
            set
            {
                dto.FireDepartmentVehiclesCriticalTreshold = value;
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

        public int CrimeRateCriticalTreshold
        {
            get => dto.CrimeRateCriticalTreshold;
            set
            {
                dto.CrimeRateCriticalTreshold = value;
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

        public int PoliceHoldingCellsCriticalTreshold
        {
            get => dto.PoliceHoldingCellsCriticalTreshold;
            set
            {
                dto.PoliceHoldingCellsCriticalTreshold = value;
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

        public int PoliceVehiclesCriticalTreshold
        {
            get => dto.PoliceVehiclesCriticalTreshold;
            set
            {
                dto.PoliceVehiclesCriticalTreshold = value;
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

        public int PrisonCellsCriticalTreshold
        {
            get => dto.PrisonCellsCriticalTreshold;
            set
            {
                dto.PrisonCellsCriticalTreshold = value;
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

        public int PrisonVehiclesCriticalTreshold
        {
            get => dto.PrisonVehiclesCriticalTreshold;
            set
            {
                dto.PrisonVehiclesCriticalTreshold = value;
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

        public int UnemploymentCriticalTreshold
        {
            get => dto.UnemploymentCriticalTreshold;
            set
            {
                dto.UnemploymentCriticalTreshold = value;
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

        public int TrafficJamCriticalTreshold
        {
            get => dto.TrafficJamCriticalTreshold;
            set
            {
                dto.TrafficJamCriticalTreshold = value;
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

        public int RoadMaintenanceVehiclesCriticalTreshold
        {
            get => dto.RoadMaintenanceVehiclesCriticalTreshold;
            set
            {
                dto.RoadMaintenanceVehiclesCriticalTreshold = value;
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

        public int SnowDumpCriticalTreshold
        {
            get => dto.SnowDumpCriticalTreshold;
            set
            {
                dto.SnowDumpCriticalTreshold = value;
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

        public int SnowDumpVehiclesCriticalTreshold
        {
            get => dto.SnowDumpVehiclesCriticalTreshold;
            set
            {
                dto.SnowDumpVehiclesCriticalTreshold = value;
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

        public int ParkMaintenanceVehiclesCriticalTreshold
        {
            get => dto.ParkMaintenanceVehiclesCriticalTreshold;
            set
            {
                dto.ParkMaintenanceVehiclesCriticalTreshold = value;
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

        public int CityUnattractivenessCriticalTreshold
        {
            get => dto.CityUnattractivenessCriticalTreshold;
            set
            {
                dto.CityUnattractivenessCriticalTreshold = value;
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
            this.MainPanelPositionX = dto.MainPanelPositionX;
            this.MainPanelPositionY = dto.MainPanelPositionY;
            this.MainPanelUpdateEveryXSeconds = dto.MainPanelUpdateEveryXSeconds;
            this.MainPanelAutoHide = dto.MainPanelAutoHide;
            this.MainPanelColumnCount = dto.MainPanelColumnCount;
            this.MainPanelBackgroundColor = dto.MainPanelBackgroundColor.GetColor32();
            this.MainPanelForegroundColor = dto.MainPanelForegroundColor.GetColor32();
            this.MainPanelAccentColor = dto.MainPanelAccentColor.GetColor32();
            this.ItemWidth = dto.ItemWidth;
            this.ItemHeight = dto.ItemHeight;
            this.ItemPadding = dto.ItemPadding;
            this.ItemTextScale = dto.ItemTextScale;
            this.Electricity = dto.Electricity;
            this.ElectricityCriticalTreshold = dto.ElectricityCriticalTreshold;
            this.ElectricitySortOrder = dto.ElectricitySortOrder;
            this.Heating = dto.Heating;
            this.HeatingCriticalTreshold = dto.HeatingCriticalTreshold;
            this.HeatingSortOrder = dto.HeatingSortOrder;
            this.Water = dto.Water;
            this.WaterCriticalTreshold = dto.WaterCriticalTreshold;
            this.WaterSortOrder = dto.WaterSortOrder;
            this.SewageTreatment = dto.SewageTreatment;
            this.SewageTreatmentCriticalTreshold = dto.SewageTreatmentCriticalTreshold;
            this.SewageTreatmentSortOrder = dto.SewageTreatmentSortOrder;
            this.WaterReserveTank = dto.WaterReserveTank;
            this.WaterReserveTankCriticalTreshold = dto.WaterReserveTankCriticalTreshold;
            this.WaterReserveTankSortOrder = dto.WaterReserveTankSortOrder;
            this.WaterPumpingServiceStorage = dto.WaterPumpingServiceStorage;
            this.WaterPumpingServiceStorageCriticalTreshold = dto.WaterPumpingServiceStorageCriticalTreshold;
            this.WaterPumpingServiceStorageSortOrder = dto.WaterPumpingServiceStorageSortOrder;
            this.WaterPumpingServiceVehicles = dto.WaterPumpingServiceVehicles;
            this.WaterPumpingServiceVehiclesCriticalTreshold = dto.WaterPumpingServiceVehiclesCriticalTreshold;
            this.WaterPumpingServiceVehiclesSortOrder = dto.WaterPumpingServiceVehiclesSortOrder;
            this.Landfill = dto.Landfill;
            this.LandfillCriticalTreshold = dto.LandfillCriticalTreshold;
            this.LandfillSortOrder = dto.LandfillSortOrder;
            this.LandfillVehicles = dto.LandfillVehicles;
            this.LandfillVehiclesCriticalTreshold = dto.LandfillVehiclesCriticalTreshold;
            this.LandfillVehiclesSortOrder = dto.LandfillVehiclesSortOrder;
            this.GarbageProcessing = dto.GarbageProcessing;
            this.GarbageProcessingCriticalTreshold = dto.GarbageProcessingCriticalTreshold;
            this.GarbageProcessingSortOrder = dto.GarbageProcessingSortOrder;
            this.GarbageProcessingVehicles = dto.GarbageProcessingVehicles;
            this.GarbageProcessingVehiclesCriticalTreshold = dto.GarbageProcessingVehiclesCriticalTreshold;
            this.GarbageProcessingVehiclesSortOrder = dto.GarbageProcessingVehiclesSortOrder;
            this.ElementarySchool = dto.ElementarySchool;
            this.ElementarySchoolCriticalTreshold = dto.ElementarySchoolCriticalTreshold;
            this.ElementarySchoolSortOrder = dto.ElementarySchoolSortOrder;
            this.HighSchool = dto.HighSchool;
            this.HighSchoolCriticalTreshold = dto.HighSchoolCriticalTreshold;
            this.HighSchoolSortOrder = dto.HighSchoolSortOrder;
            this.University = dto.University;
            this.UniversityCriticalTreshold = dto.UniversityCriticalTreshold;
            this.UniversitySortOrder = dto.UniversitySortOrder;
            this.Healthcare = dto.Healthcare;
            this.HealthcareCriticalTreshold = dto.HealthcareCriticalTreshold;
            this.HealthcareSortOrder = dto.HealthcareSortOrder;
            this.HealthcareVehicles = dto.HealthcareVehicles;
            this.HealthcareVehiclesCriticalTreshold = dto.HealthcareVehiclesCriticalTreshold;
            this.HealthcareVehiclesSortOrder = dto.HealthcareVehiclesSortOrder;
            this.AverageIllnessRate = dto.AverageIllnessRate;
            this.AverageIllnessRateCriticalTreshold = dto.AverageIllnessRateCriticalTreshold;
            this.AverageIllnessRateSortOrder = dto.AverageIllnessRateSortOrder;
            this.Cemetery = dto.Cemetery;
            this.CemeteryCriticalTreshold = dto.CemeteryCriticalTreshold;
            this.CemeterySortOrder = dto.CemeterySortOrder;
            this.CemeteryVehicles = dto.CemeteryVehicles;
            this.CemeteryVehiclesCriticalTreshold = dto.CemeteryVehiclesCriticalTreshold;
            this.CemeteryVehiclesSortOrder = dto.CemeteryVehiclesSortOrder;
            this.Crematorium = dto.Crematorium;
            this.CrematoriumCriticalTreshold = dto.CrematoriumCriticalTreshold;
            this.CrematoriumSortOrder = dto.CrematoriumSortOrder;
            this.CrematoriumVehicles = dto.CrematoriumVehicles;
            this.CrematoriumVehiclesCriticalTreshold = dto.CrematoriumVehiclesCriticalTreshold;
            this.CrematoriumVehiclesSortOrder = dto.CrematoriumVehiclesSortOrder;
            this.GroundPollution = dto.GroundPollution;
            this.GroundPollutionCriticalTreshold = dto.GroundPollutionCriticalTreshold;
            this.GroundPollutionSortOrder = dto.GroundPollutionSortOrder;
            this.DrinkingWaterPollution = dto.DrinkingWaterPollution;
            this.DrinkingWaterPollutionCriticalTreshold = dto.DrinkingWaterPollutionCriticalTreshold;
            this.DrinkingWaterPollutionSortOrder = dto.DrinkingWaterPollutionSortOrder;
            this.NoisePollution = dto.NoisePollution;
            this.NoisePollutionCriticalTreshold = dto.NoisePollutionCriticalTreshold;
            this.NoisePollutionSortOrder = dto.NoisePollutionSortOrder;
            this.FireHazard = dto.FireHazard;
            this.FireHazardCriticalTreshold = dto.FireHazardCriticalTreshold;
            this.FireHazardSortOrder = dto.FireHazardSortOrder;
            this.FireDepartmentVehicles = dto.FireDepartmentVehicles;
            this.FireDepartmentVehiclesCriticalTreshold = dto.FireDepartmentVehiclesCriticalTreshold;
            this.FireDepartmentVehiclesSortOrder = dto.FireDepartmentVehiclesSortOrder;
            this.CrimeRate = dto.CrimeRate;
            this.CrimeRateCriticalTreshold = dto.CrimeRateCriticalTreshold;
            this.CrimeRateSortOrder = dto.CrimeRateSortOrder;
            this.PoliceHoldingCells = dto.PoliceHoldingCells;
            this.PoliceHoldingCellsCriticalTreshold = dto.PoliceHoldingCellsCriticalTreshold;
            this.PoliceHoldingCellsSortOrder = dto.PoliceHoldingCellsSortOrder;
            this.PoliceVehicles = dto.PoliceVehicles;
            this.PoliceVehiclesCriticalTreshold = dto.PoliceVehiclesCriticalTreshold;
            this.PoliceVehiclesSortOrder = dto.PoliceVehiclesSortOrder;
            this.PrisonCells = dto.PrisonCells;
            this.PrisonCellsCriticalTreshold = dto.PrisonCellsCriticalTreshold;
            this.PrisonCellsSortOrder = dto.PrisonCellsSortOrder;
            this.PrisonVehicles = dto.PrisonVehicles;
            this.PrisonVehiclesCriticalTreshold = dto.PrisonVehiclesCriticalTreshold;
            this.PrisonVehiclesSortOrder = dto.PrisonVehiclesSortOrder;
            this.Unemployment = dto.Unemployment;
            this.UnemploymentCriticalTreshold = dto.UnemploymentCriticalTreshold;
            this.UnemploymentSortOrder = dto.UnemploymentSortOrder;
            this.TrafficJam = dto.TrafficJam;
            this.TrafficJamCriticalTreshold = dto.TrafficJamCriticalTreshold;
            this.TrafficJamSortOrder = dto.TrafficJamSortOrder;
            this.RoadMaintenanceVehicles = dto.RoadMaintenanceVehicles;
            this.RoadMaintenanceVehiclesCriticalTreshold = dto.RoadMaintenanceVehiclesCriticalTreshold;
            this.RoadMaintenanceVehiclesSortOrder = dto.RoadMaintenanceVehiclesSortOrder;
            this.SnowDump = dto.SnowDump;
            this.SnowDumpCriticalTreshold = dto.SnowDumpCriticalTreshold;
            this.SnowDumpSortOrder = dto.SnowDumpSortOrder;
            this.SnowDumpVehicles = dto.SnowDumpVehicles;
            this.SnowDumpVehiclesCriticalTreshold = dto.SnowDumpVehiclesCriticalTreshold;
            this.SnowDumpVehiclesSortOrder = dto.SnowDumpVehiclesSortOrder;
            this.ParkMaintenanceVehicles = dto.ParkMaintenanceVehicles;
            this.ParkMaintenanceVehiclesCriticalTreshold = dto.ParkMaintenanceVehiclesCriticalTreshold;
            this.ParkMaintenanceVehiclesSortOrder = dto.ParkMaintenanceVehiclesSortOrder;
            this.CityUnattractiveness = dto.CityUnattractiveness;
            this.CityUnattractivenessCriticalTreshold = dto.CityUnattractivenessCriticalTreshold;
            this.CityUnattractivenessSortOrder = dto.CityUnattractivenessSortOrder;
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
                ElectricityCriticalTreshold = this.ElectricityCriticalTreshold,
                ElectricitySortOrder = this.ElectricitySortOrder,
                Heating = this.Heating,
                HeatingCriticalTreshold = this.HeatingCriticalTreshold,
                HeatingSortOrder = this.HeatingSortOrder,
                Water = this.Water,
                WaterCriticalTreshold = this.WaterCriticalTreshold,
                WaterSortOrder = this.WaterSortOrder,
                SewageTreatment = this.SewageTreatment,
                SewageTreatmentCriticalTreshold = this.SewageTreatmentCriticalTreshold,
                SewageTreatmentSortOrder = this.SewageTreatmentSortOrder,
                WaterReserveTank = this.WaterReserveTank,
                WaterReserveTankCriticalTreshold = this.WaterReserveTankCriticalTreshold,
                WaterReserveTankSortOrder = this.WaterReserveTankSortOrder,
                WaterPumpingServiceStorage = this.WaterPumpingServiceStorage,
                WaterPumpingServiceStorageCriticalTreshold = this.WaterPumpingServiceStorageCriticalTreshold,
                WaterPumpingServiceStorageSortOrder = this.WaterPumpingServiceStorageSortOrder,
                WaterPumpingServiceVehicles = this.WaterPumpingServiceVehicles,
                WaterPumpingServiceVehiclesCriticalTreshold = this.WaterPumpingServiceVehiclesCriticalTreshold,
                WaterPumpingServiceVehiclesSortOrder = this.WaterPumpingServiceVehiclesSortOrder,
                Landfill = this.Landfill,
                LandfillCriticalTreshold = this.LandfillCriticalTreshold,
                LandfillSortOrder = this.LandfillSortOrder,
                LandfillVehicles = this.LandfillVehicles,
                LandfillVehiclesCriticalTreshold = this.LandfillVehiclesCriticalTreshold,
                LandfillVehiclesSortOrder = this.LandfillVehiclesSortOrder,
                GarbageProcessing = this.GarbageProcessing,
                GarbageProcessingCriticalTreshold = this.GarbageProcessingCriticalTreshold,
                GarbageProcessingSortOrder = this.GarbageProcessingSortOrder,
                GarbageProcessingVehicles = this.GarbageProcessingVehicles,
                GarbageProcessingVehiclesCriticalTreshold = this.GarbageProcessingVehiclesCriticalTreshold,
                GarbageProcessingVehiclesSortOrder = this.GarbageProcessingVehiclesSortOrder,
                ElementarySchool = this.ElementarySchool,
                ElementarySchoolCriticalTreshold = this.ElementarySchoolCriticalTreshold,
                ElementarySchoolSortOrder = this.ElementarySchoolSortOrder,
                HighSchool = this.HighSchool,
                HighSchoolCriticalTreshold = this.HighSchoolCriticalTreshold,
                HighSchoolSortOrder = this.HighSchoolSortOrder,
                University = this.University,
                UniversityCriticalTreshold = this.UniversityCriticalTreshold,
                UniversitySortOrder = this.UniversitySortOrder,
                Healthcare = this.Healthcare,
                HealthcareCriticalTreshold = this.HealthcareCriticalTreshold,
                HealthcareSortOrder = this.HealthcareSortOrder,
                HealthcareVehicles = this.HealthcareVehicles,
                HealthcareVehiclesCriticalTreshold = this.HealthcareVehiclesCriticalTreshold,
                HealthcareVehiclesSortOrder = this.HealthcareVehiclesSortOrder,
                AverageIllnessRate = this.AverageIllnessRate,
                AverageIllnessRateCriticalTreshold = this.AverageIllnessRateCriticalTreshold,
                AverageIllnessRateSortOrder = this.AverageIllnessRateSortOrder,
                Cemetery = this.Cemetery,
                CemeteryCriticalTreshold = this.CemeteryCriticalTreshold,
                CemeterySortOrder = this.CemeterySortOrder,
                CemeteryVehicles = this.CemeteryVehicles,
                CemeteryVehiclesCriticalTreshold = this.CemeteryVehiclesCriticalTreshold,
                CemeteryVehiclesSortOrder = this.CemeteryVehiclesSortOrder,
                Crematorium = this.Crematorium,
                CrematoriumCriticalTreshold = this.CrematoriumCriticalTreshold,
                CrematoriumSortOrder = this.CrematoriumSortOrder,
                CrematoriumVehicles = this.CrematoriumVehicles,
                CrematoriumVehiclesCriticalTreshold = this.CrematoriumVehiclesCriticalTreshold,
                CrematoriumVehiclesSortOrder = this.CrematoriumVehiclesSortOrder,
                GroundPollution = this.GroundPollution,
                GroundPollutionCriticalTreshold = this.GroundPollutionCriticalTreshold,
                GroundPollutionSortOrder = this.GroundPollutionSortOrder,
                DrinkingWaterPollution = this.DrinkingWaterPollution,
                DrinkingWaterPollutionCriticalTreshold = this.DrinkingWaterPollutionCriticalTreshold,
                DrinkingWaterPollutionSortOrder = this.DrinkingWaterPollutionSortOrder,
                NoisePollution = this.NoisePollution,
                NoisePollutionCriticalTreshold = this.NoisePollutionCriticalTreshold,
                NoisePollutionSortOrder = this.NoisePollutionSortOrder,
                FireHazard = this.FireHazard,
                FireHazardCriticalTreshold = this.FireHazardCriticalTreshold,
                FireHazardSortOrder = this.FireHazardSortOrder,
                FireDepartmentVehicles = this.FireDepartmentVehicles,
                FireDepartmentVehiclesCriticalTreshold = this.FireDepartmentVehiclesCriticalTreshold,
                FireDepartmentVehiclesSortOrder = this.FireDepartmentVehiclesSortOrder,
                CrimeRate = this.CrimeRate,
                CrimeRateCriticalTreshold = this.CrimeRateCriticalTreshold,
                CrimeRateSortOrder = this.CrimeRateSortOrder,
                PoliceHoldingCells = this.PoliceHoldingCells,
                PoliceHoldingCellsCriticalTreshold = this.PoliceHoldingCellsCriticalTreshold,
                PoliceHoldingCellsSortOrder = this.PoliceHoldingCellsSortOrder,
                PoliceVehicles = this.PoliceVehicles,
                PoliceVehiclesCriticalTreshold = this.PoliceVehiclesCriticalTreshold,
                PoliceVehiclesSortOrder = this.PoliceVehiclesSortOrder,
                PrisonCells = this.PrisonCells,
                PrisonCellsCriticalTreshold = this.PrisonCellsCriticalTreshold,
                PrisonCellsSortOrder = this.PrisonCellsSortOrder,
                PrisonVehicles = this.PrisonVehicles,
                PrisonVehiclesCriticalTreshold = this.PrisonVehiclesCriticalTreshold,
                PrisonVehiclesSortOrder = this.PrisonVehiclesSortOrder,
                Unemployment = this.Unemployment,
                UnemploymentCriticalTreshold = this.UnemploymentCriticalTreshold,
                UnemploymentSortOrder = this.UnemploymentSortOrder,
                TrafficJam = this.TrafficJam,
                TrafficJamCriticalTreshold = this.TrafficJamCriticalTreshold,
                TrafficJamSortOrder = this.TrafficJamSortOrder,
                RoadMaintenanceVehicles = this.RoadMaintenanceVehicles,
                RoadMaintenanceVehiclesCriticalTreshold = this.RoadMaintenanceVehiclesCriticalTreshold,
                RoadMaintenanceVehiclesSortOrder = this.RoadMaintenanceVehiclesSortOrder,
                SnowDump = this.SnowDump,
                SnowDumpCriticalTreshold = this.SnowDumpCriticalTreshold,
                SnowDumpSortOrder = this.SnowDumpSortOrder,
                SnowDumpVehicles = this.SnowDumpVehicles,
                SnowDumpVehiclesCriticalTreshold = this.SnowDumpVehiclesCriticalTreshold,
                SnowDumpVehiclesSortOrder = this.SnowDumpVehiclesSortOrder,
                ParkMaintenanceVehicles = this.ParkMaintenanceVehicles,
                ParkMaintenanceVehiclesCriticalTreshold = this.ParkMaintenanceVehiclesCriticalTreshold,
                ParkMaintenanceVehiclesSortOrder = this.ParkMaintenanceVehiclesSortOrder,
                CityUnattractiveness = this.CityUnattractiveness,
                CityUnattractivenessCriticalTreshold = this.CityUnattractivenessCriticalTreshold,
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
            this.dto = new ConfigurationDto();
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
