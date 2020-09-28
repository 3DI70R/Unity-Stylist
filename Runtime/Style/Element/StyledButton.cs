using System.Collections.Generic;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    public class StyledButton : StyledElement
    {
        public Button button;
        public Image background;
        public Text innerText;
        public Graphic[] innerGraphic;

        protected override void ApplyStyle(List<ResolvedStyle> outProperties)
        {
            var resolver = Style.CreateResolver<ButtonStyleData>();
            var claimer = new StyleUtils.LayoutClaimer();

            StyleUtils.ApplyStyle(claimer, resolver, button).AddTo(outProperties);
            StyleUtils.ApplyStyle(claimer, resolver.As(d => d.backgroundStyle), background).AddTo(outProperties);
            StyleUtils.ApplyStyle(claimer, resolver.As(d => d.innerText), innerText).AddTo(outProperties);
            StyleUtils.ApplyStyle(claimer, resolver.As(d => d.innerGraphic), innerGraphic).AddTo(outProperties);
        }
    }
}