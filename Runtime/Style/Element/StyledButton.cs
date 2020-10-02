using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledButton : StyledElement<ButtonStyle, ButtonStyleData>
    {
        public UIReference<Button> button;
        public UIReference<Image> background;
        public UIReference<Text> text;
        public UIReference<Image> icon;

        protected override void ApplyStyle(ButtonStyleData style)
        {
            StyleUtils.Apply(style, button);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.text, text);
            StyleUtils.Apply(style.icon, icon);
        }
    }
}