using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeBase : MonoBehaviour
{
    public string gameModeName;

    public List<SpawnPoint> playerSpawns;

    public int minPlayers;
    public int maxPlayers;
    
    public virtual void Activate()
    {
        
    }

    public virtual void EndMode()
    {
        
    }
}
