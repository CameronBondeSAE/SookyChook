using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : ManagerBase<GameManager>
{
    [SerializeField]
    private GameObject playerPrefab;

    public CinemachineTargetGroup cinemachineTargetGroup;

    public GameModeBase[] gameModes;
    public GameModeBase gameMode;
    public List<CharacterModel> players;

    public bool inGame = false;

    [SerializeField]
    private bool debugForceSpawnPlayersAndCamera = false;

    [Button]
    public void StartGame()
    {
	    // TODO: Get gamemodes to do this
	    if (debugForceSpawnPlayersAndCamera)
	    {
		    SpawnPlayers(2);
	    }
        
	    AssignPlayersToCameraGroup();
	    
        gameMode.Activate();

        inGame = true;
    }

    public void AssignPlayersToCameraGroup()
    {
	    // HACK: to make a single player camera zoom out more
	    if (players.Count == 1)
	    {
		    cinemachineTargetGroup.AddMember(players[0].transform, 1f, 6f);
	    }
	    else
	    {
		    foreach (var characterModel in players)
		    {
			    cinemachineTargetGroup.AddMember(characterModel.transform, 1f, 3f);
		    }
	    }
    }

    public void SetGameMode(GameModeBase _gameMode)
    {
	    gameMode = _gameMode;
    }

    public void SpawnPlayers(int numberOfPlayers)
    {
        PlayerInput p1 = PlayerInput.Instantiate(playerPrefab, 1, "Keyboard WASD", -1, Keyboard.current);
        players.Add(p1
                        .GetComponent<CharacterModel>()); // HACK: Could make more generic I guess, but don't have a character base class

        // HACK hardcoded spawn
        p1.transform.position = gameMode.playerSpawns[0].transform.position;
        p1.transform.rotation = gameMode.playerSpawns[0].transform.rotation;
        
        
        if (numberOfPlayers>1)
        {
	        PlayerInput p2 = PlayerInput.Instantiate(playerPrefab, 2, "Keyboard Arrows", -1, Keyboard.current);
	        players.Add(p2
		        .GetComponent<CharacterModel>()); // HACK: Could make more generic I guess, but don't have a character base class
	        
	        // HACK hardcoded spawn
	        p2.transform.position = gameMode.playerSpawns[1].transform.position;
	        p2.transform.rotation = gameMode.playerSpawns[1].transform.rotation;
        }
    }

    [Button]
    public void EndGame()
    {
        foreach (var characterModel in players)
        {
            Destroy(characterModel.gameObject);
        }
        players.Clear();

        foreach (var characterModel in players)
        {
            cinemachineTargetGroup.RemoveMember(characterModel.transform);
        }
 
        gameMode.EndMode();

        inGame = false;
    }
}