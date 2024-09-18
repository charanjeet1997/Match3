using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;
	
	[Serializable]
	public class SwipeDetector : MonoBehaviour
	{

		#region PRIVATE_VARS
		private Vector2 swipeStartPos;
		private Vector2 swipeEndPos;
		[SerializeField] private float minDistanceForSwipe = 20f;
		[SerializeField] bool canSwipe = true;
		#endregion

		#region PUBLIC_VARS
		public event Action<SwipeDirection> OnSwipe;
		#endregion

		#region PUBLIC_METHODS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}
		
		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<SwipeDetector>();
		}

		public void Update()
		{
			if (canSwipe)
			{
				if (Input.GetMouseButtonDown(0))
				{
					swipeStartPos = Input.mousePosition;
				}

				if (Input.GetMouseButtonUp(0))
				{
					swipeEndPos = Input.mousePosition;

					float distance = Vector2.Distance(swipeStartPos, swipeEndPos);
					if (distance > minDistanceForSwipe)
					{
						DetectSwipe();
					}
				}
			}
		}
		public void ResetSwipe()
		{
			canSwipe = true;
		}
		#endregion

		#region PRIVATE_METHODS
		private void DetectSwipe()
		{
			if (IsVerticalSwipe())
			{
				OnSwipe?.Invoke(swipeEndPos.y - swipeStartPos.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
			}
			else
			{
				OnSwipe?.Invoke(swipeEndPos.x - swipeStartPos.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
			}
			canSwipe = false;
		}
		
		private bool IsVerticalSwipe()
		{
			return Mathf.Abs(swipeEndPos.y - swipeStartPos.y) > Mathf.Abs(swipeEndPos.x - swipeStartPos.x);
		}
		#endregion

	}
	
	public enum SwipeDirection
	{
		Up,
		Down,
		Left,
		Right
	}
}