using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct WaterPumpingServiceVehiclesPercent
    {
        public readonly int? waterPumpingServiceVehiclesPercent;
        public readonly int? waterPumpingServiceStoragePercent;

        public WaterPumpingServiceVehiclesPercent(
            int? waterPumpingServiceVehiclesPercent,
            int? waterPumpingServiceStoragePercent
            )
        {
            this.waterPumpingServiceVehiclesPercent = waterPumpingServiceVehiclesPercent;
            this.waterPumpingServiceStoragePercent = waterPumpingServiceStoragePercent;
        }
    }
}
