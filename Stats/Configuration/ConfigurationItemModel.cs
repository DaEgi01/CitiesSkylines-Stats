using System;

namespace Stats.Configuration
{
    public class ConfigurationItemModel
    {
        private bool enabled;
        private int criticalThreshold;
        private int sortOrder;

        public ConfigurationItemModel(ItemData itemData, bool enabled, int criticalThreshold, int sortOrder)
        {
            ItemData = itemData;
            Enabled = enabled;
            CriticalThreshold = criticalThreshold;
            SortOrder = sortOrder;
        }

        public ItemData ItemData { get; }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                this.OnPropertyChanged();
            }
        }

        public int CriticalThreshold
        {
            get => criticalThreshold;
            set
            {
                criticalThreshold = value;
                this.OnPropertyChanged();
            }
        }

        public int SortOrder
        {
            get => sortOrder;
            set
            {
                sortOrder = value;
                this.OnPropertyChanged();
            }
        }

        public event Action PropertyChanged;

        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}
