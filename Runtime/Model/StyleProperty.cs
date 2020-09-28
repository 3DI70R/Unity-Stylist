using System;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public struct StyleProperty<P>
    {
        public PropertyApplyType type;
        public P value;

        public bool IsAssigned => type == PropertyApplyType.Assigned;

        public static implicit operator StyleProperty<P>(P value) => 
            new StyleProperty<P> { value = value};
        
        public static implicit operator P(StyleProperty<P> value) => 
            value.value;
    }
}