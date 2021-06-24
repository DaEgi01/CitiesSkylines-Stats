namespace Stats.Ui
{
    using System;
    using System.Reflection;
    using ColossalFramework.UI;
    using Stats.Config;
    using Stats.Localization;
    using UnityEngine;

    public class ItemPanel : UIPanel
    {
        private const string _textToDeterminePercentButtonSize = "888%";

        private Configuration? _configuration;
        private ConfigurationItemData? _configurationItemData;
        private LanguageResource? _languageResource;
        private Func<int?>? _getPercentFromGame;
        private InfoManager? _infoManager;

        private UIButton? _iconButton;
        private UIButton? _percentButton;

        private Func<UIButton, UIFontRenderer>? _obtainTextRenderer;

        public ConfigurationItemData? ConfigurationItemData => _configurationItemData;

        // TODO: refactor to localized item instead
        public void Initialize(Configuration configuration, ConfigurationItemData configurationItemData, LanguageResource languageResource, Func<int?> getPercentFromGame, InfoManager infoManager)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            if (configurationItemData is null)
                throw new ArgumentNullException(nameof(configurationItemData));

            if (languageResource is null)
                throw new ArgumentNullException(nameof(languageResource));

            if (getPercentFromGame is null)
                throw new ArgumentNullException(nameof(getPercentFromGame));

            if (infoManager is null)
                throw new ArgumentNullException(nameof(infoManager));

            _configuration = configuration;
            _configurationItemData = configurationItemData;
            _languageResource = languageResource;
            _getPercentFromGame = getPercentFromGame;
            _infoManager = infoManager;

            var obtainTextRendererMethodInfo = typeof(UIButton).GetMethod("ObtainTextRenderer", BindingFlags.NonPublic | BindingFlags.Instance);
            if (obtainTextRendererMethodInfo is null)
                throw new Exception("Could not get method 'ObtainTextRenderer'");

            _obtainTextRenderer = (Func<UIButton, UIFontRenderer>)Delegate.CreateDelegate(typeof(Func<UIButton, UIFontRenderer>), obtainTextRendererMethodInfo);

            isVisible = _configurationItemData.Enabled;

            CreateAndAddIconButton();
            if (_configuration.ItemTextPosition != ItemTextPosition.None)
            {
                CreateAndAddPercentButton();
            }

            UpdateButtonSizesAndLayoutAndPanelSize();
            UpdateLocalization();
        }

        public void CreateAndAddPercentButton()
        {
            var percentButton = AddUIComponent<UIButton>();

            percentButton.autoSize = false;
            percentButton.horizontalAlignment = UIHorizontalAlignment.Left;
            percentButton.verticalAlignment = UIVerticalAlignment.Top;

            if (_configuration.ItemTextPosition == ItemTextPosition.Top || _configuration.ItemTextPosition == ItemTextPosition.Bottom)
            {
                percentButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            }
            else if (_configuration.ItemTextPosition == ItemTextPosition.Left || _configuration.ItemTextPosition == ItemTextPosition.Right)
            {
                percentButton.textHorizontalAlignment = UIHorizontalAlignment.Right;
            }

            percentButton.textVerticalAlignment = UIVerticalAlignment.Top;
            percentButton.color = _configuration.MainPanelForegroundColor;
            percentButton.hoveredTextColor = _configuration.MainPanelAccentColor;
            percentButton.pressedTextColor = _configuration.MainPanelForegroundColor;
            percentButton.text = "888%";

            percentButton.eventClicked += ButtonClicked;

            _percentButton = percentButton;
        }

        public void DestroyPercentButton()
        {
            GameObject.Destroy(_percentButton);
            _percentButton = null;
        }

        public void UpdateButtonSizesAndLayoutAndPanelSize()
        {
            UpdateButtonSizes();
            var panelSize = CalculatePanelSize();
            UpdateLayout(panelSize);
            UpdatePanelSize(panelSize);
        }

        public Vector2 CalculatePanelSize()
        {
            var itemTextPosition = _configuration.ItemTextPosition;
            if (itemTextPosition == ItemTextPosition.None)
            {
                return new Vector2(_iconButton.width, _iconButton.height);
            }
            else if (itemTextPosition == ItemTextPosition.Top || itemTextPosition == ItemTextPosition.Bottom)
            {
                var width = Mathf.Max(_iconButton.width, _percentButton.width);
                var height = _iconButton.height + (_configuration.ItemPadding / 2f) + _percentButton.height;

                return new Vector2(width, height);
            }
            else if (itemTextPosition == ItemTextPosition.Right || itemTextPosition == ItemTextPosition.Left)
            {
                var width = _iconButton.width + (_configuration.ItemPadding / 2f) + _percentButton.width;
                var height = Mathf.Max(_iconButton.height, _percentButton.height);

                return new Vector2(width, height);
            }
            else
            {
                throw new Exception($"Unknown ItemTextPosition '{itemTextPosition.Name}'");
            }
        }

        public void UpdateButtonSizes()
        {
            UpdateIconButtonSize();
            if (_configuration.ItemTextPosition != ItemTextPosition.None)
            {
                UpdatePercentButtonSize();
            }
        }

        public void UpdateLocalization()
        {
            var localizedTooltip = _languageResource.GetLocalizedItemString(_configurationItemData.ItemData);
            _iconButton.tooltip = localizedTooltip;

            if (_configuration.ItemTextPosition != ItemTextPosition.None)
            {
                _percentButton.tooltip = localizedTooltip;
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
            if (isVisible && _configuration.ItemTextPosition != ItemTextPosition.None)
            {
                _percentButton.text = GetUsagePercentString(percent);
                _percentButton.textColor = GetItemTextColor(percent, _configurationItemData.CriticalThreshold);
                _percentButton.focusedColor = GetItemTextColor(percent, _configurationItemData.CriticalThreshold);
                _percentButton.focusedTextColor = GetItemTextColor(percent, _configurationItemData.CriticalThreshold);
                _percentButton.hoveredTextColor = GetItemHoveredTextColor(percent, _configurationItemData.CriticalThreshold);
            }

            return new ItemVisibilityAndChanged(isVisible, oldVisiblity != isVisible);
        }

        private static string GetUsagePercentString(int? percent)
        {
            return percent.HasValue
                ? percent.Value.ToString() + "%"
                : "-%";
        }

        private void CreateAndAddIconButton()
        {
            var iconButton = AddUIComponent<UIButton>();

            iconButton.autoSize = false;
            iconButton.disabledBgSprite = "InfoIconBaseDisabled";
            iconButton.disabledFgSprite = $"{_configurationItemData.ItemData.Icon}Disabled";
            iconButton.focusedBgSprite = "InfoIconBaseNormal"; // don't use focused state
            iconButton.focusedFgSprite = $"{_configurationItemData.ItemData.Icon}"; // don't use focused state
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

        private void UpdateIconButtonSize()
        {
            _iconButton.width = _configuration.ItemIconSize;
            _iconButton.height = _configuration.ItemIconSize;
        }

        private void UpdatePercentButtonSize()
        {
            _percentButton.textScale = _configuration.ItemTextScale;
            using UIFontRenderer fontRenderer = _obtainTextRenderer(_percentButton);
            var dynamicFont = (UIDynamicFont)fontRenderer.font;
            _percentButton.size = GameMethods.MeasureText(
                dynamicFont,
                _textToDeterminePercentButtonSize,
                _percentButton.textScale,
                FontStyle.Normal);
        }

        private void UpdatePanelSize(Vector2 panelSize)
        {
            width = panelSize.x;
            height = panelSize.y;
        }

        private void UpdateLayout(Vector2 panelSize)
        {
            _iconButton.relativePosition = CalculateIconButtonRelativePosition(_configuration.ItemTextPosition, panelSize);
            if (_configuration.ItemTextPosition == ItemTextPosition.None)
                return;

            _percentButton.relativePosition = CalculatePercentButtonRelativePosition(_configuration.ItemTextPosition, panelSize);

            if (_configuration.ItemTextPosition == ItemTextPosition.Top || _configuration.ItemTextPosition == ItemTextPosition.Bottom)
            {
                _percentButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            }
            else if (_configuration.ItemTextPosition == ItemTextPosition.Left || _configuration.ItemTextPosition == ItemTextPosition.Right)
            {
                _percentButton.textHorizontalAlignment = UIHorizontalAlignment.Right;
            }
        }

        private Vector2 CalculateIconButtonRelativePosition(ItemTextPosition itemTextPosition, Vector2 panelSize)
        {
            if (itemTextPosition == ItemTextPosition.None)
            {
                return Vector2.zero;
            }
            else if (itemTextPosition == ItemTextPosition.Top)
            {
                var left = _iconButton.width < panelSize.x
                    ? (panelSize.x / 2f) - (_iconButton.width / 2f)
                    : 0f;

                return new Vector2(left, _percentButton.height + (_configuration.ItemPadding / 2f));
            }
            else if (itemTextPosition == ItemTextPosition.Right)
            {
                var top = _iconButton.height < panelSize.y
                    ? (panelSize.y / 2f) - (_iconButton.height / 2f)
                    : 0f;

                return new Vector2(0f, top);
            }
            else if (itemTextPosition == ItemTextPosition.Bottom)
            {
                var left = _iconButton.width < panelSize.x
                    ? (panelSize.x / 2f) - (_iconButton.width / 2f)
                    : 0f;

                return new Vector2(left, 0f);
            }
            else if (itemTextPosition == ItemTextPosition.Left)
            {
                var top = _iconButton.height < panelSize.y
                    ? (panelSize.y / 2f) - (_iconButton.height / 2f)
                    : 0f;

                return new Vector2(_percentButton.width + (_configuration.ItemPadding / 2f), top);
            }
            else
            {
                throw new ArgumentException($"Unknown value '{itemTextPosition.Name}'", nameof(itemTextPosition));
            }
        }

        private Vector2 CalculatePercentButtonRelativePosition(ItemTextPosition itemTextPosition, Vector2 panelSize)
        {
            if (itemTextPosition == ItemTextPosition.None)
            {
                return Vector2.zero;
            }
            else if (itemTextPosition == ItemTextPosition.Top)
            {
                var left = _percentButton.width < panelSize.x
                    ? (panelSize.x / 2f) - (_percentButton.width / 2f)
                    : 0f;

                return new Vector2(left, 0f);
            }
            else if (itemTextPosition == ItemTextPosition.Right)
            {
                var top = _percentButton.height < panelSize.y
                    ? (panelSize.y / 2f) - (_percentButton.height / 2f)
                    : 0f;

                return new Vector2(_iconButton.width + (_configuration.ItemPadding / 2f), top);
            }
            else if (itemTextPosition == ItemTextPosition.Bottom)
            {
                var left = _percentButton.width < panelSize.x
                    ? (panelSize.x / 2f) - (_percentButton.width / 2f)
                    : 0f;

                return new Vector2(left, _iconButton.height + (_configuration.ItemPadding / 2f));
            }
            else if (itemTextPosition == ItemTextPosition.Left)
            {
                var top = _percentButton.height < panelSize.y
                    ? (panelSize.y / 2f) - (_percentButton.height / 2f)
                    : 0f;

                return new Vector2(0f, top);
            }
            else
            {
                throw new ArgumentException($"Unknown value '{itemTextPosition.Name}'", nameof(itemTextPosition));
            }
        }

        private void ButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (_infoManager.CurrentMode == _configurationItemData.ItemData.InfoMode
                && _infoManager.CurrentSubMode == _configurationItemData.ItemData.SubInfoMode)
            {
                _infoManager.SetCurrentMode(
                    InfoManager.InfoMode.None,
                    InfoManager.SubInfoMode.Default);
            }
            else
            {
                _infoManager.SetCurrentMode(
                    _configurationItemData.ItemData.InfoMode,
                    _configurationItemData.ItemData.SubInfoMode);
            }
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
    }
}
