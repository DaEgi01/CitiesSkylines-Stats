using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct HealthCareVehiclesPercent
    {
        public readonly int? healthCareVehiclesPercent;
        public readonly int? medicalHelicoptersPercent;
        public readonly int? cemeteryVehiclesPercent;
        public readonly int? crematoriumVehiclesPercent;

        public HealthCareVehiclesPercent(
            int? healthCareVehiclesPercent,
            int? medicalHelicoptersPercent,
            int? cemeteryVehiclesPercent,
            int? crematoriumVehiclesPercent
        )
        {
            this.healthCareVehiclesPercent = healthCareVehiclesPercent;
            this.medicalHelicoptersPercent = medicalHelicoptersPercent;
            this.cemeteryVehiclesPercent = cemeteryVehiclesPercent;
            this.crematoriumVehiclesPercent = crematoriumVehiclesPercent;
        }
    }
}