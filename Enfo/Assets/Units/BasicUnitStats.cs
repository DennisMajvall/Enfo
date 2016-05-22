using UnityEngine;
using System.Collections;

public class BasicUnitStats : MonoBehaviour {

    // Fields
    public float currentHealth;
    public float maxHealth;
    public float currentMana;
    public float maxMana;
    public float currentMovementSpeed;

    // Getters
    public float CurrentHealth { get { return currentHealth; } }
    public float MaxHealth { get { return maxHealth; } }
    public float CurrentMana { get { return currentMana; } }
    public float MaxMana { get { return maxMana; } }
    public float CurrentMovementSpeed { get { return currentMovementSpeed; } }

    // Setters
    public void changeHealth(float delta)
    {
        currentHealth += delta;
        print(currentHealth);
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void changeMaxHealth(float delta)
    {
        maxHealth += delta;
    }

    public void changeMana(float delta)
    {
        currentMana += delta;
    }

    public void changeMaxMana(float delta)
    {
        maxMana += delta;
    }

    public void changeMovementSpeed(float delta)
    {
        currentMovementSpeed += delta;
    }


}
