using UnityEngine;
using System.Collections;

public class Courage : AuraAbility
{
	protected override void OnAuraEnter(Collider other)
	{
		CourageEffect component = other.gameObject.AddComponent<CourageEffect>();
		component.ApplyEffect(Level);
		Debug.Log(other.gameObject.name + " entered the Courage Aura.");
	}

	protected override void OnAuraExit(Collider other)
	{
		CourageEffect component = other.gameObject.GetComponent<CourageEffect>();
		component.RemoveEffect();
		Destroy(component);
		Debug.Log(other.gameObject.name + " exited the Courage Aura.");
	}
}
