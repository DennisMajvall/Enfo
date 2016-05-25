using UnityEngine;
using System.Collections;

public class CombatMastery : AuraAbility
{
	protected override void OnAuraEnter(Collider other)
	{
		CombatMasteryEffect component = other.gameObject.GetComponent<CombatMasteryEffect>();
		if (!component) {
			component = other.gameObject.AddComponent<CombatMasteryEffect>();
		}
		component.ApplyEffect(Level);
	}

	protected override void OnAuraExit(Collider other)
	{
		CombatMasteryEffect component = other.gameObject.GetComponent<CombatMasteryEffect>();
		component.RemoveEffect();
		Destroy(component);
	}
}
