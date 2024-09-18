/// <summary>
/// This Class Helps to give extra functionality to String class
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StringExtension
{
	public static IEnumerator CountTo (this Text text, int from, int to, float duration, string prefix = null, string postFix = null) {
		int currentCount = 0;
		for (float timer = 0; timer < duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			currentCount = (int) Mathf.Lerp (from, to, progress);
			text.text = prefix + currentCount.ToString () + postFix;
			yield return null;
		}
		text.text = prefix + to + postFix;

		yield return null;
	}
	
	/// <summary>
	/// Converts letter to its ASCII equivalent.
	/// </summary>
	/// <returns>ASCII value of the letter.</returns>
	/// <param name="str">String sent.</param>
	/// <param name="index">Index of char in the string to convert, 0 by default.</param>
	public static int LetterToASCII (this string str, int index = 0) {
		char i = str[index];
		int j = i;
		return j;
	}
}
