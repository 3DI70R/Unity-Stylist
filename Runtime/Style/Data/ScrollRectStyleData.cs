using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ScrollRectStyleData : ElementStyleData
    {
        public StyleProperty<ScrollRect.MovementType> movementType = ScrollRect.MovementType.Elastic;
        
        public StyleProperty<float> elasticity = 0.1f;
        public StyleProperty<bool> inertia = true;
        public StyleProperty<float> decelerationRate = 0.135f;
        public StyleProperty<float> scrollSensitivity = 1f;
        
        [Header("References")]
        public StyleReference<ImageStyleData> background;
        public StyleReference<ScrollbarStyleData> scrollBar;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var scrollResolver = resolver.ForType<ScrollRectStyleData>();
            scrollResolver.Resolve(ref movementType, d => d.movementType);
            
            scrollResolver.Resolve(ref elasticity, d => d.elasticity);
            scrollResolver.Resolve(ref inertia, d => d.inertia);
            scrollResolver.Resolve(ref decelerationRate, d => d.decelerationRate);
            scrollResolver.Resolve(ref scrollSensitivity, d => d.scrollSensitivity);
            
            scrollResolver.Resolve(ref background, d => d.background);
            scrollResolver.Resolve(ref scrollBar, d => d.scrollBar);
        }
    }
}