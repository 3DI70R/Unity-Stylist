using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledText : StyledElement<TextStyle, TextStyleData>
    {
        public Text text;
        
        protected override void ApplyStyle(TextStyleData style)
        {
            StyleUtils.Apply(style, text);
        }
    }
}