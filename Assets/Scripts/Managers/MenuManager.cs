using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MenuManager : MonoBehaviour
{
    public bool autoRun = false;
    
    public GameObject menuView;
    [SerializeField]
    private GameObject gameModeButton;

    [SerializeField]
    private Transform gameModeParent;

    private void Awake()
    {
        var mainActions = new MainActions();
        mainActions.InMenu.Enable();
        mainActions.InMenu.ToggleMenu.performed += ToggleMenuOnperformed;

        foreach (GameModeBase gameMode in GameManager.Instance.gameModes)
        {
	        GameObject newButton = Instantiate(gameModeButton, gameModeParent);
	        newButton.GetComponentInChildren<TextMeshProUGUI>().text = gameMode.gameModeName;
	        newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameManager.Instance.SetGameMode(gameMode); });
        }
        
        if (autoRun)
        {
            StartGame(2);
        }
    }

    private void ToggleMenuOnperformed(InputAction.CallbackContext aObj)
    {
        if (menuView.activeSelf)
            HideMenu();
        else
            ShowMenu();
    }

    public void StartGame(int numberOfPlayers)
    {
        menuView.SetActive(false);
        
        GameManager.Instance.SpawnPlayers(numberOfPlayers);

        // Just restart if already playing
        if (GameManager.Instance.inGame)
        {
            GameManager.Instance.EndGame();
        }

        GameManager.Instance.StartGame();
    }

    public void ShowMenu()
    {
        menuView.SetActive(true);
    }

    public void HideMenu()
    {
        menuView.SetActive(false);
    }
}