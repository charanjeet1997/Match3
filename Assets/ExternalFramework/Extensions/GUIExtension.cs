/// <summary>
/// This Class Helps to give extra functionality to GUI
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIExtension
{
	private static GUIStyle guiStyle = new GUIStyle ();
	public static void DebugText (string text, Vector3 pos, Color color = default (Color), int size = 15) {
		//check if default
		if (color.a == 0.000f) {
			color = Color.white;
		}
		guiStyle.fontSize = size;
		guiStyle.normal.textColor = color;
		Vector3 screenPos = Camera.main.WorldToScreenPoint (pos);
		Vector2 textSize = GUI.skin.label.CalcSize (new GUIContent (text));
		// GUI.color = color;
		GUI.Label (new Rect (screenPos.x - (textSize.x / 2f) - (size), UnityEngine.Screen.height - screenPos.y, textSize.x, textSize.y), text, guiStyle);
	}
}
