using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class DropdownStyleData : SelectableStyleData
    {
        [Header("Dropdown")] 
        public StyleProperty<float> alphaFadeSpeed = 0.15f;

        [Header("References")] 
        public StyleReference<ImageStyleData> background;
        public StyleReference<ImageStyleData> arrow;
        public StyleReference<TextStyleData> text;
        public StyleReference<ScrollRectStyleData> dropdownBackground;
        public StyleReference<ToggleStyleData> dropdownItem;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var dropdownResolver = resolver.ForType<DropdownStyleData>();
            dropdownResolver.Resolve(ref alphaFadeSpeed, d => d.alphaFadeSpeed);
            
            dropdownResolver.Resolve(ref background, d => d.background);
            dropdownResolver.Resolve(ref arrow, d => d.arrow);
            dropdownResolver.Resolve(ref text, d => d.text);
            dropdownResolver.Resolve(ref dropdownBackground, d => d.dropdownBackground);
            dropdownResolver.Resolve(ref dropdownItem, d => d.dropdownItem);
        }
    }
}