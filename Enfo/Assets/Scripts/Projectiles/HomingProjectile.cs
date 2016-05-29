using UnityEngine;
using System.Collections;

public class HomingProjectile : ProjectileBehaviour
{
	public GameObject Target;
	public GameObject Owner; // the unit who threw/shot/cast the projectile at the target
	
	void Start()
	{
		UnitStatsComponent throwerStats = Owner.GetComponent<UnitStatsComponent>();
		stats.evasionChance = throwerStats.EvasionChance;
		stats.critChance = throwerStats.CritChance;
		stats.critExtraMultiplier = throwerStats.CritExtraMultiplier;
		stats.attackType = throwerStats.AttackType;
		stats.damage = throwerStats.Damage;
		stats.projectileSpeed = throwerStats.ProjectileSpeed;
	}

	void Update()
	{
		if (!Target) {
			Destroy(gameObject);
			return;
		}

		float distanceLeft = Vector3.Distance(transform.position, Target.transform.position);
		float currentSpeed = stats.projectileSpeed * Time.deltaTime;

		if (currentSpeed >= distanceLeft) {
			// Deal damage to the target
			Target.GetComponent<UnitStatsComponent>().DealDamage (stats.damage, stats);

			Destroy(gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
