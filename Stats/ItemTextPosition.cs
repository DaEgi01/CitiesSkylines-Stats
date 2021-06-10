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

        public static ItemTextPosition None { get; } = new ItemTextPosition(0, "None");
        public static ItemTextPosition Top { get; } = new ItemTextPosition(1, "Top");
        public static ItemTextPosition Right { get; } = new ItemTextPosition(2, "Right");
        public static ItemTextPosition Bottom { get; } = new ItemTextPosition(3, "Bottom");
        public static ItemTextPosition Left { get; } = new ItemTextPosition(4, "Left");

        public static IEnumerable<ItemTextPosition> All { get; } = new[]
        {
            None,
            Top,
            Right,
            Bottom,
            Left
        };

        public static ItemTextPosition Parse(string value)
        {
            if (value == None.Name)
            {
                return None;
            }
            else if (value == Top.Name)
            {
                return Top;
            }
            else if (value == Right.Name)
            {
                return Right;
            }
            else if (value == Bottom.Name)
            {
                return Bottom;
            }
            else if (value == Left.Name)
            {
                return Left;
            }
            else
            {
                throw new ArgumentException($"Could not parse '{value}'.", nameof(value));
            }
        }
    }
}
