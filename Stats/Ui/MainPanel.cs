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
        private UIDragHandleWithDragState uiDragHandle;
        private string modSystemName;
        private Configuration configuration;
        private LanguageResource languageResource;
        private GameEngineService gameEngineService;
        private InfoManager infoManager;

        private ItemPanel[] itemPanelsInDisplayOrder;

        public ItemPanel[] ItemPanelsInDisplayOrder => itemPanelsInDisplayOrder;

        public void Initialize(
            string modSystemName,
            Configuration configuration,
            LanguageResource languageResource,
            GameEngineService gameEngineService,
            InfoManager infoManager)
        {
            this.modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (this.configuration.MainPanelColumnCount < 1)
            {
                throw new ArgumentOutOfRangeException($"'{nameof(this.configuration.MainPanelColumnCount)}' parameter must be bigger or equal to 1.");
            }
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
            this.gameEngineService = gameEngineService ?? throw new ArgumentNullException(nameof(gameEngineService));
            this.infoManager = infoManager;

            this.color = configuration.MainPanelBackgroundColor;
            this.name = modSystemName + "MainPanel";
            this.backgroundSprite = "GenericPanelLight";
            this.isInteractive = false;

            this.CreateAndAddDragHandle();
            this.CreateAndAddAllUiItems();

            this.UpdateItemsLayoutAndSize();
            this.relativePosition = this.configuration.MainPanelPosition;

            this.uiDragHandle.eventMouseUp += UiDragHandle_eventMouseUp;

            StartCoroutine(KeepUpdatingUICoroutine());

        }

        public override void OnDestroy()
        {
            this.uiDragHandle.eventMouseUp -= UiDragHandle_eventMouseUp;

            base.OnDestroy();
        }

        private void CreateAndAddDragHandle()
        {
            var dragHandle = this.AddUIComponent<UIDragHandleWithDragState>();
            dragHandle.name = this.modSystemName + "DragHandle";
            dragHandle.relativePosition = Vector2.zero;
            dragHandle.target = this;
            dragHandle.constrainToScreen = true;
            dragHandle.SendToBack();
            this.uiDragHandle = dragHandle;
        }

        private void CreateAndAddAllUiItems()
        {
            this.itemPanelsInDisplayOrder = this.gameEngineService.MapHasSnowDumps
                ? ItemData.AllItems
                    .Select(i => this.CreateUiItemAndAddButtons(this.configuration.GetConfigurationItemData(i), this.gameEngineService.GetPercentFunc(i), infoManager))
                    .OrderBy(x => x.ConfigurationItemData.SortOrder)
                    .ToArray()
                : ItemData.AllItems
                    .Where(i => i != ItemData.SnowDump && i != ItemData.SnowDumpVehicles)
                    .Select(i => this.CreateUiItemAndAddButtons(this.configuration.GetConfigurationItemData(i), this.gameEngineService.GetPercentFunc(i), infoManager))
                    .OrderBy(x => x.ConfigurationItemData.SortOrder)
                    .ToArray();
        }

        private ItemPanel CreateUiItemAndAddButtons(ConfigurationItemData configurationItemData, Func<int?> getPercentFromGame, InfoManager infoManager)
        {
            var itemPanel = this.CreateAndAddItemPanel();
            itemPanel.Initialize(this.configuration, configurationItemData, this.languageResource, getPercentFromGame, infoManager);
            return itemPanel;
        }

        private ItemPanel CreateAndAddItemPanel()
        {
            var itemPanel = this.AddUIComponent<ItemPanel>();
            itemPanel.width = this.configuration.ItemWidth;
            itemPanel.height = this.configuration.ItemHeight;
            itemPanel.zOrder = zOrder;
            return itemPanel;
        }

        public void UpdateSortOrder()
        {
            this.itemPanelsInDisplayOrder = this.itemPanelsInDisplayOrder
                .OrderBy(x => x.ConfigurationItemData.SortOrder)
                .ToArray();

            this.UpdateItemsLayout();
        }

        public void UpdateItemsLayoutAndSize()
        {
            this.UpdateItemsLayout();
            this.UpdatePanelSize();
        }

        public override void Update()
        {
            if (this.configuration.MainPanelAutoHide && !this.containsMouse)
            {
                this.opacity = 0;
            }
            else
            {
                this.opacity = 1;
            }
        }

        private void UpdateItemsLayout()
        {
            var lastLayoutPosition = Vector2.zero;
            int index = 0;

            for (int i = 0; i < this.itemPanelsInDisplayOrder.Length; i++)
            {
                var currentItem = this.itemPanelsInDisplayOrder[i];
                if (!currentItem.isVisible)
                {
                    continue;
                }

                var layoutPosition = new Vector2(
                    index % this.configuration.MainPanelColumnCount,
                    index / this.configuration.MainPanelColumnCount
                );

                currentItem.relativePosition = CalculateRelativePosition(layoutPosition);
                currentItem.AdjustButtonAndUiItemSize();

                lastLayoutPosition = CalculateNextLayoutPosition(lastLayoutPosition);
                index++;
            }
        }

        private Vector2 CalculateNextLayoutPosition(Vector2 position)
        {
            if (position.x < this.configuration.MainPanelColumnCount - 1)
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
            var posX = (layoutPosition.x + 1) * this.configuration.ItemPadding
                + layoutPosition.x * this.configuration.ItemWidth;
            var posY = (layoutPosition.y + 1) * this.configuration.ItemPadding
                + layoutPosition.y * this.configuration.ItemHeight;

            return new Vector3(posX, posY);
        }

        private void UpdatePanelSize()
        {
            var visibleItemCount = GetVisibleItemsCount(this.itemPanelsInDisplayOrder);
            if (visibleItemCount == 0)
            {
                this.isVisible = false;
                return;
            }

            this.isVisible = true;

            var newWidth = this.CalculatePanelWidth(visibleItemCount);
            var newHeight = this.CalculatePanelHeight(visibleItemCount);

            this.width = newWidth;
            this.height = newHeight;

            this.uiDragHandle.width = newWidth;
            this.uiDragHandle.height = newHeight;
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
            if (uiDragHandle.IsDragged)
            {
                return;
            }

            this.relativePosition = this.configuration.MainPanelPosition;
        }

        public void UpdateLocalization()
        {
            for (int i = 0; i < itemPanelsInDisplayOrder.Length; i++)
            {
                itemPanelsInDisplayOrder[i].UpdateLocalization();
            }
        }

        private float CalculatePanelWidth(int visibleItemCount)
        {
            if (visibleItemCount < this.configuration.MainPanelColumnCount)
            {
                return (visibleItemCount + 1) * this.configuration.ItemPadding
                    + visibleItemCount * this.configuration.ItemWidth;
            }
            else
            {
                return (this.configuration.MainPanelColumnCount + 1) * this.configuration.ItemPadding
                    + this.configuration.MainPanelColumnCount * this.configuration.ItemWidth;
            }
        }

        private float CalculatePanelHeight(int visibleItemCount)
        {
            var rowCount = Mathf.CeilToInt(visibleItemCount / (float)this.configuration.MainPanelColumnCount);
            return (rowCount + 1) * this.configuration.ItemPadding + rowCount * this.configuration.ItemHeight;
        }

        private void UiDragHandle_eventMouseUp(UIComponent component, UIMouseEventParameter eventParam)
        {
            SaveMainPanelPosition();
        }

        private void SaveMainPanelPosition()
        {
            this.configuration.MainPanelPosition = this.relativePosition;
            this.configuration.Save();
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
            var enabledItemsCount = this.configuration.GetEnabledItemsCount();
            var totalTimeToUpdate = this.configuration.MainPanelUpdateEveryXSeconds;

            var itemUpdateInterval = enabledItemsCount == 0
                ? 0.1f
                : totalTimeToUpdate / (float)enabledItemsCount;

            for (int i = 0; i < this.itemPanelsInDisplayOrder.Length; i++)
            {
                var itemPanel = this.itemPanelsInDisplayOrder[i];
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

            //wait at least one frame, even if all Items are off.
            yield return new WaitForSecondsRealtime(itemUpdateInterval);
        }
    }
}
