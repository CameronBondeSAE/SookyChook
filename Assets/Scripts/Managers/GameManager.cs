using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameModeBase gameMode;

    public void StartGame()
    {
        gameMode.Activate();
    }
}
