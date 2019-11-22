using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct FireDepartmentVehiclesPercent
    {
        public readonly int? fireDepartmentVehiclesPercent;
        public readonly int? fireHelicoptersPercent;

        public FireDepartmentVehiclesPercent(int? fireDepartmentVehiclesPercent, int? fireDepartmentHelicoptersPercent)
        {
            this.fireDepartmentVehiclesPercent = fireDepartmentVehiclesPercent;
            this.fireHelicoptersPercent = fireDepartmentHelicoptersPercent;
        }
    }
}
