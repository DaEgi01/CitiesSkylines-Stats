using ColossalFramework;
using System;
using UnityEngine;

namespace Stats
{
    public static class GameMethods
    {
        //MaintenanceDepotAI.GetTransferReason rewritten as a static method (private instance method).
        public static TransferManager.TransferReason GetTransferReason(MaintenanceDepotAI maintenanceDepotAI)
        {
            ItemClass.Service service = maintenanceDepotAI.m_info.m_class.m_service;
            if (service == ItemClass.Service.Road)
            {
                return TransferManager.TransferReason.RoadMaintenance;
            }
            if (service != ItemClass.Service.Beautification)
            {
                return TransferManager.TransferReason.None;
            }
            return TransferManager.TransferReason.ParkMaintenance;
        }

        //CommonBuildingAI.CalculateOwnVehicles rewritten as a static method (protected instance method).
        public static void CalculateOwnVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            int num;
            int num1;
            VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
            ushort mOwnVehicles = data.m_ownVehicles;
            int num2 = 0;
            while (mOwnVehicles != 0)
            {
                if (vehicleManager.m_vehicles.m_buffer[mOwnVehicles].m_transferType == (byte)material)
                {
                    VehicleInfo info = vehicleManager.m_vehicles.m_buffer[mOwnVehicles].Info;
                    info.m_vehicleAI.GetSize(mOwnVehicles, ref vehicleManager.m_vehicles.m_buffer[mOwnVehicles], out num, out num1);
                    cargo += Mathf.Min(num, num1);
                    capacity += num1;
                    count++;
                    if ((int)(vehicleManager.m_vehicles.m_buffer[mOwnVehicles].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != 0)
                    {
                        outside++;
                    }
                }
                mOwnVehicles = vehicleManager.m_vehicles.m_buffer[mOwnVehicles].m_nextOwnVehicle;
                int num3 = num2 + 1;
                num2 = num3;
                if (num3 <= 16384)
                {
                    continue;
                }
                CODebugBase<LogChannel>.Error(LogChannel.Core, string.Concat("Invalid list detected!\n", Environment.StackTrace));
                break;
            }
        }
    }
}
