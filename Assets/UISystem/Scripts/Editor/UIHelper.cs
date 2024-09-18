using UnityEngine.EventSystems;

namespace ModulerUISystem.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEngine.Rendering.Universal;
    public class UIHelper : EditorWindow
    {
        #region PRIVATE_VARS
        private List<BaseHelperEditorUI> _baseHelperEditorUis;
        private GUIStyle headerStyle;
        private Vector2 scrollPos;
        #endregion
        
        #region Editor_Callback
        private void OnEnable()
        {
            SetHeaderStyle();
            InitializeEditorBaseUI();
            CheckAndCreateDirector();
            Selection.selectionChanged -= OnSelectionChange;
            Selection.selectionChanged += OnSelectionChange;
        }
        [MenuItem("Tools/UIHelper")]
        private static void Initialize()
        {
            UIHelper helper = (UIHelper) EditorWindow.GetWindow(typeof(UIHelper));
            helper.title = "UIHelper";
            helper.Show();
        }
        private void OnGUI()
        {
            scrollPos= EditorGUILayout.BeginScrollView(scrollPos);
                
            foreach (var baseHelperEditorUi in _baseHelperEditorUis)
            {    
                baseHelperEditorUi.Draw();
            }
            
            EventSystem tempObject = GameObject.FindObjectOfType<EventSystem>();
            if (tempObject == null)
            {
                EditorGUILayout.HelpBox("There is no event system in heirarchy! Create Event System.",MessageType.Error);
            }
            EditorGUILayout.EndScrollView();
        }
        #endregion

        #region PRIVATE_METHODS
        private void SetHeaderStyle()
        {
            headerStyle = new GUIStyle();
            headerStyle.normal.textColor = Color.green;
            headerStyle.fontStyle = FontStyle.Bold;
        }

        private void CheckAndCreateDirector()
        {
            if (!AssetDatabase.IsValidFolder("Assets/UISystem"))
                AssetDatabase.CreateFolder("Assets", "Assets/UISystem");

            if (!AssetDatabase.IsValidFolder("Assets/UISystem/UI"))
                AssetDatabase.CreateFolder("Assets/UISystem", "UI");

            foreach (var baseHelperEditor in _baseHelperEditorUis)
            {
                baseHelperEditor.CheckAndCreateDirectory();
            }
        }

        private void InitializeEditorBaseUI()
        {
            _baseHelperEditorUis = new List<BaseHelperEditorUI>();
            _baseHelperEditorUis.Add(new SkinnedHelperEditorUI(headerStyle));
            _baseHelperEditorUis.Add(new ViewAndTransitionHelperEditorUI(headerStyle));
            _baseHelperEditorUis.Add(new TempletAssetsHelperEditorUI(headerStyle));
        }

        private void OnSelectionChange()
        {
            if (Selection.activeGameObject != null)
            {
                foreach (var baseHelperEditorUi in _baseHelperEditorUis)
                {
                    baseHelperEditorUi.OnSelectionChange(Selection.activeGameObject);
                }
            }
        }
        #endregion

        #region PUBLIC_METHODS
        #endregion
    }

    public class BaseHelperEditorUI
    {
        public GUIStyle headerStyle;
        public GameObject selectedObject;
        public BaseHelperEditorUI(GUIStyle headerStyle)
        {
            this.headerStyle = headerStyle;
        }

        public void OnSelectionChange(GameObject selectedObject)
        {
            this.selectedObject = selectedObject;
        }

        public virtual void Draw()
        {
            
        }

        public virtual void CheckAndCreateDirectory()
        {
            
        }
        
        
    }
    public class SkinnedHelperEditorUI : BaseHelperEditorUI
    {
        public SkinnedHelperEditorUI(GUIStyle headerStyle) : base(headerStyle)
        {
            
        }

        public override void Draw()
        {
            string[] foldersToSearch = {"Assets/UISystem/UI/SkinnedAssets/"};
            List<GameObject> prefabs = EditorUtility.GetAssets<GameObject>(foldersToSearch,"t:prefab");
            
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Skinned Assets : ",headerStyle);
                for (int indexOfPrefab = 0; indexOfPrefab < prefabs.Count; indexOfPrefab++)
                {
                    if (GUILayout.Button(prefabs[indexOfPrefab].name)&&selectedObject.transform!=null)
                    {
                        GameObject.Instantiate(prefabs[indexOfPrefab], selectedObject.transform);
                        Debug.Log("Button Clicked");
                    }
                }
            }
        }

        public override void CheckAndCreateDirectory()
        {
            if (!AssetDatabase.IsValidFolder("Assets/UISystem/UI/SkinnedAssets"))
                AssetDatabase.CreateFolder("Assets/UISystem/UI", "SkinnedAssets");
        }
    }
    public class ViewAndTransitionHelperEditorUI : BaseHelperEditorUI
    {
        private Type[] subTypes;
        public ViewAndTransitionHelperEditorUI(GUIStyle headerStyle) : base(headerStyle)
        {
            subTypes = EditorUtility.GetSubType(typeof(TransitionConfig));
        }

        public override void Draw()
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Views and Transitions : ",headerStyle);
                if (GUILayout.Button(typeof(ViewConfig).Name))
                {
                    ViewConfig asset = ScriptableObject.CreateInstance<ViewConfig>();
                    AssetDatabase.CreateAsset(asset, "Assets/UISystem/UI/Views/NewViewConfig.asset");
                    AssetDatabase.SaveAssets();
                    UnityEditor.EditorUtility.FocusProjectWindow();
                    Selection.activeObject = asset;
                }
                foreach (var types in subTypes)
                {
                    if (GUILayout.Button(types.Name))
                    {
                        var asset= ScriptableObject.CreateInstance(types);
                        AssetDatabase.CreateAsset(asset, "Assets/UISystem/UI/Transitions/New"+types.Name+".asset");
                        AssetDatabase.SaveAssets();
                        UnityEditor.EditorUtility.FocusProjectWindow();
                        Selection.activeObject = asset;
                    }
                }
            }
        }

        public override void CheckAndCreateDirectory()
        {
            if (!AssetDatabase.IsValidFolder("Assets/UISystem/UI/Views"))
                AssetDatabase.CreateFolder("Assets/UISystem/UI", "Views");
            
            if (!AssetDatabase.IsValidFolder("Assets/UISystem/UI/Transitions"))
                AssetDatabase.CreateFolder("Assets/UISystem/UI", "Transitions");
        }
    }
    
    public class TempletAssetsHelperEditorUI : BaseHelperEditorUI
    {
        public TempletAssetsHelperEditorUI(GUIStyle headerStyle) : base(headerStyle)
        {
            
        }

        public override void Draw()
        {
            string[] foldersToSearch = {"Assets/UISystem/UI/TempletAssets/"};
            List<GameObject> prefabs = EditorUtility.GetAssets<GameObject>(foldersToSearch,"t:prefab");
            
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Templet Assets : ",headerStyle);
                for (int indexOfPrefab = 0; indexOfPrefab < prefabs.Count; indexOfPrefab++)
                {
                    if (GUILayout.Button(prefabs[indexOfPrefab].name))
                    {
                        GameObject tempObject = GameObject.Instantiate(prefabs[indexOfPrefab]);
                        if (selectedObject != null)
                        {
                            tempObject.transform.SetParent(selectedObject.transform);
                        }
                    }
                }
            }
        }

        public override void CheckAndCreateDirectory()
        {
            if (!AssetDatabase.IsValidFolder("Assets/UISystem/UI/TempletAssets"))
                AssetDatabase.CreateFolder("Assets/UISystem/UI", "TempletAssets"); 
        }
    }
}