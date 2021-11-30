using System.Collections;
using System.Collections.Generic;
using Rob;
using UnityEngine;


public class DeliverySpawn : MonoBehaviour
{

    public Spawner spawner;
    public float deliveryEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        deliveryEvent = FindObjectOfType<DayNightManager>().currentTime;
        if (deliveryEvent == 12)
        {
            spawner = GetComponent<Spawner>();
            spawner.Spawn();
            Debug.Log("Worked");
        }
    }
        
        
    
}
