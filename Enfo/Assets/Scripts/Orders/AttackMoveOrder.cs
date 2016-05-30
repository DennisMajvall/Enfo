using UnityEngine;
using System.Collections.Generic;

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
		attackOrder.SetTarget(target);
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

		attackOrder = new AttackOrder(stats, currentPosition, null, seeker, attacker);
		moveOrder = new MoveOrder(stats, currentPosition);
	}

	public override void Update()
	{
		if (!attackOrder.target)
			GetTarget();

		if (attackOrder.target) {
			attackOrder.currentPosition = currentPosition;
			attackOrder.Update();
			currentPosition = attackOrder.currentPosition;
		}

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
			Collider[] colliderArray = Physics.OverlapSphere(currentPosition, attackOrder.stats.AcquisitionRange, enemyLayer, QueryTriggerInteraction.Ignore);

			if (colliderArray.Length > 0) {
				Collider closestCollider = Globals.GetClosestCollider(colliderArray, currentPosition, true);
				
				attackOrder.SetTarget(closestCollider ? closestCollider.gameObject : null);
				if (attackOrder.target) {
					attackOrder.isCompleted = false;
				}
			}
		}
	}

	public void SetProjectilePrefab(GameObject go)
	{
		attackOrder.projectilePrefab = go;
	}
}
