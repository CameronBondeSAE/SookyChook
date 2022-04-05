using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class SceneManager : MonoBehaviour
{
    public List<VehicleBase> carPrefabs;
    public VehicleBase cO;
    public List<GameObject> mainCamera;
    public List<GameObject> mC;
    public GameObject mC1;
    public GameObject mC2;
    public VehicleManager vehicleManager;
    public GameObject player;
    //public Rigidbody chassis;
    void Start()
    {
        for (int i = 0; i < carPrefabs.Count && i < mainCamera.Count; i++)
        {
               cO = Instantiate(carPrefabs[i]);
               cO.playerController = player.GetComponent<PlayerController>();
               vehicleManager.vehicles.Add(cO);
               //chassis = cO.GetComponent<Rigidbody>(); 
               mC[i] = Instantiate(mainCamera[i], cO.transform);
               
               //cameraManager.cameraList.Add(mC);
        }
        vehicleManager.vehicles[0].driverIn = false; 
        vehicleManager.vehicles[1].driverIn = true;
        /*mainCamera[0].SetActive(true);
        mainCamera[1].SetActive(false); */
        //mC.SetActive(true);
        /*mainCamera[0].SetActive(true);
        mainCamera[1].SetActive(false);*/
        /*mainCamera[0].gameObject.SetActive(true);
        mainCamera[1].gameObject.SetActive(false);*/

    }
    
}
