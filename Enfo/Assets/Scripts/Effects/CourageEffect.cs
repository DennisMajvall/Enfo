using UnityEngine;
using System.Collections;

public class CourageEffect : Effect
{
	const int numLevels = 10;

	public float[] HealthRegen = new float[numLevels];
	public float[] MovementSpeed = new float[numLevels];

	UnitStatsComponent unitStats;

	public override void ApplyEffect(int Level = 0)
	{
		unitStats = GetComponent<UnitStatsComponent>();
		unitStats.ChangeHealthRegeneration(HealthRegen[Level]);
	}

	public override void RemoveEffect()
	{
		unitStats.ChangeHealthRegeneration(-HealthRegen[Level]);
	}

	public override void SetLevel(int level)
	{
		if (Level != level) {
			//Level =
		}
	}
}
