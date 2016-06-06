using UnityEngine;
using System.Collections;

public class Barkskin : Ability
{

	public bool KeyWasPressed = false;

	public void Activate(GameObject target) {
		KeyWasPressed = false;

		if (target.layer == LayerNames.Ally10) {
			BarkskinEffect componentExistsButIsDisabled = target.GetComponent<BarkskinEffect> ();
			if (componentExistsButIsDisabled) {
				target.GetComponent<BarkskinEffect> ().ApplyEffect (1);
			} else {
				BarkskinEffect effect = target.AddComponent<BarkskinEffect> ();
				effect.ApplyEffect (1);
			}
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
