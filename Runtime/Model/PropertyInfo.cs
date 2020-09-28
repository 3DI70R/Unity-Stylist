using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    public struct PropertyInfo<T>
    {
        public StyleProperty<T> value;
        public ElementStyle style;
    }
}