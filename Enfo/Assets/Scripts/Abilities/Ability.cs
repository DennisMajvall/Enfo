using UnityEngine;
using System.Collections.Generic;
using System;

public class Ability : MonoBehaviour
{
	[HideInInspector]
	public string Name = "No Name";

	public int Level = 0;
	public int LevelRequirement = 0;

	//public float CurrentCooldown = 0f;
	//public float MaxCooldown = 0f;

	protected List<Action> OnAttackActions = new List<Action>();
	protected List<Action> OnAttackedActions = new List<Action>();
	protected List<Action> OnActivation = new List<Action>();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void DoOnAttackActions()
	{
		foreach (Action action in OnAttackActions) {
			action();
		}
	}
}
