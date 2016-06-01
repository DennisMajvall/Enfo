using UnityEngine;
using System.Collections;

public class Barkskin : Ability
{

	bool keyWasPressed = false;

	void Start()
	{

	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
			keyWasPressed = true;
		}
	}
}
