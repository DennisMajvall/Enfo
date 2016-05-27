using System;
using UnityEngine;

public class GiantSaltCrabData : ScriptableObject
{
	public string[] abilities_hero = { "", };
	public string[] abilities_normal = { "" }; // Not hero glow effect and inventory

	public float selection_scale = 1.30f;

	public float acquisition_range = 700.0f;
	public string armor_type = "Wood";

	public string attack_type = "Normal";
	public float cooldown_time = 1.80f;
	public float damage_base = 3;
	public float damage_factor_medium = 0.0f;
	public float damage_factor_small = 0.0f;
	public float damage_number_of_dice = 1;
	public float damage_sides_per_die = 2;

	public bool projectile_homing_enabled = false; // false or true
	public float projectile_speed = 0;
	public float range = 100;

	public string weapon_type = "Normal";

	public float defense_base = 1;
	public string defense_type = "Large";

	public float speed_base = 100;
	public float turn_rate = 0.5f;

	public float Gold_Bounty_Awarded_Base = 5;
	public float Gold_Bounty_Awarded_Numbers_per_Dice = 1;
	public float Gold_Bounty_Awarded_Sides_per_Dice = 8;

	public float Hit_points_Maxium_Base = 50;
	public float Hit_Points_Regeneration_Rate = 0.50F;
	public float Level = 1;

	public float mana = 30;

	public float mana_regeneration = 0.02f;

	public string Text_Name = "(Level 01) Giant Salt Crab";
}
