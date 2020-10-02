using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledScrollbar : StyledElement<ScrollbarStyle, ScrollbarStyleData>
    {
        public UIReference<Scrollbar> scrollbar;
        public UIReference<Image> background;
        public UIReference<Image> handle;
        
        protected override void ApplyStyle(ScrollbarStyleData style)
        {
            StyleUtils.Apply(style, scrollbar);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.handle, handle);
        }
    }
}