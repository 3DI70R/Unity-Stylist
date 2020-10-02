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
        
        public Shadow shadowComponent;
        public LayoutElement layoutElement;
        public ContentSizeFitter sizeFitter;

        public static implicit operator bool(UIReference<T> reference)
        {
            return reference != null && reference.value;
        }
    }
}