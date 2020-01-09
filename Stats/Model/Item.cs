using Stats.Config;
using System;

namespace Stats.Model
{
    public class Item
    {
        private readonly Configuration configuration;
        private readonly ItemData itemData;
        
        private bool enabled;
        private int criticalThreshold;
        private int sortOrder;

        private int? percent = null;
        private bool isVisible = false;

        public Item(Configuration configuration, ItemData itemData, bool enabled, int criticalThreshold, int sortOrder)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.itemData = itemData;

            this.enabled = enabled;
            this.criticalThreshold = criticalThreshold;
            this.sortOrder = sortOrder;
        }

        public ItemData ItemData => itemData;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value)
                {
                    return;
                }

                enabled = value;
                this.UpdateVisibilityAndRaiseOnVisibilityChanged();
            }
        }

        public int CriticalThreshold
        {
            get => criticalThreshold;
            set
            {
                if (criticalThreshold == value)
                {
                    return;
                }

                criticalThreshold = value;
                UpdateVisibilityAndRaiseOnVisibilityChanged();
            }
        }

        public int SortOrder
        {
            get => sortOrder;
            set
            {
                if (sortOrder == value)
                {
                    return;
                }

                sortOrder = value;
                OnSortOrderChanged();
            }
        }

        public int? Percent
        {
            get => percent;
            set
            {
                if (percent == value)
                {
                    return;
                }

                percent = value;
                UpdateVisibilityAndRaiseOnVisibilityChanged();
                OnPercentChanged();
            }
        }

        public bool IsVisible => isVisible;

        private void UpdateVisibilityAndRaiseOnVisibilityChanged()
        {
            var oldVisibility = isVisible;
            isVisible = GetItemVisibility(
                enabled,
                percent,
                criticalThreshold,
                configuration.MainPanelHideItemsBelowThreshold,
                configuration.MainPanelHideItemsNotAvailable);

            if (oldVisibility != isVisible)
            {
                OnVisibilityChanged();
            }
        }

        private bool GetItemVisibility(bool enabled, int? percent, int threshold, bool hideItemsBelowThreshold, bool hideItemsNotAvailable)
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

        public event Action PercentChanged;

        private void OnPercentChanged()
        {
            PercentChanged?.Invoke();
        }

        public event Action VisibilityChanged;

        private void OnVisibilityChanged()
        {
            VisibilityChanged?.Invoke();
        }

        public event Action SortOrderChanged;

        private void OnSortOrderChanged()
        {
            SortOrderChanged?.Invoke();
        }
    }
}
