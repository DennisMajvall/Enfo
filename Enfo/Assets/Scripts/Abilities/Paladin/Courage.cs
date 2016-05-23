using UnityEngine;
using System.Collections;

public class Courage : AuraAbility
{
	protected override void OnAuraEnter(Collider other)
	{
		CourageEffect component = other.gameObject.AddComponent<CourageEffect>();
		foreach(CourageEffect fx in Effects) {
			component.stats = fx.stats;
		}
		Debug.Log(other.gameObject.name + " entered the Courage Aura.");
	}

	protected override void OnAuraExit(Collider other)
	{
		Destroy(other.gameObject.GetComponent<CourageEffect>());
		Debug.Log(other.gameObject.name + " exited the Courage Aura.");
	}
}
