using System.Runtime.InteropServices;

namespace Stats;

[StructLayout(LayoutKind.Auto)]
public readonly struct ItemVisibilityAndChanged
{
    public readonly bool IsVisible;
    public readonly bool IsVisibleChanged;

    public ItemVisibilityAndChanged(bool isVisible, bool isVisibleChanged)
    {
        IsVisible = isVisible;
        IsVisibleChanged = isVisibleChanged;
    }
}
