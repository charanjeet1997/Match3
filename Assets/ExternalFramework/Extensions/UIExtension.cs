/// <summary>
/// This Class Helps to give extra functionality to UI
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIExtension
{
	public static Vector2 WorldToScreenWithScale (Canvas myCanvas, Camera camera, Vector2 targetPosition) {
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint (camera, targetPosition);
		screenPoint = screenPoint / myCanvas.scaleFactor; //apply scaling
		return screenPoint;
	}
	public static Vector2 PlaceRectAtWorldPosition (Canvas myCanvas, Camera camera, Vector2 targetPosition, RectTransform myRect) {
		targetPosition = WorldToScreenWithScale (myCanvas, camera, targetPosition);

		//get half of screen width and height, taking scaling into account
		RectTransform canvasRect = myCanvas.GetComponent<RectTransform> ();
		int screenWidthFromCenter = (int) (Screen.width / myCanvas.scaleFactor) / 2;
		int screenHeightFromCenter = (int) (Screen.height / myCanvas.scaleFactor) / 2;

		//"convert" this value to bottom left of parent (i.e. pretend that anchor = 0,0)
		Vector2 bottomLeftOfParent = new Vector2 ((-myRect.anchorMax.x * 2) * screenWidthFromCenter, (-myRect.anchorMax.y * 2) * screenHeightFromCenter);

		//move the "converted" value to targetPosition
		return bottomLeftOfParent + targetPosition;
	}
}
