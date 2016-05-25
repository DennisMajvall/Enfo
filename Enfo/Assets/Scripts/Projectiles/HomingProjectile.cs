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
			if (stats.evasionChance > 0f && Random.value < stats.evasionChance) {
				// Projectile was evaded - Do Nothing.
			} else {
				// Projectile hit the target
				UnitStatsComponent targetStats = Target.GetComponent<UnitStatsComponent>();

				// Check if the thrower crits and increase damage if successful
				if (stats.critChance > 0f && Random.value < stats.critChance) {
					// Crit was successful
					targetStats.ChangeHealth(-stats.damage * (1 + stats.critExtraMultiplier));
					print("crit! dmg: " + stats.damage * (1 + stats.critExtraMultiplier));
				} else {
					// Crit was unsuccessful
					targetStats.ChangeHealth(-stats.damage);
					stats.damage *= (1 + stats.critExtraMultiplier);
				}

				// Modify damage from target's armor and armor type, and attacker's attack type.
				stats.damage *= GameplayConstants.ArmorDamageReduction(stats.attackType, targetStats.ArmorType, targetStats.Armor);

				// Reduce the target's health
				targetStats.ChangeHealth (-stats.damage);
			}

			Destroy(gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
