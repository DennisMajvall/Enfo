using UnityEngine;
using System.Collections;

public class HealthStat : MonoBehaviour {

	public float health = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
