using UnityEngine;
using System.Collections;

public class HomingProjectile : ProjectileBehaviour
{
	public GameObject Target;
	public GameObject Thrower; // the unit who threw/shot/cast the projectile at the target
	
	void Update()
	{
		if (!Target) {
			Destroy(gameObject);
			return;
		}

		float distanceLeft = Vector3.Distance(transform.position, Target.transform.position);
		float currentSpeed = Speed * Time.deltaTime;

		if (currentSpeed >= distanceLeft) {

			UnitStatsComponent targetStats = Target.GetComponent<UnitStatsComponent> ();
			UnitStatsComponent throwerStats = Thrower.GetComponent<UnitStatsComponent> ();

			float evasionChance = targetStats.EvasionChance;

			if (evasionChance > 0f && Random.value < evasionChance) {
				// Projectile was evaded
			} else {
				// Projectile hit the target

				// Check if the thrower crits and increase damage if successful
				float critChance = throwerStats.CritChance;
				if (critChance > 0f && Random.value < critChance) {
					Damage *= (1 + throwerStats.CritExtraMultiplier);
				}

				// Modify damage from target's armor and armor type, and attacker's attack type.
				Damage *= GameplayConstants.ArmorDamageReduction(throwerStats.AttackType, targetStats.ArmorType, targetStats.Armor);

				// Reduce the target's health
				Target.GetComponent<UnitStatsComponent> ().ChangeHealth (-1 * Damage);
			}

			Destroy (gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
