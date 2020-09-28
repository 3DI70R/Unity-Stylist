using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomPropertyDrawer(typeof(StyleProperty<>))]
    public class StylePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty enumProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.type));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.value));
            
            var overridePosition = new Rect(EditorGUI.indentLevel + position.x, position.y, 128, position.height);
            var valuePosition = new Rect(position.x + 24, position.y, position.width - 24, position.height);
            var oldEnabled = GUI.enabled;
            var type = enumProperty.enumValueIndex;

            var enumType = (PropertyApplyType) type;
            var isEnabled = enumType == PropertyApplyType.Assigned;

            if (enumType == PropertyApplyType.Clear)
            {
                EditorGUI.DrawRect(position, new Color(1f, 0, 0, 0.1f));
                EditorGUI.showMixedValue = true;
            }
            else if (enumType == PropertyApplyType.Assigned)
            {
                EditorGUI.DrawRect(position, new Color(0, 1f, 0, 0.1f));
            }

            if (isEnabled != EditorGUI.Toggle(overridePosition, isEnabled))
            {
                var index = enumProperty.enumValueIndex;
                index++;

                if (index >= enumProperty.enumNames.Length)
                {
                    index = 0;
                }
                
                enumProperty.enumValueIndex = index;
            }
            EditorGUI.showMixedValue = false;
            
            if (!isEnabled)
                GUI.enabled = false;
            
            EditorGUI.PropertyField(valuePosition, valueProperty, 
                new GUIContent(property.displayName), false);
            
            if (!isEnabled)
                GUI.enabled = oldEnabled;
        }
    }
}