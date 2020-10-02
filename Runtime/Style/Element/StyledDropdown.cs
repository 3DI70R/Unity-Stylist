using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledDropdown : StyledElement<DropdownStyle, DropdownStyleData>
    {
        public UIReference<Dropdown> dropdown;
        public UIReference<Image> background;
        public UIReference<Image> arrow;
        public UIReference<Text> text;

        public StyledScrollRect dropdownBackground;
        public StyledToggle dropdownItem;
        
        protected override void ApplyStyle(DropdownStyleData style)
        {
            StyleUtils.Apply(style, dropdown);
            StyleUtils.Apply(style.background, background);
            StyleUtils.Apply(style.arrow, arrow);
            StyleUtils.Apply(style.text, text);
            
            if(dropdownBackground)
                dropdownBackground.ApplyStyleFromParent(style.dropdownBackground);
            
            if(dropdownItem)
                dropdownItem.ApplyStyleFromParent(style.dropdownItem);
        }
    }
}