using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class EditorUIUtility
{
    public static void DrawLabel(string labelString, GUIStyle style = null, params GUILayoutOption[] gUILayoutOption)
    {
        if (style == null) style = GUI.skin.GetStyle("label");
        EditorGUILayout.LabelField(labelString, style, gUILayoutOption);
    }
    public static void DrawLabel(string labelString, params GUILayoutOption[] gUILayoutOption)
    {
        EditorGUILayout.LabelField(labelString, gUILayoutOption);
    }
    public static void DrawLableWithoutSpace(string labelString, GUISkin skin = null, params GUILayoutOption[] gUILayoutOption)
    {
        if (!skin) skin = GUI.skin;
        Vector2 size = skin.GetStyle("label").CalcSize(new GUIContent(labelString));
        EditorUIUtility.DrawLabel(labelString, GUILayout.Width(size.x));
    }

    public static UnityEngine.Object DrawObjectFieldWithLabel(string lable, UnityEngine.Object obj, Type type, params GUILayoutOption[] gUILayouts)
    {
        DrawLableWithoutSpace(lable);
        return EditorGUILayout.ObjectField( obj, type, true, gUILayouts);
    }
    public static UnityEngine.Object DrawObjectField(UnityEngine.Object obj, Type type, params GUILayoutOption[] gUILayouts)
    {
        return EditorGUILayout.ObjectField(obj, type, true, gUILayouts);
    }
    public static Vector2Int DrawVector2FieldWithName(string name, Vector2Int field)
    {
        return EditorGUILayout.Vector2IntField(name, field);
    }
    public static void DrawButton(string name, System.Action action, GUIStyle style, params GUILayoutOption[] gUILayoutOption)
    {
        if (GUILayout.Button(name, style, gUILayoutOption))
        {
            action();
        }
    }
    public static string DrawTextFieldWithName(string label, string field, params GUILayoutOption[] gUILayouts)
    {
        DrawLableWithoutSpace(label);
        return EditorGUILayout.TextField(field, gUILayouts);
    }
    
    public static string DrawTextField(string field, params GUILayoutOption[] gUILayoutOptions)
    {
        return EditorGUILayout.TextField(field, gUILayoutOptions);
    }
    
    public static bool DrawToggle(bool field, params GUILayoutOption[] gUILayoutOptions)
    {
        return EditorGUILayout.Toggle(field, gUILayoutOptions);
    }
    public static void PrintSomething()
    {
        Debug.Log("Print Something :");
    }
    public static void DrawButton(string name, System.Action action, params GUILayoutOption[] gUILayoutOption)
    {
        if (GUILayout.Button(name, gUILayoutOption))
        {
            action();
        }
    }
    public static void DrawButton(Texture texture, System.Action action, GUIStyle style, params GUILayoutOption[] gUILayoutOption)
    {
        if (GUILayout.Button(texture, style, gUILayoutOption))
        {
            action();
        }
    }
    public static void DrawButton(Texture texture, System.Action action, params GUILayoutOption[] gUILayoutOption)
    {
        if (GUILayout.Button(texture, gUILayoutOption))
        {
            action();
        }
    }

    public static void DrawHorizontalLayout(System.Action action, params GUILayoutOption[] option)
    {
        EditorGUILayout.BeginHorizontal(option);
        action();
        EditorGUILayout.EndHorizontal();
    }

    public static void DrawHorizontalLayout(System.Action action, GUIStyle style, params GUILayoutOption[] option)
    {
        EditorGUILayout.BeginHorizontal(style, option);
        action();
        EditorGUILayout.EndHorizontal();
    }
    public static void DrawVerticalLayout(System.Action action, params GUILayoutOption[] option)
    {
        EditorGUILayout.BeginVertical(option);
        action();
        EditorGUILayout.EndVertical();
    }

    public static void DrawLabel(string label, int fontSize, TextAnchor alignment, Color color,GUIStyle style = null, params GUILayoutOption[] gUILayoutOption)
    {
        if (style == null) style = new GUIStyle();

        style.fontSize = fontSize;
        style.alignment = alignment;
        style.normal.textColor = color;
        EditorGUILayout.LabelField(label, style, gUILayoutOption);
    }
    public static string DrawTextArea(string field, params GUILayoutOption[] options)
    {
        return EditorGUILayout.TextArea(field, options);
    }
}