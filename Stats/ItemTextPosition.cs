using System;
using System.Collections.Generic;

namespace Stats
{
    public class ItemTextPosition
    {
        private ItemTextPosition(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; }
        public string Name { get; }

        private const string _none = "None";
        public static readonly ItemTextPosition None = new ItemTextPosition(0, _none);

        private const string _top = "Top";
        public static readonly ItemTextPosition Top = new ItemTextPosition(1, _top);

        private const string _right = "Right";
        public static readonly ItemTextPosition Right = new ItemTextPosition(2, _right);

        private const string _bottom = "Bottom";
        public static readonly ItemTextPosition Bottom = new ItemTextPosition(3, _bottom);

        private const string _left = "Left";
        public static readonly ItemTextPosition Left = new ItemTextPosition(4, _left);

        public static IEnumerable<ItemTextPosition> All { get; } = new[]
        {
            None,
            Top,
            Right,
            Bottom,
            Left
        };

        public static ItemTextPosition Parse(string name)
        {
            return name switch
            {
                _none => None,
                _top => Top,
                _right => Right,
                _left => Left,
                _ => throw new ArgumentException($"Could not parse '{name}'.", nameof(name))
            };
        }
    }
}
