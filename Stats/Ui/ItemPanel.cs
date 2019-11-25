using ColossalFramework;
using ColossalFramework.UI;
using Stats.Configuration;
using Stats.Localization;
using System;
using UnityEngine;

namespace Stats.Ui
{
    public class ItemPanel : UIPanel
    {
        private ConfigurationModel configuration;
        private ConfigurationItemModel configurationItem;
        private LanguageResourceModel languageResourceModel;

        //TODO: refactor to localized item instead
        public void Initialize(ConfigurationModel configuration, ConfigurationItemModel configurationItem, LanguageResourceModel languageResourceModel)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.configurationItem = configurationItem ?? throw new ArgumentNullException(nameof(configurationItem));
            this.languageResourceModel = languageResourceModel ?? throw new ArgumentNullException(nameof(languageResourceModel));

            this.width = configuration.ItemWidth;
            this.height = configuration.ItemHeight;

            this.CreateAndAddIconButton();
            this.CreateAndAddPercentButton();

            configurationItem.PropertyChanged += ConfigurationItem_PropertyChanged;
        }

        public override void OnDestroy()
        {
            configurationItem.PropertyChanged -= ConfigurationItem_PropertyChanged;

            base.OnDestroy();
        }

        public ConfigurationItemModel ConfigurationItem => this.configurationItem;
        public UIButton IconButton { get; private set; }
        public UIButton PercentButton { get; private set; }
        public ItemVisibilityAndChanged ItemVisibility { get; private set; }

        private int? percent;

        public int? Percent
        {
            get => this.percent;
            set
            {
                this.percent = value;
                UpdateItemVisibilityAndValues();
            }
        }

        private void CreateAndAddPercentButton()
        {
            var percentButton = this.AddUIComponent<UIButton>();

            percentButton.autoSize = false;
            percentButton.height = this.height;
            percentButton.width = this.width - this.height;
            percentButton.relativePosition = Vector3.zero;
            percentButton.textHorizontalAlignment = UIHorizontalAlignment.Right;
            percentButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            percentButton.textScale = this.configuration.ItemTextScale;
            percentButton.color = this.configuration.MainPanelForegroundColor;
            percentButton.hoveredTextColor = this.configuration.MainPanelAccentColor;
            percentButton.pressedTextColor = this.configuration.MainPanelForegroundColor;

            percentButton.eventClicked += ButtonClicked;

            this.PercentButton = percentButton;
        }

        private void CreateAndAddIconButton()
        {
            var iconButton = this.AddUIComponent<UIButton>();

            iconButton.autoSize = false;
            iconButton.width = this.height;
            iconButton.height = this.height;
            iconButton.relativePosition = new Vector3(this.width - this.height, 0);
            iconButton.disabledBgSprite = "InfoIconBaseDisabled";
            iconButton.disabledFgSprite = $"{configurationItem.Item.Icon}Disabled";
            iconButton.focusedBgSprite = "InfoIconBaseNormal"; //don't use focused state
            iconButton.focusedFgSprite = $"{configurationItem.Item.Icon}"; //don't use focused state
            iconButton.hoveredBgSprite = "InfoIconBaseHovered";
            iconButton.hoveredFgSprite = $"{configurationItem.Item.Icon}Hovered";
            iconButton.pressedBgSprite = "InfoIconBasePressed";
            iconButton.pressedFgSprite = $"{configurationItem.Item.Icon}Pressed";
            iconButton.normalBgSprite = "InfoIconBaseNormal";
            iconButton.normalFgSprite = $"{configurationItem.Item.Icon}";
            iconButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            iconButton.textScaleMode = UITextScaleMode.ControlSize;

            iconButton.eventClicked += ButtonClicked;

            this.IconButton = iconButton;
        }

        private void ButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            var infoManager = Singleton<InfoManager>.instance;

            if (infoManager.CurrentMode == configurationItem.Item.InfoMode && infoManager.CurrentSubMode == configurationItem.Item.SubInfoMode)
            {
                infoManager.SetCurrentMode(InfoManager.InfoMode.None, InfoManager.SubInfoMode.Default);
            }
            else
            {
                infoManager.SetCurrentMode(configurationItem.Item.InfoMode, configurationItem.Item.SubInfoMode);
            }
        }

        public void UpdateLocalizedTooltips()
        {
            var localizedTooltip = this.languageResourceModel.GetItemLocalizedItemString(configurationItem.Item);

            this.IconButton.tooltip = localizedTooltip;
            this.PercentButton.tooltip = localizedTooltip;
        }

        private void ConfigurationItem_PropertyChanged()
        {
            UpdateItemVisibilityAndValues();
        }

        private void UpdateItemVisibilityAndValues()
        {
            this.ItemVisibility = this.GetItemVisibilityAndChanged();
            this.UpdateUiFromModel();
        }

        private void UpdateUiFromModel()
        {
            this.isVisible = this.ItemVisibility.IsVisibile;
            if (!this.isVisible)
            {
                return;
            }

            this.PercentButton.text = GetUsagePercentString(this.percent);
            this.PercentButton.textColor = GetItemTextColor(this.percent, configurationItem.CriticalThreshold);
            this.PercentButton.focusedColor = GetItemTextColor(this.percent, configurationItem.CriticalThreshold);
            this.PercentButton.hoveredTextColor = GetItemHoveredTextColor(this.percent, configurationItem.CriticalThreshold);
        }

        public ItemVisibilityAndChanged GetItemVisibilityAndChanged()
        {
            var oldItemVisible = this.isVisible;
            var newItemVisible = GetItemVisibility(configurationItem.Enabled, this.percent, configurationItem.CriticalThreshold);

            return new ItemVisibilityAndChanged(newItemVisible, oldItemVisible != newItemVisible);
        }

        private bool GetItemVisibility(bool enabled, int? percent, int threshold)
        {
            if (!enabled)
            {
                return false;
            }

            if (percent.HasValue)
            {
                if (this.configuration.MainPanelHideItemsBelowThreshold)
                {
                    return threshold < percent.Value;
                }

                return true;
            }
            else
            {
                return !this.configuration.MainPanelHideItemsNotAvailable;
            }
        }

        private string GetUsagePercentString(int? percent)
        {
            if (percent.HasValue)
            {
                return percent.Value.ToString() + "%";
            }

            return "-%";
        }

        private Color32 GetItemTextColor(int? percent, int threshold)
        {
            if (!percent.HasValue || percent.Value >= threshold)
            {
                return this.configuration.MainPanelAccentColor;
            }

            return this.configuration.MainPanelForegroundColor;
        }

        private Color32 GetItemHoveredTextColor(int? percent, int threshold)
        {
            if (!percent.HasValue || percent.Value >= threshold)
            {
                return this.configuration.MainPanelForegroundColor;
            }

            return this.configuration.MainPanelAccentColor;
        }

        public void AdjustButtonAndUiItemSize()
        {
            this.width = this.configuration.ItemWidth;
            this.height = this.configuration.ItemHeight;

            this.IconButton.width = this.height;
            this.IconButton.height = this.height;
            this.IconButton.relativePosition = new Vector3(this.width - this.height, 0);

            this.PercentButton.width = this.width - this.height;
            this.PercentButton.height = this.height;
            this.PercentButton.textScale = this.configuration.ItemTextScale;
        }
    }
}
