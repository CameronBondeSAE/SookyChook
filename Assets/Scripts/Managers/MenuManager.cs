using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public bool autoRun = false;
    
    public GameObject menuView;

    private void Awake()
    {
        var mainActions = new MainActions();
        mainActions.InMenu.Enable();
        mainActions.InMenu.ToggleMenu.performed += ToggleMenuOnperformed;

        if (autoRun)
        {
            StartGame();
        }
    }

    private void ToggleMenuOnperformed(InputAction.CallbackContext aObj)
    {
        if (menuView.activeSelf)
            HideMenu();
        else
            ShowMenu();
    }

    public void StartGame()
    {
        menuView.SetActive(false);

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