using System;
using System.Collections.ObjectModel;

namespace Stats;

public sealed class ItemData
{
    public static readonly ItemData AverageIllnessRate = new(0, "AverageIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare, srv => srv.GetAverageIllnessRate());
    public static readonly ItemData AverageChildrenIllnessRate = new(1, "AverageChildrenIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.ChildCare, srv => srv.GetAverageChildrenIllnessRate());
    public static readonly ItemData AverageElderlyIllnessRate = new(2, "AverageElderlyIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.ElderCare, srv => srv.GetAverageElderlyIllnessRate());
    public static readonly ItemData Cemetery = new(3, "Cemetery", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare, srv => srv.GetCemeteryPercent());
    public static readonly ItemData CemeteryVehicles = new(4, "CemeteryVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare, srv => srv.GetCemeteryVehiclesPercent());
    public static readonly ItemData CityUnattractiveness = new(5, "CityUnattractiveness", "InfoIconTourism", InfoManager.InfoMode.Tourism, InfoManager.SubInfoMode.Attractiveness, srv => srv.GetCityUnattractivenessPercent());
    public static readonly ItemData Crematorium = new(6, "Crematorium", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare, srv => srv.GetCrematoriumPercent());
    public static readonly ItemData CrematoriumVehicles = new(7, "CrematoriumVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare, srv => srv.GetCrematoriumVehiclesPercent());
    public static readonly ItemData CrimeRate = new(8, "CrimeRate", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default, srv => srv.GetCrimeRatePercent());
    public static readonly ItemData DisasterResponseHelicopters = new(9, "DisasterResponseHelicopters", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default, srv => srv.GetDisasterResponseHelicoptersPercent());
    public static readonly ItemData DisasterResponseVehicles = new(10, "DisasterResponseVehicles", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default, srv => srv.GetDisasterResponseVehiclesPercent());
    public static readonly ItemData DrinkingWaterPollution = new(11, "DrinkingWaterPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.GroundWater, srv => srv.GetDrinkingWaterPollutionPercent());
    public static readonly ItemData Electricity = new(12, "Electricity", "InfoIconElectricity", InfoManager.InfoMode.Electricity, InfoManager.SubInfoMode.Default, srv => srv.GetElectricityPercent());
    public static readonly ItemData ElementarySchool = new(13, "ElementarySchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.ElementarySchool, srv => srv.GetElementarySchoolPercent());
    public static readonly ItemData FireDepartmentVehicles = new(14, "FireDepartmentVehicles", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default, srv => srv.GetFireDepartmentVehiclesPercent());
    public static readonly ItemData FireHazard = new(15, "FireHazard", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default, srv => srv.GetFireHazardPercent());
    public static readonly ItemData FireHelicopters = new(16, "FireHelicopters", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default, srv => srv.GetFireHelicoptersPercent());
    public static readonly ItemData GarbageProcessing = new(17, "GarbageProcessing", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default, srv => srv.GetGarbageProcessingPercent());
    public static readonly ItemData GarbageProcessingVehicles = new(18, "GarbageProcessingVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default, srv => srv.GetGarbageProcessingVehiclesPercent());
    public static readonly ItemData GroundPollution = new(19, "GroundPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.Default, srv => srv.GetGroundPollutionPercent());
    public static readonly ItemData Healthcare = new(20, "Healthcare", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare, srv => srv.GetHealthCarePercent());
    public static readonly ItemData HealthcareVehicles = new(21, "HealthcareVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare, srv => srv.GetHealthCareVehiclesPercent());
    public static readonly ItemData Heating = new(22, "Heating", "InfoIconHeating", InfoManager.InfoMode.Heating, InfoManager.SubInfoMode.Default, srv => srv.GetHeatingPercent());
    public static readonly ItemData HighSchool = new(23, "HighSchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.HighSchool, srv => srv.GetHighSchoolPercent());
    public static readonly ItemData Landfill = new(24, "Landfill", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default, srv => srv.GetLandfillPercent());
    public static readonly ItemData LandfillVehicles = new(25, "LandfillVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default, srv => srv.GetLandfillVehiclesPercent());
    public static readonly ItemData Library = new(26, "Library", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.LibraryEducation, srv => srv.GetLibraryPercent());
    public static readonly ItemData MedicalHelicopters = new(27, "MedicalHelicopters", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare, srv => srv.GetMedicalHelicoptersPercent());
    public static readonly ItemData NoisePollution = new(28, "NoisePollution", "InfoIconNoisePollution", InfoManager.InfoMode.NoisePollution, InfoManager.SubInfoMode.Default, srv => srv.GetNoisePollutionPercent());
    public static readonly ItemData ParkMaintenanceVehicles = new(29, "ParkMaintenanceVehicles", "InfoIconParkMaintenance", InfoManager.InfoMode.ParkMaintenance, InfoManager.SubInfoMode.Default, srv => srv.GetParkMaintenanceVehiclesPercent());
    public static readonly ItemData PoliceHelicopters = new(30, "PoliceHelicopters", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default, srv => srv.GetPoliceHelicoptersPercent());
    public static readonly ItemData PoliceHoldingCells = new(31, "PoliceHoldingCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default, srv => srv.GetPoliceHoldingCellsPercent());
    public static readonly ItemData PoliceVehicles = new(32, "PoliceVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default, srv => srv.GetPoliceVehiclesPercent());
    public static readonly ItemData PostTrucks = new(33, "PostTrucks", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default, srv => srv.GetPostTrucksPercent());
    public static readonly ItemData PostVans = new(34, "PostVans", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default, srv => srv.GetPostVansPercent());
    public static readonly ItemData PrisonCells = new(35, "PrisonCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default, srv => srv.GetPrisonCellsPercent());
    public static readonly ItemData PrisonVehicles = new(36, "PrisonVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default, srv => srv.GetPrisonVehiclesPercent());
    public static readonly ItemData RoadMaintenanceVehicles = new(37, "RoadMaintenanceVehicles", "InfoIconMaintenance", InfoManager.InfoMode.Maintenance, InfoManager.SubInfoMode.Default, srv => srv.GetRoadMaintenanceVehiclesPercent());
    public static readonly ItemData SewageTreatment = new(38, "SewageTreatment", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default, srv => srv.GetSewageTreatmentPercent());
    public static readonly ItemData SnowDump = new(39, "SnowDump", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default, srv => srv.GetSnowDumpPercent());
    public static readonly ItemData SnowDumpVehicles = new(40, "SnowDumpVehicles", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default, srv => srv.GetSnowDumpVehiclesPercent());
    public static readonly ItemData Taxis = new(41, "Taxis", "SubBarPublicTransportTaxi", InfoManager.InfoMode.Transport, InfoManager.SubInfoMode.NormalTransport, srv => srv.GetTaxisPercent());
    public static readonly ItemData TrafficJam = new(42, "TrafficJam", "InfoIconTrafficCongestion", InfoManager.InfoMode.Traffic, InfoManager.SubInfoMode.Default, srv => srv.GetTrafficJamPercent());
    public static readonly ItemData Unemployment = new(43, "Unemployment", "InfoIconPopulation", InfoManager.InfoMode.Density, InfoManager.SubInfoMode.Default, srv => srv.GetUnemploymentPercent());
    public static readonly ItemData UnhappinessCommercial = new(44, "UnhappinessCommercial", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default, srv => srv.GetUnhappinessCommercialPercent());
    public static readonly ItemData UnhappinessIndustrial = new(45, "UnhappinessIndustrial", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default, srv => srv.GetUnhappinessIndustrialPercent());
    public static readonly ItemData UnhappinessOffice = new(46, "UnhappinessOffice", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default, srv => srv.GetUnhappinessOfficePercent());
    public static readonly ItemData UnhappinessResidential = new(47, "UnhappinessResidential", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default, srv => srv.GetUnhappinessResidentialPercent());
    public static readonly ItemData University = new(48, "University", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.University, srv => srv.GetUniversityPercent());
    public static readonly ItemData Water = new(49, "Water", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default, srv => srv.GetWaterPercent());
    public static readonly ItemData WaterPumpingServiceStorage = new(50, "WaterPumpingServiceStorage", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default, srv => srv.GetWaterPumpingServiceStoragePercent());
    public static readonly ItemData WaterPumpingServiceVehicles = new(51, "WaterPumpingServiceVehicles", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default, srv => srv.GetWaterPumpingServiceVehiclesPercent());
    public static readonly ItemData WaterReserveTank = new(52, "WaterReserveTank", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default, srv => srv.GetWaterReserveTanksPercent());

    static ItemData()
    {
        var allItems = new[]
        {
            AverageIllnessRate,
            AverageChildrenIllnessRate,
            AverageElderlyIllnessRate,
            Cemetery,
            CemeteryVehicles,
            CityUnattractiveness,
            Crematorium,
            CrematoriumVehicles,
            CrimeRate,
            DisasterResponseHelicopters,
            DisasterResponseVehicles,
            DrinkingWaterPollution,
            Electricity,
            ElementarySchool,
            FireDepartmentVehicles,
            FireHazard,
            FireHelicopters,
            GarbageProcessing,
            GarbageProcessingVehicles,
            GroundPollution,
            Healthcare,
            HealthcareVehicles,
            Heating,
            HighSchool,
            Landfill,
            LandfillVehicles,
            Library,
            MedicalHelicopters,
            NoisePollution,
            ParkMaintenanceVehicles,
            PoliceHelicopters,
            PoliceHoldingCells,
            PoliceVehicles,
            PostTrucks,
            PostVans,
            PrisonCells,
            PrisonVehicles,
            RoadMaintenanceVehicles,
            SewageTreatment,
            SnowDump,
            SnowDumpVehicles,
            Taxis,
            TrafficJam,
            Unemployment,
            UnhappinessCommercial,
            UnhappinessIndustrial,
            UnhappinessOffice,
            UnhappinessResidential,
            University,
            Water,
            WaterPumpingServiceStorage,
            WaterPumpingServiceVehicles,
            WaterReserveTank,
        };

        ValidateIndexes(allItems);

        AllItems = new ReadOnlyCollection<ItemData>(allItems);
    }

    /// <param name="index">Used as index in an array.</param>
    /// <param name="name">Must be a unique string.</param>
    /// <param name="icon">Sprite name of the icon.</param>
    /// <param name="infoMode">InfoMode of the Item.</param>
    /// <param name="subInfoMode">SubInfoMode of the Item.</param>
    /// <param name="getPercentFunc">Function to get percent values.</param>
    private ItemData(
        int index,
        string name,
        string icon,
        InfoManager.InfoMode infoMode,
        InfoManager.SubInfoMode subInfoMode,
        Func<GameEngineService, int?> getPercentFunc)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        if (icon is null)
            throw new ArgumentNullException(nameof(icon));
        if (getPercentFunc is null)
            throw new ArgumentNullException(nameof(getPercentFunc));

        Index = index;
        Name = name;
        Icon = icon;
        InfoMode = infoMode;
        SubInfoMode = subInfoMode;
        GetPercentFunc = getPercentFunc;
    }

    public static ReadOnlyCollection<ItemData> AllItems { get; }

    public int Index { get; }
    public string Name { get; }
    public string Icon { get; }
    public InfoManager.InfoMode InfoMode { get; }
    public InfoManager.SubInfoMode SubInfoMode { get; }
    public Func<GameEngineService, int?> GetPercentFunc { get; }

    private static void ValidateIndexes(ItemData[] allItems)
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if (i != allItems[i].Index)
            {
                throw new IndexesMessedUpException(i);
            }
        }
    }
}
