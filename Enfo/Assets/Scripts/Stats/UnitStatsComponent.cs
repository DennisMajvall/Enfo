using UnityEngine;
using System.Collections;

[System.Serializable]
public class UnitStats
{
	public float currentHealth;
	public float maxHealth;
	public float healthRegeneration;

	public float currentMana;
	public float maxMana;
	public float manaRegeneration;

	public float movementSpeed;
	public float goldDropped;

	public float damage;
	public float projectileSpeed;
	public float range;
	public float acquisitionRange;
}

public class UnitStatsComponent : MonoBehaviour
{
	
	[SerializeField] UnitStats unitStats;

	// Getters
	public float CurrentHealth { get { return unitStats.currentHealth; } }
	public float MaxHealth { get { return unitStats.maxHealth; } }
	public float HealthRegeneration { get { return unitStats.healthRegeneration; } }

	public float CurrentMana { get { return unitStats.currentMana; } }
	public float MaxMana { get { return unitStats.maxMana; } }
	public float ManaRegeneration { get { return unitStats.manaRegeneration; } }

	public float MovementSpeed { get { return unitStats.movementSpeed; } }
	public float GoldDropped { get { return unitStats.goldDropped; } }

	public float Damage { get { return unitStats.damage; } }
	public float ProjectileSpeed { get { return unitStats.projectileSpeed; } }
	public float Range { get { return unitStats.range; } }
	public float AcquisitionRange { get { return unitStats.acquisitionRange; } }

	public bool IsDead { get { return CurrentHealth <= 0f; } }

	// Changers
	public void ChangeHealth(float delta)
	{
		unitStats.currentHealth += delta;

		if (IsDead) {
			GameObject client = GameObject.Find("Client");
			client.GetComponent<GoldContainer>().ChangeGold(gameObject.GetComponent<UnitStatsComponent>().GoldDropped);
			Destroy(gameObject);
		} else if (CurrentHealth > MaxHealth) {
			unitStats.currentHealth = unitStats.maxHealth;
		}
	}

	public void ChangeMaxHealth(float delta) { unitStats.maxHealth += delta; }

	public void ChangeHealthRegeneration(float delta) { unitStats.healthRegeneration += delta; }

	public void ChangeManaRegeneration(float delta) { unitStats.manaRegeneration += delta; }

	public void ChangeMana(float delta)
	{
		unitStats.currentMana += delta;

		if (CurrentMana <= 0f)
			unitStats.currentMana = 0f;
		else if (CurrentMana > MaxMana)
			unitStats.currentMana = unitStats.maxMana;
	}

	public void ChangeMaxMana(float delta) { unitStats.maxMana += delta; }

	public void ChangeMovementSpeed(float delta) { unitStats.movementSpeed += delta; }

	public void ChangeGoldDropped(float delta) { unitStats.goldDropped += delta; }

	public void ChangeDamage(float delta) { unitStats.damage += delta; }

	public void ChangeProjectileSpeed(float delta) { unitStats.projectileSpeed += delta; }

	public void ChangeRange(float delta) { unitStats.range += delta; }

	public void ChangeAcquisionRange(float delta) { unitStats.acquisitionRange += delta; }

	// Update
	void Update()
	{
		if (HealthRegeneration != 0f)
			ChangeHealth(HealthRegeneration * Time.deltaTime);

		if (ManaRegeneration != 0f)
			ChangeMana(ManaRegeneration * Time.deltaTime);
	}





}
