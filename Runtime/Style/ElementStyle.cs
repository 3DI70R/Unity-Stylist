using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    public abstract class ElementStyle : ScriptableObject
    {
        protected const string MenuCategory = "UI Style/";
        
        [SerializeField] private ElementStyle[] inherits;
        
        public ElementStyle[] Inherits => inherits;
        public abstract ElementStyleData Overrides { get; }
    }
    
    public abstract class ElementStyle<T> : ElementStyle
        where T : ElementStyleData
    {
        [SerializeField] 
        private T overrides;
        
        public override ElementStyleData Overrides => overrides;
    }
}