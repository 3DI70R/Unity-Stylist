using System;
using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomEditor(typeof(ElementStyle), editorForChildClasses: true)]
    public class ElementStyleEditor : Editor
    {
        public static readonly Color inheritedColor = new Color(0, 0.5f, 1f, 0.1f);
        public static readonly Color assignedColor = new Color(0, 1f, 0, 0.1f);
        public static readonly Color clearColor = new Color(1f, 0, 0, 0.1f);
        
        private readonly String legendLabel = "Legend";
        private readonly String defaultPropertyName = "Unassigned";
        private readonly String inheritedPropertyName = "Inherited";
        private readonly String assignedPropertyName = "Assigned";
        private readonly String clearPropertyName = "Cleared";

        private readonly string defaultPropertyDescription = "Property is not assigned, default value will be used";
        private readonly string inheritedPropertyDescription = "Property is not assigned, but inherited from other style";
        private readonly string assignedPropertyDescription = "Property is set by this style";
        private readonly string clearPropertyDescription = "Property is set by this style to default value";
        
        public override void OnInspectorGUI()
        {
            void DrawLegend(String name, String description, Color color)
            {
                var rect = EditorGUILayout.BeginVertical(new GUIStyle { padding = new RectOffset(8, 8, 4, 4) });
                EditorGUI.DrawRect(rect, color);
                EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
                EditorGUILayout.LabelField(description, EditorStyles.wordWrappedMiniLabel);
                EditorGUILayout.EndVertical();
            }

            if (target is ElementStyle elementStyle)
            {
                elementStyle.ResolveSelf();
                EditorUtility.SetDirty(elementStyle);
                serializedObject.Update();
            }

            base.OnInspectorGUI();
            
            EditorGUILayout.LabelField(legendLabel, EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            DrawLegend(defaultPropertyName, defaultPropertyDescription, Color.clear);
            DrawLegend(inheritedPropertyName, inheritedPropertyDescription, inheritedColor);
            DrawLegend(assignedPropertyName, assignedPropertyDescription, assignedColor);
            DrawLegend(clearPropertyName, clearPropertyDescription, clearColor);
            EditorGUILayout.EndVertical();
        }
    }
}