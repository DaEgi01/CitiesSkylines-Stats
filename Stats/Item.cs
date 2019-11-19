using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct Item : IEquatable<Item>
    {
        /// <param name="index">Used as index in an array.</param>
        /// <param name="name">Must be a unique string.</param>
        private Item(
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

        static Item()
        {
            var allItems = new Item[]
            {
                AverageIllnessRate,
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
                University,
                Water,
                WaterPumpingServiceStorage,
                WaterPumpingServiceVehicles,
                WaterReserveTank
            };

            ValidateIndexes(allItems);

            AllItems = new ReadOnlyCollection<Item>(allItems);
        }

        private static void ValidateIndexes(Item[] allItems)
        {
            for (int i = 0; i < allItems.Length; i++)
            {
                if (i != allItems[i].Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        public static readonly Item AverageIllnessRate = new Item(0, "AverageIllnessRate", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly Item Cemetery = new Item(1, "Cemetery", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly Item CemeteryVehicles = new Item(2, "CemeteryVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly Item CityUnattractiveness = new Item(3, "CityUnattractiveness", "InfoIconTourism", InfoManager.InfoMode.Tourism, InfoManager.SubInfoMode.Attractiveness);
        public static readonly Item Crematorium = new Item(4, "Crematorium", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly Item CrematoriumVehicles = new Item(5, "CrematoriumVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.DeathCare);
        public static readonly Item CrimeRate = new Item(6, "CrimeRate", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly Item DisasterResponseHelicopters = new Item(7, "DisasterResponseHelicopters", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default);
        public static readonly Item DisasterResponseVehicles = new Item(8, "DisasterResponseVehicles", "InfoIconDestruction", InfoManager.InfoMode.Destruction, InfoManager.SubInfoMode.Default);
        public static readonly Item DrinkingWaterPollution = new Item(9, "DrinkingWaterPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.GroundWater);
        public static readonly Item Electricity = new Item(10, "Electricity", "InfoIconElectricity", InfoManager.InfoMode.Electricity, InfoManager.SubInfoMode.Default);
        public static readonly Item ElementarySchool = new Item(11, "ElementarySchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.ElementarySchool);
        public static readonly Item FireDepartmentVehicles = new Item(12, "FireDepartmentVehicles", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly Item FireHazard = new Item(13, "FireHazard", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly Item FireHelicopters = new Item(14, "FireHelicopters", "InfoIconFireSafety", InfoManager.InfoMode.FireSafety, InfoManager.SubInfoMode.Default);
        public static readonly Item GarbageProcessing = new Item(15, "GarbageProcessing", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly Item GarbageProcessingVehicles = new Item(16, "GarbageProcessingVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly Item GroundPollution = new Item(17, "GroundPollution", "InfoIconPollution", InfoManager.InfoMode.Pollution, InfoManager.SubInfoMode.Default);
        public static readonly Item Healthcare = new Item(18, "Healthcare", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly Item HealthcareVehicles = new Item(19, "HealthcareVehicles", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly Item Heating = new Item(20, "Heating", "InfoIconHeating", InfoManager.InfoMode.Heating, InfoManager.SubInfoMode.Default);
        public static readonly Item HighSchool = new Item(21, "HighSchool", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.HighSchool);
        public static readonly Item Landfill = new Item(22, "Landfill", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly Item LandfillVehicles = new Item(23, "LandfillVehicles", "InfoIconGarbage", InfoManager.InfoMode.Garbage, InfoManager.SubInfoMode.Default);
        public static readonly Item MedicalHelicopters = new Item(24, "MedicalHelicopters", "InfoIconHealth", InfoManager.InfoMode.Health, InfoManager.SubInfoMode.HealthCare);
        public static readonly Item NoisePollution = new Item(25, "NoisePollution", "InfoIconNoisePollution", InfoManager.InfoMode.NoisePollution, InfoManager.SubInfoMode.Default);
        public static readonly Item ParkMaintenanceVehicles = new Item(26, "ParkMaintenanceVehicles", "InfoIconParkMaintenance", InfoManager.InfoMode.ParkMaintenance, InfoManager.SubInfoMode.Default);
        public static readonly Item PoliceHelicopters = new Item(27, "PoliceHelicopters", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly Item PoliceHoldingCells = new Item(28, "PoliceHoldingCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly Item PoliceVehicles = new Item(29, "PoliceVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly Item PostTrucks = new Item(30, "PostTrucks", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default);
        public static readonly Item PostVans = new Item(31, "PostVans", "InfoIconPost", InfoManager.InfoMode.Post, InfoManager.SubInfoMode.Default);
        public static readonly Item PrisonCells = new Item(32, "PrisonCells", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly Item PrisonVehicles = new Item(33, "PrisonVehicles", "InfoIconCrime", InfoManager.InfoMode.CrimeRate, InfoManager.SubInfoMode.Default);
        public static readonly Item RoadMaintenanceVehicles = new Item(34, "RoadMaintenanceVehicles", "InfoIconMaintenance", InfoManager.InfoMode.Maintenance, InfoManager.SubInfoMode.Default);
        public static readonly Item SewageTreatment = new Item(35, "SewageTreatment", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly Item SnowDump = new Item(36, "SnowDump", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default);
        public static readonly Item SnowDumpVehicles = new Item(37, "SnowDumpVehicles", "InfoIconSnow", InfoManager.InfoMode.Snow, InfoManager.SubInfoMode.Default);
        public static readonly Item Taxis = new Item(38, "Taxis", "SubBarPublicTransportTaxi", InfoManager.InfoMode.Transport, InfoManager.SubInfoMode.NormalTransport);
        public static readonly Item TrafficJam = new Item(39, "TrafficJam", "InfoIconTrafficCongestion", InfoManager.InfoMode.Traffic, InfoManager.SubInfoMode.Default);
        public static readonly Item Unemployment = new Item(40, "Unemployment", "InfoIconPopulation", InfoManager.InfoMode.Density, InfoManager.SubInfoMode.Default);
        public static readonly Item University = new Item(41, "University", "InfoIconEducation", InfoManager.InfoMode.Education, InfoManager.SubInfoMode.University);
        public static readonly Item Water = new Item(42, "Water", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly Item WaterPumpingServiceStorage = new Item(43, "WaterPumpingServiceStorage", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly Item WaterPumpingServiceVehicles = new Item(44, "WaterPumpingServiceVehicles", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);
        public static readonly Item WaterReserveTank = new Item(45, "WaterReserveTank", "InfoIconWater", InfoManager.InfoMode.Water, InfoManager.SubInfoMode.Default);

        public static ReadOnlyCollection<Item> AllItems { get; }

        public override bool Equals(object obj)
        {
            return obj is Item item && Equals(item);
        }

        public bool Equals(Item other)
        {
            return Index == other.Index;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Index.GetHashCode();
        }

        public static bool operator ==(Item left, Item right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Item left, Item right)
        {
            return !(left == right);
        }
    }
}
