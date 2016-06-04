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
		print (gameObject);

		if (!unitStats)
			unitStats = GetComponent<UnitStatsComponent> ();

		print ("adding armor");
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
		unitStats = null;

	}

	/**
	 * Refresh duration
	 * If this spell is applied again to the same target, it's better to refresh the duration of the previous one.
	 */
	public void RefreshDuration() {
		durationCounter = Duration;
	}

	/**
	 * Update
	 */
	// Used for removing the effect once the duration has run out
	void Update () {
		durationCounter -= Time.deltaTime;
		print (durationCounter);
		if (durationCounter <= 0f) {
			RemoveEffect ();
			Destroy (gameObject.GetComponent<BarkskinEffect> ());
		}
	}
}
