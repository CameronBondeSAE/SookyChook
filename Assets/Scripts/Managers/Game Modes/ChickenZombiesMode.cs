using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenZombiesMode : GameModeBase
{
    public GraveyardManager graveyardManager;
    public CharacterModel[] players;
    public Transform[] playerSpawns;


    public override void Activate()
    {
        base.Activate();

        Debug.Log("Chicken Zombs Activate");
        for (int i = 0; i < players.Length; i++)
        {
            for (int x = 0; x < playerSpawns.Length; x++)
            {
                if (playerSpawns != null)
                {
                    Instantiate(players[i], playerSpawns[x].position, playerSpawns[x].rotation);
                }
            }
        }
    }
}