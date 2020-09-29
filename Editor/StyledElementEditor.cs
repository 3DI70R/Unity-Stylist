using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    [CustomEditor(typeof(StyledElement), true)]
    public class StyledElementEditor : Editor
    {
        private static bool hideDefault;
        
        private const string defaultOriginName = "Default";
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space();

            if (target is StyledElement element)
            {
                if(element.ResolvedStyles == null)
                    return;
                
                EditorGUILayout.LabelField("Resolved properties", EditorStyles.centeredGreyMiniLabel);
                //hideDefault = EditorGUILayout.Toggle("Hide default properties", hideDefault);

                var labelStyle = new GUIStyle(EditorStyles.miniLabel)
                {
                    richText = true
                };

                var currentGroup = "";
                
                foreach (var typeProperties in element.ResolvedStyles)
                {
                    var typeName = typeProperties.target.gameObject.name + "#" + typeProperties.target.GetType().Name;
                    
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField($"{typeName} properties:", EditorStyles.miniLabel);
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    foreach (var property in typeProperties.properties)
                    {
                        if(!property.style && hideDefault)
                            continue;

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