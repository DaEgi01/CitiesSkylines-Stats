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
        }

        public ConfigurationDto Dto => dto;

        public bool LayoutDirty { get; set; }

        public float MainPanelPositionX
        {
            get => dto.MainPanelPositionX;
            set
            {
                dto.MainPanelPositionX = value;
            }
        }

        public float MainPanelPositionY
        {
            get => dto.MainPanelPositionY;
            set
            {
                dto.MainPanelPositionY = value;
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
                LayoutDirty = true;
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => dto.MainPanelHideItemsNotAvailable;
            set
            {
                dto.MainPanelHideItemsNotAvailable = value;
                LayoutDirty = true;
            }
        }

        public int MainPanelColumnCount
        {
            get => dto.MainPanelColumnCount;
            set
            {
                dto.MainPanelColumnCount = value;
                LayoutDirty = true;
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
                LayoutDirty = true;
            }
        }

        public float ItemHeight
        {
            get => dto.ItemHeight;
            set
            {
                dto.ItemHeight = value;
                LayoutDirty = true;
            }
        }

        public float ItemPadding
        {
            get => dto.ItemPadding;
            set
            {
                dto.ItemPadding = value;
                LayoutDirty = true;
            }
        }

        public float ItemTextScale
        {
            get => dto.ItemTextScale;
            set
            {
                dto.ItemTextScale = value;
                LayoutDirty = true;
            }
        }

        public bool Electricity
        {
            get => dto.Electricity;
            set
            {
                dto.Electricity = value;
                LayoutDirty = true;
            }
        }

        public int ElectricityCriticalThreshold
        {
            get => dto.ElectricityCriticalThreshold;
            set
            {
                dto.ElectricityCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int ElectricitySortOrder
        {
            get => dto.ElectricitySortOrder;
            set
            {
                dto.ElectricitySortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Heating
        {
            get => dto.Heating;
            set
            {
                dto.Heating = value;
                LayoutDirty = true;
            }
        }

        public int HeatingCriticalThreshold
        {
            get => dto.HeatingCriticalThreshold;
            set
            {
                dto.HeatingCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int HeatingSortOrder
        {
            get => dto.HeatingSortOrder;
            set
            {
                dto.HeatingSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Water
        {
            get => dto.Water;
            set
            {
                dto.Water = value;
                LayoutDirty = true;
            }
        }

        public int WaterCriticalThreshold
        {
            get => dto.WaterCriticalThreshold;
            set
            {
                dto.WaterCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int WaterSortOrder
        {
            get => dto.WaterSortOrder;
            set
            {
                dto.WaterSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool SewageTreatment
        {
            get => dto.SewageTreatment;
            set
            {
                dto.SewageTreatment = value;
                LayoutDirty = true;
            }
        }

        public int SewageTreatmentCriticalThreshold
        {
            get => dto.SewageTreatmentCriticalThreshold;
            set
            {
                dto.SewageTreatmentCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int SewageTreatmentSortOrder
        {
            get => dto.SewageTreatmentSortOrder;
            set
            {
                dto.SewageTreatmentSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool WaterReserveTank
        {
            get => dto.WaterReserveTank;
            set
            {
                dto.WaterReserveTank = value;
                LayoutDirty = true;
            }
        }

        public int WaterReserveTankCriticalThreshold
        {
            get => dto.WaterReserveTankCriticalThreshold;
            set
            {
                dto.WaterReserveTankCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int WaterReserveTankSortOrder
        {
            get => dto.WaterReserveTankSortOrder;
            set
            {
                dto.WaterReserveTankSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool WaterPumpingServiceStorage
        {
            get => dto.WaterPumpingServiceStorage;
            set
            {
                dto.WaterPumpingServiceStorage = value;
                LayoutDirty = true;
            }
        }

        public int WaterPumpingServiceStorageCriticalThreshold
        {
            get => dto.WaterPumpingServiceStorageCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceStorageCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int WaterPumpingServiceStorageSortOrder
        {
            get => dto.WaterPumpingServiceStorageSortOrder;
            set
            {
                dto.WaterPumpingServiceStorageSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool WaterPumpingServiceVehicles
        {
            get => dto.WaterPumpingServiceVehicles;
            set
            {
                dto.WaterPumpingServiceVehicles = value;
                LayoutDirty = true;
            }
        }

        public int WaterPumpingServiceVehiclesCriticalThreshold
        {
            get => dto.WaterPumpingServiceVehiclesCriticalThreshold;
            set
            {
                dto.WaterPumpingServiceVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int WaterPumpingServiceVehiclesSortOrder
        {
            get => dto.WaterPumpingServiceVehiclesSortOrder;
            set
            {
                dto.WaterPumpingServiceVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Landfill
        {
            get => dto.Landfill;
            set
            {
                dto.Landfill = value;
                LayoutDirty = true;
            }
        }

        public int LandfillCriticalThreshold
        {
            get => dto.LandfillCriticalThreshold;
            set
            {
                dto.LandfillCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int LandfillSortOrder
        {
            get => dto.LandfillSortOrder;
            set
            {
                dto.LandfillSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool LandfillVehicles
        {
            get => dto.LandfillVehicles;
            set
            {
                dto.LandfillVehicles = value;
                LayoutDirty = true;
            }
        }

        public int LandfillVehiclesCriticalThreshold
        {
            get => dto.LandfillVehiclesCriticalThreshold;
            set
            {
                dto.LandfillVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int LandfillVehiclesSortOrder
        {
            get => dto.LandfillVehiclesSortOrder;
            set
            {
                dto.LandfillVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool GarbageProcessing
        {
            get => dto.GarbageProcessing;
            set
            {
                dto.GarbageProcessing = value;
                LayoutDirty = true;
            }
        }

        public int GarbageProcessingCriticalThreshold
        {
            get => dto.GarbageProcessingCriticalThreshold;
            set
            {
                dto.GarbageProcessingCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int GarbageProcessingSortOrder
        {
            get => dto.GarbageProcessingSortOrder;
            set
            {
                dto.GarbageProcessingSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool GarbageProcessingVehicles
        {
            get => dto.GarbageProcessingVehicles;
            set
            {
                dto.GarbageProcessingVehicles = value;
                LayoutDirty = true;
            }
        }

        public int GarbageProcessingVehiclesCriticalThreshold
        {
            get => dto.GarbageProcessingVehiclesCriticalThreshold;
            set
            {
                dto.GarbageProcessingVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int GarbageProcessingVehiclesSortOrder
        {
            get => dto.GarbageProcessingVehiclesSortOrder;
            set
            {
                dto.GarbageProcessingVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool ElementarySchool
        {
            get => dto.ElementarySchool;
            set
            {
                dto.ElementarySchool = value;
                LayoutDirty = true;
            }
        }

        public int ElementarySchoolCriticalThreshold
        {
            get => dto.ElementarySchoolCriticalThreshold;
            set
            {
                dto.ElementarySchoolCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int ElementarySchoolSortOrder
        {
            get => dto.ElementarySchoolSortOrder;
            set
            {
                dto.ElementarySchoolSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool HighSchool
        {
            get => dto.HighSchool;
            set
            {
                dto.HighSchool = value;
                LayoutDirty = true;
            }
        }

        public int HighSchoolCriticalThreshold
        {
            get => dto.HighSchoolCriticalThreshold;
            set
            {
                dto.HighSchoolCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int HighSchoolSortOrder
        {
            get => dto.HighSchoolSortOrder;
            set
            {
                dto.HighSchoolSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool University
        {
            get => dto.University;
            set
            {
                dto.University = value;
                LayoutDirty = true;
            }
        }

        public int UniversityCriticalThreshold
        {
            get => dto.UniversityCriticalThreshold;
            set
            {
                dto.UniversityCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int UniversitySortOrder
        {
            get => dto.UniversitySortOrder;
            set
            {
                dto.UniversitySortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Healthcare
        {
            get => dto.Healthcare;
            set
            {
                dto.Healthcare = value;
                LayoutDirty = true;
            }
        }

        public int HealthcareCriticalThreshold
        {
            get => dto.HealthcareCriticalThreshold;
            set
            {
                dto.HealthcareCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int HealthcareSortOrder
        {
            get => dto.HealthcareSortOrder;
            set
            {
                dto.HealthcareSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool HealthcareVehicles
        {
            get => dto.HealthcareVehicles;
            set
            {
                dto.HealthcareVehicles = value;
                LayoutDirty = true;
            }
        }

        public int HealthcareVehiclesCriticalThreshold
        {
            get => dto.HealthcareVehiclesCriticalThreshold;
            set
            {
                dto.HealthcareVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int HealthcareVehiclesSortOrder
        {
            get => dto.HealthcareVehiclesSortOrder;
            set
            {
                dto.HealthcareVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool MedicalHelicopters
        {
            get => dto.MedicalHelicopters;
            set
            {
                dto.MedicalHelicopters = value;
                LayoutDirty = true;
            }
        }

        public int MedicalHelicoptersCriticalThreshold
        {
            get => dto.MedicalHelicoptersCriticalThreshold;
            set
            {
                dto.MedicalHelicoptersCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int MedicalHelicoptersSortOrder
        {
            get => dto.MedicalHelicoptersSortOrder;
            set
            {
                dto.MedicalHelicoptersSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool AverageIllnessRate
        {
            get => dto.AverageIllnessRate;
            set
            {
                dto.AverageIllnessRate = value;
                LayoutDirty = true;
            }
        }

        public int AverageIllnessRateCriticalThreshold
        {
            get => dto.AverageIllnessRateCriticalThreshold;
            set
            {
                dto.AverageIllnessRateCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int AverageIllnessRateSortOrder
        {
            get => dto.AverageIllnessRateSortOrder;
            set
            {
                dto.AverageIllnessRateSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Cemetery
        {
            get => dto.Cemetery;
            set
            {
                dto.Cemetery = value;
                LayoutDirty = true;
            }
        }

        public int CemeteryCriticalThreshold
        {
            get => dto.CemeteryCriticalThreshold;
            set
            {
                dto.CemeteryCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int CemeterySortOrder
        {
            get => dto.CemeterySortOrder;
            set
            {
                dto.CemeterySortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool CemeteryVehicles
        {
            get => dto.CemeteryVehicles;
            set
            {
                dto.CemeteryVehicles = value;
                LayoutDirty = true;
            }
        }

        public int CemeteryVehiclesCriticalThreshold
        {
            get => dto.CemeteryVehiclesCriticalThreshold;
            set
            {
                dto.CemeteryVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int CemeteryVehiclesSortOrder
        {
            get => dto.CemeteryVehiclesSortOrder;
            set
            {
                dto.CemeteryVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Crematorium
        {
            get => dto.Crematorium;
            set
            {
                dto.Crematorium = value;
                LayoutDirty = true;
            }
        }

        public int CrematoriumCriticalThreshold
        {
            get => dto.CrematoriumCriticalThreshold;
            set
            {
                dto.CrematoriumCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int CrematoriumSortOrder
        {
            get => dto.CrematoriumSortOrder;
            set
            {
                dto.CrematoriumSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool CrematoriumVehicles
        {
            get => dto.CrematoriumVehicles;
            set
            {
                dto.CrematoriumVehicles = value;
                LayoutDirty = true;
            }
        }

        public int CrematoriumVehiclesCriticalThreshold
        {
            get => dto.CrematoriumVehiclesCriticalThreshold;
            set
            {
                dto.CrematoriumVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int CrematoriumVehiclesSortOrder
        {
            get => dto.CrematoriumVehiclesSortOrder;
            set
            {
                dto.CrematoriumVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool GroundPollution
        {
            get => dto.GroundPollution;
            set
            {
                dto.GroundPollution = value;
                LayoutDirty = true;
            }
        }

        public int GroundPollutionCriticalThreshold
        {
            get => dto.GroundPollutionCriticalThreshold;
            set
            {
                dto.GroundPollutionCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int GroundPollutionSortOrder
        {
            get => dto.GroundPollutionSortOrder;
            set
            {
                dto.GroundPollutionSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool DrinkingWaterPollution
        {
            get => dto.DrinkingWaterPollution;
            set
            {
                dto.DrinkingWaterPollution = value;
                LayoutDirty = true;
            }
        }

        public int DrinkingWaterPollutionCriticalThreshold
        {
            get => dto.DrinkingWaterPollutionCriticalThreshold;
            set
            {
                dto.DrinkingWaterPollutionCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int DrinkingWaterPollutionSortOrder
        {
            get => dto.DrinkingWaterPollutionSortOrder;
            set
            {
                dto.DrinkingWaterPollutionSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool NoisePollution
        {
            get => dto.NoisePollution;
            set
            {
                dto.NoisePollution = value;
                LayoutDirty = true;
            }
        }

        public int NoisePollutionCriticalThreshold
        {
            get => dto.NoisePollutionCriticalThreshold;
            set
            {
                dto.NoisePollutionCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int NoisePollutionSortOrder
        {
            get => dto.NoisePollutionSortOrder;
            set
            {
                dto.NoisePollutionSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool FireHazard
        {
            get => dto.FireHazard;
            set
            {
                dto.FireHazard = value;
                LayoutDirty = true;
            }
        }

        public int FireHazardCriticalThreshold
        {
            get => dto.FireHazardCriticalThreshold;
            set
            {
                dto.FireHazardCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int FireHazardSortOrder
        {
            get => dto.FireHazardSortOrder;
            set
            {
                dto.FireHazardSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool FireDepartmentVehicles
        {
            get => dto.FireDepartmentVehicles;
            set
            {
                dto.FireDepartmentVehicles = value;
                LayoutDirty = true;
            }
        }

        public int FireDepartmentVehiclesCriticalThreshold
        {
            get => dto.FireDepartmentVehiclesCriticalThreshold;
            set
            {
                dto.FireDepartmentVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int FireDepartmentVehiclesSortOrder
        {
            get => dto.FireDepartmentVehiclesSortOrder;
            set
            {
                dto.FireDepartmentVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool FireHelicopters
        {
            get => dto.FireHelicopters;
            set
            {
                dto.FireHelicopters = value;
                LayoutDirty = true;
            }
        }

        public int FireHelicoptersCriticalThreshold
        {
            get => dto.FireHelicoptersCriticalThreshold;
            set
            {
                dto.FireHelicoptersCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int FireHelicoptersSortOrder
        {
            get => dto.FireHelicoptersSortOrder;
            set
            {
                dto.FireHelicoptersSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool CrimeRate
        {
            get => dto.CrimeRate;
            set
            {
                dto.CrimeRate = value;
                LayoutDirty = true;
            }
        }

        public int CrimeRateCriticalThreshold
        {
            get => dto.CrimeRateCriticalThreshold;
            set
            {
                dto.CrimeRateCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int CrimeRateSortOrder
        {
            get => dto.CrimeRateSortOrder;
            set
            {
                dto.CrimeRateSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool PoliceHoldingCells
        {
            get => dto.PoliceHoldingCells;
            set
            {
                dto.PoliceHoldingCells = value;
                LayoutDirty = true;
            }
        }

        public int PoliceHoldingCellsCriticalThreshold
        {
            get => dto.PoliceHoldingCellsCriticalThreshold;
            set
            {
                dto.PoliceHoldingCellsCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int PoliceHoldingCellsSortOrder
        {
            get => dto.PoliceHoldingCellsSortOrder;
            set
            {
                dto.PoliceHoldingCellsSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool PoliceVehicles
        {
            get => dto.PoliceVehicles;
            set
            {
                dto.PoliceVehicles = value;
                LayoutDirty = true;
            }
        }

        public int PoliceVehiclesCriticalThreshold
        {
            get => dto.PoliceVehiclesCriticalThreshold;
            set
            {
                dto.PoliceVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int PoliceVehiclesSortOrder
        {
            get => dto.PoliceVehiclesSortOrder;
            set
            {
                dto.PoliceVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool PoliceHelicopters
        {
            get => dto.PoliceHelicopters;
            set
            {
                dto.PoliceHelicopters = value;
                LayoutDirty = true;
            }
        }

        public int PoliceHelicoptersCriticalThreshold
        {
            get => dto.PoliceHelicoptersCriticalThreshold;
            set
            {
                dto.PoliceHelicoptersCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int PoliceHelicoptersSortOrder
        {
            get => dto.PoliceHelicoptersSortOrder;
            set
            {
                dto.PoliceHelicoptersSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool PrisonCells
        {
            get => dto.PrisonCells;
            set
            {
                dto.PrisonCells = value;
                LayoutDirty = true;
            }
        }

        public int PrisonCellsCriticalThreshold
        {
            get => dto.PrisonCellsCriticalThreshold;
            set
            {
                dto.PrisonCellsCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int PrisonCellsSortOrder
        {
            get => dto.PrisonCellsSortOrder;
            set
            {
                dto.PrisonCellsSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool PrisonVehicles
        {
            get => dto.PrisonVehicles;
            set
            {
                dto.PrisonVehicles = value;
                LayoutDirty = true;
            }
        }

        public int PrisonVehiclesCriticalThreshold
        {
            get => dto.PrisonVehiclesCriticalThreshold;
            set
            {
                dto.PrisonVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int PrisonVehiclesSortOrder
        {
            get => dto.PrisonVehiclesSortOrder;
            set
            {
                dto.PrisonVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Unemployment
        {
            get => dto.Unemployment;
            set
            {
                dto.Unemployment = value;
                LayoutDirty = true;
            }
        }

        public int UnemploymentCriticalThreshold
        {
            get => dto.UnemploymentCriticalThreshold;
            set
            {
                dto.UnemploymentCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int UnemploymentSortOrder
        {
            get => dto.UnemploymentSortOrder;
            set
            {
                dto.UnemploymentSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool TrafficJam
        {
            get => dto.TrafficJam;
            set
            {
                dto.TrafficJam = value;
                LayoutDirty = true;
            }
        }

        public int TrafficJamCriticalThreshold
        {
            get => dto.TrafficJamCriticalThreshold;
            set
            {
                dto.TrafficJamCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int TrafficJamSortOrder
        {
            get => dto.TrafficJamSortOrder;
            set
            {
                dto.TrafficJamSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool RoadMaintenanceVehicles
        {
            get => dto.RoadMaintenanceVehicles;
            set
            {
                dto.RoadMaintenanceVehicles = value;
                LayoutDirty = true;
            }
        }

        public int RoadMaintenanceVehiclesCriticalThreshold
        {
            get => dto.RoadMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.RoadMaintenanceVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int RoadMaintenanceVehiclesSortOrder
        {
            get => dto.RoadMaintenanceVehiclesSortOrder;
            set
            {
                dto.RoadMaintenanceVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool SnowDump
        {
            get => dto.SnowDump;
            set
            {
                dto.SnowDump = value;
                LayoutDirty = true;
            }
        }

        public int SnowDumpCriticalThreshold
        {
            get => dto.SnowDumpCriticalThreshold;
            set
            {
                dto.SnowDumpCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int SnowDumpSortOrder
        {
            get => dto.SnowDumpSortOrder;
            set
            {
                dto.SnowDumpSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool SnowDumpVehicles
        {
            get => dto.SnowDumpVehicles;
            set
            {
                dto.SnowDumpVehicles = value;
                LayoutDirty = true;
            }
        }

        public int SnowDumpVehiclesCriticalThreshold
        {
            get => dto.SnowDumpVehiclesCriticalThreshold;
            set
            {
                dto.SnowDumpVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int SnowDumpVehiclesSortOrder
        {
            get => dto.SnowDumpVehiclesSortOrder;
            set
            {
                dto.SnowDumpVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool ParkMaintenanceVehicles
        {
            get => dto.ParkMaintenanceVehicles;
            set
            {
                dto.ParkMaintenanceVehicles = value;
                LayoutDirty = true;
            }
        }

        public int ParkMaintenanceVehiclesCriticalThreshold
        {
            get => dto.ParkMaintenanceVehiclesCriticalThreshold;
            set
            {
                dto.ParkMaintenanceVehiclesCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int ParkMaintenanceVehiclesSortOrder
        {
            get => dto.ParkMaintenanceVehiclesSortOrder;
            set
            {
                dto.ParkMaintenanceVehiclesSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool CityUnattractiveness
        {
            get => dto.CityUnattractiveness;
            set
            {
                dto.CityUnattractiveness = value;
                LayoutDirty = true;
            }
        }

        public int CityUnattractivenessCriticalThreshold
        {
            get => dto.CityUnattractivenessCriticalThreshold;
            set
            {
                dto.CityUnattractivenessCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int CityUnattractivenessSortOrder
        {
            get => dto.CityUnattractivenessSortOrder;
            set
            {
                dto.CityUnattractivenessSortOrder = value;
                LayoutDirty = true;
            }
        }

        public bool Taxis
        {
            get => dto.Taxis;
            set
            {
                dto.Taxis = value;
                LayoutDirty = true;
            }
        }

        public int TaxisCriticalThreshold
        {
            get => dto.TaxisCriticalThreshold;
            set
            {
                dto.TaxisCriticalThreshold = value;
                LayoutDirty = true;
            }
        }

        public int TaxisSortOrder
        {
            get => dto.TaxisSortOrder;
            set
            {
                dto.TaxisSortOrder = value;
                LayoutDirty = true;
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
            LayoutDirty = true;
        }
    }
}
