using System.Collections.Generic;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [ExecuteInEditMode]
    public abstract class StyledElement : MonoBehaviour
    {
        [SerializeField] private ElementStyle style;

        public ElementStyle Style
        {
            get => style;
            set
            {
                style = value;
                ApplyStyle();
            }
        }

        public List<ResolvedStyle> ResolvedStyles { get; private set; }

#if UNITY_EDITOR
        protected void Update()
        {
            ApplyStyle();
        }
#endif

        private void ApplyStyle()
        {
            if (Application.isPlaying || !Style)
                return;

            if (ResolvedStyles == null)
            {
                ResolvedStyles = new List<ResolvedStyle>();
            }
            else
            {
                ResolvedStyles.Clear();
            }

            ApplyStyle(ResolvedStyles);
        }

        protected abstract void ApplyStyle(List<ResolvedStyle> outProperties);
    }
}