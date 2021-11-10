using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGrowingMode : GameModeBase
{
    public List<CharacterModel> players;
    public Transform[] playerSpawns = new Transform[4];
    
    public override void Activate()
    {
        base.Activate();

        for (int i = 0; i < players.Count; i++)
        {
            if (playerSpawns[i] != null)
            {
                Instantiate(players[i], playerSpawns[i].position, playerSpawns[i].rotation);
            }
        }
    }
}