using System;
using UnityEngine;


	[CreateAssetMenu(menuName = "Data/SafeAreaData",fileName = "SafeAreaData" )]
	public class SafeAreaData : ScriptableObject
	{
		public bool isCalibrated;

		public Vector2 anchoreMax
		{
			set
			{
				this._anchoreMax = value;
			}
			get
			{
				
				return this._anchoreMax;
			}
		}
		public Vector2 _anchoreMax;
	}
