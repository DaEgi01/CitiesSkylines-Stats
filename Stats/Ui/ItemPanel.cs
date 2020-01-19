using ColossalFramework;
using ColossalFramework.UI;
using Stats.Config;
using Stats.Localization;
using System;
using UnityEngine;

namespace Stats.Ui
{
    public class ItemPanel : UIPanel
    {
        private Configuration configuration;
        private ConfigurationItemData configurationItemData;
        private LanguageResource languageResource;
        private Func<int?> getPercentFromGame;

        //TODO: refactor to localized item instead
        public void Initialize(Configuration configuration, ConfigurationItemData configurationItemData, LanguageResource languageResource, Func<int?> getPercentFromGame)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.configurationItemData = configurationItemData ?? throw new ArgumentNullException(nameof(configurationItemData));
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
            this.getPercentFromGame = getPercentFromGame ?? throw new ArgumentNullException(nameof(getPercentFromGame));

            this.width = configuration.ItemWidth;
            this.height = configuration.ItemHeight;

            this.CreateAndAddIconButton();
            this.CreateAndAddPercentButton();

            this.isVisible = this.configurationItemData.Enabled;
        }

        public ConfigurationItemData ConfigurationItemData => this.configurationItemData;

        public UIButton IconButton { get; private set; }
        public UIButton PercentButton { get; private set; }

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
            iconButton.disabledFgSprite = $"{this.configurationItemData.ItemData.Icon}Disabled";
            iconButton.focusedBgSprite = "InfoIconBaseNormal"; //don't use focused state
            iconButton.focusedFgSprite = $"{this.configurationItemData.ItemData.Icon}"; //don't use focused state
            iconButton.hoveredBgSprite = "InfoIconBaseHovered";
            iconButton.hoveredFgSprite = $"{this.configurationItemData.ItemData.Icon}Hovered";
            iconButton.pressedBgSprite = "InfoIconBasePressed";
            iconButton.pressedFgSprite = $"{this.configurationItemData.ItemData.Icon}Pressed";
            iconButton.normalBgSprite = "InfoIconBaseNormal";
            iconButton.normalFgSprite = $"{this.configurationItemData.ItemData.Icon}";
            iconButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            iconButton.textScaleMode = UITextScaleMode.ControlSize;

            iconButton.eventClicked += ButtonClicked;

            this.IconButton = iconButton;
        }

        private void ButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            var infoManager = Singleton<InfoManager>.instance;

            if (infoManager.CurrentMode == this.configurationItemData.ItemData.InfoMode
                && infoManager.CurrentSubMode == this.configurationItemData.ItemData.SubInfoMode)
            {
                infoManager.SetCurrentMode(InfoManager.InfoMode.None, InfoManager.SubInfoMode.Default);
            }
            else
            {
                infoManager.SetCurrentMode(
                    this.configurationItemData.ItemData.InfoMode,
                    this.configurationItemData.ItemData.SubInfoMode);
            }
        }

        /// <summary>
        /// Update the visibility of a panel.
        /// </summary>
        /// <returns>True if visibility changed.</returns>
        public bool UpdatePercentVisibilityAndColor()
        {
            var percent = this.getPercentFromGame();
            var oldVisiblity = this.isVisible;
            this.isVisible = ItemHelper.GetItemVisibility(configurationItemData.Enabled, this.configuration.MainPanelHideItemsNotAvailable, this.configuration.MainPanelHideItemsBelowThreshold, configurationItemData.CriticalThreshold, percent);

            if (this.isVisible)
            {
                this.PercentButton.text = GetUsagePercentString(percent);
                this.PercentButton.textColor = GetItemTextColor(percent, configurationItemData.CriticalThreshold);
                this.PercentButton.focusedColor = GetItemTextColor(percent, configurationItemData.CriticalThreshold);
                this.PercentButton.hoveredTextColor = GetItemHoveredTextColor(percent, configurationItemData.CriticalThreshold);
            }

            return oldVisiblity != this.isVisible;
        }

        private string GetUsagePercentString(int? percent)
        {
            return percent.HasValue
                ? percent.Value.ToString() + "%"
                : "-%";
        }

        private Color32 GetItemTextColor(int? percent, int threshold)
        {
            return !percent.HasValue || percent.Value >= threshold
                ? this.configuration.MainPanelAccentColor
                : this.configuration.MainPanelForegroundColor;
        }

        private Color32 GetItemHoveredTextColor(int? percent, int threshold)
        {
            return !percent.HasValue || percent.Value >= threshold
                ? this.configuration.MainPanelForegroundColor
                : this.configuration.MainPanelAccentColor;
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

        protected override void OnLocalize()
        {
            base.OnLocalize();

            var localizedTooltip = this.languageResource.GetLocalizedItemString(this.configurationItemData.ItemData);

            this.IconButton.tooltip = localizedTooltip;
            this.PercentButton.tooltip = localizedTooltip;
        }
    }
}
