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

        public static readonly ItemTextPosition None = new ItemTextPosition(0, "None");
        public static readonly ItemTextPosition Top = new ItemTextPosition(1, "Top");
        public static readonly ItemTextPosition Right = new ItemTextPosition(2, "Right");
        public static readonly ItemTextPosition Bottom = new ItemTextPosition(3, "Bottom");
        public static readonly ItemTextPosition Left = new ItemTextPosition(4, "Left");

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
            if (name == None.Name)
            {
                return None;
            }
            else if (name == Top.Name)
            {
                return Top;
            }
            else if (name == Right.Name)
            {
                return Right;
            }
            else if (name == Bottom.Name)
            {
                return Bottom;
            }
            else if (name == Left.Name)
            {
                return Left;
            }
            else
            {
                throw new ArgumentException($"Could not parse '{name}'.", nameof(name));
            }
        }
    }
}
