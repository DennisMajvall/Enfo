using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
	public GameObject destination;
	
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// On collision...?
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerNames.Ally10) {
			other.gameObject.GetComponent<OrderBehaviour>().Stop();
			other.gameObject.transform.position = destination.gameObject.transform.position;
		}
	}
}