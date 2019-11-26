using System.Runtime.InteropServices;

namespace Stats
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ItemVisibilityAndChanged
    {
        public readonly bool isVisible;
        public readonly bool isVisibleChanged;

        public ItemVisibilityAndChanged(bool isVisible, bool isVisibleChanged)
        {
            this.isVisible = isVisible;
            this.isVisibleChanged = isVisibleChanged;
        }
    }
}
