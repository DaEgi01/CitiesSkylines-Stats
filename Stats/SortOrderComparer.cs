using Stats.Ui;
using System.Collections.Generic;

namespace Stats
{
    public class SortOrderComparer : IComparer<ItemPanel>
    {
        public int Compare(ItemPanel x, ItemPanel y)
        {
            return x.SortOrder.CompareTo(y.SortOrder);
        }
    }
}
