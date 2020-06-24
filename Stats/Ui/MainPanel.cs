using ColossalFramework.UI;
using Stats.Config;
using Stats.Localization;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Stats.Ui
{
    public class MainPanel : UIPanel
    {
        private UIDragHandleWithDragState _uiDragHandle;
        private string _modSystemName;
        private Configuration _configuration;
        private LanguageResource _languageResource;
        private GameEngineService _gameEngineService;
        private InfoManager _infoManager;

        private ItemPanel[] _itemPanelsInDisplayOrder;

        public ItemPanel[] ItemPanelsInDisplayOrder => _itemPanelsInDisplayOrder;

        public void Initialize(
            string modSystemName,
            Configuration configuration,
            LanguageResource languageResource,
            GameEngineService gameEngineService,
            InfoManager infoManager)
        {
            _modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (_configuration.MainPanelColumnCount < 1)
            {
                throw new ArgumentOutOfRangeException($"'{nameof(_configuration.MainPanelColumnCount)}' parameter must be bigger or equal to 1.");
            }
            _languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
            _gameEngineService = gameEngineService ?? throw new ArgumentNullException(nameof(gameEngineService));
            _infoManager = infoManager;

            color = configuration.MainPanelBackgroundColor;
            name = modSystemName + "MainPanel";
            backgroundSprite = "GenericPanelLight";
            isInteractive = false;

            CreateAndAddDragHandle();
            CreateAndAddAllUiItems();

            UpdateItemsLayoutAndSize();
            relativePosition = _configuration.MainPanelPosition;

            _uiDragHandle.eventMouseUp += UiDragHandle_eventMouseUp;

            StartCoroutine(KeepUpdatingUICoroutine());
        }

        public override void OnDestroy()
        {
            _uiDragHandle.eventMouseUp -= UiDragHandle_eventMouseUp;

            base.OnDestroy();
        }

        private void CreateAndAddDragHandle()
        {
            var dragHandle = AddUIComponent<UIDragHandleWithDragState>();
            dragHandle.name = _modSystemName + "DragHandle";
            dragHandle.relativePosition = Vector2.zero;
            dragHandle.target = this;
            dragHandle.constrainToScreen = true;
            dragHandle.SendToBack();
            _uiDragHandle = dragHandle;
        }

        private void CreateAndAddAllUiItems()
        {
            _itemPanelsInDisplayOrder = ItemData.AllItems
                .Where(i => _gameEngineService.MapHasSnowDumps || (i != ItemData.SnowDump && i != ItemData.SnowDumpVehicles))
                .Select(i => CreateUiItemAndAddButtons(
                    _configuration.GetConfigurationItemData(i),
                    _gameEngineService.GetPercentFunc(i),
                    _infoManager)
                )
                .OrderBy(x => x.ConfigurationItemData.SortOrder)
                .ToArray();
        }

        private ItemPanel CreateUiItemAndAddButtons(ConfigurationItemData configurationItemData, Func<int?> getPercentFromGame, InfoManager infoManager)
        {
            var itemPanel = CreateAndAddItemPanel();
            itemPanel.Initialize(_configuration, configurationItemData, _languageResource, getPercentFromGame, infoManager);
            return itemPanel;
        }

        private ItemPanel CreateAndAddItemPanel()
        {
            var itemPanel = AddUIComponent<ItemPanel>();
            itemPanel.width = _configuration.ItemWidth;
            itemPanel.height = _configuration.ItemHeight;
            itemPanel.zOrder = zOrder;
            return itemPanel;
        }

        public void UpdateSortOrder()
        {
            _itemPanelsInDisplayOrder = _itemPanelsInDisplayOrder
                .OrderBy(x => x.ConfigurationItemData.SortOrder)
                .ToArray();

            UpdateItemsLayout();
        }

        public void UpdateItemsLayoutAndSize()
        {
            UpdateItemsLayout();
            UpdatePanelSize();
        }

        public override void Update()
        {
            if (_configuration.MainPanelAutoHide && !containsMouse)
            {
                opacity = 0;
            }
            else
            {
                opacity = 1;
            }
        }

        private void UpdateItemsLayout()
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
                    index / _configuration.MainPanelColumnCount
                );

                currentItem.relativePosition = CalculateRelativePosition(layoutPosition);
                currentItem.AdjustButtonAndUiItemSize();

                lastLayoutPosition = CalculateNextLayoutPosition(lastLayoutPosition);
                index++;
            }
        }

        private Vector2 CalculateNextLayoutPosition(Vector2 position)
        {
            if (position.x < _configuration.MainPanelColumnCount - 1)
            {
                return new Vector2(position.x + 1, position.y);
            }
            else
            {
                return new Vector2(0, position.y + 1);
            }
        }

        private Vector3 CalculateRelativePosition(Vector2 layoutPosition)
        {
            var posX = (layoutPosition.x + 1) * _configuration.ItemPadding
                + layoutPosition.x * _configuration.ItemWidth;
            var posY = (layoutPosition.y + 1) * _configuration.ItemPadding
                + layoutPosition.y * _configuration.ItemHeight;

            return new Vector3(posX, posY);
        }

        private void UpdatePanelSize()
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

            _uiDragHandle.width = newWidth;
            _uiDragHandle.height = newHeight;
        }

        private int GetVisibleItemsCount(ItemPanel[] itemPanels)
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

        public void UpdatePosition()
        {
            if (_uiDragHandle.IsDragged)
            {
                return;
            }

            relativePosition = _configuration.MainPanelPosition;
        }

        public void UpdateLocalization()
        {
            for (int i = 0; i < _itemPanelsInDisplayOrder.Length; i++)
            {
                _itemPanelsInDisplayOrder[i].UpdateLocalization();
            }
        }

        private float CalculatePanelWidth(int visibleItemCount)
        {
            if (visibleItemCount < _configuration.MainPanelColumnCount)
            {
                return (visibleItemCount + 1) * _configuration.ItemPadding
                    + visibleItemCount * _configuration.ItemWidth;
            }
            else
            {
                return (_configuration.MainPanelColumnCount + 1) * _configuration.ItemPadding
                    + _configuration.MainPanelColumnCount * _configuration.ItemWidth;
            }
        }

        private float CalculatePanelHeight(int visibleItemCount)
        {
            var rowCount = Mathf.CeilToInt(visibleItemCount / (float)_configuration.MainPanelColumnCount);
            return (rowCount + 1) * _configuration.ItemPadding + rowCount * _configuration.ItemHeight;
        }

        private void UiDragHandle_eventMouseUp(UIComponent component, UIMouseEventParameter eventParam)
        {
            SaveMainPanelPosition();
        }

        private void SaveMainPanelPosition()
        {
            _configuration.MainPanelPosition = relativePosition;
            _configuration.Save();
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
                if (itemVisibilityAndChanged.isVisibleChanged)
                {
                    UpdateItemsLayoutAndSize();
                }

                yield return new WaitForSecondsRealtime(itemUpdateInterval);
            }
        }
    }
}
