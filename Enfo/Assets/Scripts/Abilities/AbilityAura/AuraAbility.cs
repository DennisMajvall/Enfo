using UnityEngine;
using System.Collections.Generic;

public class AuraAbility : Ability
{
	public float Range = -1f;
	public bool AffectsSelf = true;
	public List<int> AffectsLayers;
	public GameObject AuraGameObject;

	// Override this
	protected virtual void OnAuraEnter(Collider other)
	{
		foreach (int i in AffectsLayers) {
			if (other.gameObject.layer == i) {
				Debug.Log(other.gameObject.name + " entered an Aura.");
			}
		}
	}

	// Override this
	protected virtual void OnAuraExit(Collider other)
	{
		foreach (int i in AffectsLayers) {
			if (other.gameObject.layer == i) {
				Debug.Log(other.gameObject.name + " exited an Aura.");
			}
		}
	}

	// Private:
	//SphereCollider sphere;
	//AuraTriggerScript auraScript;

	void Start()
	{
		if (Range > 0f) {
			AuraGameObject = GameObject.Instantiate<GameObject>(AuraGameObject);
			AuraGameObject.transform.parent = gameObject.transform;
			AuraGameObject.transform.localPosition = Vector3.zero;

			SphereCollider sphere = AuraGameObject.GetComponent<SphereCollider>();
			sphere.radius = Range;

			AuraTriggerScript auraScript = AuraGameObject.GetComponent<AuraTriggerScript>();
			auraScript.OnEnter = OnAuraEnter;
			auraScript.OnExit = OnAuraExit;
		}
	}

}
