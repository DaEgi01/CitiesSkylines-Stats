using Stats.Localization;
using ColossalFramework.UI;
using ICities;
using System;
using System.Linq;

namespace Stats.Configuration
{
    public class ConfigurationPanel
    {
        private readonly int space = 16;

        private readonly UIHelperBase uiHelperBase;
        private readonly ModFullTitle modFullTitle;
        private readonly LanguageResourceModel languageResource;
        private readonly ConfigurationModel configuration;

        private UISlider updateEveryXSeconds;
        private UISlider columnCountSlider;
        private UISlider itemWidth;
        private UISlider itemHeight;
        private UISlider itemPadding;
        private UISlider itemTextScale;
        private UICheckBox autoHide;
        private UICheckBox hideItemsBelowThreshold;
        private UICheckBox hideItemsNotAvailable;
        private UIDropDown items;
        private UICheckBox enabled;
        private UISlider criticalThreshold;
        private UITextField sortOrder;

        private ConfigurationItemModel selectedItem;

        public ConfigurationPanel(UIHelperBase uiHelperBase, ModFullTitle modFullTitle, ConfigurationModel configuration, LanguageResourceModel languageResource)
        {
            this.uiHelperBase = uiHelperBase ?? throw new ArgumentNullException(nameof(uiHelperBase));
            this.modFullTitle = modFullTitle ?? throw new ArgumentNullException(nameof(modFullTitle));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
        }

        public void Initialize()
        {
            var mainGroupUiHelper = this.uiHelperBase.AddGroup(this.modFullTitle);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

            mainGroupUiHelper.AddButton(this.languageResource.Reset, () =>
            {
                var oldSelectedItemName = this.selectedItem.Item.Name;
                this.configuration.Reset();

                this.UpdateUiFromModel();
            });

            mainGroupUiHelper.AddButton(this.languageResource.ResetPosition, () =>
            {
                this.configuration.ResetPosition();
            });

            mainGroupUiHelper.AddSpace(space);

            var mainPanelGroupUiHelper = mainGroupUiHelper.AddGroup(languageResource.MainWindow);
            var mainPanelGroupContentPanel = (mainPanelGroupUiHelper as UIHelper).self as UIPanel;
            mainPanelGroupContentPanel.backgroundSprite = string.Empty;

            this.columnCountSlider = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ColumnCount, 1, 30, 1, this.configuration.MainPanelColumnCount, value =>
            {
                this.configuration.MainPanelColumnCount = (int)value;
                this.configuration.Save();
            });

            this.itemWidth = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemWidth, 10, 100, 1, this.configuration.ItemWidth, value =>
            {
                this.configuration.ItemWidth = value;
                this.configuration.Save();
            });

            this.itemHeight = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemHeight, 10, 100, 1, this.configuration.ItemHeight, value =>
            {
                this.configuration.ItemHeight = value;
                this.configuration.Save();
            });

            this.itemPadding = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemPadding, 0, 30, 1, this.configuration.ItemPadding, value =>
            {
                this.configuration.ItemPadding = value;
                this.configuration.Save();
            });

            this.itemTextScale = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemTextScale, 0, 4, 0.1f, this.configuration.ItemTextScale, value =>
            {
                this.configuration.ItemTextScale = value;
                this.configuration.Save();
            });

            this.autoHide = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.AutoHide, this.configuration.MainPanelAutoHide, _checked =>
            {
                this.configuration.MainPanelAutoHide = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.hideItemsBelowThreshold = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsBelowThreshold, this.configuration.MainPanelHideItemsBelowThreshold, _checked =>
            {
                this.configuration.MainPanelHideItemsBelowThreshold = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.hideItemsNotAvailable = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsNotAvailable, this.configuration.MainPanelHideItemsNotAvailable, _checked =>
            {
                this.configuration.MainPanelHideItemsNotAvailable = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            var itemGroupUiHelper = mainGroupUiHelper.AddGroup(this.languageResource.Items);
            var itemGroupContentPanel = (itemGroupUiHelper as UIHelper).self as UIPanel;
            itemGroupContentPanel.backgroundSprite = string.Empty;

            var itemStringArray = Item.AllItems
                .Select(x => this.languageResource.GetItemLocalizedItemString(x))
                .ToArray();
            var firstSelectedIndex = default(int);
            this.selectedItem = this.configuration.GetConfigurationItem(Item.AllItems[firstSelectedIndex]);
            this.items = itemGroupUiHelper.AddDropdown(" ", itemStringArray, firstSelectedIndex, (index) => {
                this.selectedItem = this.configuration.GetConfigurationItem(Item.AllItems[index]);
                this.UpdateSelectedItemFromModel();
            }) as UIDropDown;
            var itemsDropdownPanel = this.items.parent as UIPanel;
            itemsDropdownPanel.RemoveUIComponent(itemsDropdownPanel.Find("Label"));

            this.enabled = itemGroupUiHelper.AddCheckbox(languageResource.Enabled, this.selectedItem.Enabled, _checked =>
            {
                this.selectedItem.Enabled = _checked;
                this.configuration.Save();
            }) as UICheckBox;
            itemGroupUiHelper.AddSpace(space);

            this.criticalThreshold = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.selectedItem.CriticalThreshold, value =>
            {
                this.selectedItem.CriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.sortOrder = itemGroupUiHelper.AddTextfield(languageResource.SortOrder, this.selectedItem.SortOrder.ToString(), v =>
            {
                this.selectedItem.SortOrder = int.Parse(v);
                this.configuration.Save();
            }) as UITextField;
            this.sortOrder.numericalOnly = true;
        }

        private void UpdateUiFromModel()
        {
            this.columnCountSlider.value = this.configuration.MainPanelColumnCount;
            this.itemWidth.value = this.configuration.ItemWidth;
            this.itemHeight.value = this.configuration.ItemHeight;
            this.itemPadding.value = this.configuration.ItemPadding;
            this.itemTextScale.value = this.configuration.ItemTextScale;
            this.autoHide.isChecked = this.configuration.MainPanelAutoHide;
            this.hideItemsBelowThreshold.isChecked = this.configuration.MainPanelHideItemsBelowThreshold;
            this.hideItemsNotAvailable.isChecked = this.configuration.MainPanelHideItemsNotAvailable;

            this.UpdateSelectedItemFromModel();
        }

        private void UpdateSelectedItemFromModel()
        {
            this.enabled.isChecked = this.selectedItem.Enabled;
            this.criticalThreshold.value = this.selectedItem.CriticalThreshold;
            this.sortOrder.text = this.selectedItem.SortOrder.ToString();
        }
    }
}
