using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledInputField : StyledElement<InputFieldStyle, InputFieldStyleData>
    {
        public UIReference<InputField> inputField;
        public UIReference<Image> background;
        public UIReference<Text> text;
        public UIReference<Text> placeholder;
        
        protected override void ApplyStyle(InputFieldStyleData style)
        {
            StyleUtils.Apply(style, inputField);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.text, text);
            StyleUtils.Apply(style.placeholder, placeholder);
        }
    }
}