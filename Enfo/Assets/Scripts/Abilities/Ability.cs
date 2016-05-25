using UnityEngine;
using System.Collections.Generic;
using System;

public class Ability : MonoBehaviour
{
	public int Level = 0;
	public int LevelRequirement = 0;
	public const int NumLevels = 10;
	
	
	// Override this
	protected virtual void OnSetLevel(int level) { }


	public void IncrementLevel()
	{
		SetLevel(Level + 1);
	}

	public void DecrementLevel()
	{
		SetLevel(Level - 1);
	}

	public void SetLevel(int level)
	{
		Debug.Assert(level <= NumLevels && level >= 0, "Ability.SetLevel is out of range, fix ASAP.");

		Level = level;
		OnSetLevel(level);
	}
}
