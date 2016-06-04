using UnityEngine;
using System.Collections;

public class RighteousWrathEffect : Effect
{
	UnitStatsComponent unitStats;
	float durationLeft = 30f;

	const float lifeStealPerLevel = 0.02f;
	#region arrays
	public float[] LifeStealPercentage = new float[NumLevels] {
		0f * lifeStealPerLevel + 0.07f,
		1f * lifeStealPerLevel + 0.07f,
		2f * lifeStealPerLevel + 0.07f,
		3f * lifeStealPerLevel + 0.07f,
		4f * lifeStealPerLevel + 0.07f,
		5f * lifeStealPerLevel + 0.07f,
		6f * lifeStealPerLevel + 0.07f,
		7f * lifeStealPerLevel + 0.07f,
		8f * lifeStealPerLevel + 0.07f,
		9f * lifeStealPerLevel + 0.07f,
	};

	public float[] DamageIncrease = new float[NumLevels] {
		200f,
		400f,
		800f,
		1400f,
		2000f,
		2900f,
		4000f,
		5000f,
		6000f,
		7000f,
	};
	#endregion

	protected override void OnApplyEffect(int level)
	{
		if (!unitStats)
			unitStats = GetComponent<UnitStatsComponent>();

		unitStats.IncreaseDamage(DamageIncrease[level-1]);
		unitStats.ChangeLifeStealPercentage(LifeStealPercentage[level-1]);
	}

	protected override void OnRemoveEffect()
	{
		unitStats.IncreaseDamage(-DamageIncrease[Level - 1]);
		unitStats.ChangeLifeStealPercentage(-LifeStealPercentage[Level - 1]);
	}

	void Update()
	{
		if (durationLeft > 0f)
			durationLeft -= Time.deltaTime;
		else
			RemoveEffect();
	}
}
