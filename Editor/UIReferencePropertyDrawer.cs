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
            EditorGUI.PropertyField(position, GetField(property), label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(GetField(property), label);
        }

        private SerializedProperty GetField(SerializedProperty property)
        {
            return property.FindPropertyRelative(nameof(UIReference<UIBehaviour>.value));
        }
    }
}