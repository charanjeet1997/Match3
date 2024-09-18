/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveExtention
{
	public static float MapEvaluate (this AnimationCurve curve, float point, float start = 0, float end = 1) {
         return curve.Evaluate (point.Remap (start, end, 0f, 1f));
     }
	
}
