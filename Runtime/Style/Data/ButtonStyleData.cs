using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ButtonStyleData : SelectableStyleData
    {
        [Space(0)]
        [Header("Button", order = 1)]
        public StyleReference<ImageStyleData> background;
        public StyleReference<TextStyleData> text;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var buttonResolver = resolver.ForType<ButtonStyleData>();
            buttonResolver.Resolve(ref background, d => d.background);
            buttonResolver.Resolve(ref text, d => d.text);
        }
    }
}