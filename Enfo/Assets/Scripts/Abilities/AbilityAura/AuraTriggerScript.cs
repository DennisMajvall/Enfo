using UnityEngine;
using System.Collections.Generic;

public class AuraTriggerScript : MonoBehaviour
{
	public delegate void TriggerAction(Collider other);

	public TriggerAction OnEnter = null;
	public TriggerAction OnExit = null;

	void OnTriggerEnter(Collider other)
	{
		if (OnEnter != null)
			OnEnter(other);
	}

	void OnTriggerExit(Collider other)
	{
		if (OnExit != null)
			OnExit(other);
	}
}
