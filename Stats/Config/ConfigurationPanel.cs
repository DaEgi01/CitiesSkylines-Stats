namespace Stats.Config
{
    using System;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using Stats.Localization;
    using Stats.Ui;

    public class ConfigurationPanel
    {
        private readonly int _space = 16;

        private readonly UIHelperBase _uiHelperBase;
        private readonly ModInfo _modInfo;
        private readonly LanguageResource _languageResource;
        private readonly Configuration _configuration;

        private UISlider? _updateEveryXSeconds;
        private UISlider? _columnCountSlider;
        private UISlider? _itemIconSizeSlider;
        private UISlider? _itemTextScaleSlider;
        private UISlider? _itemPaddingSlider;
        private UIDropDown? _itemTextPositionDropDown;
        private UICheckBox? _autoHideCheckBox;
        private UICheckBox? _hideItemsBelowThresholdCheckBox;
        private UICheckBox? _hideItemsNotAvailableCheckBox;
        private UIDropDown? _itemsDropDown;
        private UICheckBox? _enabledCheckBox;
        private UISlider? _criticalThresholdSlider;
        private UITextField? _sortOrderTextField;

        private ItemData? _selectedItem;

        public ConfigurationPanel(
            UIHelperBase uiHelperBase,
            ModInfo modInfo,
            Configuration configuration,
            LanguageResource languageResource)
        {
            _uiHelperBase = uiHelperBase ?? throw new ArgumentNullException(nameof(uiHelperBase));
            _modInfo = modInfo ?? throw new ArgumentNullException(nameof(modInfo));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
        }

        public MainPanel? MainPanel { get; set; }

        public void Initialize()
        {
            var mainGroupUiHelper = _uiHelperBase.AddGroup(_modInfo.GetDisplayNameWithVersion());
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper)?.self as UIPanel;
            if (mainGroupContentPanel is null)
                throw new IsNullException(nameof(mainGroupContentPanel));

            mainGroupContentPanel.backgroundSprite = string.Empty;

            mainGroupUiHelper.AddButton(_languageResource.Reset, () =>
            {
                string? oldSelectedItemName = _selectedItem?.Name;
                _configuration.Reset();
                UpdateUiFromModel();

                if (MainPanel is null)
                    return;

                MainPanel.UpdateItemPanelButtonSizesAndLayoutAndPanelSize();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
                MainPanel.UpdatePosition();
            });

            mainGroupUiHelper.AddButton(_languageResource.ResetPosition, () =>
            {
                _configuration.ResetPosition();

                if (MainPanel is null)
                    return;

                MainPanel.UpdatePosition();
            });

            mainGroupUiHelper.AddSpace(_space);

            var mainPanelGroupUiHelper = mainGroupUiHelper.AddGroup(_languageResource.MainWindow);
            var mainPanelGroupContentPanel = (mainPanelGroupUiHelper as UIHelper)?.self as UIPanel;
            if (mainPanelGroupContentPanel is null)
                throw new IsNullException(nameof(mainPanelGroupContentPanel));

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

                if (MainPanel is null)
                    return;

                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            });

            _itemPaddingSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemPadding, 0, 30, 1, _configuration.ItemPadding, value =>
            {
                _configuration.ItemPadding = value;
                _configuration.Save();

                if (MainPanel is null)
                    return;

                MainPanel.UpdateItemPanelButtonSizesAndLayoutAndPanelSize();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            });

            _itemIconSizeSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemIconSize, 10, 100, 1, _configuration.ItemIconSize, value =>
            {
                _configuration.ItemIconSize = value;
                _configuration.Save();

                if (MainPanel is null)
                    return;

                MainPanel.UpdateItemPanelButtonSizesAndLayoutAndPanelSize();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            });

            _itemTextScaleSlider = mainPanelGroupUiHelper.AddSliderWithLabel(_languageResource.ItemTextScale, 0.4f, 4, 0.1f, _configuration.ItemTextScale, value =>
            {
                _configuration.ItemTextScale = value;
                _configuration.Save();

                if (MainPanel is null)
                    return;

                MainPanel.UpdateItemPanelButtonSizesAndLayoutAndPanelSize();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            });

            _itemTextPositionDropDown = mainPanelGroupUiHelper.AddDropdown(
                _languageResource.ItemTextPosition,
                ItemTextPosition.All.OrderBy(x => x.Index).Select(x => x.Name).ToArray(),
                _configuration.ItemTextPosition.Index,
                value =>
                {
                    var previousItemTextPosition = _configuration.ItemTextPosition;

                    _configuration.ItemTextPosition = ItemTextPosition.All.ElementAt(value);
                    _configuration.Save();

                    if (MainPanel is null)
                        return;

                    var shouldCreatePercentButtons = previousItemTextPosition == ItemTextPosition.None
                        && _configuration.ItemTextPosition != ItemTextPosition.None;
                    if (shouldCreatePercentButtons)
                    {
                        MainPanel.CreateItemPercentButtons();
                    }

                    var shouldDestroyPercentButtons = previousItemTextPosition != ItemTextPosition.None
                        && _configuration.ItemTextPosition == ItemTextPosition.None;
                    if (shouldDestroyPercentButtons)
                    {
                        MainPanel.DestroyItemPercentButtons();
                    }

                    MainPanel.UpdateItemPanelButtonSizesAndLayoutAndPanelSize();
                    MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
                }) as UIDropDown;

            _autoHideCheckBox = mainPanelGroupUiHelper.AddCheckbox(_languageResource.AutoHide, _configuration.MainPanelAutoHide, isChecked =>
            {
                _configuration.MainPanelAutoHide = isChecked;
                _configuration.Save();
            }) as UICheckBox;

            _hideItemsBelowThresholdCheckBox = mainPanelGroupUiHelper.AddCheckbox(_languageResource.HideItemsBelowThreshold, _configuration.MainPanelHideItemsBelowThreshold, isChecked =>
            {
                _configuration.MainPanelHideItemsBelowThreshold = isChecked;
                _configuration.Save();

                if (MainPanel is null || MainPanel.ItemPanelsInDisplayOrder is null)
                    return;

                foreach (var itemPanel in MainPanel.ItemPanelsInDisplayOrder)
                {
                    itemPanel.UpdatePercentVisibilityAndColor();
                }

                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            }) as UICheckBox;

            _hideItemsNotAvailableCheckBox = mainPanelGroupUiHelper.AddCheckbox(_languageResource.HideItemsNotAvailable, _configuration.MainPanelHideItemsNotAvailable, isChecked =>
            {
                _configuration.MainPanelHideItemsNotAvailable = isChecked;
                _configuration.Save();

                if (MainPanel is null || MainPanel.ItemPanelsInDisplayOrder is null)
                    return;

                foreach (var itemPanel in MainPanel.ItemPanelsInDisplayOrder)
                {
                    itemPanel.UpdatePercentVisibilityAndColor();
                }

                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            }) as UICheckBox;

            var itemGroupUiHelper = mainGroupUiHelper.AddGroup(_languageResource.Items);
            var itemGroupContentPanel = (itemGroupUiHelper as UIHelper)?.self as UIPanel;
            if (itemGroupContentPanel is null)
                throw new IsNullException(nameof(itemGroupContentPanel));

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

            _enabledCheckBox = itemGroupUiHelper.AddCheckbox(_languageResource.Enabled, initialConfigurationItemData.Enabled, isChecked =>
            {
                var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
                configurationItemData.Enabled = isChecked;
                _configuration.Save();

                if (MainPanel is null)
                {
                    return;
                }

                var itemPanel = MainPanel.ItemPanelsInDisplayOrder
                    .Where(x => x.ConfigurationItemData.ItemData == _selectedItem)
                    .FirstOrDefault();

                if (itemPanel is null)
                {
                    return;
                }

                itemPanel.UpdatePercentVisibilityAndColor();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            }) as UICheckBox;
            itemGroupUiHelper.AddSpace(_space);

            _criticalThresholdSlider = itemGroupUiHelper.AddSliderWithLabel(_languageResource.CriticalThreshold, 0, 100, 1, initialConfigurationItemData.CriticalThreshold, value =>
            {
                var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
                configurationItemData.CriticalThreshold = (int)value;
                _configuration.Save();

                if (MainPanel is null)
                    return;

                var itemPanel = MainPanel.ItemPanelsInDisplayOrder
                    .Where(x => x.ConfigurationItemData.ItemData == _selectedItem)
                    .FirstOrDefault();

                if (itemPanel is null)
                    return;

                itemPanel.UpdatePercentVisibilityAndColor();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
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
            _sortOrderTextField!.numericalOnly = true;
        }

        private void UpdateUiFromModel()
        {
            // Temporal coupling - call after initialization.
            _updateEveryXSeconds.value = _configuration.MainPanelUpdateEveryXSeconds;
            _columnCountSlider.value = _configuration.MainPanelColumnCount;
            _itemPaddingSlider.value = _configuration.ItemPadding;
            _itemIconSizeSlider.value = _configuration.ItemIconSize;
            _itemTextScaleSlider.value = _configuration.ItemTextScale;
            _itemTextPositionDropDown.selectedIndex = _configuration.ItemTextPosition.Index;
            _autoHideCheckBox.isChecked = _configuration.MainPanelAutoHide;
            _hideItemsBelowThresholdCheckBox.isChecked = _configuration.MainPanelHideItemsBelowThreshold;
            _hideItemsNotAvailableCheckBox.isChecked = _configuration.MainPanelHideItemsNotAvailable;

            UpdateSelectedItemFromModel();
        }

        private void UpdateSelectedItemFromModel()
        {
            if (_selectedItem is null)
                return;

            var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);

            _enabledCheckBox.isChecked = configurationItemData.Enabled;
            _criticalThresholdSlider.value = configurationItemData.CriticalThreshold;
            _sortOrderTextField.text = configurationItemData.SortOrder.ToString();
        }
    }
}
