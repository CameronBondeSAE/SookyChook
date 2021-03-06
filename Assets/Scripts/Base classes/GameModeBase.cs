using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeBase : MonoBehaviour
{
    public string gameModeName;

    public List<SpawnPoint> playerSpawns;

    public int minPlayers;
    public int maxPlayers;

    public event Action ActivateEvent;
    public event Action EndEvent;
    
    public virtual void Activate()
    {
        ActivateEvent?.Invoke();
    }

    public virtual void EndMode()
    {
        EndEvent?.Invoke();
    }
}
