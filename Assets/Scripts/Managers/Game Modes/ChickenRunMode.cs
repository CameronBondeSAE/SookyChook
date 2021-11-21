using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRunMode : GameModeBase
{
    public List<CharacterModel> Players;
    
    public Transform[] SpawnPoints;
    public Transform[] RaceCheckPoints;
    
    public override void Activate()
    {
        base.Activate();

        for (int i = 0; i < Players.Count; i++)
        {
            if (SpawnPoints != null)
            {
                Instantiate(Players[i], SpawnPoints[i].transform.position, Players[i].transform.rotation);
            }
        }
        
        //Function to Instantiate checkpoint indicators at RaceCheckPoints I suppose?
    }
}
