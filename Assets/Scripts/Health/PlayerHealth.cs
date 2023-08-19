using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected static PlayerHealth instance;
    public static PlayerHealth Instance => instance;

    protected void Awake() => CreateSingleton();

    public virtual void Heal(float amount)
    {
        // Do nothing if the character is dead 
        if (isDead) return;

        // Add HP
        hP += amount;

        // Check if HP = 100
        if (hP >= 100)
        {
            hP = 100;
        }
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
