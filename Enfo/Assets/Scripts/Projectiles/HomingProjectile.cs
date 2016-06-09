using UnityEngine;
using System.Collections;

public class HomingProjectile : MonoBehaviour
{
	public GameObject Target;
	public GameObject Owner; // the unit who threw/shot/cast the projectile at the target
	public UnitStatsComponent ownerStats;

	UnitStats stats = new UnitStats();
	Vector3 targetPosition;

	void Start()
	{
		ownerStats = Owner.GetComponent<UnitStatsComponent>();
		stats.evasionChance = ownerStats.EvasionChance;
		stats.critChance = ownerStats.CritChance;
		stats.critExtraMultiplier = ownerStats.CritExtraMultiplier;
		stats.attackType = ownerStats.AttackType;
		stats.damage = ownerStats.Damage;
		stats.projectileSpeed = ownerStats.ProjectileSpeed;
	}

	void Update()
	{
		// As long as the target is alive, we update the position field.
		// This way, if the target dies, the projectile will keep travelling toward
		// the target's last known position.
		if (Target) {
			targetPosition = Target.transform.position;
		}

		float distanceLeft = Vector3.Distance(transform.position, targetPosition);
		float currentSpeed = stats.projectileSpeed * Time.deltaTime;

		if (currentSpeed >= distanceLeft) {
			// Destroy projectile if target is dead
			if (!Target) {			
				Destroy (gameObject);
				return;
			}

			// Deal damage to the target
			Target.GetComponent<UnitStatsComponent> ().DealProjectileDamage (stats.damage, stats, ownerStats);

			Destroy (gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
