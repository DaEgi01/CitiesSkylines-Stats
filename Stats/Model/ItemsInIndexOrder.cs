using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Model
{
    public class ItemsInIndexOrder
    {
        private readonly Item[] items;

        public ItemsInIndexOrder(IEnumerable<Item> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items.OrderBy(x => x.ItemData.Index)
                .ToArray();

            ValidateIndexes(this.items);
        }

        public Item[] Items => items;

        private void ValidateIndexes(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (i != items[i].ItemData.Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        public Item GetItem(ItemData itemData)
        {
            return items[itemData.Index];
        }
    }
}
