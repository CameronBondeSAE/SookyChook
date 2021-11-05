using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameMode : GameModeBase
{
    public override void Activate()
    {
        base.Activate();
        
        Debug.Log("Test Mode Activated");
    }
}
