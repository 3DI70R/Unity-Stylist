using System;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public struct StyleReference<O> 
        where O : StyleData, new()
    {
        public InheritedStyles inherits;
        
        [FlattenAttribute]
        public O overrides;

        public static implicit operator O(StyleReference<O> reference) => reference.overrides;
    }
}