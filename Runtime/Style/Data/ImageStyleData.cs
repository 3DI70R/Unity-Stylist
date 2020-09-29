using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ImageStyleData : GraphicStyleData
    {
        [Header("Image")] 
        public StyleProperty<Sprite> sprite;

        public StyleProperty<bool> preserveAspect = false;
        public StyleProperty<bool> fillCenter = true;
        public StyleProperty<bool> useSpriteMesh = false;
        public StyleProperty<float> pixelsPerUnitMultiplier = 1f;
    }
}