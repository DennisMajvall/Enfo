using UnityEngine;
using System.Collections;

public class HomingProjectile : ProjectileBehaviour
{
	public GameObject Target;
	
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
				// Projectile missed the target
				//print ("evaded dmg");
			} else {
				// Projectile hit the target
				Target.GetComponent<UnitStatsComponent> ().ChangeHealth (-Damage);
			}
			Destroy (gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
