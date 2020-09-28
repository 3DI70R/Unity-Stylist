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
    }
}