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
                ApplyAssignedStyle();
            }
        }
        
#if UNITY_EDITOR
        public List<ResolvedProperty> ResolvedProperties { get; private set; }
        
        protected void Update()
        {
            if (Application.isPlaying)
                return;

            ApplyAssignedStyle();
        }
#endif

        private void ApplyAssignedStyle()
        {
            if(!Style)
                return;

            var resolver = new ObjectStyleResolver<object>(new StyleResolver<object>(style));
            ApplyStyle(resolver);
            ResolvedProperties = resolver.GetResult();
        }

        public void ApplyStyleFromParent<T>(ObjectStyleResolver<T> resolver)
            where T : new()
        {
            if(style) // keep assigned style
                return;

            ApplyStyle(resolver.As<object>());
        }

        protected abstract void ApplyStyle(ObjectStyleResolver<object> resolver);
    }
}