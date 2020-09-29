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
        public StyleReference<ImageStyleData> icon;
    }
}