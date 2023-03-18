namespace Stats.Ui
{
    using System;
    using System.Collections;
    using System.Linq;
    using ColossalFramework.UI;
    using Stats.Config;
    using Stats.Localization;
    using UnityEngine;

    public class MainPanel : UIPanel
    {
        private UIPanel _uiBackgroundPanel;
        private UIDragHandle? _uiDragHandle;
        private string? _modSystemName;
        private Configuration? _configuration;
        private LanguageResource? _languageResource;
        private GameEngineService? _gameEngineService;
        private InfoManager? _infoManager;
        private PercentStringCache? _percentStringCache;

        private ItemPanel[]? _itemPanelsInDisplayOrder;

        public ItemPanel[]? ItemPanelsInDisplayOrder => _itemPanelsInDisplayOrder;

        public void Initialize(
            string modSystemName,
            Configuration configuration,
            LanguageResource languageResource,
            GameEngineService gameEngineService,
            InfoManager infoManager,
            PercentStringCache percentStringCache)
        {
            if (modSystemName is null)
                throw new ArgumentNullException(nameof(modSystemName));
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));
            if (languageResource is null)
                throw new ArgumentNullException(nameof(languageResource));
            if (gameEngineService is null)
                throw new ArgumentNullException(nameof(gameEngineService));
            if (infoManager is null)
                throw new ArgumentNullException(nameof(infoManager));
            if (percentStringCache is null)
                throw new ArgumentNullException(nameof(percentStringCache));

            _modSystemName = modSystemName;
            _configuration = configuration;
            _languageResource = languageResource;
            _gameEngineService = gameEngineService;
            _infoManager = infoManager;
            _percentStringCache = percentStringCache;

            if (_configuration.MainPanelColumnCount < 1)
            {
                throw new Exception($"'{nameof(_configuration.MainPanelColumnCount)}' parameter must be bigger or equal to 1.");
            }

            name = modSystemName + "MainPanel";
            backgroundSprite = string.Empty;
            isInteractive = false;
            zOrder = 10;

            CreateAndAddDragHandle();
            CreateAndAddBackgroundPanel();
            CreateAndAddAllUiItems();

            UpdateItemPanelButtonSizesAndLayoutAndPanelSize();
            UpdatePanelLayoutAndPanelSizeAndClampToScreen();
            UpdatePosition();

            _uiDragHandle.eventMouseUp += UiDragHandle_eventMouseUp;

            StartCoroutine(KeepUpdatingUICoroutine());
        }

        public override void OnDestroy()
        {
            _uiDragHandle.eventMouseUp -= UiDragHandle_eventMouseUp;

            base.OnDestroy();
        }

        public void CreateItemPercentButtons()
        {
            for (var i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                var itemPanel = _itemPanelsInDisplayOrder[i];
                itemPanel.CreateAndAddPercentButton();
            }
        }

        public void DestroyItemPercentButtons()
        {
            for (var i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                var itemPanel = _itemPanelsInDisplayOrder[i];
                itemPanel.DestroyPercentButton();
            }
        }

        public void UpdateItemPanelButtonSizesAndLayoutAndPanelSize()
        {
            for (var i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                var itemPanel = _itemPanelsInDisplayOrder[i];
                itemPanel.UpdateButtonSizesAndLayoutAndPanelSize();
            }
        }

        public void UpdateSortOrder()
        {
            _itemPanelsInDisplayOrder = _itemPanelsInDisplayOrder
                .OrderBy(x => x.ConfigurationItemData.SortOrder)
                .ToArray();

            UpdatePanelLayout();
        }

        public void UpdatePanelLayoutAndPanelSizeAndClampToScreen()
        {
            UpdatePanelLayout();
            UpdatePanelSize();
            ClampToScreen();
        }

        public override void Update()
        {
            bool hide = _configuration.MainPanelAutoHide && !containsMouse;
            opacity = hide ? 0f : 1f;
        }

        public void UpdatePanelLayout()
        {
            var lastLayoutPosition = Vector2.zero;
            int index = 0;

            for (int i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                var currentItem = _itemPanelsInDisplayOrder[i];
                if (!currentItem.isVisible)
                {
                    continue;
                }

                var layoutPosition = new Vector2(
                    index % _configuration.MainPanelColumnCount,
                    index / _configuration.MainPanelColumnCount);

                currentItem.relativePosition = CalculateRelativePosition(layoutPosition);

                lastLayoutPosition = CalculateNextLayoutPosition(lastLayoutPosition);
                index++;
            }
        }

        public void UpdatePanelSize()
        {
            var visibleItemCount = GetVisibleItemsCount(_itemPanelsInDisplayOrder);
            if (visibleItemCount == 0)
            {
                isVisible = false;
                return;
            }

            isVisible = true;

            var newWidth = CalculatePanelWidth(visibleItemCount);
            var newHeight = CalculatePanelHeight(visibleItemCount);

            width = newWidth;
            height = newHeight;

            _uiBackgroundPanel.width = newWidth;
            _uiBackgroundPanel.height = newHeight;

            _uiDragHandle.width = newWidth;
            _uiDragHandle.height = newHeight;
        }

        public void UpdatePosition()
        {
            relativePosition = _configuration.MainPanelPosition;
        }

        public void UpdateLocalization()
        {
            for (int i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                _itemPanelsInDisplayOrder[i].UpdateLocalization();
            }
        }

        public void UpdateMainPanelAndItemColors()
        {
            _uiBackgroundPanel.color = _configuration.MainPanelBackgroundColor;

            for (int i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                _itemPanelsInDisplayOrder[i].UpdatePercentVisibilityAndColor();
            }
        }

        private static int GetVisibleItemsCount(ItemPanel[] itemPanels)
        {
            var result = 0;
            for (var i = 0; i < itemPanels.Length; i++)
            {
                if (itemPanels[i].isVisible)
                {
                    result += 1;
                }
            }

            return result;
        }

        private void CreateAndAddBackgroundPanel()
        {
            var backgroundPanel = AddUIComponent<UIPanel>();
            backgroundPanel.name = _modSystemName + "BackgroundPanel";
            backgroundPanel.relativePosition = Vector2.zero;
            backgroundPanel.SendToBack();
            backgroundPanel.backgroundSprite = "GenericPanelWhite";
            backgroundPanel.color = _configuration.MainPanelBackgroundColor;
            _uiBackgroundPanel = backgroundPanel;
        }

        private void CreateAndAddDragHandle()
        {
            var dragHandle = AddUIComponent<UIDragHandle>();
            dragHandle.name = _modSystemName + "DragHandle";
            dragHandle.relativePosition = Vector2.zero;
            dragHandle.target = this;
            dragHandle.constrainToScreen = true;
            dragHandle.SendToBack();
            _uiDragHandle = dragHandle;
        }

        private void CreateAndAddAllUiItems()
        {
            var mapHasSnowDumps = GameEngineService.HasMapSnowDumps();

            _itemPanelsInDisplayOrder = ItemData.AllItems
                .Where(i => mapHasSnowDumps || (i != ItemData.SnowDump && i != ItemData.SnowDumpVehicles))
                .Select(i => CreateUiItemAndAddButtons(
                    _configuration.GetConfigurationItemData(i),
                    _infoManager,
                    _percentStringCache))
                .OrderBy(x => x.ConfigurationItemData.SortOrder)
                .ToArray();
        }

        private ItemPanel CreateUiItemAndAddButtons(
            ConfigurationItemData configurationItemData,
            InfoManager infoManager,
            PercentStringCache percentStringCache)
        {
            var itemPanel = CreateAndAddItemPanel();
            itemPanel.Initialize(_configuration, configurationItemData, _languageResource, _gameEngineService, infoManager, percentStringCache);
            return itemPanel;
        }

        private ItemPanel CreateAndAddItemPanel()
        {
            var itemPanel = AddUIComponent<ItemPanel>();
            itemPanel.zOrder = zOrder;
            return itemPanel;
        }

        private Vector2 CalculateNextLayoutPosition(Vector2 position)
        {
            return position.x < _configuration.MainPanelColumnCount - 1
                ? new Vector2(position.x + 1, position.y)
                : new Vector2(0, position.y + 1);
        }

        private Vector3 CalculateRelativePosition(Vector2 layoutPosition)
        {
            var posX = ((layoutPosition.x + 1) * _configuration.ItemPadding)
                + (layoutPosition.x * _itemPanelsInDisplayOrder[0].width);
            var posY = ((layoutPosition.y + 1) * _configuration.ItemPadding)
                + (layoutPosition.y * _itemPanelsInDisplayOrder[0].height);

            return new Vector3(posX, posY);
        }

        private float CalculatePanelWidth(int visibleItemCount)
        {
            if (visibleItemCount <= 0)
            {
                return 0f;
            }

            if (visibleItemCount < _configuration.MainPanelColumnCount)
            {
                return ((visibleItemCount + 1) * _configuration.ItemPadding)
                    + (visibleItemCount * _itemPanelsInDisplayOrder[0].width);
            }
            else
            {
                return ((_configuration.MainPanelColumnCount + 1) * _configuration.ItemPadding)
                    + (_configuration.MainPanelColumnCount * _itemPanelsInDisplayOrder[0].width);
            }
        }

        private float CalculatePanelHeight(int visibleItemCount)
        {
            if (visibleItemCount <= 0)
            {
                return 0f;
            }

            var rowCount = Mathf.CeilToInt(visibleItemCount / (float)_configuration.MainPanelColumnCount);
            return ((rowCount + 1) * _configuration.ItemPadding) + (rowCount * _itemPanelsInDisplayOrder[0].height);
        }

        private void UiDragHandle_eventMouseUp(UIComponent component, UIMouseEventParameter eventParam)
        {
            SaveMainPanelPosition();
        }

        private void SaveMainPanelPosition()
        {
            _configuration.MainPanelPosition = relativePosition;
            _configuration.Save();

            OnPositionChanged();
        }

        private IEnumerator KeepUpdatingUICoroutine()
        {
            while (true)
            {
                yield return StartCoroutine(UpdateUICoroutine());
            }
        }

        private IEnumerator UpdateUICoroutine()
        {
            var waitTimeIfNothingIsTodo = 1.0f;

            var enabledItemsCount = _configuration.GetEnabledItemsCount();
            if (enabledItemsCount == 0)
            {
                yield return new WaitForSecondsRealtime(waitTimeIfNothingIsTodo);
                yield break;
            }

            var totalTimeToUpdate = _configuration.MainPanelUpdateEveryXSeconds;
            if (totalTimeToUpdate == 0)
            {
                yield return new WaitForSecondsRealtime(waitTimeIfNothingIsTodo);
                yield break;
            }

            var itemUpdateInterval = totalTimeToUpdate / (float)enabledItemsCount;

            for (int i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                var itemPanel = _itemPanelsInDisplayOrder[i];
                if (!itemPanel.ConfigurationItemData.Enabled)
                {
                    continue;
                }

                var itemVisibilityAndChanged = itemPanel.UpdatePercentVisibilityAndColor();
                if (itemVisibilityAndChanged.IsVisibleChanged)
                {
                    UpdatePanelLayoutAndPanelSizeAndClampToScreen();
                }

                yield return new WaitForSecondsRealtime(itemUpdateInterval);
            }
        }
    }
}
