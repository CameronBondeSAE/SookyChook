using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private GameObject lobbyUIRoot;

    public void HideLobbyUI()
    {
        if (lobbyUIRoot == null)
        {
            Debug.LogWarning("Lobby UI Root not assigned.");
            return;
        }

        lobbyUIRoot.SetActive(false);
    }

    public void ShowLobbyUI()
    {
        if (lobbyUIRoot == null)
        {
            Debug.LogWarning("Lobby UI Root not assigned.");
            return;
        }

        lobbyUIRoot.SetActive(true);
    }
}
