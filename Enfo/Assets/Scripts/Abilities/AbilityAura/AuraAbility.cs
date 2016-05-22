using UnityEngine;
using System.Collections;

public class AuraAbility : Ability
{
	public float Range = -1f;
	public int AffectsLayer = LayerNames.Ally10;
	public bool AffectsSelf = true;
	public GameObject auraGameObject;

	// Override this
	protected virtual void OnAuraEnter(Collider other)
	{
		if (other.gameObject.layer == AffectsLayer) {
			Debug.Log(other.gameObject.name + " entered the Aura of " + Name);
		}
	}

	// Override this
	protected virtual void OnAuraExit(Collider other)
	{
		if (other.gameObject.layer == AffectsLayer) {
			Debug.Log(other.gameObject.name + " exited the Aura of " + Name);
		}
	}

	// Private:
	SphereCollider sphere;
	AuraTriggerScript auraScript;

	void Start()
	{
		if (Range > 0f) {
			auraGameObject = GameObject.Instantiate<GameObject>(auraGameObject);
			auraGameObject.transform.parent = gameObject.transform;
			auraGameObject.transform.localPosition = Vector3.zero;

			sphere = auraGameObject.GetComponent<SphereCollider>();
			sphere.radius = Range;

			auraScript = auraGameObject.GetComponent<AuraTriggerScript>();
			auraScript.OnEnter = OnAuraEnter;
			auraScript.OnExit = OnAuraExit;
		}
	}

}
