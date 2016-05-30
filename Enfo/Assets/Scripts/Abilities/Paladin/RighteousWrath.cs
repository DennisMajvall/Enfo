using UnityEngine;
using System.Collections.Generic;

public class RighteousWrath : ClickableAbility
{
	float StartingManaCost = 60f;
	float ManaCostPerLevel = 20f;

	void Start()
	{
		OwnerStats = GetComponent<UnitStatsComponent>();
		SetLevel(Level);
	}

	sealed protected override void UseAbility()
	{
		RighteousWrathEffect effect = gameObject.AddComponent<RighteousWrathEffect>();
		effect.ApplyEffect(Level);
	}

	sealed protected override void OnSetLevel(int level)
	{
		ManaCost = StartingManaCost + (level * ManaCostPerLevel);
	}
}
