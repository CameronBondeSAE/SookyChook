using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public SceneManager sceneManager;
    public VehicleManager vehicleManager; 
    public event Action AccelCarEvent;
    public event Action DecelerateCarEvent;
    public event Action LeftCarEvent;   
    public event Action RightCarEvent;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            AccelCarEvent?.Invoke();
        }

        if (Input.GetKey(KeyCode.S))
        {
            DecelerateCarEvent?.Invoke();
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            LeftCarEvent?.Invoke();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            RightCarEvent?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Red Car On!");
            vehicleManager.vehicles[0].driverIn = true; 
            vehicleManager.vehicles[1].driverIn = false;
            sceneManager.mC[0].SetActive(true);
            sceneManager.mC[1].SetActive(false);
            //hack
            //GetComponentInChildren<Camera>().enabled = true; 
            /*sceneManager.mainCamera[0].SetActive(true);
            sceneManager.mainCamera[1].SetActive(false);*/
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Blue Car On!");
            vehicleManager.vehicles[0].driverIn = false; 
            vehicleManager.vehicles[1].driverIn = true;
            sceneManager.mC[0].SetActive(false);
            sceneManager.mC[1].SetActive(true);
            //hack
            //GetComponentInChildren<Camera>().enabled = true; 
            /*sceneManager.mainCamera[0].SetActive(false);
            sceneManager.mainCamera[1].SetActive(true);*/
        }
        
    }
}
