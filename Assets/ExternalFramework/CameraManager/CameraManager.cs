using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.CameraManager
{
	public class CameraManager : ICameraManager
	{
		public List<ICameraUpdateReceiver> CameraUpdateReceivers { get; }
		public List<ICameraUpdateReceiver> cameraUpdateReceivers;

		private Camera camera;
		
		public CameraManager()
		{
			this.cameraUpdateReceivers = new List<ICameraUpdateReceiver>();
		}
		
		public void UpdateCamera(Camera camera)
		{
			this.camera = camera;
			
			for (int indexOfReceiver = 0; indexOfReceiver < cameraUpdateReceivers.Count; indexOfReceiver++)
			{
				cameraUpdateReceivers[indexOfReceiver].OnCameraUpdated(this.camera);
			}
		}
		public Camera GetCamera()
		{
			return camera;
		}
		
		public void RegisterCameraUpdateReceiver(ICameraUpdateReceiver cameraUpdateReceiver)
		{
			if (!cameraUpdateReceivers.Contains(cameraUpdateReceiver))
			{
				cameraUpdateReceivers.Add(cameraUpdateReceiver);
			}

			if (camera != null)
			{
				cameraUpdateReceiver.OnCameraUpdated(camera);
			}
		}

		public void UnRegisterCameraUpdateReceiver(ICameraUpdateReceiver cameraUpdateReceiver)
		{
			if (cameraUpdateReceivers.Contains(cameraUpdateReceiver))
			{
				cameraUpdateReceivers.Remove(cameraUpdateReceiver);
			}
		}
		
	}
}