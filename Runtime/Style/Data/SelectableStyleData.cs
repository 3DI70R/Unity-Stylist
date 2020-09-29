using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class SelectableStyleData : ElementStyleData
    {
        private static readonly ColorBlock defaultColorBlock = ColorBlock.defaultColorBlock;

        [Header("Selectable")] 
        public StyleProperty<Selectable.Transition> transition = Selectable.Transition.ColorTint;
        
        [Header("Color tint")] 
        public StyleProperty<Color> colorNormal = defaultColorBlock.normalColor;
        public StyleProperty<Color> colorHighlighted = defaultColorBlock.highlightedColor;
        public StyleProperty<Color> colorPressed = defaultColorBlock.pressedColor;
        public StyleProperty<Color> colorSelected = defaultColorBlock.selectedColor;
        public StyleProperty<Color> colorDisabled = defaultColorBlock.disabledColor;
        public StyleProperty<float> colorMultiplier = defaultColorBlock.colorMultiplier;
        public StyleProperty<float> colorFadeDuration = defaultColorBlock.fadeDuration;
        
        [Header("Sprite Swap")]
        public StyleProperty<Sprite> spriteHighlighted;
        public StyleProperty<Sprite> spritePressed;
        public StyleProperty<Sprite> spriteSelected;
        public StyleProperty<Sprite> spriteDisabled;
    }
}