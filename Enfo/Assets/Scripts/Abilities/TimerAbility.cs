using UnityEngine;
using System.Collections;

public class TimerAbility : Ability
{
	public float timerInterval = -1f;
	
	// Use this for initialization
	void Start()
	{
		Debug.Assert(timerInterval == -1f);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
