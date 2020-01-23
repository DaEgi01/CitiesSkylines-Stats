using ColossalFramework.UI;
using Stats.Config;
using Stats.Localization;
using System;
using UnityEngine;

namespace Stats.Ui
{
    public class ItemPanel : UIPanel
    {
        private Configuration _configuration;
        private ConfigurationItemData _configurationItemData;
        private LanguageResource _languageResource;
        private Func<int?> _getPercentFromGame;
        private InfoManager _infoManager;

        private UIButton _iconButton;
        private UIButton _percentButton;

        public ConfigurationItemData ConfigurationItemData => _configurationItemData;

        //TODO: refactor to localized item instead
        public void Initialize(Configuration configuration, ConfigurationItemData configurationItemData, LanguageResource languageResource, Func<int?> getPercentFromGame, InfoManager infoManager)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configurationItemData = configurationItemData ?? throw new ArgumentNullException(nameof(configurationItemData));
            _languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
            _getPercentFromGame = getPercentFromGame ?? throw new ArgumentNullException(nameof(getPercentFromGame));
            _infoManager = infoManager ?? throw new ArgumentNullException(nameof(infoManager));

            width = configuration.ItemWidth;
            height = configuration.ItemHeight;

            isVisible = _configurationItemData.Enabled;

            CreateAndAddIconButton();
            CreateAndAddPercentButton();
            UpdateLocalization();
        }

        private void CreateAndAddPercentButton()
        {
            var percentButton = AddUIComponent<UIButton>();

            percentButton.autoSize = false;
            percentButton.height = height;
            percentButton.width = width - height;
            percentButton.relativePosition = Vector3.zero;
            percentButton.textHorizontalAlignment = UIHorizontalAlignment.Right;
            percentButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            percentButton.textScale = _configuration.ItemTextScale;
            percentButton.color = _configuration.MainPanelForegroundColor;
            percentButton.hoveredTextColor = _configuration.MainPanelAccentColor;
            percentButton.pressedTextColor = _configuration.MainPanelForegroundColor;

            percentButton.eventClicked += ButtonClicked;

            _percentButton = percentButton;
        }

        private void CreateAndAddIconButton()
        {
            var iconButton = AddUIComponent<UIButton>();

            iconButton.autoSize = false;
            iconButton.width = height;
            iconButton.height = height;
            iconButton.relativePosition = new Vector3(width - height, 0);
            iconButton.disabledBgSprite = "InfoIconBaseDisabled";
            iconButton.disabledFgSprite = $"{_configurationItemData.ItemData.Icon}Disabled";
            iconButton.focusedBgSprite = "InfoIconBaseNormal"; //don't use focused state
            iconButton.focusedFgSprite = $"{_configurationItemData.ItemData.Icon}"; //don't use focused state
            iconButton.hoveredBgSprite = "InfoIconBaseHovered";
            iconButton.hoveredFgSprite = $"{_configurationItemData.ItemData.Icon}Hovered";
            iconButton.pressedBgSprite = "InfoIconBasePressed";
            iconButton.pressedFgSprite = $"{_configurationItemData.ItemData.Icon}Pressed";
            iconButton.normalBgSprite = "InfoIconBaseNormal";
            iconButton.normalFgSprite = $"{_configurationItemData.ItemData.Icon}";
            iconButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            iconButton.textScaleMode = UITextScaleMode.ControlSize;

            iconButton.eventClicked += ButtonClicked;

            _iconButton = iconButton;
        }

        private void ButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (_infoManager.CurrentMode == _configurationItemData.ItemData.InfoMode
                && _infoManager.CurrentSubMode == _configurationItemData.ItemData.SubInfoMode)
            {
                _infoManager.SetCurrentMode(
                    InfoManager.InfoMode.None,
                    InfoManager.SubInfoMode.Default
                );
            }
            else
            {
                _infoManager.SetCurrentMode(
                    _configurationItemData.ItemData.InfoMode,
                    _configurationItemData.ItemData.SubInfoMode
                );
            }
        }

        /// <summary>
        /// Update the visibility of a panel.
        /// </summary>
        /// <returns>True if visibility changed.</returns>
        public ItemVisibilityAndChanged UpdatePercentVisibilityAndColor()
        {
            var percent = _getPercentFromGame();
            var oldVisiblity = isVisible;
            isVisible = ItemHelper.GetItemVisibility(_configurationItemData.Enabled, _configuration.MainPanelHideItemsNotAvailable, _configuration.MainPanelHideItemsBelowThreshold, _configurationItemData.CriticalThreshold, percent);
            if (isVisible)
            {
                _percentButton.text = GetUsagePercentString(percent);
                _percentButton.textColor = GetItemTextColor(percent, _configurationItemData.CriticalThreshold);
                _percentButton.focusedColor = GetItemTextColor(percent, _configurationItemData.CriticalThreshold);
                _percentButton.hoveredTextColor = GetItemHoveredTextColor(percent, _configurationItemData.CriticalThreshold);
            }

            return new ItemVisibilityAndChanged(isVisible, oldVisiblity != isVisible);
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
                ? _configuration.MainPanelAccentColor
                : _configuration.MainPanelForegroundColor;
        }

        private Color32 GetItemHoveredTextColor(int? percent, int threshold)
        {
            return !percent.HasValue || percent.Value >= threshold
                ? _configuration.MainPanelForegroundColor
                : _configuration.MainPanelAccentColor;
        }

        public void AdjustButtonAndUiItemSize()
        {
            width = _configuration.ItemWidth;
            height = _configuration.ItemHeight;

            _iconButton.width = height;
            _iconButton.height = height;
            _iconButton.relativePosition = new Vector3(width - height, 0);

            _percentButton.width = width - height;
            _percentButton.height = height;
            _percentButton.textScale = _configuration.ItemTextScale;
        }

        public void UpdateLocalization()
        {
            var localizedTooltip = _languageResource.GetLocalizedItemString(_configurationItemData.ItemData);
            _iconButton.tooltip = localizedTooltip;
            _percentButton.tooltip = localizedTooltip;
        }
    }
}
