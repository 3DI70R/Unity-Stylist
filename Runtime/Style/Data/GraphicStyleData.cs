using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class GraphicStyleData : ElementStyleData
    {
        [Header("Shadow")] 
        public StyleProperty<ShadowType> shadowType = ShadowType.None;
        public StyleProperty<Vector2> shadowDistance = new Vector2(1f, -1f);
        public StyleProperty<Color> shadowColor = new Color(0f, 0f, 0f, 0.5f);
        public StyleProperty<bool> shadowUseGraphicAlpha = true;
        
        [Header("Visual")] 
        public StyleProperty<Color> color = Color.white;
        public StyleProperty<Material> material;

        public enum ShadowType
        {
            None, Shadow, Outline
        }

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var graphicResolver = resolver.ForType<GraphicStyleData>();
            graphicResolver.Resolve(ref shadowType, d => d.shadowType);
            graphicResolver.Resolve(ref shadowDistance, d => d.shadowDistance);
            graphicResolver.Resolve(ref shadowColor, d => d.shadowColor);
            graphicResolver.Resolve(ref shadowUseGraphicAlpha, d => d.shadowUseGraphicAlpha);
            
            graphicResolver.Resolve(ref color, d => d.color);
            graphicResolver.Resolve(ref material, d => d.material);
        }
    }
}