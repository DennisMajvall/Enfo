using UnityEngine;
using System.Collections.Generic;

public class AuraAbility : Ability
{
	public float Range = -1f;
	public bool AffectsSelf = true;
	public List<LayerEnum> AffectsLayers;
	public GameObject AuraGameObject;

	// Override this
	protected virtual void OnAuraEnter(Collider other) { }

	// Override this
	protected virtual void OnAuraExit(Collider other) { }


	// Private:

	void Start()
	{
		if (Range > 0f) {
			AuraGameObject = GameObject.Instantiate<GameObject>(AuraGameObject);
			AuraGameObject.transform.parent = gameObject.transform;
			AuraGameObject.transform.localPosition = Vector3.zero;

			SphereCollider sphere = AuraGameObject.GetComponent<SphereCollider>();
			sphere.radius = Range;

			AuraTriggerScript auraScript = AuraGameObject.GetComponent<AuraTriggerScript>();
			auraScript.OnEnter = SendOnAuraEnter;
			auraScript.OnExit = SendOnAuraExit;
		}
	}

	void SendOnAuraEnter(Collider other)
	{
		foreach (int i in AffectsLayers)
			if (other.gameObject.layer == i)
				OnAuraEnter(other);
	}

	void SendOnAuraExit(Collider other)
	{
		foreach (int i in AffectsLayers)
			if (other.gameObject.layer == i)
				OnAuraExit(other);
	}

}
