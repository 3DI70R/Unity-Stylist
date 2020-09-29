﻿using System.Collections.Generic;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledButton : StyledElement
    {
        public Button button;
        public Image background;
        public Text text;
        public Image icon;

        protected override void ApplyStyle(ObjectStyleResolver<object> resolver)
        {
            var style = resolver.As<ButtonStyleData>();
            style.Apply(button);
            style.As(d => d.background).Apply(background);
            style.As(d => d.text).Apply(text);
            style.As(d => d.icon).Apply(icon);
        }
    }
}