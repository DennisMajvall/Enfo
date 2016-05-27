using System;
using UnityEngine;

public class CrazedSpearman : ScriptableObject
{
	public string[] abilities_hero = { "Critical Strike (Neutral Hostile)", };
	public string[] abilities_normal = { "" }; // Not hero glow effect and inventory

	public float selection_scale = 1.00f;

	public float acquisition_range = 700.0f;
	public string armor_type = "Flesh";

	public string attack_type = "Pierce";
	public float cooldown_time = 2.20f;
	public float damage_base = 2;
	public float damage_factor_medium = 0.0f;
	public float damage_factor_small = 0.0f;
	public float damage_number_of_dice = 1;
	public float damage_sides_per_die = 6;

	public bool projectile_homing_enabled = false; // false or true
	public float projectile_speed = 0;
	public float range = 400;

	public string weapon_type = "Missile";

	public float defense_base = 0;
	public string defense_type = "Small";

	public float speed_base = 110;
	public float turn_rate = 0.5f;

	public float Gold_Bounty_Awarded_Base = 30;
	public float Gold_Bounty_Awarded_Numbers_per_Dice = 1;
	public float Gold_Bounty_Awarded_Sides_per_Dice = 20;

	public float Hit_points_Maxium_Base = 100;
	public float Hit_Points_Regeneration_Rate = 0.50F;
	public float Level = 2;

	public float mana = 00;

	public float mana_regeneration = 0.00f;

	public string Text_Name = "(Level 04) Crazed Speerman";
}
