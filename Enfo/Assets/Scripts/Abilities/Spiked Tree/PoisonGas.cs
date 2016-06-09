using UnityEngine;
using System.Collections;

public class PoisonGas : ClickableAbility {

	/**
	 * Settings
	 */
	const float StartingManaCost = 15f;
	const float ManaCostPerLevel = 15f;

	const float StartingCooldown = 40f;



	float[] areaOfEffect = new float[10] {
		150f,
		195f,
		240f,
		280f,
		330f,
		375f,
		420f,
		465f,
		510f,
		555f,
	};

	// Use this for initialization
	void Start () {
		ManaCost = StartingManaCost + ManaCostPerLevel;
		Cooldown = StartingCooldown;
		OwnerStats = GetComponent<UnitStatsComponent>();
		SetLevel(Level);
	}

	protected override void UseAbility() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
