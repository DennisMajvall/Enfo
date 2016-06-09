using UnityEngine;
using System.Collections;

public class PoisonGasEffect : Effect {

	int appliedLevel;
	UnitStatsComponent unitStats;

	float[] armorReduction = new float[10] {
		5f,
		10f,
		15f,
		20f,
		25f,
		30f,
		35f,
		40f,
		45f,
		50f,
	};

	float[] initialDamage = new float[10] {
		200f,
		500f,
		1000f,
		1400f,
		2200f,
		3800f,
		6200f,
		9000f,
		13000f,
		19000f,
	};

	float[] poisonDamagePerSecond = new float[10] {
		1f,
		3f,
		6f,
		11f,
		18f,
		27f,
		38f,
		52f,
		69f,
		90f,
	};

	protected override void OnApplyEffect(int level) {
		if (!unitStats)
			unitStats = gameObject.GetComponent<UnitStatsComponent> ();

		appliedLevel = level;

		unitStats.DealSpellDamage (initialDamage[level-1]);			// deal dmg
		unitStats.IncreaseArmor (-armorReduction [level - 1]); 	// decrease armor
	}

	protected override void OnRemoveEffect() {
		unitStats.IncreaseArmor (armorReduction [appliedLevel - 1]); // increase armor
	}

	void Update() {
		unitStats.DealSpellDamage (poisonDamagePerSecond[appliedLevel-1]);
	}
}
