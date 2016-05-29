using UnityEngine;

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

	public static Collider GetClosestCollider(Collider[] colliders, Vector3 pos)
	{
		int shortest_index = 0;
		float shortest_dist = (colliders[0].transform.position - pos).sqrMagnitude;

		for (int i = 1; i < colliders.Length; ++i) {
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
}