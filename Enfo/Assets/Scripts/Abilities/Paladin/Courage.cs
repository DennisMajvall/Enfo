using UnityEngine;
using System.Collections.Generic;

public class Courage : AuraAbility
{
	const float radiusPerLevel = 1f;

	List<CourageEffect> appliedEffects = new List<CourageEffect>();

	protected override void OnAuraEnter(Collider other)
	{
		CourageEffect component = other.gameObject.AddComponent<CourageEffect>();
		if (!appliedEffects.Contains(component)) {
			appliedEffects.Add(component);
		}

		component.ApplyEffect(Level);
		Debug.Log(other.gameObject.name + " entered the Courage Aura.");
	}

	protected override void OnAuraExit(Collider other)
	{
		CourageEffect component = other.gameObject.GetComponent<CourageEffect>();
		appliedEffects.Remove(component);

		component.RemoveEffect();
		Destroy(component);
		Debug.Log(other.gameObject.name + " exited the Courage Aura.");
	}

	protected override void OnSetLevel(int level)
	{
		foreach (CourageEffect effect in appliedEffects) {
			effect.ApplyEffect(level);
			ChangeAuraRadius(level * radiusPerLevel);
		}
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.U)) {
			IncrementLevel();
		} else if (Input.GetKeyUp(KeyCode.Q)) {
			DecrementLevel();
		}
	}
}
