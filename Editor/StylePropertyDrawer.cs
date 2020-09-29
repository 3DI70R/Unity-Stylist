using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomPropertyDrawer(typeof(StyleProperty<>))]
    public class StylePropertyDrawer : PropertyDrawer
    {
        private const int toggleWidth = 24;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty enumProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.type));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.value));

            var indentedRect = EditorGUI.IndentedRect(position); // to fix weird clickable region
            var overridePosition = Rect.MinMaxRect(position.xMin, position.yMin, indentedRect.xMin + toggleWidth, position.yMax);
            var valuePosition = new Rect(position.x + toggleWidth, position.y, position.width - toggleWidth, position.height);
            var oldEnabled = GUI.enabled;
            var type = enumProperty.enumValueIndex;

            var enumType = (PropertyApplyType) type;
            var isEnabled = enumType == PropertyApplyType.Assigned;
            var highlightRect = position;
            highlightRect.y -= 1;
            highlightRect.height += 2;

            if (enumType == PropertyApplyType.Clear)
            {
                EditorGUI.DrawRect(highlightRect, new Color(1f, 0, 0, 0.1f));
                EditorGUI.showMixedValue = true;
            }
            else if (enumType == PropertyApplyType.Assigned)
            {
                EditorGUI.DrawRect(highlightRect, new Color(0, 1f, 0, 0.1f));
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