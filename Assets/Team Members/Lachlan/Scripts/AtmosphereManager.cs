using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AtmosphereManager : NetworkBehaviour
{
    // Sun plus Moon Variables
    public NetworkVariable<float> sunPosition = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public Gradient sunColour;
    
    // Sun and Moon Objects
    public GameObject Sun;
    private Light sunLight;
    public GameObject Moon;
    private Light moonLight;
    public GameObject currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        //sunLight = Sun.GetComponent<Light>();
        //moonLight = Sun.GetComponent<Light>();
        
        //sunLight = GetComponent<Light>();
        //moonLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only the server should update the sun position
        if (IsServer)
        {
            // 360 degrees / 24hrs = 15 to calculate day-night cycle. Then - 180 as zero is noon
            sunPosition.Value = DayNightManager.Instance.currentTime.Value * 15.0f - 180.0f;
        }
        
        // Sun Rotation (all clients can rotate based on the networked value)
        transform.rotation = Quaternion.Euler(sunPosition.Value, 0, 0);

        //time is dawn
        if (sunPosition.Value >= -95)
        {
            Sun.SetActive(true);
        }
        
        // if time is morning
        if (sunPosition.Value >= -75)
        {
            Moon.SetActive(false);
            //Sun.SetActive(true);
            currentState = Sun;
        }
        
        // if time is noon  
        if (sunPosition.Value >= 0)
        {
            currentState = Sun;
        } 
            
        // if time is evening
        if (sunPosition.Value >= 75)
        {
            currentState = Moon;
            //Sun.SetActive(false);
            Moon.SetActive(true);
        }

        if (sunPosition.Value >= 100)
        {
            Sun.SetActive(false);
        }
        
        // if time is night
        if (sunPosition.Value >= 135)
        {
            //currentState = Moon;
        }
        
        // if time is midnight
        if (sunPosition.Value >= -180)
        {
            //currentState = Moon;
        }
        
        //TODO: Change the values, as it never goes to 360, it changes between 180 and -180
    }
}
