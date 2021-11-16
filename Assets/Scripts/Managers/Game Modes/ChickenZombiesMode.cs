using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenZombiesMode : GameModeBase
{

    public GraveyardManager graveyardManager;
    
    
    public override void Activate()
    {
        base.Activate();
        
        Debug.Log("Chicken Zombs Activate");
    }
}
