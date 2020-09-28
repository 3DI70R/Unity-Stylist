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

                        var rect = EditorGUILayout.BeginHorizontal();
                        var originName = defaultOriginName;

                        if (property.style)
                        {
                            originName = $"<b>{property.style.name}</b>";
                            EditorGUI.DrawRect(rect, new Color(0, 1f, 0f, 0.1f));
                        }

                        var fullValue = $"{originName}, {property.value ?? "null"}";
                        
                        EditorGUILayout.LabelField(property.name, fullValue, labelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    EditorGUILayout.EndVertical();
                }
            }
        }
    }
}