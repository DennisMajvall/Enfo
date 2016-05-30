﻿using UnityEngine;

public class PassiveAbility : Ability
{
	// Override this
	protected virtual void OnApplyEffect(int level)
	{
		throw new System.Exception("PassiveAbility.OnApplyEffect must be overriden!");
	}

	void Start()
	{
		SetLevel(Level);
	}

	sealed protected override void OnSetLevel(int level)
	{
		OnApplyEffect(level);
	}
}