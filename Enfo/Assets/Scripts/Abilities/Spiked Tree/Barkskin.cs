using UnityEngine;
using System.Collections;

public class Barkskin : Ability
{
	public void Activate(GameObject target) {

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
}
