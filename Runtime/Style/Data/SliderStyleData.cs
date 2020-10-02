using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class SliderStyleData : SelectableStyleData
    {
        [Header("References")]
        public StyleReference<ImageStyleData> background;
        public StyleReference<ImageStyleData> fill;
        public StyleReference<ImageStyleData> handle;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var sliderResolver = resolver.ForType<SliderStyleData>();
            sliderResolver.Resolve(ref background, d => d.background);
            sliderResolver.Resolve(ref fill, d => d.fill);
            sliderResolver.Resolve(ref handle, d => d.handle);
        }
    }
}