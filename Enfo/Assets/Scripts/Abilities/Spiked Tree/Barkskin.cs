using UnityEngine;
using System.Collections;

public class Barkskin : Ability
{

	public bool KeyWasPressed = false;

	public void Activate(GameObject target) {
		KeyWasPressed = false;

		if (target.layer == LayerNames.Ally10) {
			target.AddComponent<BarkskinEffect> ();
			target.GetComponent<BarkskinEffect> ().ApplyEffect (1);
		}

	}

	void Start()
	{

	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
			KeyWasPressed = true;
		}
	}
}
