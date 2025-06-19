using System;

namespace Stats.Config;

public sealed class ConfigurationItemData
{
    public ConfigurationItemData(ItemData itemData, bool enabled, int criticalThreshold, int sortOrder)
    {
        if (itemData is null)
            throw new ArgumentNullException(nameof(itemData));

        ItemData = itemData;
        Enabled = enabled;
        CriticalThreshold = criticalThreshold;
        SortOrder = sortOrder;
    }

    public ItemData ItemData { get; }
    public bool Enabled { get; set; }
    public int CriticalThreshold { get; set; }
    public int SortOrder { get; set; }
}
