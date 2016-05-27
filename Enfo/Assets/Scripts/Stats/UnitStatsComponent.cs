using UnityEngine;
using System.Collections;

[System.Serializable]
public class UnitStats
{
	public string[] abilitiesHero;
	public string[] abilitiesNormal;

	public float selectionScale = 1.70f;
	public float acquisitionRange = 400f;

	public AttackTypeEnum attackType = AttackTypeEnum.Hero;
	public float cooldownTime = 1.20f;
	public float damage = 2f;
	public float damageFactorMedium;
	public float damageFactorSmall;
	public float damageNumDice = 1f;
	public float damageSidesPerDice = 8f;

	public bool projectileHomingEnabled;
	public float projectileSpeed = 900f;
	public float range = 180f;

	public float armor;
	public ArmorTypeEnum armorType = ArmorTypeEnum.Normal;

	public float movementSpeed = 350f;
	public float turnRate = 0.6f;
	public float goldBountyAwardedBase;
	public float goldBountyAwardedNumbersPerDice = 1f;
	public float goldBountyAwardedSidesPerDice;
	
	public float currentHealth;
	public float maxHealth;
	public float healthRegeneration = 0.5f;

	public float currentMana;
	public float maxMana = 30f;
	public float manaRegeneration = 0.02f;

	public string Name = "ProperName/TextName";

	public float goldDropped;

	public float evasionChance; // Between 0.0 - 1.0
	public float critChance;
	public float critExtraMultiplier;

	[HideInInspector]
	public float movementSpeedPercentage = 1.0f;
	public float attackSpeedPercentage = 1.0f;
}

public class UnitStatsComponent : MonoBehaviour
{

	[SerializeField] UnitStats unitStats;

	/**
	 * GETTERS
	 */
	public float CurrentHealth 		{ get { return unitStats.currentHealth; } }
	public float MaxHealth 			{ get { return unitStats.maxHealth; } }
	public float HealthRegeneration { get { return unitStats.healthRegeneration; } }

	public float CurrentMana 		{ get { return unitStats.currentMana; } }
	public float MaxMana 			{ get { return unitStats.maxMana; } }
	public float ManaRegeneration 	{ get { return unitStats.manaRegeneration; } }

	public float MovementSpeed { get { return unitStats.movementSpeed * unitStats.movementSpeedPercentage; } }
	public float MovementSpeedPercentage { get { return unitStats.movementSpeedPercentage; } }

	public float AttackCooldown { get { return unitStats.cooldownTime / unitStats.attackSpeedPercentage; } }
	public float AttackSpeedPercentage { get { return unitStats.attackSpeedPercentage; } }
	public float GoldDropped 		{ get { return unitStats.goldDropped; } }

	public float EvasionChance		{ get { return unitStats.evasionChance; } }
	public float CritChance			{ get { return unitStats.critChance; } }
	public float CritExtraMultiplier{ get { return unitStats.critExtraMultiplier; } }

	public float Damage 			{ get { return unitStats.damage; } }
	public AttackTypeEnum AttackType 	{ get { return unitStats.attackType; } }
	public float ProjectileSpeed 	{ get { return unitStats.projectileSpeed; } }
	public float Range 				{ get { return unitStats.range; } }
	public float AcquisitionRange 	{ get { return unitStats.acquisitionRange; } }

	public float Armor				{ get { return unitStats.armor; } }
	public ArmorTypeEnum ArmorType	{ get { return unitStats.armorType; } }

	public string Name				{ get { return unitStats.Name; } }
	public void ChangeName(string newName) { unitStats.Name = newName; }

	public bool IsDead 				{ get { return CurrentHealth <= 0f; } }


	/**
	 * SETTERS AND CHANGERS
	 */
	public void ChangeHealth(float delta)
	{
		unitStats.currentHealth += delta;

		if (IsDead) {
			// Give gold to killing player
			GameObject client = GameObject.Find("Client");
			client.GetComponent<GoldContainer>().ChangeGold(gameObject.GetComponent<UnitStatsComponent>().GoldDropped);

			// and destroy
			Destroy(gameObject);
		} else if (CurrentHealth > MaxHealth) {
			unitStats.currentHealth = unitStats.maxHealth;
		}
	}
	public void ChangeMaxHealth(float delta, bool alsoChangeCurrentHealth)
	{
		float oldMaxHealth = unitStats.maxHealth;
		unitStats.maxHealth += delta;

		if (alsoChangeCurrentHealth) {
			float relativeHealth = unitStats.currentHealth / (Mathf.Max(oldMaxHealth, 1));
			ChangeHealth(relativeHealth * delta);
		}
	}

	public void ChangeHealthRegeneration(float delta) 	{ unitStats.healthRegeneration += delta; }


	public void ChangeManaRegeneration(float delta) 	{ unitStats.manaRegeneration += delta; }
	public void ChangeMana(float delta)
	{
		unitStats.currentMana += delta;

		if (CurrentMana <= 0f)
			unitStats.currentMana = 0f;
		else if (CurrentMana > MaxMana)
			unitStats.currentMana = unitStats.maxMana;
	}
	public void ChangeMaxMana(float delta, bool alsoChangeCurrentMana)
	{
		float oldMaxMana = unitStats.maxMana;
		unitStats.maxMana += delta;

		if (alsoChangeCurrentMana) {
			float relativeMana = unitStats.currentMana / (Mathf.Max(oldMaxMana, 1));
			ChangeMana(relativeMana * delta);
		}
	}


	public void ChangeMovementSpeed(float delta) 			{ unitStats.movementSpeed += delta; }
	public void ChangeMovementSpeedPercentage(float delta)	{ unitStats.movementSpeedPercentage += delta; }
	public void ChangeGoldDropped(float delta) 				{ unitStats.goldDropped += delta; }

	public void ChangeEvasionChance(float delta)			{ unitStats.evasionChance += delta; }
	public void ChangeCritChance(float delta)				{ unitStats.critChance += delta; }
	public void ChangeCritExtraMultiplier(float delta)		{ unitStats.critExtraMultiplier += delta; }

	public void ChangeDamage(float delta) 					{ unitStats.damage += delta; }
	public void ChangeAttackSpeedPercentage(float delta)	{ unitStats.attackSpeedPercentage += delta; }
	public void SetAttackType(AttackTypeEnum attackType)	{ unitStats.attackType = attackType; }
	public void ChangeProjectileSpeed(float delta) 			{ unitStats.projectileSpeed += delta; }
	public void ChangeRange(float delta) 					{ unitStats.range += delta; }
	public void ChangeAcquisionRange(float delta) 			{ unitStats.acquisitionRange += delta; }

	public void ChangeArmor(float delta)					{ unitStats.armor += delta; }
	public void SetArmorType(ArmorTypeEnum newArmorType)	{ unitStats.armorType = newArmorType; }

	/**
	 * UPDATE()
	 */
	void Update()
	{
		if (HealthRegeneration != 0f)
			ChangeHealth(HealthRegeneration * Time.deltaTime);

		if (ManaRegeneration != 0f)
			ChangeMana(ManaRegeneration * Time.deltaTime);
	}





}
