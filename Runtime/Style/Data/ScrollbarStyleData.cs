﻿using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class ScrollbarStyleData : SelectableStyleData
    {
        [Header("References")]
        public StyleReference<ImageStyleData> background;
        public StyleReference<ImageStyleData> handle;

        public override void Resolve(StyleResolver<StyleData> resolver)
        {
            base.Resolve(resolver);

            var scrollbarResolver = resolver.ForType<ScrollbarStyleData>();
            scrollbarResolver.Resolve(ref background, d => d.background);
            scrollbarResolver.Resolve(ref handle, d => d.handle);
        }
    }
}