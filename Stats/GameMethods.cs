﻿using ColossalFramework;
using System;
using UnityEngine;

namespace Stats
{
    public static class GameMethods
    {
        //MaintenanceDepotAI.GetTransferReason rewritten as a public static method (private instance method).
        public static TransferManager.TransferReason GetTransferReason(MaintenanceDepotAI maintenanceDepotAI)
        {
            switch (maintenanceDepotAI.m_info.m_class.m_service)
            {
                case ItemClass.Service.Road:
                    return TransferManager.TransferReason.RoadMaintenance;
                case ItemClass.Service.Beautification:
                    return TransferManager.TransferReason.ParkMaintenance;
                default:
                    return TransferManager.TransferReason.None;
            }
        }

        //CommonBuildingAI.CalculateOwnVehicles rewritten as a public static method (protected instance method).
        public static void CalculateOwnVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_ownVehicles;
            int num2 = 0;
            do
            {
                if (num == 0)
                {
                    return;
                }
                if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[num].m_transferType == material)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[num].Info;
                    info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int size, out int max);
                    cargo += Mathf.Min(size, max);
                    capacity += max;
                    count++;
                    if ((instance.m_vehicles.m_buffer[num].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != 0)
                    {
                        outside++;
                    }
                }
                num = instance.m_vehicles.m_buffer[num].m_nextOwnVehicle;
            }
            while (++num2 <= 16384);
            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
        }

        //PostOfficeAI.CalculateOwnVehicles rewritten as a public static method (private instance method).
        public static void CalculateVehicles(ushort buildingID, ref Building data, ref int unsortedMail, ref int sortedMail, ref int unsortedCapacity, ref int sortedCapacity, ref int ownVanCount, ref int ownTruckCount, ref int import, ref int export)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_ownVehicles;
            int num2 = 0;
            while (num != 0)
            {
                switch (instance.m_vehicles.m_buffer[num].m_transferType)
                {
                    case 92:
                        {
                            ownVanCount++;
                            VehicleInfo info = instance.m_vehicles.m_buffer[num].Info;
                            info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int size, out int max);
                            unsortedMail += Mathf.Min(size, max);
                            unsortedCapacity += Mathf.Min(size, max);
                            sortedMail += Mathf.Max(0, max - size);
                            sortedCapacity += Mathf.Max(0, max - size);
                            break;
                        }
                    case 93:
                        ownTruckCount++;
                        if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToTarget) != 0)
                        {
                            VehicleInfo info4 = instance.m_vehicles.m_buffer[num].Info;
                            info4.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int _, out int max4);
                            sortedCapacity += max4;
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Exporting) != 0)
                            {
                                export++;
                            }
                        }
                        else if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToSource) != 0)
                        {
                            VehicleInfo info5 = instance.m_vehicles.m_buffer[num].Info;
                            info5.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int size5, out int max5);
                            unsortedMail += Mathf.Min(size5, max5);
                            unsortedCapacity += max5;
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Importing) != 0)
                            {
                                import++;
                            }
                        }
                        break;
                    case 94:
                    case 96:
                        ownTruckCount++;
                        if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToTarget) != 0)
                        {
                            VehicleInfo info2 = instance.m_vehicles.m_buffer[num].Info;
                            info2.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int _, out int max2);
                            unsortedCapacity += max2;
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Exporting) != 0)
                            {
                                export++;
                            }
                        }
                        else if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToSource) != 0)
                        {
                            VehicleInfo info3 = instance.m_vehicles.m_buffer[num].Info;
                            info3.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int size3, out int max3);
                            sortedMail += Mathf.Min(size3, max3);
                            sortedCapacity += max3;
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Importing) != 0)
                            {
                                import++;
                            }
                        }
                        break;
                    case 95:
                        ownTruckCount++;
                        if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Exporting) != 0)
                        {
                            export++;
                        }
                        break;
                }
                num = instance.m_vehicles.m_buffer[num].m_nextOwnVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            num = data.m_guestVehicles;
            num2 = 0;
            do
            {
                if (num == 0)
                {
                    return;
                }
                switch (instance.m_vehicles.m_buffer[num].m_transferType)
                {
                    case 93:
                        if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToSource) != 0)
                        {
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Exporting) != 0)
                            {
                                export++;
                            }
                        }
                        else if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToTarget) != 0)
                        {
                            VehicleInfo info6 = instance.m_vehicles.m_buffer[num].Info;
                            info6.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int size6, out int max6);
                            unsortedMail += Mathf.Min(size6, max6);
                            unsortedCapacity += Mathf.Min(size6, max6);
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Importing) != 0)
                            {
                                import++;
                            }
                        }
                        break;
                    case 94:
                    case 96:
                        if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToSource) != 0)
                        {
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Exporting) != 0)
                            {
                                export++;
                            }
                        }
                        else if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.TransferToTarget) != 0)
                        {
                            VehicleInfo info7 = instance.m_vehicles.m_buffer[num].Info;
                            info7.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out int size7, out int max7);
                            sortedMail += Mathf.Min(size7, max7);
                            sortedCapacity += Mathf.Min(size7, max7);
                            if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Importing) != 0)
                            {
                                import++;
                            }
                        }
                        break;
                    case 95:
                        if ((instance.m_vehicles.m_buffer[num].m_flags & Vehicle.Flags.Exporting) != 0)
                        {
                            export++;
                        }
                        break;
                }
                num = instance.m_vehicles.m_buffer[num].m_nextGuestVehicle;
            }
            while (++num2 <= 16384);
            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
        }
    }
}
