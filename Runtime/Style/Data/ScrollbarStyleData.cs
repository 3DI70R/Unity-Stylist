using System;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ScrollbarStyleData : SelectableStyleData
    {
        public StyleReference<ImageStyleData> background;
        public StyleReference<ImageStyleData> handle;
    }
}