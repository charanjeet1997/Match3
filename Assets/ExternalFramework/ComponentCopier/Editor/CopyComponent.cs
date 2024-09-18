using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ComponentCopyier
{
    public class CopyComponent : Editor
    {
        static Dictionary<Component,bool> sourceComponents = null;

        private Vector2 visualizerEditorScrollPos;
        private GameObject prevSelectedObject;


        [MenuItem("GameObject/Copy Components", false, 10)]
        private static void CopyComponents(MenuCommand menuCommand)
        {
            GameObject selectedObject = menuCommand.context as GameObject;

            if (selectedObject == null)
            {
                Debug.LogError("Please select a GameObject in the Hierarchy.");
                return;
            }

            GameObject sourceObject = Selection.activeGameObject;
            GameObject targetObject = selectedObject;

            if (sourceObject != null)
            {
                CopyComponentsFromSource(sourceObject);
                Debug.Log("Components copied successfully!");
            }
            else
            {
                Debug.LogError("Please select both source and target GameObjects.");
            }
        }
        
        [MenuItem("GameObject/Paste Components", false, 10)]
        private static void PasteComponents(MenuCommand menuCommand)
        {
            GameObject selectedObject = menuCommand.context as GameObject;

            if (selectedObject == null)
            {
                Debug.LogError("Please select a GameObject in the Hierarchy.");
                return;
            }

            GameObject sourceObject = Selection.activeGameObject;
            GameObject targetObject = selectedObject;

            if (targetObject != null)
            {
                PasteComponentsToTarget(targetObject);
                Debug.Log("Components copied successfully!");
            }
            else
            {
                Debug.LogError("Please select both source and target GameObjects.");
            }
        }

        private static void CopyComponentsFromSource(GameObject sourceObject)
        {
            var components = sourceObject.GetComponents<Component>();
            sourceComponents = new Dictionary<Component, bool>();
            if (sourceComponents != null && components.Length > 0)
            {
                foreach (Component component in components)
                {
                    if (component == null) continue;
                    System.Type type = component.GetType();
                    sourceComponents.Add(component,true);
                    UnityEditorInternal.ComponentUtility.CopyComponent(component);
                }
            }
        }
        
        private static void PasteComponentsToTarget( GameObject targetObject)
        {
            if (sourceComponents != null)
            {
                var components = new List<Component>(sourceComponents.Keys);
                foreach (Component component in components)
                {
                    if (sourceComponents[component])
                    {
                        if (component == null) continue;

                        System.Type type = component.GetType();
                        UnityEditorInternal.ComponentUtility.CopyComponent(component);
                        var componentType = component.GetType();
                        if (targetObject.TryGetComponent(componentType,out Component c))
                        {
                            UnityEditorInternal.ComponentUtility.PasteComponentValues(c);
                        }
                        else
                        {
                            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetObject);
                        }
                    }
                }
            }
        }
    }
}