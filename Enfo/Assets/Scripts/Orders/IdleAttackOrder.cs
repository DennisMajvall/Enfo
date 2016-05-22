using UnityEngine;
using Pathfinding;

public class IdleAttackOrder : Order
{
	AttackOrder attackOrder;

	float currentCooldown = 0f;
	float cooldown = 0.35f;

	public IdleAttackOrder(UnitStats stats, Vector3 currentPosition, Seeker seeker)
	{
		this.stats = stats;
		this.currentPosition = currentPosition;

		attackOrder = new AttackOrder(stats, currentPosition, null, seeker);
	}


	public override void Update()
	{
		if (attackOrder.target) {
			attackOrder.currentPosition = currentPosition;
			attackOrder.Update();
			if (!attackOrder.TargetIsInsideRange()) {
				attackOrder.target = null;
				attackOrder.isCompleted = false;
			}
		} else
			GetTarget();
	}

	public void SetProjectilePrefab(GameObject go)
	{
		attackOrder.projectilePrefab = go;
	}

	void GetTarget()
	{
		currentCooldown -= Time.deltaTime;

		if (currentCooldown <= 0f) {
			currentCooldown = cooldown;
			
			Collider[] colliders = Physics.OverlapSphere(currentPosition, attackOrder.stats.Range, LayerMasks.Targetable9, QueryTriggerInteraction.Ignore);
			if (colliders.Length > 0) {
				attackOrder.target = Globals.GetClosestCollider(colliders, currentPosition).gameObject;
				attackOrder.isCompleted = false;
			}
		}
	}

}
