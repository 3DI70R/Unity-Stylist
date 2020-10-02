using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class InputFieldStyleData : SelectableStyleData
    {
        [Header("Input")] public StyleProperty<float> caretBlinkRate = 0.85f;
        public StyleProperty<int> caretWidth = 1;
        public StyleProperty<Color> caretColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        public StyleProperty<Color> selectionColor = new Color(168f / 255f, 206f / 255f, 255f / 255f, 192f / 255f);

        [Header("References")] public StyleReference<ImageStyleData> background;
        public StyleReference<TextStyleData> text;
        public StyleReference<TextStyleData> placeholder;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var inputResolver = resolver.ForType<InputFieldStyleData>();
            inputResolver.Resolve(ref caretBlinkRate, d => d.caretBlinkRate);
            inputResolver.Resolve(ref caretWidth, d => d.caretWidth);
            inputResolver.Resolve(ref caretColor, d => d.caretColor);
            inputResolver.Resolve(ref selectionColor, d => d.selectionColor);

            inputResolver.Resolve(ref background, d => d.background);
            inputResolver.Resolve(ref placeholder, d => d.placeholder);
            inputResolver.Resolve(ref text, d => d.text);
        }
    }
}