using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
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
	    // If this is a networked object, only server can change health
	    if (NetworkObject != null && !IsServer)
	    {
		    Debug.LogWarning("Only server can change health on networked objects");
		    return;
	    }
	    
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
	        
	        // If networked, notify all clients
	        if (NetworkObject != null && IsServer)
	        {
		        TriggerDeathClientRpc();
	        }
	        else
	        {
		        // Non-networked object, just invoke locally
		        DeathEvent?.Invoke(gameObject);
		        Die();
	        }
        }
        else
        {
            isAlive = true;
        }
    }

    [ClientRpc]
    private void TriggerDeathClientRpc()
    {
	    isAlive = false;
	    DeathEvent?.Invoke(gameObject);
	    Die();
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
