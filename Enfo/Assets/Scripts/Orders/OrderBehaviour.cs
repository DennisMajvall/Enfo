using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class OrderBehaviour : MonoBehaviour
{
	public GameObject projectilePrefab;
	List<Order> orders;
	IdleAttackOrder idleAttackOrder;
	Seeker seeker;
	UnitStats stats;

	public void Stop()
	{
		orders.Clear();
	}

	void Start()
	{
		seeker = GetComponent<Seeker>();
		stats = GetComponent<UnitStats>();
		orders = new List<Order>();
		idleAttackOrder = new IdleAttackOrder(stats, transform.position, seeker);
		idleAttackOrder.SetProjectilePrefab(projectilePrefab);
		var go = GameObject.Find("StartPos");
		transform.position = go.transform.position;
	}

	void MoveCharacter(Vector3 delta)
	{
		transform.position += delta;
		Camera.main.transform.position += delta;
	}

	void PerformedClickOnTerrain(int mouse_button, bool queue_order, ref RaycastHit hit)
	{
		if (mouse_button == 1) {
			if (!queue_order)
				orders.Clear();

			MoveOrder move_order = new MoveOrder(stats, transform.position);
			orders.Add(move_order);
			move_order.hasStarted = true;

			seeker.StartPath(transform.position, hit.point, move_order.SetNewPath);
		} else if (mouse_button == 0) {
			if (!queue_order)
				orders.Clear();

			AttackMoveOrder attack_move_order = new AttackMoveOrder(stats, transform.position, seeker, hit.point);
			attack_move_order.SetProjectilePrefab(projectilePrefab);
			orders.Add(attack_move_order);
		}
	}

	void PerformedClickOnTargetable(int mouse_button, bool queue_order, ref RaycastHit hit)
	{
		if (mouse_button == 1) {
			if (!queue_order)
				orders.Clear();

			AttackOrder attack_order = new AttackOrder(stats, gameObject.transform.position, hit.collider.gameObject, seeker);
			attack_order.projectilePrefab = projectilePrefab;
			orders.Add(attack_order);
		}
	}

	void Update()
	{
		UpdateInput();
		UpdateCurrentOrder();
	}

	void UpdateInput()
	{
		if (Input.GetMouseButtonDown(1)) {
			RaycastHit[] ray_hits = Globals.RaycastFromMouse(LayerMasks.Terrain8 | LayerMasks.Targetable9);

			if (ray_hits.Length > 0) {
				bool queue_order = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
				RaycastHit closestRaycastHit = Globals.GetClosestRaycastHit(ray_hits);

				if (closestRaycastHit.collider.gameObject.layer == LayerNames.Terrain8)
					PerformedClickOnTerrain(1, queue_order, ref closestRaycastHit);
				else
					PerformedClickOnTargetable(1, queue_order, ref closestRaycastHit);
			}
		} else if (Input.GetMouseButtonDown(0)) {
			RaycastHit[] ray_hits = Globals.RaycastFromMouse(LayerMasks.Terrain8);

			if (ray_hits.Length > 0) {
				bool queue_order = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
				PerformedClickOnTerrain(0, queue_order, ref ray_hits[0]);
			}
		}
	}

	void UpdateCurrentOrder()
	{
		if (orders.Count >= 1) {
			Order current_order = orders[0];

			current_order.currentPosition = transform.position;
			current_order.Update();
			transform.position = current_order.currentPosition;
			
			if (current_order.isCompleted)
				orders.RemoveAt(0);
		} else {
			idleAttackOrder.currentPosition = transform.position;
			idleAttackOrder.Update();
			//transform.position = idleAttackOrder.currentPosition;
		}
	}


}
