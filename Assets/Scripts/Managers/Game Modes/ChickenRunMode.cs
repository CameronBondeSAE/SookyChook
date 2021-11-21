using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRunMode : GameModeBase
{
    public List<CharacterModel> Players;
    public Transform[] SpawnPoints;
    public override void Activate()
    {
        base.Activate();

        for (int i = 0; i < Players.Count; i++)
        {
            if (SpawnPoints != null)
            {
                //TODO below
                //Instantiate(Players[i], SpawnPoints[i], Players[i].transform.rotation);
            }
        }
    }
}
