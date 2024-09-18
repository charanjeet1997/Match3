using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptDataContainer")]
public class ScriptDataContainer : ScriptableObject
{
    public ScriptType scriptType;
    public ClassType classType;
    public string className;
    public string namespaceName;
    public InheritedClass inheritedClass;
    string fileName = "ScriptCreater.dat";
    public ScriptData scriptData;

    public void SaveData()
    {
        string directoryPath = Path.GetDirectoryName(GetCurrentFileName());
        string path = Path.Combine(directoryPath, fileName);
        FileStream fileStream = new FileStream(path,FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream,scriptData);
        fileStream.Close();
    }

    public void LoadData()
    {
        string directoryPath = Path.GetDirectoryName(GetCurrentFileName());
        string path = Path.Combine(directoryPath, fileName);
        if (File.Exists(path))
        {
            FileStream fileStream = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            scriptData = (ScriptData)formatter.Deserialize(fileStream);
            fileStream.Close();
        }
    }
    
    string GetCurrentFileName([System.Runtime.CompilerServices.CallerFilePath] string fileName = null)
    {
        return fileName;
    }
}

[Serializable]
public class ScriptData
{
    public Dictionary<string, bool> interfaces;
    //public List<string> interfaces;
    public string customInheritedClassName;
    public Dictionary<string, bool> regionNames;
    //public List<string> regionNames;
}

public enum ScriptType
{
    Class,
    Interface
}

public enum ClassType
{
    Abstract,
    Static,
    Simple,
    Serializable,
    AbstractSerializable,
}

public enum InheritedClass
{
    ScriptableObject,
    MonoBehaviour,
    Custom,
    None
}
