namespace Stats.Ui
{
    public static class ItemHelper
    {
        public static bool GetItemVisibility(bool enabled, bool hideItemsNotAvailable, bool hideItemsBelowThreshold, int threshold, int? percent)
        {
            if (!enabled)
            {
                return false;
            }

            if (!percent.HasValue)
            {
                return !hideItemsNotAvailable;
            }

            if (hideItemsBelowThreshold)
            {
                return threshold < percent.Value;
            }

            return true;
        }
    }
}
