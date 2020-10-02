using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomPropertyDrawer(typeof(UIReference<>))]
    public class UIReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            var field = GetField(property);
            EditorGUI.PropertyField(position, field, label);

            if (EditorGUI.EndChangeCheck())
            {
                OnValueChanged(property, (MonoBehaviour) field.objectReferenceValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(GetField(property), label);
        }

        private SerializedProperty GetField(SerializedProperty property)
        {
            return property.FindPropertyRelative(nameof(UIReference<UIBehaviour>.value));
        }
        
        protected virtual void OnValueChanged(SerializedProperty property, MonoBehaviour value)
        {
            var layoutProperty = property.FindPropertyRelative(nameof(UIReference<MonoBehaviour>.layoutElement));
            var shadowProperty = property.FindPropertyRelative(nameof(UIReference<MonoBehaviour>.shadow));
            var sizeFitterProperty = property.FindPropertyRelative(nameof(UIReference<MonoBehaviour>.sizeFitter));

            if (value)
            {
                value.TryGetComponent<LayoutElement>(out var layoutElement);
                value.TryGetComponent<Shadow>(out var shadow);
                value.TryGetComponent<ContentSizeFitter>(out var contentSizeFitter);

                layoutProperty.objectReferenceValue = layoutElement;
                shadowProperty.objectReferenceValue = shadow;
                sizeFitterProperty.objectReferenceValue = contentSizeFitter;
            }
            else
            {
                layoutProperty.objectReferenceValue = null;
                shadowProperty.objectReferenceValue = null;
                sizeFitterProperty.objectReferenceValue = null;
            }
        }
    }
}