using Stats.Localization;
using ColossalFramework.UI;
using ICities;
using System;
using System.Linq;
using Stats.Model;

namespace Stats.Config
{
    public class ConfigurationPanel
    {
        private readonly int space = 16;

        private readonly UIHelperBase uiHelperBase;
        private readonly ModFullTitle modFullTitle;
        private readonly LanguageResource languageResource;
        private readonly Configuration configuration;

        private UISlider columnCountSlider;
        private UISlider itemWidthSlider;
        private UISlider itemHeightSlider;
        private UISlider itemPaddingSlider;
        private UISlider itemTextScaleSlider;
        private UICheckBox autoHideCheckBox;
        private UICheckBox hideItemsBelowThresholdCheckBox;
        private UICheckBox hideItemsNotAvailableCheckBox;
        private UIDropDown itemsDropDown;
        private UICheckBox enabledCheckBox;
        private UISlider criticalThresholdSlider;
        private UITextField sortOrderTextField;

        private readonly ItemsInIndexOrder itemsInIndexOrder;
        private Item selectedItem;

        public ConfigurationPanel(
            UIHelperBase uiHelperBase,
            ModFullTitle modFullTitle,
            Configuration configuration,
            LanguageResource languageResource,
            ItemsInIndexOrder itemsInIndexOrder)
        {
            this.uiHelperBase = uiHelperBase ?? throw new ArgumentNullException(nameof(uiHelperBase));
            this.modFullTitle = modFullTitle ?? throw new ArgumentNullException(nameof(modFullTitle));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));

            this.itemsInIndexOrder = itemsInIndexOrder;
        }

        public void Initialize()
        {
            var mainGroupUiHelper = this.uiHelperBase.AddGroup(this.modFullTitle);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

            mainGroupUiHelper.AddButton(this.languageResource.Reset, () =>
            {
                var oldSelectedItemName = this.selectedItem.ItemData.Name;
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

            this.itemWidthSlider = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemWidth, 10, 100, 1, this.configuration.ItemWidth, value =>
            {
                this.configuration.ItemWidth = value;
                this.configuration.Save();
            });

            this.itemHeightSlider = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemHeight, 10, 100, 1, this.configuration.ItemHeight, value =>
            {
                this.configuration.ItemHeight = value;
                this.configuration.Save();
            });

            this.itemPaddingSlider = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemPadding, 0, 30, 1, this.configuration.ItemPadding, value =>
            {
                this.configuration.ItemPadding = value;
                this.configuration.Save();
            });

            this.itemTextScaleSlider = mainPanelGroupUiHelper.AddSliderWithLabel(languageResource.ItemTextScale, 0, 4, 0.1f, this.configuration.ItemTextScale, value =>
            {
                this.configuration.ItemTextScale = value;
                this.configuration.Save();
            });

            this.autoHideCheckBox = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.AutoHide, this.configuration.MainPanelAutoHide, _checked =>
            {
                this.configuration.MainPanelAutoHide = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.hideItemsBelowThresholdCheckBox = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsBelowThreshold, this.configuration.MainPanelHideItemsBelowThreshold, _checked =>
            {
                this.configuration.MainPanelHideItemsBelowThreshold = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            this.hideItemsNotAvailableCheckBox = mainPanelGroupUiHelper.AddCheckbox(this.languageResource.HideItemsNotAvailable, this.configuration.MainPanelHideItemsNotAvailable, _checked =>
            {
                this.configuration.MainPanelHideItemsNotAvailable = _checked;
                this.configuration.Save();
            }) as UICheckBox;

            var itemGroupUiHelper = mainGroupUiHelper.AddGroup(this.languageResource.Items);
            var itemGroupContentPanel = (itemGroupUiHelper as UIHelper).self as UIPanel;
            itemGroupContentPanel.backgroundSprite = string.Empty;

            var itemStringArray = ItemData.AllItems
                .Select(x => this.languageResource.GetItemLocalizedItemString(x))
                .ToArray();
            var firstSelectedIndex = default(int);
            this.selectedItem = itemsInIndexOrder.GetItem(ItemData.AllItems[firstSelectedIndex]);
            this.itemsDropDown = itemGroupUiHelper.AddDropdown(" ", itemStringArray, firstSelectedIndex, (index) => {
                this.selectedItem = itemsInIndexOrder.GetItem(ItemData.AllItems[index]);
                this.UpdateSelectedItemFromModel();
            }) as UIDropDown;
            var itemsDropdownPanel = this.itemsDropDown.parent as UIPanel;
            itemsDropdownPanel.RemoveUIComponent(itemsDropdownPanel.Find("Label"));

            this.enabledCheckBox = itemGroupUiHelper.AddCheckbox(languageResource.Enabled, this.selectedItem.Enabled, _checked =>
            {
                this.selectedItem.Enabled = _checked;
                this.configuration.Save();
            }) as UICheckBox;
            itemGroupUiHelper.AddSpace(space);

            this.criticalThresholdSlider = itemGroupUiHelper.AddSliderWithLabel(languageResource.CriticalThreshold, 0, 100, 1, this.selectedItem.CriticalThreshold, value =>
            {
                this.selectedItem.CriticalThreshold = (int)value;
                this.configuration.Save();
            });

            this.sortOrderTextField = itemGroupUiHelper.AddTextfield(languageResource.SortOrder, this.selectedItem.SortOrder.ToString(), v =>
            {
                this.selectedItem.SortOrder = int.Parse(v);
                this.configuration.Save();
            }) as UITextField;
            this.sortOrderTextField.numericalOnly = true;
        }

        private void UpdateUiFromModel()
        {
            this.columnCountSlider.value = this.configuration.MainPanelColumnCount;
            this.itemWidthSlider.value = this.configuration.ItemWidth;
            this.itemHeightSlider.value = this.configuration.ItemHeight;
            this.itemPaddingSlider.value = this.configuration.ItemPadding;
            this.itemTextScaleSlider.value = this.configuration.ItemTextScale;
            this.autoHideCheckBox.isChecked = this.configuration.MainPanelAutoHide;
            this.hideItemsBelowThresholdCheckBox.isChecked = this.configuration.MainPanelHideItemsBelowThreshold;
            this.hideItemsNotAvailableCheckBox.isChecked = this.configuration.MainPanelHideItemsNotAvailable;

            this.UpdateSelectedItemFromModel();
        }

        private void UpdateSelectedItemFromModel()
        {
            this.enabledCheckBox.isChecked = this.selectedItem.Enabled;
            this.criticalThresholdSlider.value = this.selectedItem.CriticalThreshold;
            this.sortOrderTextField.text = this.selectedItem.SortOrder.ToString();
        }
    }
}
