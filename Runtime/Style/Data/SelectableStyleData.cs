using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class SelectableStyleData : ElementStyleData
    {
        private static readonly ColorBlock defaultColorBlock = ColorBlock.defaultColorBlock;
    
        [Header("Color block")] 
        public StyleProperty<Color> colorNormal = defaultColorBlock.normalColor;
        public StyleProperty<Color> colorHighlighted = defaultColorBlock.highlightedColor;
        public StyleProperty<Color> colorPressed = defaultColorBlock.pressedColor;
        public StyleProperty<Color> colorSelected = defaultColorBlock.selectedColor;
        public StyleProperty<Color> colorDisabled = defaultColorBlock.disabledColor;
        public StyleProperty<float> colorMultiplier = defaultColorBlock.colorMultiplier;
        public StyleProperty<float> colorFadeDuration = defaultColorBlock.fadeDuration;
    }
}