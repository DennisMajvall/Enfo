using UnityEngine;
using System.Collections.Generic;

public class ChadatrusBlessing : ClickableAbility
{

	List<GameObject> affectedAllies = new List<GameObject>();
	float StartingManaCost = 20f;
	float ManaCostPerLevel = 10f;

	float StartingCooldown = 90f;
	float CooldownDecreasePerLevel = 10f;

	void Start()
	{
		OwnerStats = GetComponent<UnitStatsComponent>();
		SetLevel(Level);
	}

	sealed protected override void UseAbility()
	{
		HeroStatsComponent heroStats = (HeroStatsComponent)OwnerStats;
		affectedAllies = Globals.Teams.GetTeamMembers(heroStats.IsInWestTeam);
		affectedAllies.Remove(gameObject); // Do not affect self

		foreach (GameObject go in affectedAllies) {
			ChadatrusBlessingEffect effect = go.AddComponent<ChadatrusBlessingEffect>();
			effect.ApplyEffect(Level);
		}
	}

	sealed protected override void OnSetLevel(int level)
	{
		ManaCost = StartingManaCost + (level * ManaCostPerLevel);
		Cooldown = StartingCooldown - (level * CooldownDecreasePerLevel);
	}
}
