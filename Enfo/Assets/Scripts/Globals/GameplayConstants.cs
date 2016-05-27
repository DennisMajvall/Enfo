using UnityEngine;
using System.Collections;

public class GameplayConstants : MonoBehaviour {

	/**
	 * ARMOR DAMAGE REDUCTION MULTIPLIER
	 */
	public const float ARMOR_REDUCTION_MULTIPLIER = 0.06f;

	public static float ArmorDamageReduction(int attackType, int armorType, float armorAmount) {
		float typeRed = attackTypeVsArmorTypeMultiplier [attackType, armorType];
		float red;

		if (armorAmount >= 0f) {
			red = 1 - (armorAmount * ARMOR_REDUCTION_MULTIPLIER) / (1 + (armorAmount * ARMOR_REDUCTION_MULTIPLIER));
		} else {
			red = 2f - Mathf.Pow(1f - ARMOR_REDUCTION_MULTIPLIER, -1f * armorAmount);
		}

		return red * typeRed;
	}
		
	/**
	 * ARMOR AND ATTACK TYPES
	 */ 
	public const int ATTACK_TYPE_CHAOS		= 0;
	public const int ATTACK_TYPE_HERO 		= 1;
	public const int ATTACK_TYPE_NORMAL 	= 2;
	public const int ATTACK_TYPE_MAGIC  	= 3;
	public const int ATTACK_TYPE_PIERCE 	= 4;
	public const int ATTACK_TYPE_SIEGE 		= 5;
	public const int ATTACK_TYPE_SPELLS 	= 6;

	public const int ARMOR_TYPE_LIGHT 		= 0;
	public const int ARMOR_TYPE_MEDIUM 		= 1;
	public const int ARMOR_TYPE_HEAVY 		= 2;
	public const int ARMOR_TYPE_FORTIFIED 	= 3;
	public const int ARMOR_TYPE_NORMAL 		= 4;
	public const int ARMOR_TYPE_HERO 		= 5;
	public const int ARMOR_TYPE_UNARMORED	= 6;

	// attackTypeVsArmorTypeMultiplier[ATTACK_TYPE, ARMOR_TYPE]
	public static float[,] attackTypeVsArmorTypeMultiplier = new float[7,7] {
		/* 				  Light,	Medium,		Heavy,		Fortified,	Normal,		Hero,		Unarmored */
		/* Chaos 	*/	{ 1.00f,	1.00f, 		1.00f,		1.00f, 		1.00f, 		1.00f, 		1.00f },
		/* Hero 	*/ 	{ 1.00f, 	1.00f, 		1.00f,		0.50f, 		1.00f, 		1.00f, 		1.00f },
		/* Magic 	*/ 	{ 1.25f,	0.75f, 		1.50f, 		0.40f, 		1.00f, 		0.50f, 		1.00f },
		/* Normal 	*/ 	{ 1.00f, 	1.25f, 		1.00f, 		0.80f, 		1.00f, 		1.00f, 		1.00f },
		/* Pierce 	*/ 	{ 2.00f, 	0.75f, 		1.00f, 		0.50f, 		1.00f, 		0.50f, 		1.00f },
		/* Siege 	*/ 	{ 1.00f, 	0.50f, 		1.00f, 		1.50f, 		1.00f, 		0.50f, 		1.50f },
		/* Spells 	*/ 	{ 1.00f, 	1.00f, 		1.25f, 		1.00f, 		1.00f, 		0.75f, 		1.00f },
	};

	/*
	 * WARCRAFT III DISTANCE TO UNITY DISTANCE
	 */
	public static float wc3distanceToUnityDistance(float wc3dist) {
		return wc3dist / 128f;
	}
}
