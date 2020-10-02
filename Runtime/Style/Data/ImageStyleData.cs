using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ImageStyleData : GraphicStyleData
    {
        [Header("Image")] 
        public StyleProperty<Sprite> sprite;

        public StyleProperty<bool> preserveAspect = false;
        public StyleProperty<bool> fillCenter = true;
        public StyleProperty<bool> useSpriteMesh = false;
        public StyleProperty<float> pixelsPerUnitMultiplier = 1f;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var imageResolver = resolver.ForType<ImageStyleData>();
            imageResolver.Resolve(ref sprite, d => d.sprite);
            
            imageResolver.Resolve(ref preserveAspect, d => d.preserveAspect);
            imageResolver.Resolve(ref fillCenter, d => d.fillCenter);
            imageResolver.Resolve(ref useSpriteMesh, d => d.useSpriteMesh);
            imageResolver.Resolve(ref pixelsPerUnitMultiplier, d => d.pixelsPerUnitMultiplier);
        }
    }
}