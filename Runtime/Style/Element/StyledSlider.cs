using System;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledSlider : StyledElement<SliderStyle, SliderStyleData>
    {
        public UIReference<Slider> slider;
        public UIReference<Image> background;
        public UIReference<Image> fill;
        public UIReference<Image> handle;
        
        protected override void ApplyStyle(SliderStyleData style)
        {
            StyleUtils.Apply(style, slider);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.fill, fill);
            StyleUtils.Apply(style.handle, handle);
        }
    }
}