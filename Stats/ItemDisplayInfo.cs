namespace Stats
{
    public struct ItemDisplayInfo
    {
        public ItemDisplayInfo(bool enabled, int criticalTresholdInPercent, int sortOrder)
        {
            this.Enabled = enabled;
            this.CriticalTreshold = criticalTresholdInPercent;
            this.SortOrder = sortOrder;
        }

        public bool Enabled { get; }
        public int CriticalTreshold { get; }
        public int SortOrder { get; }
    }
}
