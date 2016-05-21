using UnityEngine;
using Pathfinding;

public class AttackOrder : Order
{
	public GameObject projectilePrefab;

	public GameObject target;
	public float range = 20f;

	MoveOrder moveOrder;
	Seeker seeker;

	bool isChannelingAttack = false;
	float currentChannelTime = 0f;
	float channelTime = 0.2f;

	float currentCooldown = 0f;
	float cooldown = 0.35f;

	float timeSinceMoveOrdered = 0f;
	float moveOrderInterval = 3f;

	public AttackOrder(Vector3 currentPosition, GameObject target, Seeker seeker)
	{
		this.currentPosition = currentPosition;
		this.target = target;
		this.seeker = seeker;
		moveOrder = new MoveOrder(currentPosition);
	}

	public bool TargetIsInsideRange() {
		return (currentPosition - target.transform.position).sqrMagnitude < range * range;
	}
	
	public override void Update()
	{
		if (target == null || isCompleted) {
			isCompleted = true;
			return;
		}

		float distanceToTarget = Vector3.Distance(currentPosition, target.transform.position);
		if (!isChannelingAttack)
			currentCooldown -= Time.deltaTime;

		if (distanceToTarget <= range || isChannelingAttack) {
			AttemptToAttackTarget();
		} else {
			AttemptToReachTarget();
		}
	}

	void AttackTarget()
	{
		isChannelingAttack = true;
		currentChannelTime += Time.deltaTime;
		moveOrder.hasStarted = false;
		if (currentChannelTime >= channelTime) {
			currentChannelTime = 0f;
			currentCooldown = cooldown;
			isChannelingAttack = false;
			GameObject projectile = (GameObject)GameObject.Instantiate(projectilePrefab, currentPosition, new Quaternion());
			projectile.GetComponent<HomingProjectile>().target = target;
		}
	}

	void AttemptToAttackTarget()
	{
		if (currentCooldown <= 0f || isChannelingAttack)
			AttackTarget();
	}
	
	void AttemptToReachTarget()
	{
		timeSinceMoveOrdered += Time.deltaTime;

		if (!moveOrder.hasStarted || timeSinceMoveOrdered > moveOrderInterval) {
			if (seeker.IsDone()) {
				moveOrder.currentPosition = currentPosition;
				moveOrder.hasStarted = true;
				seeker.StartPath(currentPosition, target.transform.position, moveOrder.SetNewPath);
				timeSinceMoveOrdered = 0f;
			}
		}
		if (moveOrder.hasStarted) {
			moveOrder.currentPosition = currentPosition;
			moveOrder.Update();
			currentPosition = moveOrder.currentPosition;
			if (moveOrder.isCompleted)
				moveOrder.hasStarted = false;
		}
	}

}
