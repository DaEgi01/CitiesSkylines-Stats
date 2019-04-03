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

        //CommonBuildingAI.CalculateOwnVehicles rewritten as a static method (protected instance method).
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
    }
}
