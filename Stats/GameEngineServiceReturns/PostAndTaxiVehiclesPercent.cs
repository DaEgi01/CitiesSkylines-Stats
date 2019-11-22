using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct PostAndTaxiVehiclesPercent
    {
        public readonly int? taxisPercent;
        public readonly int? postVansPercent;
        public readonly int? postTrucksPercent;

        public PostAndTaxiVehiclesPercent(int? taxisPercent, int? postVansPercent, int? postTrucksPercent)
        {
            this.taxisPercent = taxisPercent;
            this.postVansPercent = postVansPercent;
            this.postTrucksPercent = postTrucksPercent;
        }
    }
}
