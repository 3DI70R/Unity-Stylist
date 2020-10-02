using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ElementStyleData : StyleData
    {
        [Header("Layout Element")] 
        public StyleProperty<int> minWidth = -1;
        public StyleProperty<int> minHeight = -1;
        public StyleProperty<int> preferredWidth = -1;
        public StyleProperty<int> preferredHeight = -1;
        public StyleProperty<int> flexibleWidth = -1;
        public StyleProperty<int> flexibleHeight = -1;
            
        [Header("Content Size Fitter")]
        public StyleProperty<ContentSizeFitter.FitMode> horizontalFit;
        public StyleProperty<ContentSizeFitter.FitMode> verticalFit;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            var elementResolver = resolver.ForType<ElementStyleData>();
            elementResolver.Resolve(ref minWidth, d => d.minWidth);
            elementResolver.Resolve(ref minHeight, d => d.minHeight);
            elementResolver.Resolve(ref preferredWidth, d => d.preferredWidth);
            elementResolver.Resolve(ref preferredHeight, d => d.preferredHeight);
            elementResolver.Resolve(ref flexibleWidth, d => d.flexibleWidth);
            elementResolver.Resolve(ref flexibleHeight, d => d.flexibleHeight);
            
            elementResolver.Resolve(ref horizontalFit, d => d.horizontalFit);
            elementResolver.Resolve(ref verticalFit, d => d.verticalFit);
        }
    }
}