using System;

namespace Stats.Configuration
{
    public class ConfigurationItemModel
    {
        private bool enabled;
        private int criticalThreshold;
        private int sortOrder;

        public ConfigurationItemModel(Item item, bool enabled, int criticalThreshold, int sortOrder)
        {
            Item = item;
            Enabled = enabled;
            CriticalThreshold = criticalThreshold;
            SortOrder = sortOrder;
        }

        public Item Item { get; }

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
