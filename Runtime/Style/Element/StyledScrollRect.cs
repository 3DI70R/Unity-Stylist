using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledScrollRect : StyledElement
    {
        public ScrollRect scrollView;
        public Image background;
        public StyledScrollbar horizontalScrollbar;
        public StyledScrollbar verticalScrollbar;

        protected override void ApplyStyle(ObjectStyleResolver<object> resolver)
        {
            var style = resolver.As<ScrollRectStyleData>();
            style.Apply(scrollView);
            style.As(d => d.background).Apply(background);
            
            if(horizontalScrollbar)
                horizontalScrollbar.ApplyStyleFromParent(style.As(d => d.scrollBar));
            
            if(verticalScrollbar)
                verticalScrollbar.ApplyStyleFromParent(style.As(d => d.scrollBar));
        }
    }
}