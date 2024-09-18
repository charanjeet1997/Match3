#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MakeFolder : EditorWindow
{
    public string projectName;
    public List<FolderDetails> folderDetails;
    [MenuItem("Tools/MakeFolder/Editor")]
    static void Initialize()
    {
        MakeFolder window = (MakeFolder)EditorWindow.GetWindow(typeof(MakeFolder));
        window.title = "MakeFolder";
        window.minSize = new Vector2(600, 200);
        window.maxSize = new Vector2(600, 200);
        window.Show();
    }


    private void OnEnable()
    {
        folderDetails = new List<FolderDetails>();
        folderDetails.Add(new FolderDetails("Materials", false));
        folderDetails.Add(new FolderDetails("Prefabs", false));
        folderDetails.Add(new FolderDetails("Scripts", false));
        folderDetails.Add(new FolderDetails("Shaders", false));
        folderDetails.Add(new FolderDetails("Sprites", false));
        folderDetails.Add(new FolderDetails("Models", false));
        folderDetails.Add(new FolderDetails("Sounds", false));
        folderDetails.Add(new FolderDetails("Fonts", false));
        folderDetails.Add(new FolderDetails("Editor", false));
        folderDetails.Add(new FolderDetails("Audio Mixers", false));
        folderDetails.Add(new FolderDetails("Resources", false));
        folderDetails.Add(new FolderDetails("Animator", false));
        folderDetails.Add(new FolderDetails("Animations", false));
        
    }
    public void OnGUI()
    {
        DrawEditor();
    }

    public void DrawEditor()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        DrawProjectFolderEditor();
        DrawFolderNameEditor();

        EditorGUILayout.EndHorizontal();
    }
    public void DrawProjectFolderEditor()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawProjectDetails();
        DrawFoldersDetails();
        AddSubmitButton();
        EditorGUILayout.EndVertical();
    }
    public void DrawFolderNameEditor()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawFolderNameAdder();
        DrawFolderNameList();
        EditorGUILayout.EndVertical();
    }
    string tempFolderName;
    string tempString;

    public void DrawFolderNameAdder()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        tempFolderName = EditorGUILayout.TextField(tempFolderName);
        Debug.Log(folderDetails.Count);
        if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyDown && tempFolderName!=tempString)
        {
            folderDetails.Add(new FolderDetails(tempFolderName, false));
            tempString=tempFolderName;
            Repaint();
            Event.current.Use();
        }

        DrawButton("Add", () => folderDetails.Add(new FolderDetails(tempFolderName, false)));
        EditorGUILayout.EndHorizontal();
    }
    public void DrawFolderNameList()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        for (int indexOfFolderDetail = folderDetails.Count - 1; indexOfFolderDetail >= 0; indexOfFolderDetail--)
        {
            EditorGUILayout.BeginHorizontal();
            DrawLable(folderDetails[indexOfFolderDetail].name);
            DrawButton("x", () => folderDetails.RemoveAt(indexOfFolderDetail));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }


    public void DrawLable(string lableName)
    {
        EditorGUILayout.LabelField(lableName);
    }



    public void DrawProjectDetails()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        projectName = EditorGUILayout.TextField("Project Name...", projectName);
        EditorGUILayout.EndHorizontal();
    }
    public void DrawFoldersDetails()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        foreach (FolderDetails folder in folderDetails)
        {
            AddCheckBox(folder);
        }
        EditorGUILayout.EndVertical();
    }
    public void AddCheckBox(FolderDetails folderDetails)
    {
        EditorGUILayout.BeginHorizontal();
        folderDetails.toAdd = EditorGUILayout.Toggle(folderDetails.name, folderDetails.toAdd);
        EditorGUILayout.EndHorizontal();
    }
    public void AddSubmitButton()
    {
        if (GUILayout.Button("Submit"))
        {
            AssetDatabase.CreateFolder("Assets", projectName);
            foreach (FolderDetails folder in folderDetails)
            {
                if (folder.toAdd)
                {
                    Debug.Log(projectName + ">" + folder.name);
                    AssetDatabase.CreateFolder("Assets/" + projectName, folder.name);
                }
            }
        }
    }
    public void DrawButton(string buttonName,params System.Action[] actions)
    {
        if (GUILayout.Button(buttonName))
        {
            foreach(System.Action action in actions)
            {
                action();
            }
        }
    }


}

public class FolderDetails
{
    public string name;
    public bool toAdd;
    public FolderDetails(string name, bool toAdd)
    {
        this.name = name;
        this.toAdd = toAdd;
    }
}
#endif