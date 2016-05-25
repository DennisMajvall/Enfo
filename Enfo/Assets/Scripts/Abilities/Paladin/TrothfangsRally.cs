using UnityEngine;

public class TrothfangsRally : ClickableAbility
{
	public new const int NumLevels = 1;
	public float Radius = 4f;

	void Start()
	{
		OwnerStats = GetComponent<UnitStatsComponent>();
		Cooldown = 20f;
	}

	protected override void UseAbility() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, LayerMasks.Targetable9, QueryTriggerInteraction.Ignore);
		for (int i = 0; i < colliders.Length; ++i) {
			EnemyBehaviour behaviour = colliders[i].gameObject.GetComponent<EnemyBehaviour>();
			if (behaviour) {
				behaviour.SetTarget(gameObject);
			}
		}
	}
	
}
