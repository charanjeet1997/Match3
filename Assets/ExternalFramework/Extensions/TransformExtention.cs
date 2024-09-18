/// <summary>
/// This Class Helps to give extra functionality to Transform Class
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtention
{
	public static void Width (this Transform tr, float widthMultiplier) {
		float screenHeight = Camera.main.orthographicSize * 2;
		float screenWidth = (screenHeight) / UnityEngine.Screen.height * UnityEngine.Screen.width;
		if (tr.GetComponent<SpriteRenderer> ()) {
			SpriteRenderer sr = tr.GetComponent<SpriteRenderer> ();
			tr.transform.localScale = new Vector3 ((screenWidth / sr.sprite.bounds.size.x) * widthMultiplier, tr.transform.localScale.y, 1);
		}
		if (tr.GetComponent<MeshRenderer> ()) {
			MeshRenderer mr = tr.GetComponent<MeshRenderer> ();
			tr.transform.localScale = new Vector3 ((screenWidth / mr.bounds.size.x) * widthMultiplier, tr.transform.localScale.y, 1);
		}
	}
	
	public static void Height (this Transform tr, float heightMultiplier) {
		float screenHeight = Camera.main.orthographicSize * 2;
		// float screenWidth = (screenHeight) / UnityEngine.Screen.height * UnityEngine.Screen.width;
		if (tr.GetComponent<SpriteRenderer> ()) {
			SpriteRenderer sr = tr.GetComponent<SpriteRenderer> ();
			tr.transform.localScale = new Vector3 (tr.transform.localScale.x, (screenHeight / sr.sprite.bounds.size.y) * heightMultiplier, 1);

		}
		if (tr.GetComponent<MeshRenderer> ()) {
			MeshRenderer mr = tr.GetComponent<MeshRenderer> ();
			tr.transform.localScale = new Vector3 (tr.transform.localScale.x, (screenHeight / mr.bounds.size.y) * heightMultiplier, 1);
		}
	}
	
	//if(!SanePrefs.isBonusMiniGame) helper
	public enum Axis { X, Y, Z, XY, XZ, YZ, ALL }
	/// <summary>
	/// Transform to apply if(!SanePrefs.isBonusMiniGame) to.
	/// </summary>
	/// <param name="transform">Transform of the gameObject.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="magnitude">Magnitude.</param>
	/// <param name="axis">Axis.</param>
	/// <param name="usePause">If set to <c>true</c> use pause.</param>
	public static IEnumerator Shake (this Transform transform, float duration, float magnitude, Axis axis, bool usePause = true) {
		for (float time = 0; time < duration; time += usePause ? Time.deltaTime : Time.unscaledDeltaTime) {
			if (!usePause || Time.timeScale != 0) {
				Vector3 offset = new Vector3 (
					axis == Axis.X || axis == Axis.XY || axis == Axis.XZ || axis == Axis.ALL ? UnityEngine.Random.Range (-magnitude, magnitude) : 0,
					axis == Axis.Y || axis == Axis.XY || axis == Axis.YZ || axis == Axis.ALL ? UnityEngine.Random.Range (-magnitude, magnitude) : 0,
					axis == Axis.Z || axis == Axis.XZ || axis == Axis.YZ || axis == Axis.ALL ? UnityEngine.Random.Range (-magnitude, magnitude) : 0);
				transform.position += offset;
				yield return new WaitForEndOfFrame ();
				transform.position -= offset;
			}
		}
	}
	public static string[] GetChildrenName(this Transform t)
	{
		List<string> names =new List<string>();
		for (int indexOfchild = 0; indexOfchild < t.childCount; indexOfchild++)
		{
			names.Add(t.GetChild(indexOfchild).gameObject.name);	
		}
		return names.ToArray();
	}
}
