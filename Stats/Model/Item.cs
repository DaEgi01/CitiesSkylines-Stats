using System;

namespace Stats.Model
{
    public class Item
    {
        private readonly ItemData itemData;
        private readonly Func<bool> hideItemsBelowTreshold;
        private readonly Func<bool> hideItemsNotAvailable;

        private bool enabled;
        private int criticalThreshold;
        private int sortOrder;

        private int? percent = null;
        private bool isVisible = false;

        public Item(ItemData itemData, Func<bool> hideItemsBelowTreshold, Func<bool> hideItemsNotAvailable, bool enabled, int criticalThreshold, int sortOrder)
        {
            this.hideItemsBelowTreshold = hideItemsBelowTreshold;
            this.hideItemsNotAvailable = hideItemsNotAvailable;
            this.itemData = itemData;

            this.enabled = enabled;
            this.criticalThreshold = criticalThreshold;
            this.sortOrder = sortOrder;
        }

        public ItemData ItemData => itemData;

        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }

        public int CriticalThreshold
        {
            get => criticalThreshold;
            set => criticalThreshold = value;
        }

        public int SortOrder
        {
            get => sortOrder;
            set => sortOrder = value;
        }

        public int? Percent
        {
            get => percent;
            set
            {
                percent = value;
                this.UpdateVisibility();
            }
        }

        public bool IsVisible => isVisible;

        private void UpdateVisibility()
        {
            var oldVisibility = isVisible;
            isVisible = GetItemVisibility(
                enabled,
                hideItemsNotAvailable(),
                hideItemsBelowTreshold(),
                criticalThreshold,
                percent
            );
        }

        public static bool GetItemVisibility(bool enabled, bool hideItemsNotAvailable, bool hideItemsBelowThreshold, int threshold, int? percent)
        {
            if (!enabled)
            {
                return false;
            }

            if (percent.HasValue)
            {
                if (hideItemsBelowThreshold)
                {
                    return threshold < percent.Value;
                }

                return true;
            }
            else
            {
                return !hideItemsNotAvailable;
            }
        }
    }
}
