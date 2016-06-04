using UnityEngine;
using System.Collections;

public class ChadatrusBlessingEffect : Effect
{
	UnitStatsComponent unitStats;
	float durationLeft = 10f;

	protected override void OnApplyEffect(int level)
	{
		if (!unitStats)
			unitStats = GetComponent<UnitStatsComponent>();

		unitStats.SetInvulnerable(true);
	}

	protected override void OnRemoveEffect()
	{
		unitStats.SetInvulnerable(false);
	}

	void Update()
	{
		if (durationLeft > 0f)
			durationLeft -= Time.deltaTime;
		else
			RemoveEffect();
	}
}
