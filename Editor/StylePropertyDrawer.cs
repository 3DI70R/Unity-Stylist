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
            SerializedProperty enumProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.applyType));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.ownValue));
            SerializedProperty resolvedProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.resolvedValue));
            SerializedProperty resolvedAssetProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.resolvedAsset));

            var indentedRect = EditorGUI.IndentedRect(position); // to fix weird clickable region
            var overridePosition = Rect.MinMaxRect(position.xMin, position.yMin, indentedRect.xMin + toggleWidth, position.yMax);
            var valuePosition = new Rect(position.x + toggleWidth, position.y, position.width - toggleWidth, position.height);
            
            var oldEnabled = GUI.enabled;
            var enumType = (PropertyApplyType) enumProperty.enumValueIndex;
            var isEnabled = enumType == PropertyApplyType.Assigned;
            var resolvedAsset = (ElementStyle) resolvedAssetProperty.objectReferenceValue;
            var showResolvedValue = enumType == PropertyApplyType.Unassigned ||
                                    enumType == PropertyApplyType.Clear;
            
            var highlightRect = position;
            highlightRect.xMin -= 1000;
            highlightRect.xMax += 1000;
            highlightRect.yMin -= 1;
            highlightRect.yMax += 1;

            switch (enumType)
            {
                case PropertyApplyType.Assigned:
                    EditorGUI.DrawRect(highlightRect, ElementStyleEditor.assignedColor);
                    break;
                
                case PropertyApplyType.Clear:
                    EditorGUI.DrawRect(highlightRect, ElementStyleEditor.clearColor);
                    EditorGUI.showMixedValue = true;
                    break;
                
                case PropertyApplyType.Unassigned:
                    if(resolvedAsset)
                        EditorGUI.DrawRect(highlightRect, ElementStyleEditor.inheritedColor);
                    break;
            }

            if (isEnabled != EditorGUI.Toggle(overridePosition, isEnabled))
            {
                var index = enumProperty.enumValueIndex;
                index++;

                if (index >= enumProperty.enumNames.Length)
                    index = 0;

                enumProperty.enumValueIndex = index;
            }
            
            EditorGUI.showMixedValue = false;
            
            if (!isEnabled)
                GUI.enabled = false;

            var name = showResolvedValue && resolvedAsset
                ? $"{property.displayName} ({resolvedAsset.name})" 
                : property.displayName;

            var value = showResolvedValue
                ? resolvedProperty
                : valueProperty;
            
            EditorGUI.PropertyField(valuePosition, value, new GUIContent(name), false);
            
            if (!isEnabled)
                GUI.enabled = oldEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(StyleProperty<object>.ownValue));
            return EditorGUI.GetPropertyHeight(valueProperty, label);
        }
    }
}