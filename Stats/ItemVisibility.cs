using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ItemVisibilityAndChanged
    {
        public ItemVisibilityAndChanged(bool visibility, bool visibilityChanged)
        {
            IsVisibile = visibility;
            VisibilityChanged = visibilityChanged;
        }

        public bool IsVisibile { get; }

        public bool VisibilityChanged { get; }
    }
}
