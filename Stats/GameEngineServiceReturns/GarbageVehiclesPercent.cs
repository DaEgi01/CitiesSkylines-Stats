using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct GarbageVehiclesPercent
    {
        public readonly int? garbageProcessingVehicles;
        public readonly int? landfillVehicles;

        public GarbageVehiclesPercent(int? garbageProcessingVehicles, int? landfillVehicles)
        {
            this.garbageProcessingVehicles = garbageProcessingVehicles;
            this.landfillVehicles = landfillVehicles;
        }
    }
}
