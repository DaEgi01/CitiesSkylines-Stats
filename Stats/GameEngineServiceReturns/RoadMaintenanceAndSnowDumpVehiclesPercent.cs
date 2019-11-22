using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct RoadMaintenanceAndSnowDumpVehiclesPercent
    {
        public readonly int? roadMaintenanceVehiclesPercent;
        public readonly int? snowDumpPercent;
        public readonly int? snowDumpVehiclesPercent;

        public RoadMaintenanceAndSnowDumpVehiclesPercent(
            int? roadMaintenanceVehiclesPercent, 
            int? snowDumpPercent, 
            int? snowDumpVehiclesPercent)
        {
            this.roadMaintenanceVehiclesPercent = roadMaintenanceVehiclesPercent;
            this.snowDumpPercent = snowDumpPercent;
            this.snowDumpVehiclesPercent = snowDumpVehiclesPercent;
        }
    }
}
