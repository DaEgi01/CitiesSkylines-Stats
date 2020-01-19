namespace Stats.Config
{
    public class ConfigurationItemData
    {
        public ConfigurationItemData(ItemData itemData, bool enabled, int criticalThreshold, int sortOrder)
        {
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
}
