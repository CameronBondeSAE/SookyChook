using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;




namespace Unity.Netcode.Samples
{
    /// <summary>
    /// Simple Class to display helper buttons and status labels on the GUI, as well as buttons to start host/client/server.
    /// </summary>
    public class BootstrapManager : MonoBehaviour
    {

        public static BootstrapManager Instance { get; private set; }

        [SerializeField]
        private float buttonWidth = 70f;




        [SerializeField]
        private float buttonHeight = 40f;




        public UnityTransport transport;




        string ipAddress = "127.0.0.1";


        public string joinCode;

        private void Awake()
        {
            Instance = this;
        }

        public async Task<string> StartHostWithRelay(int maxConnections, string connectionType)
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            var allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(AllocationUtils.ToRelayServerData(allocation, connectionType));
            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return NetworkManager.Singleton.StartHost() ? joinCode : null;
        }


        public async Task<bool> StartClientWithRelay(string joinCode, string connectionType)
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }


            var allocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(AllocationUtils.ToRelayServerData(allocation, connectionType));
            return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
        }




        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 200, 400));




            var networkManager = NetworkManager.Singleton;
            if (!networkManager.IsClient && !networkManager.IsServer)
            {
                GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
                myButtonStyle.fontSize = 12;
                myButtonStyle.fixedWidth = buttonWidth * 1.5f;
                myButtonStyle.fixedHeight = buttonHeight / 2f;

                if (GUILayout.Button("Host", myButtonStyle))
                {
                    networkManager.StartHost();
                }

                GUILayout.Space(20);

                GUILayout.Label("Manual IP");
                ipAddress = GUILayout.TextField(ipAddress, 15);
                if (GUILayout.Button("Client", myButtonStyle))
                {
                    NewIPAddress(ipAddress);
                    networkManager.StartClient();
                }

                GUILayout.Space(30);
                if (GUILayout.Button("Host Relay", myButtonStyle))
                {
                    StartHostWithRelay(1, "udp");


                }

                // ipAddress = GUILayout.TextField(ipAddress, 15);
                if (GUILayout.Button("Client Relay", myButtonStyle))
                {
                    // NewIPAddress(ipAddress);
                    StartClientWithRelay(joinCode, "udp");
                }




                GUILayout.Space(20);

                if (GUILayout.Button("Server", myButtonStyle))
                {
                    networkManager.StartServer();
                }
            }
            GUILayout.Label("JoinCode");
            joinCode = GUILayout.TextField(joinCode, 15);


            GUILayout.EndArea();
        }




        public void NewIPAddress(string _ipAddress)
        {
            Debug.Log("New IP Address: " + _ipAddress);
            transport.SetConnectionData(_ipAddress, 7777);
        }
    }
}
