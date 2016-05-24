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

			float evasionChance = Target.GetComponent<UnitStatsComponent> ().EvasionChance;

			if (evasionChance > 0f && Random.value < evasionChance) {
				// Projectile was evaded

			} else {
				// Projectile hit the target
				float critChance = Thrower.GetComponent<UnitStatsComponent> ().CritChance;

				if (critChance > 0f && Random.value < critChance) {
					// Crit was successful
					Target.GetComponent<UnitStatsComponent> ().ChangeHealth (-1 * Damage * (1 + Thrower.GetComponent<UnitStatsComponent> ().CritMultiplier));
				} else {
					// Crit was unsuccessful
					Target.GetComponent<UnitStatsComponent> ().ChangeHealth (-1 * Damage);
				}
			}

			Destroy (gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
