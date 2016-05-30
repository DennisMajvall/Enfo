using UnityEngine;
using System.Collections.Generic;

public static class Globals
{
	public static GameObject 	GlobalsGameObject	= GameObject.Find ("Globals");				// The Globals game object in the hierarchy
	public static Teams 		Teams 				= GlobalsGameObject.GetComponent<Teams> ();	// The Teams game component of Globals

	public static RaycastHit GetClosestRaycastHit(RaycastHit[] ray_hits)
	{
		int shortest_index = 0;
		float shortest_dist = ray_hits[0].distance;

		for (int i = 1; i < ray_hits.Length; ++i) {
			if (ray_hits[i].distance < shortest_dist) {
				shortest_index = i;
				shortest_dist = ray_hits[i].distance;
			}
		}

		return ray_hits[shortest_index];
	}

	public static Collider GetClosestCollider(Collider[] colliderArray, Vector3 pos, bool ignoreInvulnerable = false)
	{
		List<Collider> colliders = new List<Collider>(colliderArray);

		if (ignoreInvulnerable) {
			for (int i = 0; i < colliders.Count; ++i) {
				if (colliders[i].GetComponent<UnitStatsComponent>().Invulnerable) {
					colliders.RemoveAt(i--);
				}
			}
		}

		if (colliders.Count == 0)
			return null;

		int shortest_index = 0;
		float shortest_dist = (colliders[0].transform.position - pos).sqrMagnitude;
		
		for (int i = 1; i < colliders.Count; ++i) {

			float dist = (colliders[i].transform.position - pos).sqrMagnitude;
			if (dist < shortest_dist) {
				shortest_index = i;
				shortest_dist = dist;
			}
		}

		return colliders[shortest_index];
	}
	

	public static RaycastHit[] RaycastFromMouse(int layerMasks)
	{
		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, Camera.main.nearClipPlane));
		return Physics.RaycastAll(ray, 100f, layerMasks, QueryTriggerInteraction.Ignore);
	}
	
	public static void Swap<T>(ref T left, ref T right)
	{
		T temp = left;
		left = right;
		right = temp;
	}
}