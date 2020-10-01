using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [ExecuteInEditMode]
    public abstract class StyledElement<T> : MonoBehaviour
        where T : ElementStyleData, new()
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

            StyleUtils.ClearTrackedObjects();
            ApplyStyle(Style.Resolve<T>());
        }

        public void ApplyStyleFromParent(T parentStyle)
        {
            // ignore parent style if style already assigned by element
            if(style || parentStyle == null) 
                return;

            ApplyStyle(parentStyle);
        }

        protected abstract void ApplyStyle(T style);
    }
}