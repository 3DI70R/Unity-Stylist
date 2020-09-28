using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ButtonStyleData : SelectableStyleData
    {
        [Space(0)]
        [Header("Button", order = 1)]
        public StyleReference<ImageStyleData> backgroundStyle;
        public StyleReference<TextStyleData> innerText;
        public StyleReference<GraphicStyleData> innerGraphic;
    }
}