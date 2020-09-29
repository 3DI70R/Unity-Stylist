using System;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ScrollRectStyleData : ElementStyleData
    {
        public StyleProperty<ScrollRect.MovementType> movementType = ScrollRect.MovementType.Elastic;
        public StyleProperty<float> elasticity = 0.1f;
        public StyleProperty<bool> inertia = true;
        public StyleProperty<float> decelerationRate = 0.135f;
        public StyleProperty<float> scrollSensitivity = 1f;
        
        public StyleReference<ImageStyleData> background;
        public StyleReference<ScrollbarStyleData> scrollBar;
    }
}