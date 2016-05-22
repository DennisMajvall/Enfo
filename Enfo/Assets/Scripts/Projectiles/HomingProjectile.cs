using UnityEngine;
using System.Collections;

public class HomingProjectile : MonoBehaviour {

	public GameObject target;
	public float damage = 50f;
	public float speed = 10f;
	
	// Update is called once per frame
	void Update () {
		if (target) {
			float distanceLeft = Vector3.Distance(transform.position, target.transform.position);
			float currentSpeed = speed * Time.deltaTime;

			bool canReachCheckpointNow = currentSpeed >= distanceLeft;
			if (canReachCheckpointNow) {
				currentSpeed *= (distanceLeft / currentSpeed);
			}

			Vector3 moveDelta = (target.transform.position - transform.position).normalized;
			moveDelta *= currentSpeed;
			transform.position += moveDelta;

			if (canReachCheckpointNow) {
                target.GetComponent<UnitStats>().changeHealth(-damage);
				Destroy(gameObject);
			}
		} else {
			Destroy(gameObject);
		}
	}
}
