using UnityEngine;
using System.Collections;

public class HomingProjectile : ProjectileBehaviour
{
	public GameObject Target;
	public GameObject Thrower; // the unit who threw/shot/cast the projectile at the target

	UnitStatsComponent targetStats;
	UnitStatsComponent throwerStats;

	void Update()
	{
		if (!Target) {
			Destroy(gameObject);
			return;
		}

		float distanceLeft = Vector3.Distance(transform.position, Target.transform.position);
		float currentSpeed = Speed * Time.deltaTime;

		if (currentSpeed >= distanceLeft) {
			if (!targetStats) {
				targetStats = Target.GetComponent<UnitStatsComponent>();
				if (!targetStats)
					return;
			}
			if (!throwerStats) {
				throwerStats = Target.GetComponent<UnitStatsComponent>();
				if (!throwerStats)
					return;
			}
			
			float evasionChance = targetStats.EvasionChance;

			if (evasionChance > 0f && Random.value < evasionChance) {
				// Projectile was evaded
			} else {
				// Projectile hit the target
				// Check if the thrower crits and increase damage if successful
				float critChance = throwerStats.CritChance;
				if (critChance > 0f && Random.value < critChance) {
					// Crit was successful
					targetStats.ChangeHealth(-Damage * (1 + throwerStats.CritExtraMultiplier));
					print("crit! dmg: " + Damage * (1 + throwerStats.CritExtraMultiplier));
				} else {
					// Crit was unsuccessful
					targetStats.ChangeHealth(-Damage);
					Damage *= (1 + throwerStats.CritExtraMultiplier);
				}

				// Modify damage from target's armor and armor type, and attacker's attack type.
				Damage *= GameplayConstants.ArmorDamageReduction(throwerStats.AttackType, targetStats.ArmorType, targetStats.Armor);

				// Reduce the target's health
				targetStats.ChangeHealth (-Damage);
			}

			Destroy(gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
