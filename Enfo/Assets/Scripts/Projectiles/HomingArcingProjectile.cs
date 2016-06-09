﻿using UnityEngine;
using System.Collections;

public class HomingArcingProjectile : MonoBehaviour
{
	public GameObject Target;
	public GameObject Owner; // the unit who threw/shot/cast the projectile at the target
	public UnitStatsComponent ownerStats;
	UnitStats stats = new UnitStats();

	void Start()
	{
		ownerStats = Owner.GetComponent<UnitStatsComponent>();
		stats.evasionChance = ownerStats.EvasionChance;
		stats.critChance = ownerStats.CritChance;
		stats.critExtraMultiplier = ownerStats.CritExtraMultiplier;
		stats.attackType = ownerStats.AttackType;
		stats.damage = ownerStats.Damage;
		stats.projectileSpeed = ownerStats.ProjectileSpeed;
	}

	void Update()
	{
		if (!Target) {
			Destroy(gameObject);
			return;
		}

		float distanceLeft = Vector3.Distance(transform.position, Target.transform.position);
		float currentSpeed = stats.projectileSpeed * Time.deltaTime;

		if (currentSpeed >= distanceLeft) {
			// Deal damage to the target
			Target.GetComponent<UnitStatsComponent>().DealProjectileDamage (stats.damage, stats, ownerStats);

			Destroy(gameObject);
			return;
		}

		Vector3 moveDelta = (Target.transform.position - transform.position).normalized;
		moveDelta *= currentSpeed;
		transform.position += moveDelta;
	}
}
