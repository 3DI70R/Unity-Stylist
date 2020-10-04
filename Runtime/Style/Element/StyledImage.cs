using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledImage : StyledElement<ImageStyle, ImageStyleData>
    {
        public Image image;
        
        protected override void ApplyStyle(ImageStyleData style)
        {
            StyleUtils.Apply(style, image);
        }
    }
}