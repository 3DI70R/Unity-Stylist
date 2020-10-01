using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ElementStyleData
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

        public virtual void Resolve(StyleResolver<ElementStyleData> resolver)
        {
            resolver.Resolve(ref minWidth, d => d.minWidth);
            resolver.Resolve(ref minHeight, d => d.minHeight);
            resolver.Resolve(ref preferredWidth, d => d.preferredWidth);
            resolver.Resolve(ref preferredHeight, d => d.preferredHeight);
            resolver.Resolve(ref flexibleWidth, d => d.flexibleWidth);
            resolver.Resolve(ref flexibleHeight, d => d.flexibleHeight);
            
            resolver.Resolve(ref horizontalFit, d => d.horizontalFit);
            resolver.Resolve(ref verticalFit, d => d.verticalFit);
        }
    }
}