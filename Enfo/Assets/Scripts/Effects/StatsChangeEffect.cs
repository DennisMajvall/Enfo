using UnityEngine;
using System.Collections;

public class StatsChangeEffect : Effect
{
	public HeroStats stats = new HeroStats();

	void Start()
	{
		stats.enabled = false;
		stats.ChangeHealthRegeneration(3f);
	}
}
