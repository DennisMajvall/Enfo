using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    // Fields
    public float currentHealth;
    public float maxHealth;
    public float currentMana;
    public float maxMana;
    public float currentExperience;
    public float currentMovementSpeed;

    // Getters
    public float CurrentHealth { get { return currentHealth; } }
    public float MaxHealth { get { return maxHealth; } }
    public float CurrentMana { get { return currentMana; } }
    public float MaxMana { get { return maxMana; } }
    public float CurrentExperience { get { return currentExperience; } }
    public float CurrentMovementSpeed { get { return currentMovementSpeed; } }

    // Setters
    void changeHealth(float delta)
    {
        currentHealth += delta;
    }

    void changeMaxHealth(float delta)
    {
        maxHealth += delta;
    }

    void changeMana(float delta)
    {
        currentMana += delta;
    }

    void changeMaxMana(float delta)
    {
        maxMana += delta;
    }

    void changeExperience(float delta)
    {
        currentExperience += delta;
    }

    void changeMovementSpeed(float delta)
    {
        currentMovementSpeed += delta;
    }


}
