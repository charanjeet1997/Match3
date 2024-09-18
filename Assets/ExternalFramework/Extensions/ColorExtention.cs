/// <summary>
/// This Class Helps to give extra functionality to Color Class
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtention
{
	/// <summary>
	/// Returns the color with the desired alpha level
	/// </summary>
	/// <returns>Alpha value</returns>
	/// <param name="color">Color whose alpha is to be changed.</param>
	/// <param name="alpha">Alpha value between 0 and 1.</param>
	public static Color WithAlpha (this Color color, float alpha) {
		return new Color (color.r, color.g, color.b, alpha);
	}
	
	//the 4 methods below are taken from http://www.quickfingers.net/quick-bites-01-color-extensions/
	/// 
	/// Output a hex string from a color
	/// 
	///
	/// Set to true to include a # character at the start
	/// 
	public static string HexFromColor (this Color color, bool includeHash = false) {
		string red = Mathf.FloorToInt (color.r * 255).ToString ("X2");
		string green = Mathf.FloorToInt (color.g * 255).ToString ("X2");
		string blue = Mathf.FloorToInt (color.b * 255).ToString ("X2");
		return (includeHash ? "#" : "") + red + green + blue;
	}
	
	
	/// 
	/// Create a Color object from a Hex string (It's not important if you have a # character at
	/// the start or not)
	/// 
	/// The hex string to convert
	/// A Color object
	public static Color ColorFromHex (this string color) {
		// remove the # character if there is one.
		color = color.TrimStart ('#');
		float red = (HexToInt (color[1]) + HexToInt (color[0]) * 16f) / 255f;
		float green = (HexToInt (color[3]) + HexToInt (color[2]) * 16f) / 255f;
		float blue = (HexToInt (color[5]) + HexToInt (color[4]) * 16f) / 255f;
		Color finalColor = new Color { r = red, g = green, b = blue, a = 1 };
		return finalColor;
	}
	
	/// 
	/// Create a color object from integer R G B (A) components
	/// 
	///The red component
	///The green component
	///The blue component
	///The alpha component (Defaults to 255, or fully opaque)
	/// A Color object
	public static Color ColorFromInt (int r, int g, int b, int a = 255) {
		return new Color (r / 255f, g / 255f, b / 255f, a / 255f);
	}

	private static int HexToInt (this char hexValue) {
		return int.Parse (hexValue.ToString (), System.Globalization.NumberStyles.HexNumber);
	}
	
	public static Color ChangeColorBrightness (this Color color, float correctionFactor) {
		float red = (float) color.r;
		float green = (float) color.g;
		float blue = (float) color.b;
		if (correctionFactor < 0) {
			correctionFactor = 1 + correctionFactor;
			red *= correctionFactor;
			green *= correctionFactor;
			blue *= correctionFactor;
		} else {
			red = (255 - red) * correctionFactor + red;
			green = (255 - green) * correctionFactor + green;
			blue = (255 - blue) * correctionFactor + blue;
		}
		return FromArgb ((int) color.a, (int) red, (int) green, (int) blue);
	}

	public static Color FromArgb (int alpha, int red, int green, int blue) {
		//      float fa = ((float)alpha) / 255.0f;
		float fa = 255.0f;
		float fr = ((float) red) / 255.0f;
		float fg = ((float) green) / 255.0f;
		float fb = ((float) blue) / 255.0f;
		return new Color (fr, fg, fb, fa);
	}
	
	public static Texture2D MakeTex (this Color col,int width, int height) {
		Color[] pix = new Color[width * height];
		for (int i = 0; i < pix.Length; ++i) {
			pix[i] = col;
		}
		Texture2D result = new Texture2D (width, height);
		result.SetPixels (pix);
		result.Apply ();
		return result;
	}

}
