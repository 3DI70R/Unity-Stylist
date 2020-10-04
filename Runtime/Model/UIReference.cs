using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [Serializable]
    public class UIReference<T> 
        where T : MonoBehaviour
    {
        public T value;

        public Shadow shadow;
        public LayoutElement layoutElement;
        public ContentSizeFitter sizeFitter;

        public virtual void UpdateReferences()
        {
            if(!value)
                return;
            
            value.TryGetComponent(out layoutElement);
            value.TryGetComponent(out shadow);
            value.TryGetComponent(out sizeFitter);
        }

        public static implicit operator T(UIReference<T> reference) => reference?.value;
        public static implicit operator UIReference<T>(T value) => new UIReference<T> { value = value };

        public static implicit operator bool(UIReference<T> reference) => 
            reference != null && reference.value;
    }
}