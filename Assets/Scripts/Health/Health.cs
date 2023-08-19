using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public float hP = 100;

    public bool isDead;

    protected void OnEnable()
    {
        Respawn();
    }

    public virtual void TakeDamage(float damage)
    {
        // Do nothing if the character is dead 
        if (isDead) return;

        // Deduct HP
        hP -= damage;

        // Check if HP = 0
        if (hP <= 0)
        {
            isDead = true;
            hP = 0;
        }
    }

    public virtual void Respawn()
    {
        // Set character isDead boolean to false
        isDead = false;
    }

    public virtual void OnDead()
    {
        if (isDead == true)
        {
            // Set character layer to default
            transform.parent.gameObject.layer = 0;
            // Destroy character gameObject
            Destroy(transform.parent.gameObject, 3f);
        }
    }
}