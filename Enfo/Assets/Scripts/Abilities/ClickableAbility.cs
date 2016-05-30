using UnityEngine;
using System.Collections.Generic;

public class ClickableAbility : Ability
{
	public float CurrentCooldown;
	public float Cooldown;

	public float ManaCost;
	protected UnitStatsComponent OwnerStats;


	// Override this
	protected virtual void UseAbility() { }

	// Override this (Optional but recommended for setting ManaCost, Cooldown etc)
	protected override void OnSetLevel(int level) { }

	public virtual bool ManaIsEnough()
	{
		Debug.Assert(OwnerStats, "The OwnerStats variable may not be null when calling ClickableAbility.ManaIsEnough");
		return ManaCost < OwnerStats.Mana;
	}

	public virtual bool CanBeUsed()
	{
		return CooldownIsReady() && ManaIsEnough();
	}

	public bool CooldownIsReady() { return CurrentCooldown <= 0f; }

	public void OnClick()
	{
		if (CanBeUsed()) {
			CurrentCooldown = Cooldown;
			if (ManaCost > 0 && OwnerStats) {
				OwnerStats.ChangeMana(-ManaCost);
			}

			UseAbility();
		}
	}

	void Update()
	{
		if (CurrentCooldown > 0f)
			CurrentCooldown -= Time.deltaTime;

		if (Input.GetKeyUp(KeyCode.J)) {
			OnClick();
		}
	}
}
