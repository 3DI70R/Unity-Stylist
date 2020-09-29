using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomEditor(typeof(StyledElement), true)]
    public class StyledElementEditor : Editor
    {
        private const string defaultOriginName = "Default";
        private static bool showDefaultProperties = false;
        private MonoBehaviour lastClickedBehaviour;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space();

            if (target is StyledElement element)
            {
                if(element.ResolvedProperties == null)
                    return;
                
                EditorGUILayout.LabelField("Resolved properties", EditorStyles.centeredGreyMiniLabel);
                showDefaultProperties = EditorGUILayout.ToggleLeft("Show unassigned values", showDefaultProperties);

                var labelStyle = new GUIStyle(EditorStyles.miniLabel)
                {
                    richText = true
                };

                var currentGroup = "";
                
                foreach (var typeProperties in element.ResolvedProperties.GroupBy(e => e.target))
                {
                    var propertiesToShow = (showDefaultProperties
                        ? typeProperties
                        : typeProperties.Where(p => p.style != null)).ToList();
                    
                    if(propertiesToShow.Count == 0)
                        continue;

                    var targetComponent = typeProperties.Key;
                    var typeName = targetComponent.gameObject.name + "#" + targetComponent.GetType().Name;
                    
                    EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
                    var clickableRect = EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    if (GUI.Button(clickableRect, $"{typeName}", new GUIStyle(EditorStyles.miniButtonMid)))
                    {
                        EditorGUIUtility.PingObject(targetComponent);
                    }
                    
                    EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

                    foreach (var property in propertiesToShow)
                    {
                        if (currentGroup != property.group)
                        {
                            currentGroup = property.group;
                            
                            var headerRect = EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(property.group, EditorStyles.centeredGreyMiniLabel);
                            EditorGUI.DrawRect(new Rect(headerRect.x, headerRect.yMax - 2, headerRect.width, 2), 
                                new Color(0, 0, 0, 0.1f));
                            EditorGUILayout.EndHorizontal();
                        }

                        var rect = EditorGUILayout.BeginHorizontal();
                        var originName = defaultOriginName;

                        if (property.style)
                        {
                            originName = $"<b>{property.style.name}</b>";
                            EditorGUI.DrawRect(rect, new Color(0, 1f, 0f, 0.1f));
                        }

                        var fullName = property.name;
                        var fullValue = $"{originName}, {property.value ?? "null"}";
                        
                        EditorGUILayout.LabelField(fullName, fullValue, labelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    EditorGUILayout.EndVertical();
                }
            }
        }
    }
}