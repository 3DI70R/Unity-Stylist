using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomPropertyDrawer(typeof(InheritedStyles))]
    public class StyleInheritancePropertyDrawer : PropertyDrawer
    {
        private readonly string listTitle = "Inherits";

        private static readonly float topSpacing = EditorGUIUtility.singleLineHeight / 2f;
        
        private ReorderableList list;
        private SerializedObject listObject;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.y += topSpacing;
            
            var indent = EditorGUI.indentLevel;
            var rect = EditorGUI.IndentedRect(position);
            EditorGUI.indentLevel = 0;
            GetList(property).DoList(rect);
            AcceptDragAndDrop(rect);
            EditorGUI.indentLevel = indent;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetList(property).GetHeight() + topSpacing;
        }

        private ReorderableList GetList(SerializedProperty property)
        {
            if (listObject == property.serializedObject)
                return list;

            var arrayProperty = property.FindPropertyRelative(nameof(InheritedStyles.array));

            listObject = property.serializedObject;
            list = new ReorderableList(property.serializedObject, arrayProperty, true, false, true, true);
            list.drawHeaderCallback += rect => { EditorGUI.LabelField(rect, listTitle); };
            list.drawElementCallback += (rect, i, isActive, isFocused) =>
            {
                EditorGUI.PropertyField(rect, arrayProperty.GetArrayElementAtIndex(i), new GUIContent());
            };
            
            return list;
        }

        private void AcceptDragAndDrop(Rect rect)
        {
            var e = Event.current;
            
            if (!rect.Contains(e.mousePosition))
                return;

            switch (e.type)
            {
                case EventType.DragUpdated:
                    DragAndDrop.visualMode = DragAndDrop.objectReferences.Any(s => s is ElementStyle)
                        ? DragAndDropVisualMode.Copy
                        : DragAndDropVisualMode.Rejected;
                    break;
                
                case EventType.DragPerform:
                {
                    foreach (var elementStyle in DragAndDrop.objectReferences
                        .Where(s => s is ElementStyle)
                        .Cast<ElementStyle>())
                    {
                        var arrayIndex = list.serializedProperty.arraySize;
                        list.serializedProperty.InsertArrayElementAtIndex(arrayIndex);
                        list.serializedProperty.GetArrayElementAtIndex(arrayIndex).objectReferenceValue = elementStyle;
                    }

                    DragAndDrop.AcceptDrag();
                    break;
                }
            }
        }
    }
}