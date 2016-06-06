using UnityEngine;
using System.Collections.Generic;

public class Courage : AuraAbility
{
	const float radiusPerLevel = 0.78125f;

	//List<CourageEffect> appliedEffects = new List<CourageEffect>();
	Dictionary<Collider, CourageEffect> appliedEffects = new Dictionary<Collider, CourageEffect>();


	protected override void OnAuraEnter(Collider other)
	{
		GetEffectFromCollider(other).ApplyEffect(Level);
	}

	protected override void OnAuraExit(Collider other)
	{
		CourageEffect component = GetEffectFromCollider(other);
		if (component) {
			component.RemoveEffect();
		}
		appliedEffects.Remove(other);
	}

	protected override void OnSetLevel(int level)
	{
		ChangeAuraRadius(level * radiusPerLevel);
		foreach (KeyValuePair<Collider, CourageEffect> pair in appliedEffects) {
			pair.Value.ApplyEffect(level);
		}
	}

	CourageEffect GetEffectFromCollider(Collider other)
	{
		CourageEffect component = null;
		if (!appliedEffects.TryGetValue(other, out component)) {
			component = other.gameObject.GetComponent<CourageEffect>();
			if (!component) {
				component = other.gameObject.AddComponent<CourageEffect>();
			}
			appliedEffects[other] = component;
		}
		return component;
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.N))
			IncrementLevel();
	}
}
