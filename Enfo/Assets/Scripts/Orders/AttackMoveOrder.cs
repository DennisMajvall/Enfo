using UnityEngine;
using Pathfinding;

public class AttackMoveOrder : Order
{
	public int enemyLayer = LayerMasks.Targetable9;
	Vector3 targetPosition;
	AttackOrder attackOrder;
	MoveOrder moveOrder;
	Seeker seeker;

	float currentTargetingCooldown = 0f;
	const float targetingCooldown = 0.2f;

	public void SetTarget(GameObject target)
	{
		attackOrder.target = target;
		attackOrder.isCompleted = false;
		moveOrder.hasStarted = false;
	}

	public AttackMoveOrder(UnitStatsComponent stats, Vector3 currentPosition, Seeker seeker, Vector3 targetPosition, GameObject attacker)
	{
		this.self = attacker; // the unit issuing the attack. inherited from Order
		this.stats = stats;
		this.currentPosition = currentPosition;
		this.seeker = seeker;
		this.targetPosition = targetPosition;

		attackOrder = new AttackOrder(stats, currentPosition, null, seeker, self);
		moveOrder = new MoveOrder(stats, currentPosition);
	}

	public override void Update()
	{
		if (attackOrder.target) {
			attackOrder.currentPosition = currentPosition;
			attackOrder.Update();
			currentPosition = attackOrder.currentPosition;
		} else
			GetTarget();

		if (!attackOrder.target) {
			if (moveOrder.hasStarted == false) {
				moveOrder.hasStarted = true;
				seeker.StartPath(currentPosition, targetPosition, moveOrder.SetNewPath);
			}
			moveOrder.currentPosition = currentPosition;
			moveOrder.Update();
			currentPosition = moveOrder.currentPosition;
		}

		isCompleted = moveOrder.isCompleted;
	}

	void GetTarget()
	{
		currentTargetingCooldown -= Time.deltaTime;

		if (currentTargetingCooldown <= 0f) {
			currentTargetingCooldown = targetingCooldown;

			Collider[] colliders = Physics.OverlapSphere(currentPosition, attackOrder.stats.AcquisitionRange, enemyLayer, QueryTriggerInteraction.Ignore);
			if (colliders.Length > 0) {
				attackOrder.target = Globals.GetClosestCollider(colliders, currentPosition).gameObject;
				attackOrder.isCompleted = false;
			}
		}
	}

	public void SetProjectilePrefab(GameObject go)
	{
		attackOrder.projectilePrefab = go;
	}
}
