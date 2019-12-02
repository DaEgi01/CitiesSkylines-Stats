using ColossalFramework;
using ColossalFramework.UI;
using Stats.Configuration;
using Stats.Localization;
using Stats.Model;
using System;
using UnityEngine;

namespace Stats.Ui
{
    public class ItemPanel : UIPanel
    {
        private ConfigurationModel configuration;
        private LanguageResourceModel languageResourceModel;

        //TODO: refactor to localized item instead
        public void Initialize(ConfigurationModel configuration, Item item, LanguageResourceModel languageResourceModel)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.Item = item ?? throw new ArgumentNullException(nameof(item));
            this.languageResourceModel = languageResourceModel ?? throw new ArgumentNullException(nameof(languageResourceModel));

            this.width = configuration.ItemWidth;
            this.height = configuration.ItemHeight;

            this.CreateAndAddIconButton();
            this.CreateAndAddPercentButton();

            item.PercentChanged += this.UpdatePercentAndColors;
            item.VisibilityChanged += this.UpdateVisibility;
        }

        public override void OnDestroy()
        {
            Item.PercentChanged -= this.UpdatePercentAndColors;
            Item.VisibilityChanged -= this.UpdateVisibility;

            base.OnDestroy();
        }

        public UIButton IconButton { get; private set; }
        public UIButton PercentButton { get; private set; }
        public Item Item { get; private set; }

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
            iconButton.disabledFgSprite = $"{this.Item.ConfigurationItem.ItemData.Icon}Disabled";
            iconButton.focusedBgSprite = "InfoIconBaseNormal"; //don't use focused state
            iconButton.focusedFgSprite = $"{this.Item.ConfigurationItem.ItemData.Icon}"; //don't use focused state
            iconButton.hoveredBgSprite = "InfoIconBaseHovered";
            iconButton.hoveredFgSprite = $"{this.Item.ConfigurationItem.ItemData.Icon}Hovered";
            iconButton.pressedBgSprite = "InfoIconBasePressed";
            iconButton.pressedFgSprite = $"{this.Item.ConfigurationItem.ItemData.Icon}Pressed";
            iconButton.normalBgSprite = "InfoIconBaseNormal";
            iconButton.normalFgSprite = $"{this.Item.ConfigurationItem.ItemData.Icon}";
            iconButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            iconButton.textScaleMode = UITextScaleMode.ControlSize;

            iconButton.eventClicked += ButtonClicked;

            this.IconButton = iconButton;
        }

        private void ButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            var infoManager = Singleton<InfoManager>.instance;

            if (infoManager.CurrentMode == this.Item.ConfigurationItem.ItemData.InfoMode
                && infoManager.CurrentSubMode == this.Item.ConfigurationItem.ItemData.SubInfoMode)
            {
                infoManager.SetCurrentMode(InfoManager.InfoMode.None, InfoManager.SubInfoMode.Default);
            }
            else
            {
                infoManager.SetCurrentMode(
                    this.Item.ConfigurationItem.ItemData.InfoMode,
                    this.Item.ConfigurationItem.ItemData.SubInfoMode);
            }
        }

        public void UpdateLocalizedTooltips()
        {
            var localizedTooltip = this.languageResourceModel.GetItemLocalizedItemString(this.Item.ConfigurationItem.ItemData);

            this.IconButton.tooltip = localizedTooltip;
            this.PercentButton.tooltip = localizedTooltip;
        }

        private void UpdateVisibility()
        {
            this.isVisible = this.Item.IsVisible;
        }

        private void UpdatePercentAndColors()
        {
            this.PercentButton.text = GetUsagePercentString(this.Item.Percent);
            this.PercentButton.textColor = GetItemTextColor(this.Item.Percent, this.Item.ConfigurationItem.CriticalThreshold);
            this.PercentButton.focusedColor = GetItemTextColor(this.Item.Percent, this.Item.ConfigurationItem.CriticalThreshold);
            this.PercentButton.hoveredTextColor = GetItemHoveredTextColor(this.Item.Percent, this.Item.ConfigurationItem.CriticalThreshold);
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
