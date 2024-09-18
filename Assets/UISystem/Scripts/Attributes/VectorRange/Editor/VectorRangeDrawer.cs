namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(VectorRangeAttribute))]
    public class VectorRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rangeAttribute = (VectorRangeAttribute) base.attribute;
            float lastY = position.y;
            rangeAttribute.shouldDraw = EditorGUI.Foldout(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight), rangeAttribute.shouldDraw, label);
            if (rangeAttribute.shouldDraw)
            {
                Vector4 tempVector4Value;
                
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Vector2:
                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value = property.vector2Value;
                        tempVector4Value.x =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "X", tempVector4Value.x, rangeAttribute.min, rangeAttribute.max);

                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value.y =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Y", tempVector4Value.y, rangeAttribute.min, rangeAttribute.max);
                        property.vector2Value = tempVector4Value;    
                        break;
                    case SerializedPropertyType.Vector3:
                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value = property.vector3Value;
                        tempVector4Value.x =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "X", tempVector4Value.x, rangeAttribute.min, rangeAttribute.max);

                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value.y =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Y", tempVector4Value.y, rangeAttribute.min, rangeAttribute.max);
                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value.z =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Z", tempVector4Value.z, rangeAttribute.min, rangeAttribute.max);
                        property.vector3Value = tempVector4Value;    
                        break;
                    case SerializedPropertyType.Vector4:
                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value = property.vector4Value;
                        tempVector4Value.x =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "X", tempVector4Value.x, rangeAttribute.min, rangeAttribute.max);

                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value.y =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Y", tempVector4Value.y, rangeAttribute.min, rangeAttribute.max);
                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector4Value.z =
                            EditorGUI.Slider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Z", tempVector4Value.z, rangeAttribute.min, rangeAttribute.max);
                        property.vector4Value = tempVector4Value;    
                        break;
                    
                    case SerializedPropertyType.Vector2Int:
                        lastY += EditorGUIUtility.singleLineHeight;
                        Vector2Int tempVector2IntValue = property.vector2IntValue;
                        tempVector2IntValue.x =
                            EditorGUI.IntSlider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "X", tempVector2IntValue.x, (int)rangeAttribute.min, (int)rangeAttribute.max);

                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector2IntValue.y =
                            EditorGUI.IntSlider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Y", tempVector2IntValue.y, (int)rangeAttribute.min, (int)rangeAttribute.max);
                        property.vector2IntValue = tempVector2IntValue;    
                        break;
                    case SerializedPropertyType.Vector3Int:
                        lastY += EditorGUIUtility.singleLineHeight;
                        Vector3Int tempVector3IntValue = property.vector3IntValue;
                        tempVector3IntValue.x =
                            EditorGUI.IntSlider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "X", tempVector3IntValue.x, (int)rangeAttribute.min, (int)rangeAttribute.max);

                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector3IntValue.y =
                            EditorGUI.IntSlider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Y", tempVector3IntValue.y, (int)rangeAttribute.min, (int)rangeAttribute.max);
                        
                        lastY += EditorGUIUtility.singleLineHeight;
                        tempVector3IntValue.z =
                            EditorGUI.IntSlider(new Rect(position.x, lastY, position.width, EditorGUIUtility.singleLineHeight),
                                "Z", tempVector3IntValue.z, (int)rangeAttribute.min, (int)rangeAttribute.max);
                        
                        property.vector3IntValue = tempVector3IntValue;    
                        break;
                    default:
                        EditorGUI.LabelField(position, label.text, "Use Range with Vector2,3,4.");
                        break;
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * CalculateLineCount(property) +
                   EditorGUIUtility.standardVerticalSpacing * (CalculateLineCount(property) - 1);
        }
        
        private int CalculateLineCount(SerializedProperty property)
        {
            int lineCount = 1;
            var rangeAttribute = (VectorRangeAttribute) base.attribute;

            if (rangeAttribute.shouldDraw)
            {
                if (property.propertyType == SerializedPropertyType.Vector2)
                {
                    lineCount = 3;
                }
                else if (property.propertyType == SerializedPropertyType.Vector3)
                {
                    lineCount = 4;
                }
                else if (property.propertyType == SerializedPropertyType.Vector4)
                {
                    lineCount = 5;
                }
                else if (property.propertyType == SerializedPropertyType.Vector2Int)
                {
                    lineCount = 3;
                }
                else if (property.propertyType == SerializedPropertyType.Vector3Int)
                {
                    lineCount = 4;
                }
            }

            return lineCount;
        }
    }
}