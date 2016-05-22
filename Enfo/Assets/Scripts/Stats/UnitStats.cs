using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour
{

	// Fields
	[SerializeField]
	float currentHealth;
	[SerializeField]
	float maxHealth;
	[SerializeField]
	float healthRegeneration;

	[SerializeField]
	float currentMana;
	[SerializeField]
	float maxMana;
	[SerializeField]
	float manaRegeneration;

	[SerializeField]
	float currentMovementSpeed;

	[SerializeField]
	float damage;
	[SerializeField]
	float projectileSpeed;
	[SerializeField]
	float range;
	[SerializeField]
	float acquisitionRange;

	// Getters
	public float CurrentHealth { get { return currentHealth; } }
	public float MaxHealth { get { return maxHealth; } }
	public float HealthRegeneration { get { return healthRegeneration; } }

	public float CurrentMana { get { return currentMana; } }
	public float MaxMana { get { return maxMana; } }
	public float ManaRegeneration { get { return manaRegeneration; } }

	public float CurrentMovementSpeed { get { return currentMovementSpeed; } }

	public float Damage { get { return damage; } }
	public float ProjectileSpeed { get { return projectileSpeed; } }
	public float Range { get { return range; } }
	public float AcquisitionRange { get { return acquisitionRange; } }
	
	// Setters
	public void ChangeHealth(float delta)
	{
		currentHealth += delta;

		if (CurrentHealth <= 0) {
			Destroy(gameObject);
		}

		if (CurrentHealth > MaxHealth) {
			currentHealth = maxHealth;
		}
	}

	public void ChangeMaxHealth(float delta)
	{
		maxHealth += delta;
	}

	public void ChangeHealthRegeneration(float delta)
	{
		healthRegeneration += delta;
	}

	public void ChangeManaRegeneration(float delta)
	{
		manaRegeneration += delta;
	}

	public void ChangeMana(float delta)
	{
		currentMana += delta;

		if (CurrentMana <= 0) {
			currentMana = 0;
		}

		if (CurrentMana > MaxMana) {
			currentMana = maxMana;
		}

	}

	public void ChangeMaxMana(float delta)
	{
		maxMana += delta;
	}

	public void ChangeMovementSpeed(float delta)
	{
		currentMovementSpeed += delta;
	}

	public void ChangeDamage(float delta)
	{
		damage += delta;
	}

	public void ChangeProjectileSpeed(float delta)
	{
		projectileSpeed += delta;
	}

	public void ChangeRange(float delta)
	{
		range += delta;
	}

	public void ChangeAcquisionRange(float delta)
	{
		acquisitionRange += delta;
	}

	// Update
	void Update()
	{
		if (HealthRegeneration != 0f)
			ChangeHealth(HealthRegeneration * Time.deltaTime);

		if (ManaRegeneration != 0f)
			ChangeMana(ManaRegeneration * Time.deltaTime);
	}





}
