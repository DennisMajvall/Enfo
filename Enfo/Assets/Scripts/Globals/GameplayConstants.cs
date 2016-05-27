using UnityEngine;

public class GameplayConstants
{
	public const float DamagePerPrimaryAttribute = 2.5f;

	public const float HealthPerStrength = 40f;
	public const float HealthRegenPerStrength = 0.03f;

	public const float ArmorPerAgility = 0.05f;
	public const float AttackSpeedPerAgility = 0.01f;

	public const float ManaPerIntelligence = 16f;
	public const float ManaRegenPerIntelligence = 0.05f;

	const float ArmorReductionMultiplier = 0.06f;

	public static float ArmorDamageReduction(AttackTypeEnum attackType, ArmorTypeEnum armorType, float armorAmount)
	{
		float typeReduction = GetSpecificArmorReduction(attackType, armorType);
		float reduction = armorAmount * ArmorReductionMultiplier;

		if (armorAmount >= 0f) {
			reduction = 1f - (reduction / (1f + reduction));
		} else {
			reduction = 2f - Mathf.Pow(1f - ArmorReductionMultiplier, -armorAmount);
		}

		return reduction * typeReduction;
	}

	public static float GetSpecificArmorReduction(AttackTypeEnum attackType, ArmorTypeEnum armorType)
	{
		return attackTypeVsArmorTypeMultiplier[(int)attackType, (int)armorType];
	}

	private const int numAtk = (int)AttackTypeEnum.NUM_TYPES;
	private const int numArmor = (int)ArmorTypeEnum.NUM_TYPES;
	// attackTypeVsArmorTypeMultiplier[AttackType, ArmorType]
	private static float[,] attackTypeVsArmorTypeMultiplier = new float[numAtk, numArmor] {
		/* 				  Light,	Medium,		Heavy,		Fortified,	Normal,		Hero,		Unarmored */
		/* Chaos 	*/	{ 1.00f,    1.00f,      1.00f,      1.00f,      1.00f,      1.00f,      1.00f },
		/* Hero 	*/ 	{ 1.00f,    1.00f,      1.00f,      0.50f,      1.00f,      1.00f,      1.00f },
		/* Magic 	*/ 	{ 1.25f,    0.75f,      1.50f,      0.40f,      1.00f,      0.50f,      1.00f },
		/* Normal 	*/ 	{ 1.00f,    1.25f,      1.00f,      0.80f,      1.00f,      1.00f,      1.00f },
		/* Pierce 	*/ 	{ 2.00f,    0.75f,      1.00f,      0.50f,      1.00f,      0.50f,      1.00f },
		/* Siege 	*/ 	{ 1.00f,    0.50f,      1.00f,      1.50f,      1.00f,      0.50f,      1.50f },
		/* Spells 	*/ 	{ 1.00f,    1.00f,      1.25f,      1.00f,      1.00f,      0.75f,      1.00f },
	};
}

public enum AttackTypeEnum : int
{
	Chaos,
	Hero,
	Normal,
	Magic,
	Pierce,
	Siege,
	Spells,
	NUM_TYPES // Must be the last element
}

public enum ArmorTypeEnum : int
{
	Light,
	Medium,
	Heavy,
	Fortified,
	Normal,
	Hero,
	Unarmored,
	NUM_TYPES // Must be the last element
}
