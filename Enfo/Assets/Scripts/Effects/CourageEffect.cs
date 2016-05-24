using UnityEngine;
using System.Collections;

public class CourageEffect : Effect
{
	new const int NumLevels = 10;

	const float healthRegenPerLevel = 1f;
	public float[] HealthRegen = new float[NumLevels] {
		1f * healthRegenPerLevel,
		2f * healthRegenPerLevel,
		3f * healthRegenPerLevel,
		4f * healthRegenPerLevel,
		5f * healthRegenPerLevel,
		6f * healthRegenPerLevel,
		7f * healthRegenPerLevel,
		8f * healthRegenPerLevel,
		9f * healthRegenPerLevel,
		10f* healthRegenPerLevel,
	};

	const float movementPercentagePerLevel = 0.2f;
	public float[] MovementSpeedPercentage = new float[NumLevels] {
		1f * movementPercentagePerLevel,
		2f * movementPercentagePerLevel,
		3f * movementPercentagePerLevel,
		4f * movementPercentagePerLevel,
		5f * movementPercentagePerLevel,
		6f * movementPercentagePerLevel,
		7f * movementPercentagePerLevel,
		8f * movementPercentagePerLevel,
		9f * movementPercentagePerLevel,
		10f* movementPercentagePerLevel,
	};

	UnitStatsComponent unitStats;

	protected override void OnApplyEffect(int level = 0)
	{
		if (!unitStats)
			unitStats = GetComponent<UnitStatsComponent>();

		unitStats.ChangeHealthRegeneration(HealthRegen[level]);
		unitStats.ChangeMovementSpeedPercentage(MovementSpeedPercentage[level]);
	}

	protected override void OnRemoveEffect()
	{
		unitStats.ChangeHealthRegeneration(-HealthRegen[Level]);
		unitStats.ChangeMovementSpeedPercentage(-MovementSpeedPercentage[Level]);
	}
}
