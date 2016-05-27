using System;
using UnityEngine;

public class ArcaneMistressData : ScriptableObject
{
	public string[] abilities_hero = { "Frost Scythe", "Ethereal Shield", "Frostbite", "Hailstorm"};
	public string[] abilities_normal = { "Arcane Mistress - Targeted Magic" }; // Not hero glow effect and inventory

	public float selection_scale = 1.70f;

	public float acquisition_range = 450.0f;
	public string armor_type = "Flesh";

	public string attack_type = "Magic";
	public float cooldown_time = 1.32f;
	public float damage_base = 2;
	public float damage_factor_medium = 0.0f;
	public float damage_factor_small = 0.0f;
	public float damage_number_of_dice = 1;
	public float damage_sides_per_die = 8;

	public bool projectile_homing_enabled = false; // false or true
	public float projectile_speed = 900;
	public float range = 450;

	public string weapon_type = "Missile";

	public float defense_base = 3;
	public string defense_type = "Large";

	public float speed_base = 350;
	public float turn_rate = 0.6f;

	public float agility_per_level = 1.00f;

	public float mana = 30;
	public float intelligence_per_level = 3.0f;

	public float mana_regeneration = 0.08f;
	public string primary_attribute = "Inteligence";


	public float starting_strength = 6;
	public float starting_intelligence = 9;
	public float starting_agility = 3;

	public float strength_per_level = 2.00f;

	public string hero_name = "Arcane Mistress";
	public string proper_name = "Gizella";
}
