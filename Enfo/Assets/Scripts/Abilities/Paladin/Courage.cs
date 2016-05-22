using UnityEngine;
using System.Collections;

public class Courage : AuraAbility
{
	void Awake()
	{
		Name = "Courage";
	}

	protected override void OnAuraEnter(Collider other)
	{
		if (other.gameObject.layer == AffectsLayer) {
			Debug.Log(other.gameObject.name + " entered the Courage Aura.");
		}
	}

	protected override void OnAuraExit(Collider other)
	{
		if (other.gameObject.layer == AffectsLayer) {
			Debug.Log(other.gameObject.name + " exited the Courage Aura.");
		}
	}
}
