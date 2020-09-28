using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ImageStyleData : GraphicStyleData
    {
        [Header("Image")] 
        public StyleProperty<Sprite> sprite;
    }
}