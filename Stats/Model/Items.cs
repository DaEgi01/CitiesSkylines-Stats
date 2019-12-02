using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stats.Model
{
    public class Items
    {
        private readonly ReadOnlyCollection<Item> itemsInOrder;
        private readonly Dictionary<ItemData, Item> itemsDictionary;

        public Items(List<Item> items)
        {
            if (items is null)
            {
                throw new System.ArgumentNullException(nameof(items));
            }
            
            this.itemsInOrder = new ReadOnlyCollection<Item>(
                items.OrderBy(x => x.ConfigurationItem.SortOrder)
                    .ToList()
            );

            this.itemsDictionary = items.ToDictionary(x => x.ConfigurationItem.ItemData, x => x);
        }

        public Item GetItem(ItemData itemData)
        {
            return itemsDictionary[itemData];
        }

        public ReadOnlyCollection<Item> ItemsInIndexOrder => this.itemsInOrder;
    }
}
