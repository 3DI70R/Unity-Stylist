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

        public StyleReference<ImageStyleData> background;
        public StyleReference<ImageStyleData> checkmarkBackground;
        public StyleReference<ImageStyleData> checkmark;
        public StyleReference<TextStyleData> text;
    }
}