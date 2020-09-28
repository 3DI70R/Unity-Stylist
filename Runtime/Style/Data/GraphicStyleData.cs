using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class GraphicStyleData : ElementStyleData
    {
        [Header("Visual")] 
        public StyleProperty<Color> color = Color.white;
        public StyleProperty<Material> material;

        [Header("Shadow")] 
        public StyleProperty<ShadowType> shadowType = ShadowType.None;
        public StyleProperty<Vector2> shadowDistance = new Vector2(1f, -1f);
        public StyleProperty<Color> shadowColor = new Color(0f, 0f, 0f, 0.5f);
        
        public enum ShadowType
        {
            None, Shadow, Outline
        }
    }
}