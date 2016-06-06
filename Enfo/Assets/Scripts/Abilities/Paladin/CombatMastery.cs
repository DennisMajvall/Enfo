using UnityEngine;
using System.Collections;
using System;

public class CombatMastery : PassiveAbility
{
	CombatMasteryEffect effect_component;
	
	sealed protected override void OnApplyEffect(int level)
	{
		if (!effect_component)
			effect_component = gameObject.AddComponent<CombatMasteryEffect>();

		effect_component.ApplyEffect(level);
	}
}
