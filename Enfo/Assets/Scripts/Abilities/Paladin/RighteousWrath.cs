using UnityEngine;
using System.Collections.Generic;

public class RighteousWrath : ClickableAbility
{
	float StartingManaCost = 60f;
	float ManaCostPerLevel = 20f;
	RighteousWrathEffect effect;

	void Start()
	{
		OwnerStats = GetComponent<UnitStatsComponent>();
		SetLevel(Level);
	}

	sealed protected override void UseAbility()
	{
		GetEffectComponent().ApplyEffect(Level);
	}

	sealed protected override void OnSetLevel(int level)
	{
		ManaCost = StartingManaCost + (level * ManaCostPerLevel);
	}

	RighteousWrathEffect GetEffectComponent()
	{
		if (!effect) {
			effect = gameObject.GetComponent<RighteousWrathEffect>();
			if (!effect)
				effect = gameObject.AddComponent<RighteousWrathEffect>();
		}
		return effect;
	}
}
