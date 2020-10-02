using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [ExecuteInEditMode]
    [DefaultExecutionOrder(-1000)]
    public abstract class StyledElement<S, T> : MonoBehaviour
        where S : ElementStyle<T>
        where T : ElementStyleData, new()
    {
        [SerializeField] private S style;

        public S Style
        {
            get => style;
            set
            {
                style = value;
                ApplyAssignedStyle();
            }
        }

        private void Awake()
        {
            ApplyAssignedStyle();
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