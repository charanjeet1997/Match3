/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModulerUISystem
{
	public class CanvasGroupFade : Animatable
	{
		#region PUBLIC_VARS
		public float fromAlpha;
		public float toAlpha;
		public float finalAlpha;
		
		#endregion

		#region PRIVATE_VARS
		private CanvasGroup canvasGroup;
		
		#endregion

		#region UNITY_CALLBACKS
		
		#endregion

		#region PUBLIC_METHODS
		public override void Awake()
		{
			base.Awake();
			canvasGroup = GetComponent<CanvasGroup>();
		}
		public override void OnAnimationStarted()
		{
			base.OnAnimationStarted();
			canvasGroup.alpha = fromAlpha;
		}
		public override void OnAnimationRunning(float animPerc)
		{
			base.OnAnimationRunning(animPerc);
			canvasGroup.alpha = Mathf.Lerp(fromAlpha, toAlpha,animPerc);
		}
		public override void OnAnimationEnded()
		{
			base.OnAnimationEnded();
			canvasGroup.alpha = finalAlpha;
		}
		#endregion

		#region PRIVATE_METHODS
		
		#endregion
	}
}