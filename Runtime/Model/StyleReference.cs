using System;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public struct StyleReference<O> 
        where O : ElementStyleData
    {
        public InheritedStyles inherits;
        public O overrides;
    }
}