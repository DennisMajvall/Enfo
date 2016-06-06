using UnityEngine;
using System.Collections;

public class HardenedSkin : PassiveAbility {

	HardenedSkinEffect effectComponent;

	protected override void OnApplyEffect(int level) {
		if (!effectComponent)
			effectComponent = gameObject.AddComponent<HardenedSkinEffect>();

		effectComponent.ApplyEffect(level);
	}
}
