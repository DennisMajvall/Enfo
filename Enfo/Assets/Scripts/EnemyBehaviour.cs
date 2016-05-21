using UnityEngine;
using Pathfinding;

public class EnemyBehaviour : MonoBehaviour
{
	public GameObject projectilePrefab;

	Seeker seeker;
	GameObject goal_go;
	AttackMoveOrder attackMoveOrder;
	
	// Use this for initialization
	void Start()
	{
		seeker = GetComponent<Seeker>();
		goal_go = GameObject.FindGameObjectWithTag("goal");

		if (!goal_go) {
			Debug.Log("Could not find goal.");
		}

		attackMoveOrder = new AttackMoveOrder(transform.position, seeker, goal_go.transform.position);
		attackMoveOrder.SetProjectilePrefab(projectilePrefab);
		attackMoveOrder.enemyLayer = LayerMasks.Ally10;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsInGoal()) {
			Destroy(gameObject);
			return;
		}

		attackMoveOrder.currentPosition = transform.position;
		attackMoveOrder.Update();
		transform.position = attackMoveOrder.currentPosition;
	}

	bool IsInGoal()
	{
		return (transform.position - goal_go.transform.position).sqrMagnitude < 2f * 2f;
	}
}
