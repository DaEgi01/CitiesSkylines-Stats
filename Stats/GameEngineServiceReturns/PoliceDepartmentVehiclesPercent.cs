using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct PoliceDepartmentVehiclesPercent
    {
        public readonly int? policeHelicoptersPercent;
        public readonly int? policeHoldingCellsPercent;
        public readonly int? policeVehiclesPercent;
        public readonly int? prisonCellsPercent;
        public readonly int? prisonVehiclesPercent;

        public PoliceDepartmentVehiclesPercent(
            int? policeHelicoptersPercent,
            int? policeHoldingCellsPercent,
            int? policeVehiclesPercent,
            int? prisonCellsPercent,
            int? prisonVehiclesPercent)
        {
            this.policeHelicoptersPercent = policeHelicoptersPercent;
            this.policeHoldingCellsPercent = policeHoldingCellsPercent;
            this.policeVehiclesPercent = policeVehiclesPercent;
            this.prisonCellsPercent = prisonCellsPercent;
            this.prisonVehiclesPercent = prisonVehiclesPercent;
        }
    }
}
