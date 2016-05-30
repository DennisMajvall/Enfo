using UnityEngine;
using System.Collections.Generic;

public class Courage : AuraAbility
{
	const float radiusPerLevel = 0.78125f;

	List<CourageEffect> appliedEffects = new List<CourageEffect>();

	protected override void OnAuraEnter(Collider other)
	{
		CourageEffect component = other.gameObject.GetComponent<CourageEffect>();
		if (!component) {
			component = other.gameObject.AddComponent<CourageEffect>();
		}
		if (!appliedEffects.Contains(component)) {
			appliedEffects.Add(component);
		}

		component.ApplyEffect(Level);
	}

	protected override void OnAuraExit(Collider other)
	{
		CourageEffect component = other.gameObject.GetComponent<CourageEffect>();
		appliedEffects.Remove(component);

		component.RemoveEffect();
		Destroy(component);
	}

	protected override void OnSetLevel(int level)
	{
		foreach (CourageEffect effect in appliedEffects) {
			effect.ApplyEffect(level);
			ChangeAuraRadius(level * radiusPerLevel);
		}
	}
}
