using System;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public struct StyleReference<O> 
        where O : ElementStyleData
    {
        public ElementStyle[] inherits;
        public O overrides;
    }
}