using System.Xml.Serialization;

namespace Stats.Configuration
{
    [XmlRoot("Configuration")]
    public class ConfigurationDto
    {
        public float MainPanelPositionX { get; set; } = 0;
        public float MainPanelPositionY { get; set; } = 0;
        public int MainPanelUpdateEveryXSeconds { get; set; } = 3;
        public bool MainPanelAutoHide { get; set; } = false;
        public bool MainPanelHideItemsBelowTreshold { get; set; } = true;
        public int MainPanelColumnCount { get; set; } = 2;
        public string MainPanelBackgroundColor { get; set; } = "50, 50, 50, 255";
        public string MainPanelForegroundColor { get; set; } = "255, 255, 255, 255";
        public string MainPanelAccentColor { get; set; } = "230, 65, 65, 255";

        public float ItemWidth { get; set; } = 60f;
        public float ItemHeight { get; set; } = 20f;
        public float ItemPadding { get; set; } = 8f;
        public float ItemTextScale { get; set; } = 0.8f;

        public bool Electricity { get; set; } = true;
        public int ElectricityCriticalTreshold { get; set; } = 80;
        public int ElectricitySortOrder { get; set; } = 1000;

        public bool Heating { get; set; } = true;
        public int HeatingCriticalTreshold { get; set; } = 80;
        public int HeatingSortOrder { get; set; } = 2000;

        public bool Water { get; set; } = true;
        public int WaterCriticalTreshold { get; set; } = 80;
        public int WaterSortOrder { get; set; } = 3000;

        public bool SewageTreatment { get; set; } = true;
        public int SewageTreatmentCriticalTreshold { get; set; } = 80;
        public int SewageTreatmentSortOrder { get; set; } = 4000;

        public bool WaterReserveTank { get; set; } = true;
        public int WaterReserveTankCriticalTreshold { get; set; } = 80;
        public int WaterReserveTankSortOrder { get; set; } = 5000;

        public bool WaterPumpingServiceStorage { get; set; } = true;
        public int WaterPumpingServiceStorageCriticalTreshold { get; set; } = 80;
        public int WaterPumpingServiceStorageSortOrder { get; set; } = 6000;

        public bool WaterPumpingServiceVehicles { get; set; } = true;
        public int WaterPumpingServiceVehiclesCriticalTreshold { get; set; } = 80;
        public int WaterPumpingServiceVehiclesSortOrder { get; set; } = 7000;

        public bool Landfill { get; set; } = true;
        public int LandfillCriticalTreshold { get; set; } = 80;
        public int LandfillSortOrder { get; set; } = 8000;

        public bool LandfillVehicles { get; set; } = true;
        public int LandfillVehiclesCriticalTreshold { get; set; } = 80;
        public int LandfillVehiclesSortOrder { get; set; } = 9000;

        public bool GarbageProcessing { get; set; } = true;
        public int GarbageProcessingCriticalTreshold { get; set; } = 80;
        public int GarbageProcessingSortOrder { get; set; } = 10000;

        public bool GarbageProcessingVehicles { get; set; } = true;
        public int GarbageProcessingVehiclesCriticalTreshold { get; set; } = 80;
        public int GarbageProcessingVehiclesSortOrder { get; set; } = 11000;

        public bool ElementarySchool { get; set; } = true;
        public int ElementarySchoolCriticalTreshold { get; set; } = 80;
        public int ElementarySchoolSortOrder { get; set; } = 12000;

        public bool HighSchool { get; set; } = true;
        public int HighSchoolCriticalTreshold { get; set; } = 80;
        public int HighSchoolSortOrder { get; set; } = 13000;

        public bool University { get; set; } = true;
        public int UniversityCriticalTreshold { get; set; } = 80;
        public int UniversitySortOrder { get; set; } = 14000;

        public bool Healthcare { get; set; } = true;
        public int HealthcareCriticalTreshold { get; set; } = 80;
        public int HealthcareSortOrder { get; set; } = 15000;

        public bool HealthcareVehicles { get; set; } = true;
        public int HealthcareVehiclesCriticalTreshold { get; set; } = 80;
        public int HealthcareVehiclesSortOrder { get; set; } = 16000;

        public bool AverageIllnessRate { get; set; } = true;
        public int AverageIllnessRateCriticalTreshold { get; set; } = 20;
        public int AverageIllnessRateSortOrder { get; set; } = 17000;

        public bool Cemetery { get; set; } = true;
        public int CemeteryCriticalTreshold { get; set; } = 80;
        public int CemeterySortOrder { get; set; } = 18000;

        public bool CemeteryVehicles { get; set; } = true;
        public int CemeteryVehiclesCriticalTreshold { get; set; } = 80;
        public int CemeteryVehiclesSortOrder { get; set; } = 19000;

        public bool Crematorium { get; set; } = true;
        public int CrematoriumCriticalTreshold { get; set; } = 80;
        public int CrematoriumSortOrder { get; set; } = 20000;

        public bool CrematoriumVehicles { get; set; } = true;
        public int CrematoriumVehiclesCriticalTreshold { get; set; } = 80;
        public int CrematoriumVehiclesSortOrder { get; set; } = 21000;

        public bool GroundPollution { get; set; } = true;
        public int GroundPollutionCriticalTreshold { get; set; } = 20;
        public int GroundPollutionSortOrder { get; set; } = 22000;

        public bool DrinkingWaterPollution { get; set; } = true;
        public int DrinkingWaterPollutionCriticalTreshold { get; set; } = 20;
        public int DrinkingWaterPollutionSortOrder { get; set; } = 23000;

        public bool NoisePollution { get; set; } = true;
        public int NoisePollutionCriticalTreshold { get; set; } = 20;
        public int NoisePollutionSortOrder { get; set; } = 24000;

        public bool FireHazard { get; set; } = true;
        public int FireHazardCriticalTreshold { get; set; } = 20;
        public int FireHazardSortOrder { get; set; } = 25000;

        public bool FireDepartmentVehicles { get; set; } = true;
        public int FireDepartmentVehiclesCriticalTreshold { get; set; } = 80;
        public int FireDepartmentVehiclesSortOrder { get; set; } = 26000;

        public bool CrimeRate { get; set; } = true;
        public int CrimeRateCriticalTreshold { get; set; } = 20;
        public int CrimeRateSortOrder { get; set; } = 27000;

        public bool PoliceHoldingCells { get; set; } = true;
        public int PoliceHoldingCellsCriticalTreshold { get; set; } = 80;
        public int PoliceHoldingCellsSortOrder { get; set; } = 28000;

        public bool PoliceVehicles { get; set; } = true;
        public int PoliceVehiclesCriticalTreshold { get; set; } = 80;
        public int PoliceVehiclesSortOrder { get; set; } = 29000;

        public bool PrisonCells { get; set; } = true;
        public int PrisonCellsCriticalTreshold { get; set; } = 80;
        public int PrisonCellsSortOrder { get; set; } = 30000;

        public bool PrisonVehicles { get; set; } = true;
        public int PrisonVehiclesCriticalTreshold { get; set; } = 80;
        public int PrisonVehiclesSortOrder { get; set; } = 31000;

        public bool Unemployment { get; set; } = true;
        public int UnemploymentCriticalTreshold { get; set; } = 10;
        public int UnemploymentSortOrder { get; set; } = 32000;

        public bool TrafficJam { get; set; } = true;
        public int TrafficJamCriticalTreshold { get; set; } = 20;
        public int TrafficJamSortOrder { get; set; } = 33000;

        public bool RoadMaintenanceVehicles { get; set; } = true;
        public int RoadMaintenanceVehiclesCriticalTreshold { get; set; } = 80;
        public int RoadMaintenanceVehiclesSortOrder { get; set; } = 34000;

        public bool SnowDump { get; set; } = true;
        public int SnowDumpCriticalTreshold { get; set; } = 80;
        public int SnowDumpSortOrder { get; set; } = 35000;

        public bool SnowDumpVehicles { get; set; } = true;
        public int SnowDumpVehiclesCriticalTreshold { get; set; } = 80;
        public int SnowDumpVehiclesSortOrder { get; set; } = 36000;

        public bool ParkMaintenanceVehicles { get; set; } = true;
        public int ParkMaintenanceVehiclesCriticalTreshold { get; set; } = 80;
        public int ParkMaintenanceVehiclesSortOrder { get; set; } = 37000;

        public bool CityUnattractiveness { get; set; } = true;
        public int CityUnattractivenessCriticalTreshold { get; set; } = 20;
        public int CityUnattractivenessSortOrder { get; set; } = 38000;
    }
}
