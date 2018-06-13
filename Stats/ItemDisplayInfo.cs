namespace Stats
{
    public struct ItemDisplayInfo
    {
        public ItemDisplayInfo(bool enabled, int criticalThresholdInPercent, int sortOrder)
        {
            this.Enabled = enabled;
            this.CriticalThreshold = criticalThresholdInPercent;
            this.SortOrder = sortOrder;
        }

        public bool Enabled { get; }
        public int CriticalThreshold { get; }
        public int SortOrder { get; }
    }
}
