using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct DisasterResponseVehiclesPercent
    {
        public readonly int? disasterResponseVehicles;
        public readonly int? disasterResponseHelicopters;

        public DisasterResponseVehiclesPercent(int? disasterResponseVehicles, int? disasterResponseHelicopters)
        {
            this.disasterResponseVehicles = disasterResponseVehicles;
            this.disasterResponseHelicopters = disasterResponseHelicopters;
        }
    }
}
