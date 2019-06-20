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
        private ItemData itemData;
        private ConfigurationModel configuration;
        private LanguageResourceModel languageResourceModel;

        private bool itemEnabled;
        private int? percent;
        private int criticalThreshold;

        //TODO refactor to localized item instead
        public void Initialize(ItemData itemData, ConfigurationModel configuration, LanguageResourceModel languageResourceModel)
        {
            this.itemData = itemData ?? throw new ArgumentNullException(nameof(itemData));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.languageResourceModel = languageResourceModel ?? throw new ArgumentNullException(nameof(languageResourceModel));

            this.width = configuration.ItemWidth;
            this.height = configuration.ItemHeight;

            this.CreateAndAddIconButton();
            this.CreateAndAddPercentButton();
        }

        public UIButton IconButton { get; private set; }
        public UIButton PercentButton { get; private set; }

        public bool ItemEnabled
        {
            get => this.itemEnabled;
            set
            {
                this.itemEnabled = value;
                this.UpdateItemDisplay();
            }
        }
        public int? Percent
        {
            get => this.percent;
            set
            {
                this.percent = value;
                this.UpdateItemDisplay();
            }
        }
        public int CriticalThreshold
        {
            get => this.criticalThreshold;
            set
            {
                this.criticalThreshold = value;
                this.UpdateItemDisplay();
            }
        }
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

        public void UpdateLocalizedTooltips()
        {
            var localizedTooltip = this.languageResourceModel.GetItemLocalizedString(itemData);

            this.IconButton.tooltip = localizedTooltip;
            this.PercentButton.tooltip = localizedTooltip;
        }

        private void UpdateItemDisplay()
        {
            var oldItemVisible = this.isVisible;
            var newItemVisible = GetItemVisibility(ItemEnabled, Percent, CriticalThreshold);

            this.isVisible = newItemVisible;

            if (newItemVisible)
            {
                this.PercentButton.text = GetUsagePercentString(Percent);
                this.PercentButton.textColor = GetItemTextColor(Percent, CriticalThreshold);
                this.PercentButton.focusedColor = GetItemTextColor(Percent, CriticalThreshold);
                this.PercentButton.hoveredTextColor = GetItemHoveredTextColor(Percent, CriticalThreshold);
            }

            if (oldItemVisible != newItemVisible)
            {
                this.OnLayoutPropertyChanged();
            }
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

        public event Action LayoutPropertyChanged;

        private void OnLayoutPropertyChanged()
        {
            this.LayoutPropertyChanged?.Invoke();
        }
    }
}
