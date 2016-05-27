using System;
using UnityEngine;

public class PaladinData : ScriptableObject
{
	public string[] abilities_hero = { "Rugtheous Wrath II", "Courage", "Chadatru's Blessing", "Combat Mastery"};
	public string[] abilities_normal = { "Paladin - Geberic - Trotfang's Rally" }; // Not hero glow effect and inventory

	public float selection_scale = 1.70f;

	public float acquisition_range = 400.0f;
	public string armor_type = "Fortified";

	public string attack_type = "Hero";
	public float cooldown_time = 1.28f;
	public float damage_base = 2;
	public float damage_factor_medium = 0.0f;
	public float damage_factor_small = 0.0f;
	public float damage_number_of_dice = 1;
	public float damage_sides_per_die = 8;

	public bool projectile_homing_enabled = false; // false or true
	public float projectile_speed = 900;
	public float range = 170;

	public string weapon_type = "Normal";

	public float defense_base = 4;
	public string defense_type = "Divine";

	public float speed_base = 350;
	public float turn_rate = 0.6f;

	public float agility_per_level = 2.00f;

	public float mana = 30;
	public float intelligence_per_level = 1.0f;

	public float mana_regeneration = 0.02f;
	public string primary_attribute = "Strength";


	public float starting_strength = 9;
	public float starting_intelligence = 3;
	public float starting_agility = 6;

	public float strength_per_level = 3.70f;

	public string hero_name = "Paladin";
	public string proper_name = "Steelrose";
}
