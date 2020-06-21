using System.Xml.Serialization;

namespace Stats.Config
{
    [XmlRoot("Configuration")]
    public class ConfigurationDto
    {
        public float MainPanelPositionX { get; set; } = 0;
        public float MainPanelPositionY { get; set; } = 0;
        public int MainPanelUpdateEveryXSeconds { get; set; } = 3;
        public bool MainPanelAutoHide { get; set; } = false;
        public bool MainPanelHideItemsBelowThreshold { get; set; } = true;
        public bool MainPanelHideItemsNotAvailable { get; set; } = false;
        public int MainPanelColumnCount { get; set; } = 2;
        public string MainPanelBackgroundColor { get; set; } = "50, 50, 50, 255";
        public string MainPanelForegroundColor { get; set; } = "255, 255, 255, 255";
        public string MainPanelAccentColor { get; set; } = "230, 65, 65, 255";

        public float ItemWidth { get; set; } = 60f;
        public float ItemHeight { get; set; } = 20f;
        public float ItemPadding { get; set; } = 8f;
        public float ItemTextScale { get; set; } = 0.8f;

        public bool Electricity { get; set; } = true;
        public int ElectricityCriticalThreshold { get; set; } = 80;
        public int ElectricitySortOrder { get; set; } = 1000;

        public bool Heating { get; set; } = true;
        public int HeatingCriticalThreshold { get; set; } = 80;
        public int HeatingSortOrder { get; set; } = 2000;

        public bool Water { get; set; } = true;
        public int WaterCriticalThreshold { get; set; } = 80;
        public int WaterSortOrder { get; set; } = 3000;

        public bool SewageTreatment { get; set; } = true;
        public int SewageTreatmentCriticalThreshold { get; set; } = 80;
        public int SewageTreatmentSortOrder { get; set; } = 4000;

        public bool WaterReserveTank { get; set; } = true;
        public int WaterReserveTankCriticalThreshold { get; set; } = 20;
        public int WaterReserveTankSortOrder { get; set; } = 5000;

        public bool WaterPumpingServiceStorage { get; set; } = true;
        public int WaterPumpingServiceStorageCriticalThreshold { get; set; } = 80;
        public int WaterPumpingServiceStorageSortOrder { get; set; } = 6000;

        public bool WaterPumpingServiceVehicles { get; set; } = true;
        public int WaterPumpingServiceVehiclesCriticalThreshold { get; set; } = 80;
        public int WaterPumpingServiceVehiclesSortOrder { get; set; } = 7000;

        public bool Landfill { get; set; } = true;
        public int LandfillCriticalThreshold { get; set; } = 80;
        public int LandfillSortOrder { get; set; } = 8000;

        public bool LandfillVehicles { get; set; } = true;
        public int LandfillVehiclesCriticalThreshold { get; set; } = 80;
        public int LandfillVehiclesSortOrder { get; set; } = 9000;

        public bool GarbageProcessing { get; set; } = true;
        public int GarbageProcessingCriticalThreshold { get; set; } = 80;
        public int GarbageProcessingSortOrder { get; set; } = 10000;

        public bool GarbageProcessingVehicles { get; set; } = true;
        public int GarbageProcessingVehiclesCriticalThreshold { get; set; } = 80;
        public int GarbageProcessingVehiclesSortOrder { get; set; } = 11000;

        public bool ElementarySchool { get; set; } = true;
        public int ElementarySchoolCriticalThreshold { get; set; } = 80;
        public int ElementarySchoolSortOrder { get; set; } = 12000;

        public bool HighSchool { get; set; } = true;
        public int HighSchoolCriticalThreshold { get; set; } = 80;
        public int HighSchoolSortOrder { get; set; } = 13000;

        public bool University { get; set; } = true;
        public int UniversityCriticalThreshold { get; set; } = 80;
        public int UniversitySortOrder { get; set; } = 14000;

        public bool Library { get; set; } = true;
        public int LibraryCriticalThreshold { get; set; } = 80;
        public int LibrarySortOrder { get; set; } = 14500;

        public bool UnhappinessResidential { get; set; } = true;
        public int UnhappinessResidentialCriticalThreshold { get; set; } = 20;
        public int UnhappinessResidentialSortOrder { get; set; } = 14750;

        public bool UnhappinessCommercial { get; set; } = true;
        public int UnhappinessCommercialCriticalThreshold { get; set; } = 20;
        public int UnhappinessCommercialSortOrder { get; set; } = 14760;

        public bool UnhappinessOffice { get; set; } = true;
        public int UnhappinessOfficeCriticalThreshold { get; set; } = 20;
        public int UnhappinessOfficeSortOrder { get; set; } = 14770;

        public bool UnhappinessIndustrial { get; set; } = true;
        public int UnhappinessIndustrialCriticalThreshold { get; set; } = 20;
        public int UnhappinessIndustrialSortOrder { get; set; } = 14780;

        public bool Healthcare { get; set; } = true;
        public int HealthcareCriticalThreshold { get; set; } = 80;
        public int HealthcareSortOrder { get; set; } = 15000;

        public bool HealthcareVehicles { get; set; } = true;
        public int HealthcareVehiclesCriticalThreshold { get; set; } = 80;
        public int HealthcareVehiclesSortOrder { get; set; } = 16000;

        public bool MedicalHelicopters { get; set; } = true;
        public int MedicalHelicoptersCriticalThreshold { get; set; } = 80;
        public int MedicalHelicoptersSortOrder { get; set; } = 16500;

        public bool AverageIllnessRate { get; set; } = true;
        public int AverageIllnessRateCriticalThreshold { get; set; } = 20;
        public int AverageIllnessRateSortOrder { get; set; } = 17000;

        public bool Cemetery { get; set; } = true;
        public int CemeteryCriticalThreshold { get; set; } = 80;
        public int CemeterySortOrder { get; set; } = 18000;

        public bool CemeteryVehicles { get; set; } = true;
        public int CemeteryVehiclesCriticalThreshold { get; set; } = 80;
        public int CemeteryVehiclesSortOrder { get; set; } = 19000;

        public bool Crematorium { get; set; } = true;
        public int CrematoriumCriticalThreshold { get; set; } = 80;
        public int CrematoriumSortOrder { get; set; } = 20000;

        public bool CrematoriumVehicles { get; set; } = true;
        public int CrematoriumVehiclesCriticalThreshold { get; set; } = 80;
        public int CrematoriumVehiclesSortOrder { get; set; } = 21000;

        public bool GroundPollution { get; set; } = true;
        public int GroundPollutionCriticalThreshold { get; set; } = 20;
        public int GroundPollutionSortOrder { get; set; } = 22000;

        public bool DrinkingWaterPollution { get; set; } = true;
        public int DrinkingWaterPollutionCriticalThreshold { get; set; } = 20;
        public int DrinkingWaterPollutionSortOrder { get; set; } = 23000;

        public bool NoisePollution { get; set; } = true;
        public int NoisePollutionCriticalThreshold { get; set; } = 20;
        public int NoisePollutionSortOrder { get; set; } = 24000;

        public bool FireHazard { get; set; } = true;
        public int FireHazardCriticalThreshold { get; set; } = 20;
        public int FireHazardSortOrder { get; set; } = 25000;

        public bool FireDepartmentVehicles { get; set; } = true;
        public int FireDepartmentVehiclesCriticalThreshold { get; set; } = 80;
        public int FireDepartmentVehiclesSortOrder { get; set; } = 26000;

        public bool FireHelicopters { get; set; } = true;
        public int FireHelicoptersCriticalThreshold { get; set; } = 80;
        public int FireHelicoptersSortOrder { get; set; } = 26500;

        public bool CrimeRate { get; set; } = true;
        public int CrimeRateCriticalThreshold { get; set; } = 20;
        public int CrimeRateSortOrder { get; set; } = 27000;

        public bool PoliceHoldingCells { get; set; } = true;
        public int PoliceHoldingCellsCriticalThreshold { get; set; } = 80;
        public int PoliceHoldingCellsSortOrder { get; set; } = 28000;

        public bool PoliceVehicles { get; set; } = true;
        public int PoliceVehiclesCriticalThreshold { get; set; } = 80;
        public int PoliceVehiclesSortOrder { get; set; } = 29000;

        public bool PoliceHelicopters { get; set; } = true;
        public int PoliceHelicoptersCriticalThreshold { get; set; } = 80;
        public int PoliceHelicoptersSortOrder { get; set; } = 29500;

        public bool PrisonCells { get; set; } = true;
        public int PrisonCellsCriticalThreshold { get; set; } = 80;
        public int PrisonCellsSortOrder { get; set; } = 30000;

        public bool PrisonVehicles { get; set; } = true;
        public int PrisonVehiclesCriticalThreshold { get; set; } = 80;
        public int PrisonVehiclesSortOrder { get; set; } = 31000;

        public bool Unemployment { get; set; } = true;
        public int UnemploymentCriticalThreshold { get; set; } = 10;
        public int UnemploymentSortOrder { get; set; } = 32000;

        public bool TrafficJam { get; set; } = true;
        public int TrafficJamCriticalThreshold { get; set; } = 20;
        public int TrafficJamSortOrder { get; set; } = 33000;

        public bool RoadMaintenanceVehicles { get; set; } = true;
        public int RoadMaintenanceVehiclesCriticalThreshold { get; set; } = 80;
        public int RoadMaintenanceVehiclesSortOrder { get; set; } = 34000;

        public bool SnowDump { get; set; } = true;
        public int SnowDumpCriticalThreshold { get; set; } = 80;
        public int SnowDumpSortOrder { get; set; } = 35000;

        public bool SnowDumpVehicles { get; set; } = true;
        public int SnowDumpVehiclesCriticalThreshold { get; set; } = 80;
        public int SnowDumpVehiclesSortOrder { get; set; } = 36000;

        public bool ParkMaintenanceVehicles { get; set; } = true;
        public int ParkMaintenanceVehiclesCriticalThreshold { get; set; } = 80;
        public int ParkMaintenanceVehiclesSortOrder { get; set; } = 37000;

        public bool CityUnattractiveness { get; set; } = true;
        public int CityUnattractivenessCriticalThreshold { get; set; } = 20;
        public int CityUnattractivenessSortOrder { get; set; } = 38000;

        public bool Taxis { get; set; } = true;
        public int TaxisCriticalThreshold { get; set; } = 80;
        public int TaxisSortOrder { get; set; } = 39000;

        public bool PostVans { get; set; } = true;
        public int PostVansCriticalThreshold { get; set; } = 80;
        public int PostVansSortOrder { get; set; } = 40000;

        public bool PostTrucks { get; set; } = true;
        public int PostTrucksCriticalThreshold { get; set; } = 80;
        public int PostTrucksSortOrder { get; set; } = 41000;

        public bool DisasterResponseVehicles { get; set; } = true;
        public int DisasterResponseVehiclesCriticalThreshold { get; set; } = 80;
        public int DisasterResponseVehiclesSortOrder { get; set; } = 42000;

        public bool DisasterResponseHelicopters { get; set; } = true;
        public int DisasterResponseHelicoptersCriticalThreshold { get; set; } = 80;
        public int DisasterResponseHelicoptersSortOrder { get; set; } = 43000;
    }
}
