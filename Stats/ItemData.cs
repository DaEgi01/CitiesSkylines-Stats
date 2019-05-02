using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stats
{
    public class ItemData
    {
        private ItemData(
            string systemName,
            string icon,
            InfoManager.InfoMode infoMode,
            InfoManager.SubInfoMode subInfoMode
            )
        {
            this.SystemName = systemName;
            this.Icon = icon;
            this.InfoMode = infoMode;
            this.SubInfoMode = subInfoMode;
        }

        public string SystemName { get; }
        public string Icon { get; }
        public InfoManager.InfoMode InfoMode { get; }
        public InfoManager.SubInfoMode SubInfoMode { get; }

        public static readonly ItemData Electricity = new ItemData("Electricity", "InfoIconElectricity", InfoManager.InfoMode.Electricity, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Heating = new ItemData("Heating", "InfoIconHeating", InfoManager.InfoMode.Heating, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Water = new ItemData("Water", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData SewageTreatment = new ItemData("SewageTreatment", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData WaterReserveTank = new ItemData("WaterReserveTank", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData WaterPumpingServiceStorage = new ItemData("WaterPumpingServiceStorage", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData WaterPumpingServiceVehicles = new ItemData("WaterPumpingServiceVehicles", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Landfill = new ItemData("Landfill", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData LandfillVehicles = new ItemData("LandfillVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData GarbageProcessing = new ItemData("GarbageProcessing", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData GarbageProcessingVehicles = new ItemData("GarbageProcessingVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData ElementarySchool = new ItemData("ElementarySchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.ElementarySchool);
        public static readonly ItemData HighSchool = new ItemData("HighSchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.HighSchool);
        public static readonly ItemData University = new ItemData("University", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.University);
        public static readonly ItemData AverageIllnessRate = new ItemData("AverageIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData Healthcare = new ItemData("Healthcare", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData HealthcareVehicles = new ItemData("HealthcareVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData MedicalHelicopters = new ItemData("MedicalHelicopters", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData Cemetery = new ItemData("Cemetery", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData CemeteryVehicles = new ItemData("CemeteryVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData Crematorium = new ItemData("Crematorium", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData CrematoriumVehicles = new ItemData("CrematoriumVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData GroundPollution = new ItemData("GroundPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.Default);
        public static readonly ItemData DrinkingWaterPollution = new ItemData("DrinkingWaterPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.GroundWater);
        public static readonly ItemData NoisePollution = new ItemData("NoisePollution", "InfoIconNoisePollution", InfoManager.InfoMode.NoisePollution, InfoManager.SubInfoMode.Default);
        public static readonly ItemData FireHazard = new ItemData("FireHazard", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly ItemData FireDepartmentVehicles = new ItemData("FireDepartmentVehicles", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly ItemData FireHelicopters = new ItemData("FireHelicopters", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly ItemData CrimeRate = new ItemData("CrimeRate", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PoliceHoldingCells = new ItemData("PoliceHoldingCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PoliceVehicles = new ItemData("PoliceVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PoliceHelicopters = new ItemData("PoliceHelicopters", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PrisonCells = new ItemData("PrisonCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PrisonVehicles = new ItemData("PrisonVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Unemployment = new ItemData("Unemployment", "InfoIconPopulation", InfoManager.InfoMode.Density, InfoManager.SubInfoMode.Default);
        public static readonly ItemData TrafficJam = new ItemData("TrafficJam", "InfoIconTrafficCongestion", InfoManager.InfoMode.Traffic, InfoManager.SubInfoMode.Default);
        public static readonly ItemData RoadMaintenanceVehicles = new ItemData("RoadMaintenanceVehicles", "InfoIconMaintenance", InfoManager.InfoMode.Maintenance, InfoManager.SubInfoMode.Default);
        public static readonly ItemData ParkMaintenanceVehicles = new ItemData("ParkMaintenanceVehicles", "InfoIconParkMaintenance", InfoManager.InfoMode.ParkMaintenance, InfoManager.SubInfoMode.Default);
        public static readonly ItemData SnowDump = new ItemData("SnowDump", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default);
        public static readonly ItemData SnowDumpVehicles = new ItemData("SnowDumpVehicles", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default);
        public static readonly ItemData CityUnattractiveness = new ItemData("CityUnattractiveness", "InfoIconTourism", InfoManager.InfoMode.Tourism, InfoManager.SubInfoMode.Attractiveness);
        public static readonly ItemData Taxis = new ItemData("Taxis", "SubBarPublicTransportTaxi", InfoManager.InfoMode.Transport, InfoManager.SubInfoMode.NormalTransport);
        public static readonly ItemData PostVans = new ItemData("PostVans", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PostTrucks = new ItemData("PostTrucks", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default);
        public static readonly ItemData DisasterResponseVehicles = new ItemData("DisasterResponseVehicles", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default);
        public static readonly ItemData DisasterResponseHelicopters = new ItemData("DisasterResponseHelicopters", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default);

        public static ReadOnlyCollection<ItemData> AllItems => new List<ItemData>
        {
            Electricity,
            Heating,
            Water,
            SewageTreatment,
            WaterReserveTank,
            WaterPumpingServiceStorage,
            WaterPumpingServiceVehicles,
            Landfill,
            LandfillVehicles,
            GarbageProcessing,
            GarbageProcessingVehicles,
            ElementarySchool,
            HighSchool,
            University,
            AverageIllnessRate,
            Healthcare,
            HealthcareVehicles,
            MedicalHelicopters,
            Cemetery,
            CemeteryVehicles,
            Crematorium,
            CrematoriumVehicles,
            GroundPollution,
            DrinkingWaterPollution,
            NoisePollution,
            FireHazard,
            FireDepartmentVehicles,
            FireHelicopters,
            CrimeRate,
            PoliceHoldingCells,
            PoliceVehicles,
            PoliceHelicopters,
            PrisonCells,
            PrisonVehicles,
            Unemployment,
            TrafficJam,
            RoadMaintenanceVehicles,
            ParkMaintenanceVehicles,
            SnowDump,
            SnowDumpVehicles,
            CityUnattractiveness,
            Taxis,
            PostVans,
            PostTrucks,
            DisasterResponseVehicles,
            DisasterResponseHelicopters
        }.AsReadOnly();

        public static ItemData Parse(string systemName)
        {
            return AllItems.FirstOrDefault(x => x.SystemName == systemName);
        }
    }
}
