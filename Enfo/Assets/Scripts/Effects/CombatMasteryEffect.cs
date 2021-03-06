﻿using UnityEngine;
using System.Collections;

public class CombatMasteryEffect : Effect 
{
	const float evasionPerLevel = 0.05f;
	const float critChancePerLevel = 0.09f;
	const float critExtraMultiplier = 1f;

	#region arrays
	public float[] Evasion = new float[NumLevels] {
		1f * evasionPerLevel,
		2f * evasionPerLevel,
		3f * evasionPerLevel,
		4f * evasionPerLevel,
		5f * evasionPerLevel,
		6f * evasionPerLevel,
		7f * evasionPerLevel,
		8f * evasionPerLevel,
		9f * evasionPerLevel,
		10f* evasionPerLevel,
	};

	public float[] CritChance = new float[NumLevels] {
		1f * critChancePerLevel,
		2f * critChancePerLevel,
		3f * critChancePerLevel,
		4f * critChancePerLevel,
		5f * critChancePerLevel,
		6f * critChancePerLevel,
		7f * critChancePerLevel,
		8f * critChancePerLevel,
		9f * critChancePerLevel,
		10f* critChancePerLevel,
	};

	public float[] CritExtraMultiplier = new float[NumLevels] {
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
		critExtraMultiplier,
	};
	#endregion
	
	UnitStatsComponent unitStats;

	protected override void OnApplyEffect(int level)
	{
		if (!unitStats)
			unitStats = GetComponent<UnitStatsComponent>();

		unitStats.IncreaseEvasionChance 		(Evasion[level-1]);
		unitStats.IncreaseCritChance 			(CritChance[level-1]);
		unitStats.IncreaseCritExtraMultiplier	(CritExtraMultiplier [level-1]);
	}

	protected override void OnRemoveEffect()
	{
		unitStats.IncreaseEvasionChance			(-Evasion[Level-1]);
		unitStats.IncreaseCritChance			(-CritChance[Level-1]);
		unitStats.IncreaseCritExtraMultiplier	(-CritExtraMultiplier [Level-1]);
	}
}