using UnityEngine;
using System.Collections;

public class AuraAbility : Ability
{
	public float Range = -1f;
	public int AffectsLayer = LayerNames.Ally10;
	public bool AffectsSelf = true;

	GameObject auraGameObject;
	SphereCollider sphere;
	AuraTriggerScript auraScript;

	// Use this for initialization
	void Start()
	{
		if (Range > 0f) {
			auraGameObject = GameObject.Instantiate<GameObject>(Resources.Load("AuraGameObject") as GameObject);

			//auraGameObject = GameObject.Instantiate<GameObject>(auraGameObject);
			auraGameObject.transform.parent = gameObject.transform;
			auraGameObject.transform.localPosition = Vector3.zero;

			sphere = auraGameObject.GetComponent<SphereCollider>();
			sphere.radius = Range;

			auraScript = auraGameObject.GetComponent<AuraTriggerScript>();
			auraScript.OnEnter = OnAuraEnter;
			auraScript.OnExit = OnAuraExit;
		}
	}

	void OnAuraEnter(Collider other)
	{
		if (other.gameObject.layer == AffectsLayer) {
			Debug.Log(other.gameObject.name + " entered the Aura of " + Name);
		}
	}

	void OnAuraExit(Collider other)
	{
		if (other.gameObject.layer == AffectsLayer) {
			Debug.Log(other.gameObject.name + " exited the Aura of " + Name);
		}
	}

}
