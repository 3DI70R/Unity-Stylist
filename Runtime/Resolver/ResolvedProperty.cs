using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    public struct ResolvedProperty
    {
        public string name;
        public string group;
        public object value;
        public ElementStyle style;
        public MonoBehaviour target;
    }
}