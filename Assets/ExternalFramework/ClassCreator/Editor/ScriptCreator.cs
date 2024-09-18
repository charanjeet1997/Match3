using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptCreator : EditorWindow
{
   private ScriptDataContainer dataContainer;
   private string className;
   private string nameSpaceName;
   private Vector2 scrollPosition;
   private int labelWidth = 300;
   private string interfaceName;
   private string regionName;
   [MenuItem("Tools/ScriptCreator")]
   static void OpenScriptCreatereWindow()
   {
      ScriptCreator scriptCreator = (ScriptCreator) EditorWindow.GetWindow(typeof(ScriptCreator));
      scriptCreator.Show();
   }

   private void OnEnable()
   {
      dataContainer = Resources.Load<ScriptDataContainer>("ScriptDataContainer");
      if (dataContainer.scriptData.interfaces != null) dataContainer.scriptData.interfaces = new Dictionary<string, bool>();
      if (dataContainer.scriptData.regionNames != null) dataContainer.scriptData.regionNames = new Dictionary<string, bool>();
      dataContainer.LoadData();
   }

   private void OnGUI()
   {
      if (dataContainer != null)
      {
         EditorGUILayout.BeginVertical(GUI.skin.GetStyle("helpbox"));
         scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition,GUILayout.ExpandWidth(true),GUILayout.ExpandHeight(true));
         EditorGUILayout.BeginHorizontal();
         EditorGUILayout.LabelField("Select Script Type" ,GUILayout.MaxWidth(labelWidth));
         dataContainer.scriptType = (ScriptType)EditorGUILayout.EnumPopup(dataContainer.scriptType);
         EditorGUILayout.EndHorizontal();
         GUILayout.Space(10);
         EditorGUILayout.BeginHorizontal();
         EditorGUILayout.LabelField("Namespace Name",GUILayout.MaxWidth(labelWidth));
         dataContainer.namespaceName = EditorGUILayout.TextField(dataContainer.namespaceName);
         EditorGUILayout.EndHorizontal();
         GUILayout.Space(10);
         EditorGUILayout.BeginHorizontal();
         EditorGUILayout.LabelField("Class Name",GUILayout.MaxWidth(labelWidth));
         dataContainer.className = EditorGUILayout.TextField(dataContainer.className);
         EditorGUILayout.EndHorizontal();
         switch (dataContainer.scriptType)
         {
            case ScriptType.Class:
               GUILayout.Space(10);
               EditorGUILayout.BeginHorizontal();
               EditorGUILayout.LabelField("Select Class Type",GUILayout.MaxWidth(labelWidth),GUILayout.MinWidth(labelWidth));
               dataContainer.classType = (ClassType)EditorGUILayout.EnumPopup(dataContainer.classType);
               EditorGUILayout.EndHorizontal();
               GUILayout.Space(10);
               EditorGUILayout.BeginHorizontal();
               EditorGUILayout.LabelField("Inheritor",GUILayout.MaxWidth(labelWidth));
               dataContainer.inheritedClass = (InheritedClass)EditorGUILayout.EnumPopup(dataContainer.inheritedClass);
               EditorGUILayout.EndHorizontal();
               switch (dataContainer.inheritedClass)
               {
                  case InheritedClass.Custom:
                     GUILayout.Space(10);
                     EditorGUILayout.BeginHorizontal();
                     EditorGUILayout.LabelField("Inherited Class Name",GUILayout.MaxWidth(labelWidth),GUILayout.MinWidth(1));
                     dataContainer.scriptData.customInheritedClassName = EditorGUILayout.TextField(dataContainer.scriptData.customInheritedClassName);
                     EditorGUILayout.EndHorizontal();
                     break;
               }

               break;
         }

         GUILayout.Space(20);
         EditorGUILayout.BeginVertical();
         DrawRegionFolder();
         GUILayout.Space(20);
         DrawInterfaceFolder();
         EditorGUILayout.EndVertical();
         GUILayout.Space(20);
         EditorGUILayout.BeginHorizontal();
         if (GUILayout.Button("Create Class"))
         {
            List<string> interfaces = dataContainer.scriptData.interfaces != null ? new List<string>(dataContainer.scriptData.interfaces.Keys) : new List<string>();
            List<string> regions = dataContainer.scriptData.regionNames != null ? new List<string>(dataContainer.scriptData.regionNames.Keys) : new List<string>();
            string classTemplate = CreateClassTemplate(dataContainer.scriptType, dataContainer.classType,
               dataContainer.namespaceName, dataContainer.className,
               dataContainer.inheritedClass,
               dataContainer.scriptData.interfaces,
               dataContainer.scriptData.regionNames);
            ProjectWindowUtil.CreateAssetWithContent($"{CheckInterface(dataContainer.scriptType)}{dataContainer.className}.cs",classTemplate,null);
         }
         EditorGUILayout.EndHorizontal();
         EditorGUILayout.EndScrollView();
         EditorGUILayout.EndVertical();
      }
   }
   private void DrawRegionFolder()
   {
      EditorGUILayout.BeginVertical(EditorStyles.helpBox);
      GUIStyle style = new GUIStyle();
      style.alignment = TextAnchor.UpperCenter;
      style.fontSize = 20;
      style.normal.textColor = Color.white;
      GUILayout.Label("REGIONS",style);
      ShowRegionsData();
      GUILayout.Space(10);
      EditorGUILayout.EndVertical();
   }
   private void DrawInterfaceFolder()
   {
      EditorGUILayout.BeginVertical(EditorStyles.helpBox);
      GUIStyle style = new GUIStyle();
      style.alignment = TextAnchor.UpperCenter;
      style.fontSize = 20;
      style.normal.textColor = Color.white;
      GUILayout.Label("INTERFACES",style);
      ShowInterfaceData();
      EditorGUILayout.EndVertical();
   }

   private void ShowRegionsData()
   {
      AddRegionsData();
      SelectAllRegions();
      GUILayout.Space(10);
      if (dataContainer != null)
      {
         if (dataContainer.scriptData.regionNames != null)
         {
            List<string> keys = new List<string>(dataContainer.scriptData.regionNames.Keys);
            for (int indexOfRegion = 0; indexOfRegion < keys.Count; indexOfRegion++)
            {
               EditorGUILayout.BeginHorizontal();
               GUILayout.Label(keys[indexOfRegion], GUILayout.Width(position.x / 3));
               dataContainer.scriptData.regionNames[keys[indexOfRegion]] = EditorGUILayout.Toggle(dataContainer.scriptData.regionNames[keys[indexOfRegion]]);
               if (GUILayout.Button("Delete"))
               {
                  dataContainer.scriptData.regionNames.Remove(keys[indexOfRegion]);
                  dataContainer.SaveData();
               }

               EditorGUILayout.EndHorizontal();
               GUILayout.Space(5);
            }
         }
      }
   }
   private void AddRegionsData()
   {
      EditorGUILayout.BeginHorizontal();
      GUILayout.Label("Add New Region",GUILayout.Width(position.x / 5));
      regionName = GUILayout.TextField(regionName);
      if (GUILayout.Button("Add Region",GUILayout.MaxWidth(150)))
      {
         if (dataContainer.scriptData.regionNames == null) dataContainer.scriptData.regionNames = new Dictionary<string, bool>();
         if (!dataContainer.scriptData.regionNames.ContainsKey(regionName))
         {
            dataContainer.scriptData.regionNames.Add(regionName, false);
            dataContainer.SaveData();
         }
      }
      EditorGUILayout.EndHorizontal();
   }
   private void SelectAllRegions()
   {
      EditorGUILayout.BeginHorizontal();
      GUILayout.Label("Select All Regions",GUILayout.Width(position.x / 5));
      if (GUILayout.Button("Select All"))
      {
         SelectAllRegions(true);
      }
      
      if (GUILayout.Button("Unselect All"))
      {
         SelectAllRegions(false);
      }
      EditorGUILayout.EndHorizontal();
   }
   
   void SelectAllRegions(bool canSelect)
   {
      List<string> keys = new List<string>(dataContainer.scriptData.regionNames.Keys);
      if (dataContainer.scriptData.regionNames != null)
      {
         for (int indexofRegion = 0; indexofRegion < keys.Count; indexofRegion++)
         {
            EditorGUILayout.BeginHorizontal();
            dataContainer.scriptData.regionNames[keys[indexofRegion]] = canSelect;
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
         }
      }
   }

   private void SelectAllInterfaces()
   {
      EditorGUILayout.BeginHorizontal();
      GUILayout.Label("Select All Interfaces", GUILayout.Width(position.x / 5));
      if (GUILayout.Button("Select All"))
      {
         SelectAllInterfaces(true);
      }
      
      if (GUILayout.Button("Unselect All"))
      {
         SelectAllInterfaces(false);
      }
      EditorGUILayout.EndHorizontal();
   }

   void SelectAllInterfaces(bool canSelect)
   {
      List<string> keys = new List<string>(dataContainer.scriptData.interfaces.Keys);
      if (dataContainer.scriptData.interfaces != null)
      {
         for (int indexOfInterface = 0; indexOfInterface < keys.Count; indexOfInterface++)
         {
            EditorGUILayout.BeginHorizontal();
            dataContainer.scriptData.interfaces[keys[indexOfInterface]] = canSelect;
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
         }
      }
   }
   private void ShowInterfaceData()
   {
      AddInterfaceData();
      SelectAllInterfaces();
      GUILayout.Space(10);
      if (dataContainer != null)
      {
         if (dataContainer.scriptData.interfaces != null)
         {
            List<string> keys = new List<string>(dataContainer.scriptData.interfaces.Keys);
            for (int indexOfInterface = 0; indexOfInterface < keys.Count; indexOfInterface++)
            {
               EditorGUILayout.BeginHorizontal();
               GUILayout.Label(keys[indexOfInterface], GUILayout.Width(position.x / 3));
               dataContainer.scriptData.interfaces[keys[indexOfInterface]] = EditorGUILayout.Toggle(dataContainer.scriptData.interfaces[keys[indexOfInterface]]);
               if (GUILayout.Button("Delete"))
               {
                  dataContainer.scriptData.interfaces.Remove(keys[indexOfInterface]);
                  dataContainer.SaveData();
               }

               EditorGUILayout.EndHorizontal();
               GUILayout.Space(5);
            }
         }
      }
   }

   private void AddInterfaceData()
   {
      EditorGUILayout.BeginHorizontal();
      GUILayout.Label("Add New Interface",GUILayout.Width(position.x / 5));
      interfaceName = GUILayout.TextField(interfaceName);
      if (GUILayout.Button("Add Interface",GUILayout.MaxWidth(150)))
      {
         if (dataContainer.scriptData.interfaces == null) dataContainer.scriptData.interfaces = new Dictionary<string, bool>();
         if (!dataContainer.scriptData.interfaces.ContainsKey(interfaceName))
         {
            dataContainer.scriptData.interfaces.Add(interfaceName, false);
            dataContainer.SaveData();
         }
      }
      EditorGUILayout.EndHorizontal();
   }

   public string CreateClassTemplate(ScriptType scriptType,ClassType classType,string namespaceName,string className,InheritedClass inheritedClass,Dictionary<string,bool> interfaces,Dictionary<string,bool> regions,params string[] spaces)
   {
      string template = string.Empty;
      string space = CreateSpaces(spaces);
      switch (scriptType)
      {
         case ScriptType.Class:
            if (!string.IsNullOrEmpty(namespaceName))
            {
               template = $"namespace {namespaceName}" +
                          "\n{" +
                          $"\n{CreateClass(classType,className,inheritedClass,interfaces,regions,space,"\t")}" +
                          "\n}";
            }
            else
            {
               template = CreateClass(classType,  className, inheritedClass, interfaces, regions,space);
            }
            break;
         case ScriptType.Interface:
            if (!string.IsNullOrEmpty(namespaceName))
            {
               template = $"namespace {namespaceName}" +
                          "\n{" +
                          $"\n{CreateInterface(className,interfaces,regions,space,"\t")}" +
                          "\n}";
            }
            else
            {
               template = $"{CreateInterface(className,interfaces,regions,space)}";
            }
            break;
      }

      return template;
   }

   public string CreateInterface(string className,Dictionary<string,bool> interfaces,Dictionary<string,bool> regions,params string[] spaces)
   {
      string interfaceTemplate = string.Empty;
      string space = CreateSpaces(spaces);
      interfaceTemplate = $"{ImportNamespaces($"{space}using UnityEngine;\n",$"{space}using System;\n",$"{space}using System.Collections;\n")}\n"+
                          $"{space}public interface I{className} {CreateInterfaces(GetInterfaces(interfaces))}" +
                          $"\n{space}" +
                          "{" +
                          $"\n{CreateRegion(regions,space,"\t")}"+
                          $"\n{space}" +
                          "}";
      return interfaceTemplate;
   }

   public string CreateClass(ClassType classType,  string className, InheritedClass inheritedClass, Dictionary<string,bool> interfaces,Dictionary<string,bool> regions,params string[] spaces)
   {
      string template = String.Empty;
      string space = CreateSpaces(spaces);
      switch (classType)
      {
         case ClassType.Static:
            template = $"{ImportNamespaces($"{space}using UnityEngine;\n",$"{space}using System;\n",$"{space}using System.Collections;\n")}\n"+
                       $"{CreateScriptableObject(className,inheritedClass,space)}\n"+
                       $"{space}public static class { className }\n" +
                       $"{space}" +
                       "{" +
                       $"\n{CreateRegion(regions,space,"\t")}"+
                       $"\n{space}" +
                       "}";
            break;
         case ClassType.Abstract:
            template = $"{ImportNamespaces($"{space}using UnityEngine;\n",$"{space}using System;\n",$"{space}using System.Collections;\n")}\n"+
                       $"{CreateScriptableObject(className,inheritedClass,space)}\n"+
                       $"{space}public abstract class { className }{CreateInheritance(interfaces,inheritedClass)}\n" +
                       $"{space}" +
                       "{" +
                       $"\n{CreateRegion(regions,space,"\t")}"+
                       $"\n{space}" +
                       "}";
            break;
         case ClassType.Simple:
            template = $"{ImportNamespaces($"{space}using UnityEngine;\n",$"{space}using System;\n",$"{space}using System.Collections;\n")}\n"+
                       $"{CreateScriptableObject(className,inheritedClass,space)}"+
                       $"{space}public class { className }{CreateInheritance(interfaces,inheritedClass)}\n" +
                       $"{space}" +
                       "{" +
                       $"\n{CreateRegion(regions,space,"\t")}"+
                       $"\n{space}" +
                       "}";
            break;
         
         case ClassType.Serializable:
            template = $"{ImportNamespaces($"{space}using UnityEngine;\n",$"{space}using System;\n",$"{space}using System.Collections;\n")}\n"+
                       $"{space}[Serializable]\n"+
                       $"{space}public class { className }{CreateInheritance(interfaces,inheritedClass)}\n" +
                       $"{space}" +
                       "{" +
                       $"\n{CreateRegion(regions,space,"\t")}"+
                       $"\n{space}" +
                       "}";
            break;
         
         case ClassType.AbstractSerializable:
            template = $"{ImportNamespaces($"{space}using UnityEngine;\n",$"{space}using System;\n",$"{space}using System.Collections;\n")}\n"+
                       $"{space}[Serializable]\n"+
                       $"{space}public abstract class { className }{CreateInheritance(interfaces,inheritedClass)}\n" +
                       $"{space}" +
                       "{" +
                       $"\n{CreateRegion(regions,space,"\t")}"+
                       $"\n{space}" +
                       "}";
            break;
      }

      return template;
   }
   
   public string CreateRegion(Dictionary<string,bool> regions,params string[] spaces)
   {
      string regionTemplate = string.Empty;
      string space = CreateSpaces(spaces);
      List<string> keys = new List<string>(regions.Keys);
      for (int i = 0; i < keys.Count; i++)
      {
         if (regions[keys[i]])
         {
            regionTemplate += $"\n{space}#region {keys[i]}" +
                              $"\n\n{space}#endregion\n";
         }
      }
      return regionTemplate;
   }

   public string CreateInheritance(Dictionary<string,bool> interfaces,InheritedClass inheritedClass = InheritedClass.None)
   {
      string inhertianceTemplate = string.Empty;
      switch (inheritedClass)
      {
         case InheritedClass.MonoBehaviour:
         case InheritedClass.ScriptableObject:
            inhertianceTemplate = $":{inheritedClass.ToString()}";
            if(!string.IsNullOrEmpty(GetInterfaces(interfaces)))
               inhertianceTemplate += $",{GetInterfaces(interfaces)}";
            break;
         
         case InheritedClass.Custom:
            if (!string.IsNullOrEmpty(dataContainer.scriptData.customInheritedClassName))
            {
               inhertianceTemplate = $":{dataContainer.scriptData.customInheritedClassName}";
               if (!string.IsNullOrEmpty(GetInterfaces(interfaces)))
               {
                  inhertianceTemplate += $",{GetInterfaces(interfaces)}";
               }
            }

            break;
         case InheritedClass.None:
            if(!string.IsNullOrEmpty(GetInterfaces(interfaces)))
               inhertianceTemplate = $":{GetInterfaces(interfaces)}";
            break;
      }
      
      return inhertianceTemplate;
   }

   public string GetInterfaces(Dictionary<string,bool> interfaces)
   {
      string interfaceTemplate = string.Empty;
      List<string> keys = new List<string>(interfaces.Keys);
      if (interfaces.Count > 0)
      {
         for (int i = 0; i < interfaces.Count - 1; i++)
         {
            if (interfaces[keys[i]])
            {
               interfaceTemplate += $"{keys[i]},";
            }
         }

         if (interfaces[keys[keys.Count - 1]])
         {
            interfaceTemplate += keys[keys.Count - 1];
         }
      }

      return interfaceTemplate;
   }
   
   public string CreateInterfaces(string interfaces)
   {
      string interfaceTemplate = string.Empty;
      if (!string.IsNullOrEmpty(interfaces))
      {
         interfaceTemplate = $":{interfaces}";
      }
      return interfaceTemplate;
   }

   private string CreateSpaces(params string[] spaces)
   {
      string space = string.Empty;
      for (int i = 0; i < spaces.Length; i++)
      {
         space += spaces[i];
      }
      return space;
   }

   private string ImportNamespaces(params string[] namespaces)
   {
      string namespaceString = string.Empty;
      for (int i = 0; i < namespaces.Length; i++)
      {
         namespaceString += namespaces[i];
      }
      return namespaceString;
   }

   private string CreateScriptableObject(string className,InheritedClass inheritedClass,params string[] spaces)
   {
      string scriptableObjectTemplate = string.Empty;
      string space = CreateSpaces(spaces);
      if(inheritedClass == InheritedClass.ScriptableObject) scriptableObjectTemplate = $"{space}[CreateAssetMenu(fileName = \"{className}\")]\n";
      return scriptableObjectTemplate;
   }

   private string CheckInterface(ScriptType scriptType)
   {
      string interfaceText = string.Empty;
      if (scriptType == ScriptType.Interface) interfaceText = "I";
      return interfaceText;
   }
}

[InitializeOnLoad]
public class Startup
{
   static Startup()
   {
      ScriptDataContainer dataContainer = Resources.Load<ScriptDataContainer>("ScriptDataContainer");
      if (dataContainer.scriptData.interfaces != null) dataContainer.scriptData.interfaces = new Dictionary<string, bool>();
      if (dataContainer.scriptData.regionNames != null) dataContainer.scriptData.regionNames = new Dictionary<string, bool>();
      dataContainer.LoadData();
   }
}