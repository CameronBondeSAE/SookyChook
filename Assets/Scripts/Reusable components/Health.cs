using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Allows each object to have unique health variable
    [SerializeField]
    float currentHealth;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float deathThreshold;

    [Header("Debug")]
    [ReadOnly]
    public bool isAlive;

    //DEATH EVENT
    public delegate void DeathSignature(GameObject gameObject);
    public event DeathSignature DeathEvent;

    private void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    //Manage Object Health (Can both increase or decrease health)
    public void ChangeHealth(float amount)
    {
	    // Don't do anything if you're dead
	    if (!isAlive)
	    {
		    return;
	    }
	    
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //If health ever drops to 0 or below fire off DeathEvent
        if(currentHealth <= deathThreshold)
        {
            isAlive = false;
            DeathEvent?.Invoke(gameObject);
            Die();
        }
        else
        {
            isAlive = true;
        }
    }

    public virtual void Die()
    {
	    
    }

    [Button]
    public void ForceDie()
    {
	    ChangeHealth(-float.MaxValue);
    }

    //GET HEALTH
    public float GetHealth()
    {
        return currentHealth;
    }
}
