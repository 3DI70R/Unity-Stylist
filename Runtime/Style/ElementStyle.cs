using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    public abstract class ElementStyle : ScriptableObject
    {
        protected const string MenuCategory = "UI Style (Stylist)/";
        
#pragma warning disable 649
        [SerializeField] private InheritedStyles inherits;
#pragma warning restore 649
        
        public ElementStyle[] Inherits => inherits.array;
        public abstract ElementStyleData Overrides { get; }
    }
    
    public abstract class ElementStyle<T> : ElementStyle
        where T : ElementStyleData
    {
#pragma warning disable 649
        [SerializeField] private T overrides;
#pragma warning restore 649
        
        public override ElementStyleData Overrides => overrides;
    }
}