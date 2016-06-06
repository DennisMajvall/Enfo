using UnityEngine;
using System.Collections;

public class BarkskinEffect : Effect {

	UnitStatsComponent unitStats;
	float durationCounter = Duration;

	const float Duration = 10f;
	const float ArmorIncrease = 10f;
	const float DamageIncreaseFactor = 0.25f;

	/**
	 * Apply
	 */
	protected override void OnApplyEffect(int lvl) {
		if (!unitStats)
			unitStats = GetComponent<UnitStatsComponent> ();

		durationCounter = Duration;
		unitStats.IncreaseArmor (10f);
		unitStats.IncreaseDamageMultiplier (DamageIncreaseFactor);
	}

	/**
	 * Remove
	 */
	protected override void OnRemoveEffect() {
		unitStats.IncreaseArmor (-10f);
		unitStats.IncreaseDamageMultiplier (-DamageIncreaseFactor);

	}

	/**
	 * Update
	 */
	// Used for removing the effect once the duration has run out
	void Update () {
		durationCounter -= Time.deltaTime;
		if (durationCounter <= 0f) {
			RemoveEffect ();
		}
	}
}
