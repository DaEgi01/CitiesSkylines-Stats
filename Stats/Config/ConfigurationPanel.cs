namespace Stats.Config
{
    using System;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using Stats.Localization;
    using Stats.Ui;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ConfigurationPanel
    {
        private static readonly string _colorFieldsUiPanelName = "Stats.ColorFieldsPanel";

        private readonly int _space = 16;

        private readonly ModInfo _modInfo;
        private readonly LanguageResource _languageResource;
        private readonly Configuration _configuration;

        private UIColorField? _accentColorField;
        private UIColorField? _foregroundColorField;
        private UIColorField? _backgroundColorField;
        private UISlider? _backgroundTransparency;
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
            ModInfo modInfo,
            Configuration configuration,
            LanguageResource languageResource)
        {
            if (modInfo is null)
                throw new ArgumentNullException(nameof(modInfo));
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));
            if (languageResource is null)
                throw new ArgumentNullException(nameof(languageResource));

            _modInfo = modInfo;
            _configuration = configuration;
            _languageResource = languageResource;
        }

        public MainPanel? MainPanel { get; set; }

        public void Initialize(UIHelperBase uiHelperBase)
        {
            var mainGroupUiHelper = uiHelperBase.AddGroup(_modInfo.GetDisplayNameWithVersion());
            var mainGroupContentPanel = (mainGroupUiHelper as UIHelper)?.self as UIPanel;
            if (mainGroupContentPanel is null)
                throw new IsNullException(nameof(mainGroupContentPanel));

            mainGroupContentPanel.backgroundSprite = string.Empty;

            mainGroupUiHelper.AddButton(_languageResource.Reset, () =>
            {
                _configuration.Reset();
                UpdateUiFromModel();

                if (MainPanel is null)
                    return;

                MainPanel.UpdateMainPanelAndItemColors();
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

            if (SceneManager.GetActiveScene().name == "Game")
            {
                // Due to timing issues, all color fields are initialized during OnLevelLoaded.
                // Instead a placeholder is injected here to reserve the proper position inside the panel.
                var colorFieldsUiPanel = mainPanelGroupContentPanel.AddUIComponent<UIPanel>();
                colorFieldsUiPanel.height = 0;
                colorFieldsUiPanel.autoLayout = true;
                colorFieldsUiPanel.autoLayoutDirection = LayoutDirection.Vertical;
                colorFieldsUiPanel.autoFitChildrenVertically = true;
                colorFieldsUiPanel.name = _colorFieldsUiPanelName;

                if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
                {
                    InitializeColorFields();
                }
            }
            else
            {
                var colorFieldOutOfGameLabel = mainPanelGroupContentPanel.AddUIComponent<UILabel>();
                colorFieldOutOfGameLabel.text = _languageResource.ColorChangeDuringGameplay;
                colorFieldOutOfGameLabel.textScale = 1.125f;

                var colorFieldOutOfGameHelper = new UIHelper(mainPanelGroupContentPanel);
                colorFieldOutOfGameHelper.AddSpace(_space);
            }

            _backgroundTransparency = mainPanelGroupUiHelper.AddSliderWithLabel(
                _languageResource.Transparency,
                0f,
                1f,
                0.01f,
                1f,
                value =>
                {
                    var color = _configuration.MainPanelBackgroundColor;

                    _configuration.MainPanelBackgroundColor = new Color32(
                        color.r,
                        color.g,
                        color.b,
                        (byte)(value * 255));
                    _configuration.Save();

                    if (MainPanel is null)
                        return;

                    MainPanel.UpdateMainPanelAndItemColors();
                });

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
                    .FirstOrDefault(x => x.ConfigurationItemData.ItemData == _selectedItem);

                if (itemPanel is null)
                {
                    return;
                }

                itemPanel.UpdatePercentVisibilityAndColor();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            }) as UICheckBox;
            itemGroupUiHelper.AddSpace(_space);

            _criticalThresholdSlider = itemGroupUiHelper.AddSliderWithLabel(
                _languageResource.CriticalThreshold,
                0,
                100,
                1,
                initialConfigurationItemData.CriticalThreshold,
                value =>
            {
                var configurationItemData = _configuration.GetConfigurationItemData(_selectedItem);
                configurationItemData.CriticalThreshold = (int)value;
                _configuration.Save();

                if (MainPanel is null)
                    return;

                var itemPanel = MainPanel.ItemPanelsInDisplayOrder
                    .FirstOrDefault(x => x.ConfigurationItemData.ItemData == _selectedItem);

                if (itemPanel is null)
                    return;

                itemPanel.UpdatePercentVisibilityAndColor();
                MainPanel.UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            });

            _sortOrderTextField = itemGroupUiHelper.AddTextfield(_languageResource.SortOrder, initialConfigurationItemData.SortOrder.ToString(), _ => { }, v =>
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

        public void InitializeColorFields()
        {
            var colorFieldsUiPanel = GameObject.Find(_colorFieldsUiPanelName)
                .GetComponent<UIPanel>();
            if (colorFieldsUiPanel is null)
                throw new IsNullException(nameof(colorFieldsUiPanel));

            var colorFieldsUiPanelHelper = new UIHelper(colorFieldsUiPanel);

            _accentColorField = colorFieldsUiPanelHelper.AddColorFieldWithLabel(
                _languageResource.AccentColor,
                _configuration.MainPanelAccentColor,
                selectedColorChanged: (_, value) =>
                {
                    _configuration.MainPanelAccentColor = value;
                    _configuration.Save();

                    if (MainPanel is null)
                        return;

                    MainPanel.UpdateMainPanelAndItemColors();
                });

            _foregroundColorField = colorFieldsUiPanelHelper.AddColorFieldWithLabel(
                _languageResource.ForegroundColor,
                _configuration.MainPanelForegroundColor,
                selectedColorChanged: (_, value) =>
                {
                    _configuration.MainPanelForegroundColor = value;
                    _configuration.Save();

                    if (MainPanel is null)
                        return;

                    MainPanel.UpdateMainPanelAndItemColors();
                });

            _backgroundColorField = colorFieldsUiPanelHelper.AddColorFieldWithLabel(
                _languageResource.BackgroundColor,
                _configuration.MainPanelBackgroundColor,
                selectedColorChanged: (_, value) =>
                {
                    Color32 newColor = new Color32(
                        (byte)(value.r * byte.MaxValue),
                        (byte)(value.g * byte.MaxValue),
                        (byte)(value.b * byte.MaxValue),
                        _configuration.MainPanelBackgroundColor.a);

                    _configuration.MainPanelBackgroundColor = newColor;
                    _configuration.Save();

                    if (MainPanel is null)
                        return;

                    MainPanel.UpdateMainPanelAndItemColors();
                });
        }

        private void UpdateUiFromModel()
        {
            // Temporal coupling - call after initialization.
            if (_accentColorField is not null)
            {
                _accentColorField.selectedColor = _configuration.MainPanelAccentColor;
            }

            if (_foregroundColorField is not null)
            {
                _foregroundColorField.selectedColor = _configuration.MainPanelForegroundColor;
            }

            if (_backgroundColorField is not null)
            {
                _backgroundColorField.selectedColor = _configuration.MainPanelBackgroundColor;
            }

            _backgroundTransparency.value = _configuration.MainPanelBackgroundColor.a;
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
