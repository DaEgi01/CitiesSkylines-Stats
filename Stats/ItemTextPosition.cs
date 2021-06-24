namespace Stats
{
    using System.Collections.Generic;

    public class ItemTextPosition
    {
        public static readonly ItemTextPosition None = new(0, _none);
        public static readonly ItemTextPosition Top = new(1, _top);
        public static readonly ItemTextPosition Right = new(2, _right);
        public static readonly ItemTextPosition Bottom = new(3, _bottom);
        public static readonly ItemTextPosition Left = new(4, _left);

        private const string _none = "None";
        private const string _top = "Top";
        private const string _right = "Right";
        private const string _bottom = "Bottom";
        private const string _left = "Left";

        private ItemTextPosition(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public static IEnumerable<ItemTextPosition> All { get; } = new[]
        {
            None,
            Top,
            Right,
            Bottom,
            Left,
        };

        public int Index { get; }
        public string Name { get; }

        public static ItemTextPosition? Parse(string name)
        {
            return name switch
            {
                _none => None,
                _top => Top,
                _right => Right,
                _left => Left,
                _ => null
            };
        }
    }
}
