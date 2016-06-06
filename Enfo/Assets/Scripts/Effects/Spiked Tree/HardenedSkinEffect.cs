using UnityEngine;
using System.Collections;

public class HardenedSkinEffect : Effect 
{
	
	HeroStatsComponent heroStats;
	int appliedLevel;

	public float[] strengthAtLevel = {
		7f,
		11f,
		17f,
		25f,
		35f,
		47f,
		61f,
		77f,
		95f,
		115f,
	};

	protected override void OnApplyEffect(int level)
	{
		if (!heroStats)
			heroStats = GetComponent<HeroStatsComponent>();

		heroStats.IncreaseStrength (strengthAtLevel[level]);
		appliedLevel = level;
	}

	protected override void OnRemoveEffect()
	{
		heroStats.IncreaseStrength (-strengthAtLevel[appliedLevel]);
	}
}