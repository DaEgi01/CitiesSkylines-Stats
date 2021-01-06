using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stats.Icons
{
    public class IconSet
    {
        public IconSet(string author, string name, IEnumerable<string> icons)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (icons is null)
            {
                throw new ArgumentNullException(nameof(icons));
            }
            else
            {
                Icons = new ReadOnlyCollection<string>(icons.ToList());
            }
        }

        public string Author { get; }
        public string Name { get; }
        public ReadOnlyCollection<string> Icons { get; }
    }
}
