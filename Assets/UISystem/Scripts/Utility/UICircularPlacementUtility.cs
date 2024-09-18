using System;
using UnityEngine;

namespace ModulerUISystem
{
	public class UICircularPlacementUtility : MonoBehaviour
	{

		#region PUBLIC_VARS

		[SerializeField] private RectTransform[] uiElements;
		[SerializeField] private float offset;

		#endregion

		#region PRIVATE_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void OnDrawGizmos()
		{
			float tau = Mathf.PI * 2f;
			for (int indexOfElement = 0; indexOfElement < uiElements.Length; indexOfElement++)
			{
				float t = ((float) indexOfElement/(float)uiElements.Length) * tau;
				uiElements[indexOfElement].anchoredPosition = new Vector2(Mathf.Sin(t)*offset,Mathf.Cos(t)*offset);
			}
		}

		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}