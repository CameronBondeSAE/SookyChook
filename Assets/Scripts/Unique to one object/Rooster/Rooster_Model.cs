using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Rob;
using Tom;
using UnityEngine;

public class Rooster_Model : AnimalBase
{
    public Vision vision;
    public Allegiances allegiances;
    public Transform target;

    private void Awake()
    {
        GetComponent<Health>().DeathEvent += o => Death();
    }

    public List<GameObject> FindEnemies()
    {
        List<GameObject> seenEnemies = new List<GameObject>();
        
        // Get all wildlife and players into one list
        List<Allegiances> possibleEnemies = new List<Allegiances>();
        foreach (GameObject animal in WildLifeManager.Instance.animalsSpawned)
        {
            possibleEnemies.Add(animal.GetComponent<Allegiances>());
        }
        foreach (CharacterModel player in GameManager.Instance.players)
        {
            possibleEnemies.Add(player.GetComponent<Allegiances>());
        }

        // Go through all players and wildlife and find if you can see one that you hate
        foreach (Allegiances a in possibleEnemies)
        {
            if (a != null)
            {
                if (vision.CanSeeTarget(a.transform))
                {
                    foreach (Allegiances.Entry entry in allegiances.allegiance)
                    {
                        if (entry.type == a.whatAmI)
                        {
                            if (entry.amount < -0.8f)
                            {
                                seenEnemies.Add(a.gameObject);
                            }
                        }
                    }
                }
            }
        }

        return seenEnemies;
    }

    public IEnumerator EatFood()
    {
        for(int i = 0; i < 2; i++)
        {
            target.GetComponent<Edible>().BeingEaten(0.5f);
            yield return new WaitForSeconds(1f);
        }
            
        ChangeHunger(-0.5f);

        target = null;
        GetComponent<RoosterSense>().food = null;
    }

    public void Death()
    {
        GetComponent<Health>().isAlive = false;
        GetComponent<AntAIAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 90f);
    }

    public override void ReachedMaxHungry()
    {
        base.ReachedMaxHungry();
        
        // Hostile to players when too hungry
        foreach (Allegiances.Entry entry in allegiances.allegiance)
        {
            if (entry.type == Allegiances.Types.Player)
            {
                entry.amount = -1f;
                return;
            }
        }
    }
}