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


	protected List<Collider> affectedColliders = new List<Collider>();

	// Private:
	SphereCollider sphere;

	void Start()
	{
		if (Range > 0f) {
			InitiateAura(Range);
		}

		if (AffectsSelf) {
			Collider c = GetComponent<Collider>();
			affectedColliders.Add(c);
			OnAuraEnter(c);
		}
	}

	void InitiateAura(float range)
	{
		Range = range;
		AuraGameObject = GameObject.Instantiate<GameObject>(AuraGameObject);
		AuraGameObject.transform.parent = gameObject.transform;
		AuraGameObject.transform.localPosition = Vector3.zero;

		sphere = AuraGameObject.GetComponent<SphereCollider>();
		sphere.radius = range;

		AuraTriggerScript auraScript = AuraGameObject.GetComponent<AuraTriggerScript>();
		auraScript.OnEnter = SendOnAuraEnter;
		auraScript.OnExit = SendOnAuraExit;
	}

	// Override this (Optional)
	protected override void OnSetLevel(int level)
	{
		foreach (Collider c in affectedColliders) {
			OnAuraEnter(c.GetComponent<Collider>());
		}
	}

	void SendOnAuraEnter(Collider other)
	{
		foreach (int i in AffectsLayers)
			if (other.gameObject.layer == i) {
				affectedColliders.Add(other);
				OnAuraEnter(other);
			}
	}

	void SendOnAuraExit(Collider other)
	{
		foreach (int i in AffectsLayers)
			if (other.gameObject.layer == i) {
				affectedColliders.Remove(other);
				OnAuraExit(other);
			}
	}

	protected void ChangeAuraRadius(float range)
	{
		if (Range <= 0f) {
			InitiateAura(range);
		} else {
			Range = range;
			sphere.radius = range;
		}
	}
}
