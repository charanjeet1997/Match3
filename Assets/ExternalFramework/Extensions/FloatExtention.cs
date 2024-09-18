/// <summary>
/// This Class Helps to give extra functionality to Float Data Type
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtention
{
	
	public static bool IsBetween (this float x, float x1, float x2) {
		if (x1 < x2) {
			return (x > x1 && x <= x2);
		} else {
			return (x > x2 && x <= x1);

		}
	}
	
	public static float LerpAngleUnclamped (float a, float b, float t) {
		float delta = Mathf.Repeat ((b - a), 360.0f);
		if (delta > 180.0f) {
			delta -= 360.0f;
		}

		return a + delta * t;
	}
	
	//unity already has this called LerpUnclamped
	public static float ULerp (this float val, float from, float to) {
		return (1 - val) * from + val * to;
	}
	
	public static float UInverseLerp (this float val, float from, float to) {
		return (val - from) / (to - from);
	}

	public static float ILerp(this float val,float from,float to)
	{
		return Mathf.InverseLerp(from, to, val);
	}
	
	
	/*Mathf.Lerp(a,b,Time.deltatime) is not mathematically correct and is not framerate independent. Exponential Decay is here to help

    http://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/

    TL;DR
    Use this in the future if you want exponential smoothing that's frame rate independent. */
	// Smoothing rate dictates the proportion of source remaining after one second
	public static float Damp (float source, float target, float smoothing, float dt) {
		return Mathf.Lerp (source, target, 1 - Mathf.Pow (smoothing, dt));
	}
	
	/// Map the specified current1, current2, target1, target2 and val.
	/// </summary>
	/// <param name="current1">Lower bound of the value's current range.</param>
	/// <param name="current2">Upper bound of the value's current range.</param>
	/// <param name="target1">Lower bound of the value's target range.</param>
	/// <param name="target2">Upper bound of the value's target range.</param>
	/// <param name="val">Value to be scaled or mapped.</param>
	public static float Remap (this float val,float current1, float current2, float target1, float target2) {
		//third parameter is the interpolant between the current range which, in turn, is used in the linear interpolation of the target range. 
		return Mathf.Lerp (target1, target2, Mathf.InverseLerp (current1, current2, val));
	}
	
	//unclamped map
	public static float UnclampedRemap (this float val, float current1, float current2, float target1, float target2) {
		return FloatExtention.ULerp (target1, target2, FloatExtention.UInverseLerp (current1, current2, val));
	}
}
