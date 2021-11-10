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
    public GameObject Moon;
    
    // Start is called before the first frame update
    void Start()
    {
        // 360 degrees / 24hrs = 15 to calculate day-night cycle
        sunPosition = FindObjectOfType<DayNightManager>().currentTime * 15.0f;
        Quaternion.Euler(sunPosition, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        // if time is morning
        if (sunPosition >= 105)
        {
            
        }
        
        // if time is noon  
        if (sunPosition >= 180)
        {
            
        } 
            
        // if time is evening
        if (sunPosition >= 255)
        {
            
        }
        
        // if time is night
        if (sunPosition >= 315)
        {
            
        }
        
        // if time is midnight
        if (sunPosition >= 0)
        {
            
        }
    }
}
