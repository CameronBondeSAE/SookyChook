using Unity.Services.Authentication;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System;
using Unity.Netcode;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }

    public const string KEY_PLAYER_NAME = "PlayerName";
    public const string KEY_PLAYER_CHARACTER = "Character";
    public const string KEY_GAME_MODE = "GameMode";
    public const string KEY_START_GAME = "Start";

    public event EventHandler OnLeftLobby;

    public event EventHandler<LobbyEventArgs> OnJoinedLobby;
    public event EventHandler<LobbyEventArgs> OnJoinedLobbyUpdate;
    public event EventHandler<LobbyEventArgs> OnKickedFromLobby;
    public event EventHandler<LobbyEventArgs> OnLobbyGameModeChanged;
    public event EventHandler<LobbyEventArgs> OnGameStarted;
    public class LobbyEventArgs : EventArgs
    {
        public Lobby lobby;
    }

    public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
    public class OnLobbyListChangedEventArgs : EventArgs
    {
        public List<Lobby> lobbyList;
    }

    //public enum GameMode
    //{
    //    CaptureTheFlag,
    //    Conquest
    //}

    //public enum PlayerCharacter
    //{
    //    Marine,
    //    Ninja,
    //    Zombie
    //}

    private float heartbeatTimer;
    private float lobbyPollTimer;
    private float refreshLobbyListTimer = 5f;
    private Lobby joinedLobby;
    private string playerName;

    async void Start()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //HandleRefreshLobbyList(); // Disabled Auto Refresh for testing with multiple builds
        HandleLobbyHeartbeat();
        HandleLobbyPolling();
    }

    public async void Authenticate(string playerName)
    {
        this.playerName = playerName;
        InitializationOptions initializationOptions = new InitializationOptions();
        initializationOptions.SetProfile(playerName);

        await UnityServices.InitializeAsync(initializationOptions);

        AuthenticationService.Instance.SignedIn += () => {
            // do nothing
            Debug.Log("Signed in! " + AuthenticationService.Instance.PlayerId);

            RefreshLobbyList();
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void HandleRefreshLobbyList()
    {
        if (UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn)
        {
            refreshLobbyListTimer -= Time.deltaTime;
            if (refreshLobbyListTimer < 0f)
            {
                float refreshLobbyListTimerMax = 5f;
                refreshLobbyListTimer = refreshLobbyListTimerMax;

                RefreshLobbyList();
            }
        }
    }

    private async void HandleLobbyHeartbeat()
    {
        if (IsLobbyHost())
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f)
            {
                float heartbeatTimerMax = 15f;
                heartbeatTimer = heartbeatTimerMax;

                Debug.Log("Heartbeat");
                await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
            }
        }
    }

    private bool hasStartedGame = false;

    private async void HandleLobbyPolling()
    {
        if (joinedLobby != null)
        {
            lobbyPollTimer -= Time.deltaTime;
            if (lobbyPollTimer < 0f)
            {
                float lobbyPollTimerMax = 1.1f;
                lobbyPollTimer = lobbyPollTimerMax;

                joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                if (!IsPlayerInLobby())
                {
                    Debug.Log("Kicked from Lobby!");

                    OnKickedFromLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                    joinedLobby = null;
                    hasStartedGame = false;
                    return;
                }

                if (!hasStartedGame && joinedLobby.Data[KEY_START_GAME].Value != "0")
                {
                    hasStartedGame = true;

                    if (!IsLobbyHost())
                    {
                        TestRelay.Instance.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value);
                    }

                    OnGameStarted?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
                }
            }
        }
    }

    public Lobby GetJoinedLobby()
    {
        return joinedLobby;
    }

    public bool IsLobbyHost()
    {
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }

    private bool IsPlayerInLobby()
    {
        if (joinedLobby != null && joinedLobby.Players != null)
        {
            foreach (Player player in joinedLobby.Players)
            {
                if (player.Id == AuthenticationService.Instance.PlayerId)
                {
                    // This player is in this lobby
                    return true;
                }
            }
        }
        return false;
    }

    private Player GetPlayer()
    {

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogError("Player not signed in yet!");
            return null;
        }

        return new Player(AuthenticationService.Instance.PlayerId, null, new Dictionary<string, PlayerDataObject> {
        { KEY_PLAYER_NAME, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerName) },
        { KEY_PLAYER_CHARACTER, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public/*, PlayerCharacter.Marine.ToString()*/) }
    });
    }

    //public void ChangeGameMode()
    //{
    //    if (IsLobbyHost())
    //    {
    //        GameMode gameMode =
    //            Enum.Parse<GameMode>(joinedLobby.Data[KEY_GAME_MODE].Value);

    //        switch (gameMode)
    //        {
    //            default:
    //            case GameMode.CaptureTheFlag:
    //                gameMode = GameMode.Conquest;
    //                break;
    //            case GameMode.Conquest:
    //                gameMode = GameMode.CaptureTheFlag;
    //                break;
    //        }

    //        UpdateLobbyGameMode(gameMode);
    //    }
    //}

    public async void CreateLobby(string lobbyName, /*int maxPlayers,*/ bool isPrivate/*, GameMode gameMode*/)
    {
        Player player = GetPlayer();
        if (player == null) return;

        CreateLobbyOptions options = new CreateLobbyOptions
        {
            Player = player,
            IsPrivate = isPrivate,
            Data = new Dictionary<string, DataObject> {
                { KEY_GAME_MODE, new DataObject(DataObject.VisibilityOptions.Public/*, gameMode.ToString()*/) },
                { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, "0") }
            }
        };

        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 2, options);

        joinedLobby = lobby;

        OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });

        Debug.Log("Created Lobby " + lobby.Name);
    }

    public async void RefreshLobbyList()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;

            // Filter for open lobbies only
            options.Filters = new List<QueryFilter> {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0")
            };

            // Order by newest lobbies first
            options.Order = new List<QueryOrder> {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

            QueryResponse lobbyListQueryResponse = await LobbyService.Instance.QueryLobbiesAsync();

            OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs { lobbyList = lobbyListQueryResponse.Results });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinLobbyByCode(string lobbyCode)
    {
        Player player = GetPlayer();
        if (player == null) return;

        Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new JoinLobbyByCodeOptions
        {
            Player = player
        });

        joinedLobby = lobby;

        OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
    }

    public async void JoinLobby(Lobby lobby)
    {
        Player player = GetPlayer();
        if (player == null) return;

        joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id, new JoinLobbyByIdOptions
        {
            Player = player
        });

        OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
    }

    public async void UpdatePlayerName(string playerName)
    {
        this.playerName = playerName;

        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_NAME, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerName)
                    }
                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }

    //public async void UpdatePlayerCharacter(PlayerCharacter playerCharacter)
    //{
    //    if (joinedLobby != null)
    //    {
    //        try
    //        {
    //            UpdatePlayerOptions options = new UpdatePlayerOptions();

    //            options.Data = new Dictionary<string, PlayerDataObject>() {
    //                {
    //                    KEY_PLAYER_CHARACTER, new PlayerDataObject(
    //                        visibility: PlayerDataObject.VisibilityOptions.Public,
    //                        value: playerCharacter.ToString())
    //                }
    //            };

    //            string playerId = AuthenticationService.Instance.PlayerId;

    //            Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
    //            joinedLobby = lobby;

    //            OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
    //        }
    //        catch (LobbyServiceException e)
    //        {
    //            Debug.Log(e);
    //        }
    //    }
    //}

    public async void QuickJoinLobby()
    {
        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

            Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            joinedLobby = lobby;

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void LeaveLobby()
    {
        if (joinedLobby != null)
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);

                joinedLobby = null;

                OnLeftLobby?.Invoke(this, EventArgs.Empty);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }

    public async void KickPlayer(string playerId)
    {
        if (IsLobbyHost())
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }

    //public async void UpdateLobbyGameMode(GameMode gameMode)
    //{
    //    try
    //    {
    //        Debug.Log("UpdateLobbyGameMode " + gameMode);

    //        Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
    //        {
    //            Data = new Dictionary<string, DataObject> {
    //                { KEY_GAME_MODE, new DataObject(DataObject.VisibilityOptions.Public, gameMode.ToString()) }
    //            }
    //        });

    //        joinedLobby = lobby;

    //        OnLobbyGameModeChanged?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
    //    }
    //    catch (LobbyServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}

    public async void StartGame()
    {
        if (IsLobbyHost())
        {
            try
            {
                Debug.Log("StartGame");

                hasStartedGame = true;

                string relayCode = await TestRelay.Instance.CreatRelay();

                Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                {
                    { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                }
                });

                joinedLobby = lobby;
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }

    public async void ReturnToLobbyState()
    {
        hasStartedGame = false;

        if (joinedLobby == null) return;
        if (!IsLobbyHost()) return;

        try
        {
            Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
            {
                { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, "0") }
            }
            });

            joinedLobby = lobby;
            OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}

//public class LobbyManager : MonoBehaviour
//{
//    private Lobby hostLobby;
//    private Lobby joinedLobby;
//    private float heartbeatTimer;
//    private float lobbyUpdateTimer;
//    private string playerName;

//    async void Start()
//    {
//        await UnityServices.InitializeAsync();

//        AuthenticationService.Instance.SignedIn += () =>
//        {
//            Debug.Log("Signed in" + AuthenticationService.Instance.PlayerId);
//        };

//        await AuthenticationService.Instance.SignInAnonymouslyAsync();

//        playerName = "CodeMonkey" + UnityEngine.Random.Range(10, 99);
//        Debug.Log(playerName);
//    }

//    private void Update()
//    {
//        HandleLobbyHeartbeat();
//        HandleLobbyPollForUpdates();
//    }

//    private async void HandleLobbyPollForUpdates()
//    {
//        if (joinedLobby != null)
//        {
//            lobbyUpdateTimer -= Time.deltaTime;
//            if (lobbyUpdateTimer < 0)
//            {
//                float lobbyUpdateTimerMax = 1.1f;
//                lobbyUpdateTimer = lobbyUpdateTimerMax;

//                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
//                joinedLobby = lobby;
//            }
//        }
//    }

//    private async void HandleLobbyHeartbeat()
//    {
//        if (hostLobby != null)
//        {
//            heartbeatTimer -= Time.deltaTime;
//            if (heartbeatTimer < 0)
//            {
//                float heartbeatTimerMax = 15;
//                heartbeatTimer = heartbeatTimerMax;

//                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
//            }
//        }
//    }

//    private async void CreatLobby()
//    {
//        try
//        {
//            string lobbyName = "MyLobby";
//            int MaxPlayers = 2;
//            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
//            {
//                IsPrivate = false,
//                Player = GetPlayer(),
//                Data = new Dictionary<string, DataObject>
//                {
//                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "CaptureTheFlag") },
//                    { "Map", new DataObject(DataObject.VisibilityOptions.Public, "de_dust2") }
//                }
//            };
//            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, MaxPlayers, createLobbyOptions);

//            hostLobby = lobby;
//            joinedLobby = hostLobby;

//            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Id + " " + lobby.LobbyCode);
//            PrintPlayers(hostLobby);
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void ListLobbies()
//    {
//        try
//        {
//            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
//            {
//                Count = 25,
//                Filters = new List<QueryFilter>
//                {
//                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
//                },
//                Order = new List<QueryOrder>
//                {
//                    new QueryOrder (false, QueryOrder.FieldOptions.Created)
//                }
//            };

//            QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync();

//            Debug.Log("Lobbies found: " + response.Results.Count);
//            foreach (Lobby lobby in response.Results)
//            {
//                Debug.Log(lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Data["GameMode"].Value);
//            }
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void JoinLobbyByCode(string lobbyCode)
//    {
//        try
//        {
//            JoinLobbyByCodeOptions joinLobbyByCodeOption = new JoinLobbyByCodeOptions
//            {
//                Player = GetPlayer()
//            };
//            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOption);
//            joinedLobby = lobby;

//            Debug.Log("Joined Lobby with code " + lobbyCode);

//            PrintPlayers(lobby);
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void QuickJoinLobby()
//    {
//        try
//        {
//            await LobbyService.Instance.QuickJoinLobbyAsync();
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private Player GetPlayer()
//    {
//        return new Player
//        {
//            Data = new Dictionary<string, PlayerDataObject>
//            {
//                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
//            }
//        };
//    }

//    private void PrintPlayers()
//    {
//        PrintPlayers(joinedLobby);
//    }

//    private void PrintPlayers(Lobby lobby)
//    {
//        Debug.Log("Player in Lobby " + lobby.Name + " " + lobby.Data["GameMode"].Value + " " + lobby.Data["Map"].Value);
//        foreach (Player player in lobby.Players)
//        {
//            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
//        }
//    }

//    private async void UpdateLobbyGameMode(string gameMode)
//    {
//        try
//        {
//            hostLobby = await LobbyService.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
//            {
//                Data = new Dictionary<string, DataObject>
//                {
//                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, gameMode) }
//                }
//            });
//            joinedLobby = hostLobby;
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void UpdatePlayerName(string newPlayerName)
//    {
//        playerName = newPlayerName;
//        await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
//        {
//            Data = new Dictionary<string, PlayerDataObject>
//            {
//                 { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
//            }
//        });
//    }

//    private async void LeaveLobby()
//    {
//        try
//        {
//            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void KickPlayer()
//    {
//        try
//        {
//            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, joinedLobby.Players[1].Id);
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void MigrateLobbyHost()
//    {
//        try
//        {
//            hostLobby = await LobbyService.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
//            {
//                HostId = joinedLobby.Players[1].Id
//            });
//            joinedLobby = hostLobby;

//            PrintPlayers(hostLobby);
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }

//    private async void DeleteLobby()
//    {
//        try
//        {
//            await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
//        }
//        catch (LobbyServiceException e)
//        {
//            Debug.Log(e);
//        }
//    }
//}

