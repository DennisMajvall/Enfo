using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

    // Fields
    public float currentHealth;
    public float maxHealth;
    public float healthRegeneration;
    public float currentMana;
    public float maxMana;
    public float manaRegeneration;
    public float currentMovementSpeed;

    // Getters
    public float CurrentHealth { get { return currentHealth; } }
    public float MaxHealth { get { return maxHealth; } }
    public float HealthRegeneration { get { return healthRegeneration; } }
    public float CurrentMana { get { return currentMana; } }
    public float MaxMana { get { return maxMana; } }
    public float ManaRegeneration { get { return manaRegeneration; } }
    public float CurrentMovementSpeed { get { return currentMovementSpeed; } }

    // Setters
    public void changeHealth(float delta)
    {
        currentHealth += delta;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject); 
        }

        if (CurrentHealth > MaxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void changeMaxHealth(float delta)
    {
        maxHealth += delta;
    }

    public void changeHealthRegeneration(float delta)
    {
        healthRegeneration += delta;
    }

    public void changeMana(float delta)
    {
        currentMana += delta;

        if (CurrentMana <= 0)
        {
            currentMana = 0;
        }

        if (CurrentMana > MaxMana)
        {
            currentMana = maxMana;
        }

    }

    public void changeMaxMana(float delta)
    {
        maxMana += delta;
    }

    public void changeMovementSpeed(float delta)
    {
        currentMovementSpeed += delta;
    }

    // Update
    void Update()
    {
        changeHealth(HealthRegeneration * Time.deltaTime);
        changeMana(ManaRegeneration * Time.deltaTime);

    }





}
