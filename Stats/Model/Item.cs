using Stats.Configuration;
using System;

namespace Stats.Model
{
    public class Item
    {
        private readonly ConfigurationModel configurationModel;
        private readonly ConfigurationItemModel configurationItem;

        private int? percent;

        public Item(ConfigurationModel configurationModel, ConfigurationItemModel configurationItem)
        {
            this.configurationModel = configurationModel ?? throw new ArgumentNullException(nameof(configurationModel));
            this.configurationItem = configurationItem ?? throw new ArgumentNullException(nameof(configurationItem));

            this.configurationItem.PropertyChanged += ConfigurationItem_PropertyChanged;
        }

        public ConfigurationItemModel ConfigurationItem => this.configurationItem;

        public int? Percent
        {
            get => percent;
            set
            {
                if (percent == value)
                {
                    return;
                }

                var oldVisibility = this.IsVisible;

                percent = value;
                this.OnPercentChanged();

                if (oldVisibility != this.IsVisible)
                {
                    this.OnVisibilityChanged();
                }
            }
        }

        public bool IsVisible => GetItemVisibility(
            this.configurationItem.Enabled,
            this.percent,
            this.configurationItem.CriticalThreshold,
            this.configurationModel.MainPanelHideItemsBelowThreshold,
            this.configurationModel.MainPanelHideItemsNotAvailable);

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
            this.PercentChanged?.Invoke();
        }

        public event Action VisibilityChanged;

        private void OnVisibilityChanged()
        {
            this.VisibilityChanged?.Invoke();
        }

        //TODO could be optimized
        private void ConfigurationItem_PropertyChanged()
        {
            this.OnVisibilityChanged();
        }
    }
}
