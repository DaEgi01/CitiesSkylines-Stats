using System;
using System.Collections.ObjectModel;

namespace Stats
{
    public class ItemData : IEquatable<ItemData>
    {
        /// <param name="index">Used as index in an array.</param>
        /// <param name="name">Must be a unique string.</param>
        private ItemData(
            int index,
            string name,
            string icon,
            InfoManager.InfoMode infoMode,
            InfoManager.SubInfoMode subInfoMode)
        {
            Index = index;
            Name = name;
            Icon = icon;
            InfoMode = infoMode;
            SubInfoMode = subInfoMode;
        }

        public int Index { get; }
        public string Name { get; }
        public string Icon { get; }
        public InfoManager.InfoMode InfoMode { get; }
        public InfoManager.SubInfoMode SubInfoMode { get; }

        static ItemData()
        {
            var allItems = new ItemData[]
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
                WaterReserveTank
            };

            ValidateIndexes(allItems);

            AllItems = new ReadOnlyCollection<ItemData>(allItems);
        }

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

        public static readonly ItemData AverageIllnessRate = new ItemData(0, "AverageIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData AverageChildrenIllnessRate = new ItemData(1, "AverageChildrenIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.ChildCare);
        public static readonly ItemData AverageElderlyIllnessRate = new ItemData(2, "AverageElderlyIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.ElderCare);
        public static readonly ItemData Cemetery = new ItemData(3, "Cemetery", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData CemeteryVehicles = new ItemData(4, "CemeteryVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData CityUnattractiveness = new ItemData(5, "CityUnattractiveness", "InfoIconTourism", InfoManager.InfoMode.Tourism, InfoManager.SubInfoMode.Attractiveness);
        public static readonly ItemData Crematorium = new ItemData(6, "Crematorium", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData CrematoriumVehicles = new ItemData(7, "CrematoriumVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly ItemData CrimeRate = new ItemData(8, "CrimeRate", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData DisasterResponseHelicopters = new ItemData(9, "DisasterResponseHelicopters", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default);
        public static readonly ItemData DisasterResponseVehicles = new ItemData(10, "DisasterResponseVehicles", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default);
        public static readonly ItemData DrinkingWaterPollution = new ItemData(11, "DrinkingWaterPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.GroundWater);
        public static readonly ItemData Electricity = new ItemData(12, "Electricity", "InfoIconElectricity", InfoManager.InfoMode.Electricity, InfoManager.SubInfoMode.Default);
        public static readonly ItemData ElementarySchool = new ItemData(13, "ElementarySchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.ElementarySchool);
        public static readonly ItemData FireDepartmentVehicles = new ItemData(14, "FireDepartmentVehicles", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly ItemData FireHazard = new ItemData(15, "FireHazard", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly ItemData FireHelicopters = new ItemData(16, "FireHelicopters", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly ItemData GarbageProcessing = new ItemData(17, "GarbageProcessing", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData GarbageProcessingVehicles = new ItemData(18, "GarbageProcessingVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData GroundPollution = new ItemData(19, "GroundPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Healthcare = new ItemData(20, "Healthcare", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData HealthcareVehicles = new ItemData(21, "HealthcareVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData Heating = new ItemData(22, "Heating", "InfoIconHeating", InfoManager.InfoMode.Heating, InfoManager.SubInfoMode.Default);
        public static readonly ItemData HighSchool = new ItemData(23, "HighSchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.HighSchool);
        public static readonly ItemData Landfill = new ItemData(24, "Landfill", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData LandfillVehicles = new ItemData(25, "LandfillVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Library = new ItemData(26, "Library", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.LibraryEducation);
        public static readonly ItemData MedicalHelicopters = new ItemData(27, "MedicalHelicopters", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly ItemData NoisePollution = new ItemData(28, "NoisePollution", "InfoIconNoisePollution", InfoManager.InfoMode.NoisePollution, InfoManager.SubInfoMode.Default);
        public static readonly ItemData ParkMaintenanceVehicles = new ItemData(29, "ParkMaintenanceVehicles", "InfoIconParkMaintenance", InfoManager.InfoMode.ParkMaintenance, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PoliceHelicopters = new ItemData(30, "PoliceHelicopters", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PoliceHoldingCells = new ItemData(31, "PoliceHoldingCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PoliceVehicles = new ItemData(32, "PoliceVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PostTrucks = new ItemData(33, "PostTrucks", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PostVans = new ItemData(34, "PostVans", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PrisonCells = new ItemData(35, "PrisonCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData PrisonVehicles = new ItemData(36, "PrisonVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly ItemData RoadMaintenanceVehicles = new ItemData(37, "RoadMaintenanceVehicles", "InfoIconMaintenance", InfoManager.InfoMode.Maintenance, InfoManager.SubInfoMode.Default);
        public static readonly ItemData SewageTreatment = new ItemData(38, "SewageTreatment", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData SnowDump = new ItemData(39, "SnowDump", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default);
        public static readonly ItemData SnowDumpVehicles = new ItemData(40, "SnowDumpVehicles", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Taxis = new ItemData(41, "Taxis", "SubBarPublicTransportTaxi", InfoManager.InfoMode.Transport, InfoManager.SubInfoMode.NormalTransport);
        public static readonly ItemData TrafficJam = new ItemData(42, "TrafficJam", "InfoIconTrafficCongestion", InfoManager.InfoMode.Traffic, InfoManager.SubInfoMode.Default);
        public static readonly ItemData Unemployment = new ItemData(43, "Unemployment", "InfoIconPopulation", InfoManager.InfoMode.Density, InfoManager.SubInfoMode.Default);
        public static readonly ItemData UnhappinessCommercial = new ItemData(44, "UnhappinessCommercial", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default);
        public static readonly ItemData UnhappinessIndustrial = new ItemData(45, "UnhappinessIndustrial", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default);
        public static readonly ItemData UnhappinessOffice = new ItemData(46, "UnhappinessOffice", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default);
        public static readonly ItemData UnhappinessResidential = new ItemData(47, "UnhappinessResidential", "InfoIconHappiness", InfoManager.InfoMode.Happiness, InfoManager.SubInfoMode.Default);
        public static readonly ItemData University = new ItemData(48, "University", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.University);
        public static readonly ItemData Water = new ItemData(49, "Water", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData WaterPumpingServiceStorage = new ItemData(50, "WaterPumpingServiceStorage", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData WaterPumpingServiceVehicles = new ItemData(51, "WaterPumpingServiceVehicles", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly ItemData WaterReserveTank = new ItemData(52, "WaterReserveTank", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);

        public static ReadOnlyCollection<ItemData> AllItems { get; }

        public override bool Equals(object obj)
        {
            return obj is ItemData item && Equals(item);
        }

        public bool Equals(ItemData other)
        {
            return Index == other.Index;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Index.GetHashCode();
        }

        public static bool operator ==(ItemData left, ItemData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ItemData left, ItemData right)
        {
            return !(left == right);
        }
    }
}
