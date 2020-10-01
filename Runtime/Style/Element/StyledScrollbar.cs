using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledScrollbar : StyledElement<ScrollbarStyleData>
    {
        public Scrollbar scrollbar;
        public Image background;
        public Image handle;
        
        protected override void ApplyStyle(ScrollbarStyleData style)
        {
            StyleUtils.Apply(style, scrollbar);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.handle, handle);
        }
    }
}