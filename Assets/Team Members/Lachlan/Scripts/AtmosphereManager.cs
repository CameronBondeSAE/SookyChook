using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereManager : MonoBehaviour
{
    // Sun plus Moon Variables
    public float sunPosition;
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
        // 360 degrees / 24hrs = 15 to calculate day-night cycle. Then - 180 as zero is noon
        sunPosition=DayNightManager.Instance.currentTime* 15.0f-180.0f;
        
        // Sun Rotation
        transform.rotation=Quaternion.Euler(sunPosition, 0, 0);

        //time is dawn
        if (sunPosition >= -95)
        {
            Sun.SetActive(true);
        }
        
        // if time is morning
        if (sunPosition >= -75)
        {
            Moon.SetActive(false);
            //Sun.SetActive(true);
            currentState = Sun;
        }
        
        // if time is noon  
        if (sunPosition >= 0)
        {
            currentState = Sun;
        } 
            
        // if time is evening
        if (sunPosition >= 75)
        {
            currentState = Moon;
            //Sun.SetActive(false);
            Moon.SetActive(true);
        }

        if (sunPosition >= 100)
        {
            Sun.SetActive(false);
        }
        
        // if time is night
        if (sunPosition>=135)
        {
            //currentState = Moon;
        }
        
        // if time is midnight
        if (sunPosition >=-180)
        {
            //currentState = Moon;
        }
        
        //TODO: Change the values, as it never goes to 360, it changes between 180 and -180
    }
}


