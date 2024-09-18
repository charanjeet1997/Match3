/// <summary>
/// This Class Helps to give extra functionality to Float Data Type
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderExtension
{
	
	//unclamped map
	public static Vector3 GetContactPoint (this Collider collider,Vector3 sourcePosition,float rayDistance,LayerMask layerMask)
	{
		
		Vector3 triggerPosition = sourcePosition; // Assuming the trigger collider is attached to the same object
		Vector3 otherColliderPosition = collider.transform.position;
		RaycastHit raycastHit;
		Vector3 contactPoint = collider.transform.position;
		
		if (Physics.Raycast(triggerPosition, otherColliderPosition - triggerPosition, out raycastHit,rayDistance,layerMask))
		{
			Debug.Log("ColliderExtension Hit found");
			contactPoint = raycastHit.point;
		}
		
		return contactPoint;
	}
}
