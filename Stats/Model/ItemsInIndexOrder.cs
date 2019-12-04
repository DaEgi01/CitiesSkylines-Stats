using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Model
{
    public class ItemsInIndexOrder : IOrderedEnumerable<Item>
    {
        private readonly Item[] itemsInIndexOrder;

        public ItemsInIndexOrder(Item[] items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            itemsInIndexOrder = items.OrderBy(x => x.ItemData.Index)
                .ToArray();

            ValidateIndexes(itemsInIndexOrder);
        }

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
            return itemsInIndexOrder[itemData.Index];
        }

        public IOrderedEnumerable<Item> CreateOrderedEnumerable<TKey>(Func<Item, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return descending
                ? itemsInIndexOrder.OrderByDescending(keySelector, comparer)
                : itemsInIndexOrder.OrderBy(keySelector, comparer);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return (IEnumerator<Item>)itemsInIndexOrder.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return itemsInIndexOrder.GetEnumerator();
        }
    }
}
