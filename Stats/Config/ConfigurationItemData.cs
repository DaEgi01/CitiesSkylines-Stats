using System.Runtime.InteropServices;

namespace Stats.Config
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ConfigurationItemData
    {
        public readonly ItemData itemData;
        public readonly bool enabled;
        public readonly int criticalThreshold;
        public readonly int sortOrder;

        public ConfigurationItemData(ItemData itemData, bool enabled, int criticalThreshold, int sortOrder)
        {
            this.itemData = itemData;
            this.enabled = enabled;
            this.criticalThreshold = criticalThreshold;
            this.sortOrder = sortOrder;
        }
    }
}
