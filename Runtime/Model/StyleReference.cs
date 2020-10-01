using System;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public struct StyleReference<O> 
        where O : ElementStyleData, new()
    {
        public InheritedStyles inherits;
        public O overrides;

        public static implicit operator O(StyleReference<O> reference) => reference.overrides;
    }
}