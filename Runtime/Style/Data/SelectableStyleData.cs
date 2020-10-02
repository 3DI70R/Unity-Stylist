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

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var selectableData = resolver.ForType<SelectableStyleData>();
            selectableData.Resolve(ref transition, d => d.transition);
            
            selectableData.Resolve(ref colorNormal, d => d.colorNormal);
            selectableData.Resolve(ref colorHighlighted, d => d.colorHighlighted);
            selectableData.Resolve(ref colorPressed, d => d.colorPressed);
            selectableData.Resolve(ref colorSelected, d => d.colorSelected);
            selectableData.Resolve(ref colorDisabled, d => d.colorDisabled);
            selectableData.Resolve(ref colorMultiplier, d => d.colorMultiplier);
            selectableData.Resolve(ref colorFadeDuration, d => d.colorFadeDuration);
            
            selectableData.Resolve(ref spriteHighlighted, d => d.spriteHighlighted);
            selectableData.Resolve(ref spritePressed, d => d.spritePressed);
            selectableData.Resolve(ref spriteSelected, d => d.spriteSelected);
            selectableData.Resolve(ref spriteDisabled, d => d.spriteDisabled);
        }
    }
}