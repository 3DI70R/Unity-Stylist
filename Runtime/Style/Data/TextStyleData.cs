using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class TextStyleData : GraphicStyleData
    {
        private static readonly FontData defaultFontData = FontData.defaultFontData;
        
        [Header("Character")] 
        public StyleProperty<Font> font = defaultFontData.font;
        public StyleProperty<FontStyle> fontStyle = defaultFontData.fontStyle;
        public StyleProperty<int> fontSize = defaultFontData.fontSize;
        public StyleProperty<float> lineSpacing = defaultFontData.lineSpacing;
        public StyleProperty<bool> richText = defaultFontData.richText;

        [Header("Paragraph")] 
        public StyleProperty<TextAnchor> alignment = TextAnchor.MiddleCenter;
        public StyleProperty<bool> alignByGeometry = defaultFontData.alignByGeometry;
        public StyleProperty<HorizontalWrapMode> horizontalOverflow = defaultFontData.horizontalOverflow;
        public StyleProperty<VerticalWrapMode> verticalOverflow = defaultFontData.verticalOverflow;
        public StyleProperty<bool> bestFit = defaultFontData.bestFit;
        public StyleProperty<int> bestFitMinSize = defaultFontData.minSize;
        public StyleProperty<int> bestFitMaxSize = defaultFontData.maxSize;

        public override void Resolve(StyleResolver<ElementStyleData> resolver)
        {
            base.Resolve(resolver);

            var textResolver = resolver.ForType<TextStyleData>();
            textResolver.Resolve(ref font, d => d.font);
            textResolver.Resolve(ref fontStyle, d => d.fontStyle);
            textResolver.Resolve(ref fontSize, d => d.fontSize);
            textResolver.Resolve(ref lineSpacing, d => d.lineSpacing);
            textResolver.Resolve(ref richText, d => d.richText);
            
            textResolver.Resolve(ref alignment, d => d.alignment);
            textResolver.Resolve(ref alignByGeometry, d => d.alignByGeometry);
            textResolver.Resolve(ref horizontalOverflow, d => d.horizontalOverflow);
            textResolver.Resolve(ref verticalOverflow, d => d.verticalOverflow);
            textResolver.Resolve(ref bestFit, d => d.bestFit);
            textResolver.Resolve(ref bestFitMinSize, d => d.bestFitMinSize);
            textResolver.Resolve(ref bestFitMaxSize, d => d.bestFitMaxSize);
        }
    }
}