using ColossalFramework;
using ColossalFramework.UI;
using Stats.Configuration;
using System;
using UnityEngine;

namespace Stats.Ui
{
    public class ItemPanel : UIPanel
    {
        private ItemData itemData;
        private ConfigurationModel configuration;

        //TODO refactor to localized item instead
        public void Initialize(ItemData itemData, ConfigurationModel configuration)
        {
            this.itemData = itemData ?? throw new ArgumentNullException(nameof(itemData));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            this.width = configuration.ItemWidth;
            this.height = configuration.ItemHeight;

            this.CreateAndAddIconButton();
            this.CreateAndAddPercentButton();
        }

        public UIButton IconButton { get; private set; }
        public UIButton PercentButton { get; private set; }

        public int? Percent { get; set; }
        public int CriticalTreshold { get; set; }
        public int SortOrder { get; set; }

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
            iconButton.disabledFgSprite = $"{itemData.Icon}Disabled";
            iconButton.focusedBgSprite = "InfoIconBaseNormal"; //don't use focused state
            iconButton.focusedFgSprite = $"{itemData.Icon}"; //don't use focused state
            iconButton.hoveredBgSprite = "InfoIconBaseHovered";
            iconButton.hoveredFgSprite = $"{itemData.Icon}Hovered";
            iconButton.pressedBgSprite = "InfoIconBasePressed";
            iconButton.pressedFgSprite = $"{itemData.Icon}Pressed";
            iconButton.normalBgSprite = "InfoIconBaseNormal";
            iconButton.normalFgSprite = $"{itemData.Icon}";
            iconButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            iconButton.textScaleMode = UITextScaleMode.ControlSize;

            iconButton.eventClicked += ButtonClicked;

            this.IconButton = iconButton;
        }

        private void ButtonClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            var infoManager = Singleton<InfoManager>.instance;

            if (infoManager.CurrentMode == itemData.InfoMode && infoManager.CurrentSubMode == itemData.SubInfoMode)
            {
                infoManager.SetCurrentMode(InfoManager.InfoMode.None, InfoManager.SubInfoMode.Default);
            }
            else
            {
                infoManager.SetCurrentMode(itemData.InfoMode, itemData.SubInfoMode);
            }
        }

        public void UpdateLocalizedTooltips(string localizedTooltip)
        {
            this.IconButton.tooltip = localizedTooltip;
            this.PercentButton.tooltip = localizedTooltip;
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
