using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledButton : StyledElement<ButtonStyleData>
    {
        public Button button;
        public Image background;
        public Text text;
        public Image icon;

        protected override void ApplyStyle(ButtonStyleData style)
        {
            StyleUtils.Apply(style, button);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.text, text);
            StyleUtils.Apply(style.icon, icon);
        }
    }
}