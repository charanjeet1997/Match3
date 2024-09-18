using System;
using UnityEngine;
using UnityEngine.Events;

namespace Games.CameraManager
{
	public class CameraReceiver : MonoBehaviour,ICameraUpdateReceiver
	{
		#region PUBLIC_VARS

		public UnityEvent<Camera> UpdateCamera;
		#endregion

		#region PRIVATE_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocatorFramework.ServiceLocator.Current.Get<ICameraManager>().RegisterCameraUpdateReceiver(this);
		}
		private void OnDisable()
		{
			ServiceLocatorFramework.ServiceLocator.Current.Get<ICameraManager>().UnRegisterCameraUpdateReceiver(this);
		}
		#endregion

		#region PUBLIC_METHODS

		public void OnCameraUpdated(Camera camera)
		{
			UpdateCamera.Invoke(camera);
		}

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}