using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ToggleStyleData : SelectableStyleData
    {
        [Header("Toggle")]
        public StyleProperty<Toggle.ToggleTransition> toggleTransition;

        [Header("References")]
        public StyleReference<ImageStyleData> background;
        public StyleReference<ImageStyleData> checkmarkBackground;
        public StyleReference<ImageStyleData> checkmark;
        public StyleReference<TextStyleData> text;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var toggleResolver = resolver.ForType<ToggleStyleData>();
            toggleResolver.Resolve(ref toggleTransition, d => d.toggleTransition);
            
            toggleResolver.Resolve(ref background, d => d.background);
            toggleResolver.Resolve(ref checkmarkBackground, d => d.checkmarkBackground);
            toggleResolver.Resolve(ref checkmark, d => d.checkmark);
            toggleResolver.Resolve(ref text, d => d.text);
        }
    }
}