using Stats.Config;
using Stats.Model;
using Stats.Ui;
using System;
using System.Collections;
using UnityEngine;

namespace Stats
{
    public class UpdateAllItemsBehaviour : MonoBehaviour
    {
        private Configuration configuration;
        private ItemsInIndexOrder itemsInIndexOrder;
        private GameEngineService gameEngineService;
        private MainPanel mainPanel;
        private bool mapHasSnowDumps;

        public void Initialize(
            Configuration configuration,
            ItemsInIndexOrder itemsInIndexOrder,
            GameEngineService gameEngineService,
            MainPanel mainPanel,
            bool mapHasSnowDumps)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (itemsInIndexOrder is null)
            {
                throw new ArgumentNullException(nameof(itemsInIndexOrder));
            }

            if (gameEngineService is null)
            {
                throw new ArgumentNullException(nameof(gameEngineService));
            }

            this.configuration = configuration;
            this.itemsInIndexOrder = itemsInIndexOrder;
            this.gameEngineService = gameEngineService;
            this.mainPanel = mainPanel;
            this.mapHasSnowDumps = mapHasSnowDumps;

            this.StartCoroutine(KeepUpdatingUICoroutine());
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
            for (int i = 0; i < this.itemsInIndexOrder.Items.Length; i++)
            {
                var item = this.itemsInIndexOrder.Items[i];
                if (this.configuration.GetConfigurationItemData(item.ItemData).enabled)
                {
                    var visiblity = item.IsVisible;
                    item.UpdatePercentFromGame();
                    if (visiblity != item.IsVisible)
                    {
                        var itemPanel = this.mainPanel.ItemPanelsInIndexOrder[item.ItemData.Index];
                        itemPanel.isVisible = item.IsVisible;
                        mainPanel.UpdateLayout();
                    }

                    yield return new WaitForEndOfFrame();
                }
            }

            //wait at least one frame, even if all Items are off.
            yield return new WaitForEndOfFrame();
        }
    }
}
