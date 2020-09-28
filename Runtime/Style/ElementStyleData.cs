using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ElementStyleData
    {
        public Layout layout = Layout.GetDefault();
        
        [Serializable]
        public struct Layout
        {
            [Header("Layout Element")]
            public StyleProperty<int> minWidth;
            public StyleProperty<int> minHeight;
            public StyleProperty<int> preferredWidth;
            public StyleProperty<int> preferredHeight;
            public StyleProperty<int> flexibleWidth;
            public StyleProperty<int> flexibleHeight;
            
            [Header("Content Size Fitter")]
            public StyleProperty<ContentSizeFitter.FitMode> horizontalFit;
            public StyleProperty<ContentSizeFitter.FitMode> verticalFit;

            public static Layout GetDefault()
            {
                return new Layout
                {
                    minWidth = -1,
                    minHeight = -1,
                    preferredWidth = -1,
                    preferredHeight = -1,
                    flexibleWidth = -1,
                    flexibleHeight = -1,
                };
            }
        }
    }
}