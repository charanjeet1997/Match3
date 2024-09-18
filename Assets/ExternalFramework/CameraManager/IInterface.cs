using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.CameraManager
{
	public interface ICameraManager
	{
		List<ICameraUpdateReceiver> CameraUpdateReceivers { get; }
		void RegisterCameraUpdateReceiver(ICameraUpdateReceiver cameraUpdateReceiver);
		void UnRegisterCameraUpdateReceiver(ICameraUpdateReceiver cameraUpdateReceiver);
		void UpdateCamera(Camera camera);
		Camera GetCamera();
	}

	public interface ICameraUpdateReceiver
	{
		void OnCameraUpdated(Camera camera);
	}
}