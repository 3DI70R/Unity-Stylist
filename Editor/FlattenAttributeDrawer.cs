using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    //[CustomPropertyDrawer(typeof(FlattenAttribute))]
    public class FlattenAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = true;
            EditorGUI.indentLevel--;
            EditorGUI.PropertyField(position, property, new GUIContent(""), true);
            EditorGUI.indentLevel++;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            property.isExpanded = true;
            return EditorGUI.GetPropertyHeight(property, label);
        }
    }
}