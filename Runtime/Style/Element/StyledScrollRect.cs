﻿using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledScrollRect : StyledElement<ScrollRectStyleData>
    {
        public ScrollRect scrollView;
        public Image background;
        public StyledScrollbar horizontalScrollbar;
        public StyledScrollbar verticalScrollbar;

        protected override void ApplyStyle(ScrollRectStyleData style)
        {
            StyleUtils.Apply(style, scrollView);
            StyleUtils.Apply(style.background, background);

            if (horizontalScrollbar)
                horizontalScrollbar.ApplyStyleFromParent(style.scrollBar);
            
            if (verticalScrollbar)
                verticalScrollbar.ApplyStyleFromParent(style.scrollBar);
        }
    }
}