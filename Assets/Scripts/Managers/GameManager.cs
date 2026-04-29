using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : ManagerBase<GameManager>
{
    [SerializeField]
    private GameObject playerPrefab;

    public CinemachineTargetGroup cinemachineTargetGroup;

    public GameModeBase[] gameModes;
    public GameModeBase gameMode;
    // public List<CharacterModel> players;

    public bool inGame = false;

    [SerializeField]
    private bool debugForceSpawnPlayersAndCamera = false;

    private void OnEnable()
    {
	    NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayer;
	    LobbyManager.Instance.OnGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
	    // NetworkManager.Singleton.OnClientConnectedCallback -= SpawnPlayer;
	    // LobbyManager.Instance. -= OnJoinedLobby;
    }

    private void OnGameStarted(object sender, LobbyManager.LobbyEventArgs e)
    {
	    StartGame();
    }

    // [Button]
    public void StartGame()
    {
	    // TODO: Get gamemodes to do this
	    // if (debugForceSpawnPlayersAndCamera)
	    // {
	    // SpawnPlayers(2);
	    // }
        
	    // AssignLocalMultiplayerPlayersToCameraGroup();
	    
	    AssignLocalPlayerCamera_Rpc();
	    
	    gameMode.Activate();

	    inGame = true;
    }

    [Rpc(SendTo.ClientsAndHost, Delivery = RpcDelivery.Reliable)]
    private void AssignLocalPlayerCamera_Rpc()
    {
	    foreach (KeyValuePair<ulong, NetworkClient> character in NetworkManager.Singleton.ConnectedClients)
	    {
		    if (character.Value.PlayerObject.IsLocalPlayer)
		    {
			    cinemachineTargetGroup.AddMember(character.Value.PlayerObject.transform, 1f, 6f);
		    }
	    }
    }

    public void AssignLocalMultiplayerPlayersToCameraGroup()
    {
	    // HACK: to make a single player camera zoom out more
	    // if (players.Count == 1)
	    // {
		    // cinemachineTargetGroup.AddMember(players[0].transform, 1f, 6f);
	    // }
	    // else
	    // {
		    // foreach (var characterModel in players)
		    // {
			    // cinemachineTargetGroup.AddMember(characterModel.transform, 1f, 3f);
		    // }
	    // }
    }

    public void SetGameMode(GameModeBase _gameMode)
    {
	    gameMode = _gameMode;
    }

    public void SpawnPlayer(ulong clientId)
    {
        PlayerInput newPlayer = PlayerInput.Instantiate(playerPrefab, 1, "Keyboard Arrows", -1, Keyboard.current);
        // PlayerInput newPlayer = PlayerInput.Instantiate(playerPrefab, 1, "Keyboard WASD", -1, Keyboard.current);
        // players.Add(newPlayer
                        // .GetComponent<CharacterModel>()); // HACK: Could make more generic I guess, but don't have a character base class
        newPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
                  
        // TODO: HACK hardcoded spawn
        if (gameMode.playerSpawns.Count>=NetworkManager.Singleton.ConnectedClients.Count)
        {
	        newPlayer.transform.position = gameMode.playerSpawns[NetworkManager.Singleton.ConnectedClients.Count-1].transform.position;
	        newPlayer.transform.rotation = gameMode.playerSpawns[NetworkManager.Singleton.ConnectedClients.Count-1].transform.rotation;
        }
        else
        {
	        Debug.LogWarning("Not enough spawn points for players : " + NetworkManager.Singleton.ConnectedClients.Count);
        }
    }

    [Button]
    public void EndGame()
    {
        foreach (var characterModel in NetworkManager.Singleton.ConnectedClients)
        {
            Destroy(characterModel.Value.PlayerObject.gameObject);
            cinemachineTargetGroup.RemoveMember(characterModel.Value.PlayerObject.transform);
        }

        gameMode.EndMode();

        inGame = false;
    }
}