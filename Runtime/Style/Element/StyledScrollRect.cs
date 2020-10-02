using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledScrollRect : StyledElement<ScrollRectStyle, ScrollRectStyleData>
    {
        public UIReference<ScrollRect> scrollView;
        public UIReference<Image> background;
        public UIReference<StyledScrollbar> horizontalScrollbar;
        public UIReference<StyledScrollbar> verticalScrollbar;

        protected override void ApplyStyle(ScrollRectStyleData style)
        {
            StyleUtils.Apply(style, scrollView);
            StyleUtils.Apply(style.background, background);

            if (horizontalScrollbar)
                horizontalScrollbar.value.ApplyStyleFromParent(style.scrollBar);

            if (verticalScrollbar)
                verticalScrollbar.value.ApplyStyleFromParent(style.scrollBar);
        }
    }
}