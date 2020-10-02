using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledToggle : StyledElement<ToggleStyle, ToggleStyleData>
    {
        public UIReference<Toggle> toggle;
        public UIReference<Image> background;
        public UIReference<Image> checkmarkBackground;
        public UIReference<Image> checkmark;
        public UIReference<Text> text;

        protected override void ApplyStyle(ToggleStyleData style)
        {
            StyleUtils.Apply(style, toggle);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.checkmarkBackground, checkmarkBackground);
            StyleUtils.Apply(style.checkmark, checkmark);
            StyleUtils.Apply(style.text, text);
        }
    }
}