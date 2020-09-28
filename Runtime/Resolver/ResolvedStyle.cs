using System.Collections.Generic;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    public struct ResolvedStyle
    {
        public MonoBehaviour target;
        public List<ResolvedProperty> properties;
    }
}