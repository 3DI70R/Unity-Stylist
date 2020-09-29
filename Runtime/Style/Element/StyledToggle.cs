using System.Collections.Generic;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledToggle : StyledElement
    {
        public Toggle toggle;
        public Image background;
        public Image checkmarkBackground;
        public Image checkmark;
        public Text text;

        protected override void ApplyStyle(ObjectStyleResolver<object> resolver)
        {
            var style = resolver.As<ToggleStyleData>();
            style.Apply(toggle);
            style.As(d => d.background).Apply(background);
            style.As(d => d.checkmarkBackground).Apply(checkmarkBackground);
            style.As(d => d.checkmark).Apply(checkmark);
            style.As(d => d.text).Apply(text);
        }
    }
}