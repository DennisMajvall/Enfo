using UnityEngine;
using System.Collections;

public class Courage : AuraAbility
{
	protected override void OnAuraEnter(Collider other)
	{
		Debug.Log(other.gameObject.name + " entered the Courage Aura.");
	}

	protected override void OnAuraExit(Collider other)
	{
		Debug.Log(other.gameObject.name + " exited the Courage Aura.");
	}
}
