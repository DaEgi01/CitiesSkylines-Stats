using ColossalFramework.UI;

namespace Stats.Ui
{
    public class UIDragHandleWithDragState : UIDragHandle
    {
        public bool IsDragged { get; private set; }

        protected override void OnMouseDown(UIMouseEventParameter p)
        {
            base.OnMouseDown(p);
            IsDragged = true;
        }

        protected override void OnMouseUp(UIMouseEventParameter p)
        {
            IsDragged = false;
            base.OnMouseUp(p);
        }
    }
}
