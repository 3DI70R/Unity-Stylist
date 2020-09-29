using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledScrollbar : StyledElement
    {
        public Scrollbar scrollbar;
        public Image background;
        public Image handle;
        
        protected override void ApplyStyle(ObjectStyleResolver<object> resolver)
        {
            var style = resolver.As<ScrollbarStyleData>();
            style.Apply(scrollbar);
            style.As(d => d.background).Apply(background);
            style.As(d => d.handle).Apply(handle);
        }
    }
}