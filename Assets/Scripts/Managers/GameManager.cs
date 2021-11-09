using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameModeBase gameMode;

    
    [Button]
    public void StartGame()
    {
        gameMode.Activate();
    }
}
