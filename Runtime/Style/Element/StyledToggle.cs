using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledToggle : StyledElement<ToggleStyleData>
    {
        public Toggle toggle;
        public Image background;
        public Image checkmarkBackground;
        public Image checkmark;
        public Text text;

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