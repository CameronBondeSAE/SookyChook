using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    public GameModeBase gameMode;
    public List<CharacterModel> players;

    [Button]
    public void StartGame()
    {
        gameMode.Activate();
    }
}
