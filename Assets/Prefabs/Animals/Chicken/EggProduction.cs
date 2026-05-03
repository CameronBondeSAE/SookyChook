using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

//WOULD HAVE LOVED TO USE INHERITANCE BUT IT WAS PRETTY MESSY?
//DIDN'T REALLY WANT TO HARD RIP TO A CHICKEN
public class EggProduction : NetworkBehaviour
{
    private ChickenModel chicken;
    public GameObject egg;

    public bool canLay;

    public int layTime = 10;
    
    private float layTimeMulitplier;
    
    // Start is called before the first frame update
    void Start()
    {
        chicken = GetComponent<ChickenModel>();
        
        // Only the server should run the egg laying timer
        if (IsServer)
        {
            StartCoroutine("LayTimer");
        }
    }

    private void Update()
    {
        if (chicken.hungerThreshold < 0.2f)
        {
            canLay = false;
        }
        canLay = true;
        
        layTimeMulitplier = (1f - chicken.hungerLevel) / 10;
    }

    private IEnumerator LayTimer()
    {
        for (int i = 0; i < layTime; i++)
        {
            yield return new WaitForSeconds(1 + layTimeMulitplier);
        }

        LayEgg();
    }

    void LayEgg()
    {
        // Only server can spawn networked objects
        if (!IsServer)
        {
            return;
        }
        
        if (egg is { })
        {
            GameObject copy = egg;
            Vector3 chickenLocation = transform.position;
        
            GameObject spawnedEgg = Instantiate(copy, new Vector3(chickenLocation.x, chickenLocation.y, chickenLocation.z), copy.transform.rotation);
            
            // Check if egg has NetworkObject component and spawn it on the network
            NetworkObject networkObject = spawnedEgg.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                networkObject.Spawn();
            }
        }
    }
}
