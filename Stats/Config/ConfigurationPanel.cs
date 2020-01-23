using ColossalFramework.UI;
using ICities;
using Stats.Localization;
using Stats.Ui;
using System;
using System.Linq;

namespace Stats.Config
{
    public class ConfigurationPanel
    {
        private readonly int _space = 16;

        private readonly UIHelperBase _uiHelperBase;
        private readonly ModFullTitle _modFullTitle;
        private readonly LanguageResource _languageResource;
        private readonly Configuration _configuration;

        private UISlider _updateEveryXSeconds;
        private UISlider _columnCountSlider;
        private UISlider _itemWidthSlider;
        private UISlider _itemHeightSlider;
        private UISlider _itemPaddingSlider;
        private UISlider _itemTextScaleSlider;
        private UICheckBox _autoHideCheckBox;
        private UICheckBox _hideItemsBelowThresholdCheckBox;
        private UICheckBox _hideItemsNotAvailableCheckBox;
        private UIDropDown _itemsDropDown;
        private UICheckBox _enabledCheckBox;
        private UISlider _criticalThresholdSlider;
        private UITextField _sortOrderTextField;

        private ItemData _selectedItem;

        public ConfigurationPanel(
            UIHelperBase uiHelperBase,
            ModFullTitle modFullTitle,
            Configuration configuration,
            LanguageResource languageResource)
        {
            _uiHelperBase = uiHelperBase ?? throw new ArgumentNullException(nameof(uiHelperBase));
            _modFullTitle = modFullTitle ?? throw new ArgumentNullException(nameof(modFullTitle));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
        }

        public MainPanel MainPanel { get; set; }

        public void Initialize()
        {
            var mainGroupUiHelper = _uiHelperBase.AddGroup(_modFullTitle);
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper).self as UIPanel;
            mainGroupContentPanel.backgroundSprite = string.Empty;

            mainGroupUiHelper.AddButton(_languageResource.Reset, () =>
            {
                var oldSelectedItemName = _selectedItem.Name;
                _configuration.Reset();
                UpdateUiFromModel();

                if (MainPanel != null)
                {
                    MainPanel.UpdatePosition();
                }
            });

            mainGroupUiHelper.AddButton(_languageResource.ResetPosition, () =>
            {
                _configuration.ResetPosition();

                if (MainPanel != null)
                {
                    MainPanel.UpdatePosition();
                }
            });

            mainGroupUiHelper.AddSpace(_space);

            var mainPanelGroupUiHelper = mainGroupUiHelper.AddGroup(_languageResource.MainWindow);
            var mainPanelGroupContentPanel = (mainPanelGroupUiHelper as UIHelper).self as UIPanel;
            mainPanelGroupContentPanel.backgroundSprite = string.Empty;

            _updateEveryXSeconds = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.UpdateEveryXSeconds, 0, 30, 1, _configuration.MainPanelUpdateEveryXSeconds, value =>
            {
                _configuration.MainPanelUpdateEveryXSeconds = (int)value;
                _configuration.Save();
            });

            _columnCountSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ColumnCount, 1, ItemData.AllItems.Count, 1, _configuration.MainPanelColumnCount, value =>
            {
                _configuration.MainPanelColumnCount = (int)value;
                _configuration.Save();

                if (MainPanel != null)
                {
                    MainPanel.UpdateItemsLayoutAndSize();
                }
            });

            _itemWidthSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemWidth, 10, 100, 1, _configuration.ItemWidth, value =>
            {
                _configuration.ItemWidth = value;
                _configuration.Save();

                if (MainPanel != null)
                {
                    MainPanel.UpdateItemsLayoutAndSize();
                }
            });

            _itemHeightSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemHeight, 10, 100, 1, _configuration.ItemHeight, value =>
            {
                _configuration.ItemHeight = value;
                _configuration.Save();

                if (MainPanel != null)
                {
                    MainPanel.UpdateItemsLayoutAndSize();
                }
            });

            _itemPaddingSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemPadding, 0, 30, 1, _configuration.ItemPadding, value =>
            {
                _configuration.ItemPadding = value;
                _configuration.Save();

                if (MainPanel != null)
                {
                    MainPanel.UpdateItemsLayoutAndSize();
                }
            });

            _itemTextScaleSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemTextScale, 0, 4, 0.1f, _configuration.ItemTextScale, value =>
            {
                _configuration.ItemTextScale = value;
                _configuration.Save();

                if (MainPanel != null)
                {
                    MainPanel.UpdateItemsLayoutAndSize();
                }
            });

            _autoHideCheckBox = mainPanelGroupUiHelper.AddCheckbox(_languageResource.AutoHide, _configuration.MainPanelAutoHide, _checked =>
            {
                _configuration.MainPanelAutoHide = _checked;
                _configuration.Save();
            }) as UICheckBox;

            _hideItemsBelowThresholdCheckBox = mainPanelGroupUiHelper.AddCheckbox(_languageResource.HideItemsBelowThreshold, _configuration.MainPanelHideItemsBelowThreshold, _checked =>
            {
                _configuration.MainPanelHideItemsBelowThreshold = _checked;
                _configuration.Save();

                if (MainPanel != null)
                {
                    foreach (var itemPanel in MainPanel.ItemPanelsInDisplayOrder)
                    {
                        itemPanel.UpdatePercentVisibilityAndColor();
                    }

                    MainPanel.UpdateItemsLayoutAndSize();
                }
            }) as UICheckBox;

            _hideItemsNotAvailableCheckBox = mainPanelGroupUiHelper.AddCheckbox(_languageResource.HideItemsNotAvailable, _configuration.MainPanelHideItemsNotAvailable, _checked =>
            {
                _configuration.MainPanelHideItemsNotAvailable = _checked;
                _configuration.Save();

                if (MainPanel != null)
                {
                    foreach (var itemPanel in MainPanel.ItemPanelsInDisplayOrder)
                    {
                        itemPanel.UpdatePercentVisibilityAndColor();
                    }

                    MainPanel.UpdateItemsLayoutAndSize();
                }
            }) as UICheckBox;

            var itemGroupUiHelper = mainGroupUiHelper.AddGroup(_languageResource.Items);
            var itemGroupContentPanel = (itemGroupUiHelper as UIHelper).self as UIPanel;
            itemGroupContentPanel.backgroundSprite = string.Empty;

            var itemStringArray = ItemData.AllItems
                .Select(itemData => _languageResource.GetLocalizedItemString(itemData))
                .ToArray();
            var firstSelectedIndex = default(int);
            _selectedItem = ItemData.AllItems[firstSelectedIndex];
            _itemsDropDown = itemGroupUiHelper.AddDropdown(" ", itemStringArray, firstSelectedIndex, (index) =>
            {
                _selectedItem = ItemData.AllItems[index];
                UpdateSelectedItemFromModel();
            }) as UIDropDown;
            var itemsDropdownPanel = _itemsDropDown.parent as UIPanel;
            itemsDropdownPanel.RemoveUIComponent(itemsDropdownPanel.Find("Label"));

            var initialConfigurationItemData = _configuration.GetConfigurationItemData(_selectedItem);

            _enabledCheckBox = itemGroupUiHelper.AddCheckbox(_languageResource.Enabled, initialConfigurationItemData.Enabled, _checked =>
            {
                var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
                configurationItemData.Enabled = _checked;
                _configuration.Save();

                if (MainPanel == null)
                {
                    return;
                }

                var itemPanel = MainPanel.ItemPanelsInDisplayOrder
                    .Where(x => x.ConfigurationItemData.ItemData == _selectedItem)
                    .FirstOrDefault();

                if (itemPanel == null)
                {
                    return;
                }

                itemPanel.UpdatePercentVisibilityAndColor();
                MainPanel.UpdateItemsLayoutAndSize();
            }) as UICheckBox;
            itemGroupUiHelper.AddSpace(_space);

            _criticalThresholdSlider = itemGroupUiHelper.AddSliderWithLabel(_languageResource.CriticalThreshold, 0, 100, 1, initialConfigurationItemData.CriticalThreshold, value =>
            {
                var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
                configurationItemData.CriticalThreshold = (int)value;
                _configuration.Save();

                if (MainPanel == null)
                {
                    return;
                }

                var itemPanel = MainPanel.ItemPanelsInDisplayOrder
                    .Where(x => x.ConfigurationItemData.ItemData == _selectedItem)
                    .FirstOrDefault();

                if (itemPanel == null)
                {
                    return;
                }

                itemPanel.UpdatePercentVisibilityAndColor();
                MainPanel.UpdateItemsLayoutAndSize();
            });

            _sortOrderTextField = itemGroupUiHelper.AddTextfield(_languageResource.SortOrder, initialConfigurationItemData.SortOrder.ToString(), (v) => { }, v =>
             {
                 var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
                 configurationItemData.SortOrder = int.Parse(v);
                 _configuration.Save();

                 if (MainPanel != null)
                 {
                     MainPanel.UpdateSortOrder();
                 }
             }) as UITextField;
            _sortOrderTextField.numericalOnly = true;
        }

        private void UpdateUiFromModel()
        {
            _updateEveryXSeconds.value = _configuration.MainPanelUpdateEveryXSeconds;
            _columnCountSlider.value = _configuration.MainPanelColumnCount;
            _itemWidthSlider.value = _configuration.ItemWidth;
            _itemHeightSlider.value = _configuration.ItemHeight;
            _itemPaddingSlider.value = _configuration.ItemPadding;
            _itemTextScaleSlider.value = _configuration.ItemTextScale;
            _autoHideCheckBox.isChecked = _configuration.MainPanelAutoHide;
            _hideItemsBelowThresholdCheckBox.isChecked = _configuration.MainPanelHideItemsBelowThreshold;
            _hideItemsNotAvailableCheckBox.isChecked = _configuration.MainPanelHideItemsNotAvailable;

            UpdateSelectedItemFromModel();
        }

        private void UpdateSelectedItemFromModel()
        {
            var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
            _enabledCheckBox.isChecked = configurationItemData.Enabled;
            _criticalThresholdSlider.value = configurationItemData.CriticalThreshold;
            _sortOrderTextField.text = configurationItemData.SortOrder.ToString();
        }
    }
}
