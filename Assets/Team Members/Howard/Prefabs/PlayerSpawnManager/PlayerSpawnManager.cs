using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnManager : NetworkBehaviour
{
    public Transform[] spawnPoints;
    public NetworkObject playerPrefab;
    
    // public CommandSystemManager commandManager;
    private int nextSpawnIndex = 0;

    private readonly Dictionary<ulong, int> playerNumberByClientId = new();

    private void Awake()
    {
        // commandManager = CommandSystemManager.Instance;
    }

    private void OnEnable()
    {
        // if (commandManager == null)
        // {
        //     commandManager = CommandSystemManager.Instance;
        // }
        //
        // if (commandManager != null)
        // {
        //     commandManager.playerSpawned_Event += SpawnPlayer;
        // }
    }

    private void OnDisable()
    {
        // if (commandManager != null)
        // {
            // commandManager.playerSpawned_Event -= SpawnPlayer;
        // }
    }

    public void SpawnPlayer(ulong clientId)
    {
        if (!IsServer) return;

        if (nextSpawnIndex >= spawnPoints.Length)
        {
            Debug.LogWarning("Not enough spawn points.");
            return;
        }

        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out var client))
        {
            if (client.PlayerObject != null)
            {
                Debug.LogWarning("Client " + clientId + " already has PlayerObject.");
                return;
            }
        }

        Transform spawn = spawnPoints[nextSpawnIndex];

        NetworkObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);
        player.SpawnAsPlayerObject(clientId);

        int playerNumber = nextSpawnIndex + 1;
        playerNumberByClientId[clientId] = playerNumber;

        nextSpawnIndex++;

        Debug.Log("Spawned player for client" + clientId + " as Player " + playerNumber);
    }

    public int GetPlayerNumber(ulong clientId)
    {
        if (playerNumberByClientId.TryGetValue(clientId, out int playerNumber))
        {
            return playerNumber;
        }

        return -1;
    }

    public void DespawnAllPlayers()
    {
        if (!IsServer) return;

        foreach (var clientPair in NetworkManager.Singleton.ConnectedClients)
        {
            var playerObject = clientPair.Value.PlayerObject;
            if (playerObject != null && playerObject.IsSpawned)
            {
                playerObject.Despawn(true);
            }
        }

        playerNumberByClientId.Clear();
        nextSpawnIndex = 0;
    }

    public void ResetSpawnIndex()
    {
        nextSpawnIndex = 0;
        playerNumberByClientId.Clear();
    }

    //private void OnEnable()
    //{
    //    commandManager.playerSpawned_Event += SpawnPlayer;
    //}

    //private void OnDisable()
    //{
    //    commandManager.playerSpawned_Event -= SpawnPlayer;
    //}

    //public override void OnNetworkSpawn()
    //{
    //    if (IsServer)
    //    {
    //        NetworkManager.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
    //    }
    //}

    //public override void OnNetworkDespawn()
    //{
    //    if (NetworkManager != null && NetworkManager.SceneManager != null)
    //    {
    //        NetworkManager.SceneManager.OnSceneEvent -= SceneManager_OnSceneEvent;
    //    }
    //}

    //private void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    //{
    //    if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
    //    {
    //        Debug.Log("Player " + sceneEvent.ClientId + " Loading complete, ready to generate!");
    //        SpawnPlayer(sceneEvent.ClientId);
    //    }
    //}

    //public void SpawnPlayer(ulong clientId)
    //{
    //    Transform spawn = spawnPoints[nextSpawnIndex];
    //    nextSpawnIndex++;

    //    NetworkObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);

    //    player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    //}
}