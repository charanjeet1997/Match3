#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
[InitializeOnLoad]
public static class CanvasUtitlities
{
#region CanvasUtilities
    static CanvasUtitlities()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= DrawCanvasButton;
        EditorApplication.hierarchyWindowItemOnGUI += DrawCanvasButton;
        
        EditorApplication.hierarchyWindowItemOnGUI -= OnMultiSelectChildren;
        EditorApplication.hierarchyWindowItemOnGUI += OnMultiSelectChildren;


        EditorApplication.update -= EditorUpdate;
        EditorApplication.update += EditorUpdate;
    }
#endregion

#region PUBLIC_METHODS

    [MenuItem("Tools/CanvasUtility / SetAnchoreToCenter")]
    public static void SetAnchoreToCenter()
    {
        if (Selection.gameObjects.Length > 0)
        {
            for (int indexOfObject = 0; indexOfObject < Selection.gameObjects.Length; indexOfObject++)
            {
                RectTransform targetObjectTransform = Selection.gameObjects[indexOfObject].GetComponent<RectTransform>();
                RectTransform targetObjectParentTransform = targetObjectTransform.parent.GetComponent<RectTransform>();

                // old workflow
                Vector2 parentSize = targetObjectParentTransform.sizeDelta;

                if (parentSize.x == 0f)
                {
                    parentSize.x = targetObjectTransform.GetComponentInParent<CanvasScaler>().referenceResolution.x;
                }

                if (parentSize.y == 0f)
                {
                    parentSize.y = targetObjectTransform.GetComponentInParent<CanvasScaler>().referenceResolution.y;
                    Debug.Log(targetObjectTransform.GetComponentInParent<CanvasScaler>().referenceResolution.y);
                }

                Debug.Log(parentSize.ToString());


                Vector2 anchoredPosition = targetObjectTransform.anchoredPosition;

                Vector2 targetPivot = Vector2.one * .5f;

                Vector2 minAnchoredPosition = new Vector2((targetObjectTransform.anchorMin.x * parentSize.x) + anchoredPosition.x, (targetObjectTransform.anchorMin.y * parentSize.y) + anchoredPosition.y);
                Vector2 maxAnchoredPosition = new Vector2((targetObjectTransform.anchorMax.x * parentSize.x) + anchoredPosition.x, (targetObjectTransform.anchorMax.y * parentSize.y) + anchoredPosition.y); 

                targetObjectTransform.pivot = targetPivot;

                Debug.Log(minAnchoredPosition.ToString());
                Debug.Log(maxAnchoredPosition.ToString());

                Debug.Log(new Vector2(minAnchoredPosition.x / parentSize.x, minAnchoredPosition.y / parentSize.y).ToString());
                Debug.Log(new Vector2(maxAnchoredPosition.x / parentSize.x, maxAnchoredPosition.y / parentSize.y).ToString());

                targetObjectTransform.anchorMin = new Vector2(minAnchoredPosition.x / parentSize.x, minAnchoredPosition.y / parentSize.y);
                targetObjectTransform.anchorMax = new Vector2(maxAnchoredPosition.x / parentSize.x, maxAnchoredPosition.y / parentSize.y);
                targetObjectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
    [MenuItem("Tools/CanvasUtility/EnableSelectedCanvas _g")]
    public static void EnableSelectedCanvas()
    {
        if (Selection.gameObjects.Length == 1)
        {
            Canvas[] objectInScene = GameObject.FindObjectsOfType<Canvas>();
            List<Canvas> tempCanvas = new List<Canvas>();
            tempCanvas.AddRange(Selection.gameObjects[0].GetComponents<Canvas>());
            tempCanvas.AddRange(Selection.gameObjects[0].GetComponentsInChildren<Canvas>());
            tempCanvas.AddRange(Selection.gameObjects[0].GetComponentsInParent<Canvas>());

            foreach (Canvas canvas in objectInScene)
            {
                foreach (Canvas selectedCanvas in tempCanvas)
                {
                    if (canvas != selectedCanvas)
                    {
                        canvas.enabled = false;
                    }
                    else
                    {
                        canvas.enabled = true;
                    }
                }
            }
        }
    }

    [MenuItem("Tools/CanvasUtility/SetAnchoreToCorner _j")]
    public static void SetAnchoreToCorner()
    {
        if (Selection.gameObjects.Length == 1)
        {
            EditorGUI.BeginChangeCheck();
            RectTransform tempRect = Selection.gameObjects[0].GetComponent<RectTransform>();
          
            Vector2 anchorMin =  tempRect.anchorMin;
            Vector2 anchorMax = tempRect.anchorMax;
            Vector2 offsetMax =  tempRect.offsetMax;
            Vector2 offsetMin =  tempRect.offsetMin;
             
             
             // Debug.Log(tempRect.sizeDelta);
             // Debug.Log(tempRect.anchoredPosition);
            
             CanvasScaler scaler = tempRect.GetComponentInParent<CanvasScaler>();
            
             float height = tempRect.transform.parent.GetComponentInParent<RectTransform>().rect.height;
             float width = tempRect.transform.parent.GetComponentInParent<RectTransform>().rect.width;
            
             Vector2 minAnchorePos = new Vector2(tempRect.anchoredPosition.x - (tempRect.sizeDelta.x / 2), tempRect.anchoredPosition.y - (tempRect.sizeDelta.y / 2));
             Vector2 maxAnchorePos = new Vector2(tempRect.anchoredPosition.x + (tempRect.sizeDelta.x / 2), tempRect.anchoredPosition.y + (tempRect.sizeDelta.y / 2));
            
            
             tempRect.anchorMin = new Vector2(.5f + (minAnchorePos.x / width), .5f + (minAnchorePos.y / height));
             tempRect.anchorMax = new Vector2(.5f + (maxAnchorePos.x / width), .5f + (maxAnchorePos.y / height));
            
             tempRect.offsetMax = Vector3.zero;
             tempRect.offsetMin = Vector3.zero;
             if (EditorGUI.EndChangeCheck())
             {
                 Undo.RecordObject(tempRect,"Rect Changed");
                 tempRect.anchorMin = anchorMin;
                 tempRect.anchorMax = anchorMax;
                 tempRect.offsetMax = offsetMax;
                 tempRect.offsetMin = offsetMin;
             }
        }

        
    }

    [MenuItem("Tools/CanvasUtility/SetGeneralSpriteSettings _k")]
    public static void SetGeneralSpriteSettings()
    {
        Debug.Log(  AssetDatabase.FindAssets("t:Sprite").Length);

        string[] GUID = AssetDatabase.FindAssets("t:Sprite");

        string[] paths = new string[GUID.Length];
        for(int indexOfGUID=0;indexOfGUID<GUID.Length;indexOfGUID++)
        {
            paths[indexOfGUID] = AssetDatabase.GUIDToAssetPath(GUID[indexOfGUID]);
        }

        Debug.Log(paths.Length);
        int count=0;
        foreach (string path in paths)
        {
            if (path.Contains("Assets"))
            {
                // Debug.Log("Texture Path : " + path);
                TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath(path);
                textureImporter.spritePixelsPerUnit = 300;
                
                FileInfo fInfo = new FileInfo(path);
                string dirName = fInfo.Directory.Name;
                textureImporter.spritePackingTag = dirName;
                UnityEngine.Sprite sprite = (Sprite)AssetDatabase.LoadAssetAtPath(path,typeof(UnityEngine.Sprite));
                
                if(sprite==null)
                {
                    Debug.Log("Sprite is null");
                    return;
                }
                
                textureImporter.alphaIsTransparency = true;
                textureImporter.mipmapEnabled = false;
                count++;
                
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 32)
                {
                    textureImporter.maxTextureSize = 32;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 64)
                {
                    textureImporter.maxTextureSize = 64;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 128)
                {
                    textureImporter.maxTextureSize = 128;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 256)
                {
                    textureImporter.maxTextureSize = 256;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 512)
                {
                    textureImporter.maxTextureSize = 512;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 1024)
                {
                    textureImporter.maxTextureSize = 1024;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 2048)
                {
                    textureImporter.maxTextureSize = 2048;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 4096)
                {
                    textureImporter.maxTextureSize = 4096;

                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 8192)
                {
                    textureImporter.maxTextureSize = 8192;
                    textureImporter.SaveAndReimport();
                    continue;
                }
            }
        }
        Debug.Log(count);
    }

    [MenuItem("Tools/CanvasUtility/SetPixelPerUnitOfImageInScene #P")]
    public static void SetPixelPerUnitOfImageInScene()
    {
        Image[] images = GameObject.FindObjectsOfType<Image>();
        foreach (Image image in images)
        {
            // image.pixelsPerUnitMultiplier = 1f;
        }
    }
#endregion

#region EditorCallBack

    [MenuItem("Tools/CanvasUtility/SelectChild #1")]
    public static void SelectChild()
    {
        List<GameObject> selectedObjectChilds=new List<GameObject>();
        if (Selection.gameObjects.Length == 1)
        {
            GameObject selectedObject = Selection.gameObjects[0];
            
            for(int indexOfChild=0;indexOfChild<selectedObject.transform.childCount;indexOfChild++)
            {
                // Selection.activeGameObject = selectedObject.transform.GetChild(indexOfChild).gameObject;
                selectedObjectChilds.Add(selectedObject.transform.GetChild(indexOfChild).gameObject);
            }
        }
        Selection.objects = selectedObjectChilds.ToArray();
    }
    
    private static void DrawCanvasButton(int instanceID,Rect selectionRect)
    {
        GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go != null)
        {
            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas != null)
            {
                Rect canvasSelectionRect = new Rect(selectionRect);
                canvasSelectionRect.x = UnityEngine.Screen.width - 35;
                canvasSelectionRect.width = 16;
                canvasSelectionRect.height = 16;

                var tex = EditorGUIUtility.IconContent("Canvas Icon");
                GUI.color = Color.clear;
                if (tex == null)
                {
                    Debug.Log("tex");
                }

                if (GUI.Button(canvasSelectionRect, tex))
                {
                    if (canvas.enabled)
                    {
                        canvas.enabled = false;
                    }
                    else
                    {
                        canvas.enabled = true;
                    }
                    Debug.Log("Button Clicked");
                }

                GUI.color = Color.white;

                if (canvas.enabled)
                {
                    GUI.contentColor = Color.white;
                    GUI.Label(canvasSelectionRect, tex);
                }
                else
                {
                    GUI.contentColor = Color.black;
                    GUI.Label(canvasSelectionRect, tex);
                }
                GUI.contentColor = Color.white;
            }
        }
    }
    public static void EditorUpdate()
    {
        // Debug.Log("EditorUpdate");
    }
    public static void OnMultiSelectChildren(int instanceID,Rect selectionRect)
    {
        if(Event.current.isKey )
        {
            if(Event.current.control && Event.current.shift)
            {
                if(Event.current.type==EventType.KeyUp)
                {
                    if(Event.current.keyCode==KeyCode.Space)
                    {
                        SelectAllChildObject();
                        Event.current.Use();
                        return;
                    }
                    int pressedNumber=GetPressedNumber(Event.current);
                    if(pressedNumber!=-1)
                    {
                        SelectIndexedChild(pressedNumber);
                        Debug.Log("Index child called");
                        Event.current.Use();
                    }
                }
            }
        }
    }
    public static void SelectIndexedChild(int childIndex)
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        List<GameObject> toBeSelectedObject=new List<GameObject>();

        for(int indexOfSelectedObject=0;indexOfSelectedObject<selectedObjects.Length;indexOfSelectedObject++)
        {
            if(selectedObjects[indexOfSelectedObject].transform.childCount-1>=childIndex)
            {
                GameObject childObject = selectedObjects[indexOfSelectedObject].transform.GetChild(childIndex).gameObject;
                toBeSelectedObject.Add(childObject);
            }
        }
        Selection.objects = toBeSelectedObject.ToArray();
    }    
    public static void SelectAllChildObject()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        List<GameObject> toBeSelectedObject=new List<GameObject>();
        for(int indexOfSelectedObject=0;indexOfSelectedObject<selectedObjects.Length;indexOfSelectedObject++)
        {
            toBeSelectedObject.AddRange(GetChildObjects(selectedObjects[indexOfSelectedObject]));
        }
        Selection.objects = toBeSelectedObject.ToArray();
    }
    
#endregion

#region UtilityFunctions

    public static List<GameObject> GetChildObjects(GameObject selectedObject)
    {
        List<GameObject> childObjects = new List<GameObject>();
        for(int indexOfChild=0;indexOfChild<selectedObject.transform.childCount;indexOfChild++)
        {
            childObjects.Add(selectedObject.transform.GetChild(indexOfChild).gameObject);
        }
        return childObjects;
    }
    public static int GetPressedNumber(Event e)
    {
        if(e.keyCode==KeyCode.Alpha0) {
        //We return 1 because the 1 button was pressed
        return 0;
        }
        else if(e.keyCode==KeyCode.Alpha1) 
        {
            //We will return 2 because the 2 button was pressed
            return 1;
        }
        else if(e.keyCode==KeyCode.Alpha2) 
        {
            //We will return 2 because the 2 button was pressed
            return 2;
        }
        else if(e.keyCode==KeyCode.Alpha3) 
        {
            //We will return 2 because the 2 button was pressed
            return 3;
        }
        else if(e.keyCode==KeyCode.Alpha4) 
        {
            //We will return 2 because the 2 button was pressed
            return 4;
        }
        else if(e.keyCode==KeyCode.Alpha5) 
        {
            //We will return 2 because the 2 button was pressed
            return 5;
        }
        else if(e.keyCode==KeyCode.Alpha6) 
        {
            //We will return 2 because the 2 button was pressed
            return 6;
        }
        else if(e.keyCode==KeyCode.Alpha7) 
        {
            //We will return 2 because the 2 button was pressed
            return 7;
        }
        else if(e.keyCode==KeyCode.Alpha8) 
        {
            //We will return 2 because the 2 button was pressed
            return 8;
        }
        else if(e.keyCode==KeyCode.Alpha9) 
        {
            //We will return 2 because the 2 button was pressed
            return 9;
        }
        return -1;
    }

#endregion

}
#endif