using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GraveyardManager graveyardManager;
    public CharacterModel[] players;
    public Transform[] playerSpawns = new Transform[4];


    public void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (playerSpawns != null)
            {
                Instantiate(players[i], playerSpawns[i].position, playerSpawns[i].rotation);
            }
        }
    }


    // private void OnDrawGizmos()
    // {
    //     for (int i = 0; i < playerSpawns.Length; i++)
    //     {
    //         Gizmos.color = Color.magenta;
    //         Gizmos.DrawLine(playerSpawns[i].localPosition,playerSpawns[i].localPosition + Vector3.forward);
    //     }
    //     
    // }
}