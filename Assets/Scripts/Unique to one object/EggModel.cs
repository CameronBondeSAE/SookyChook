using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class EggModel : MonoBehaviour
{
    private bool isFertilised;

    private float hatchTimer;
    
    // Start is called before the first frame update
    private void Awake()
    {
        int randomNumber = Random.Range(0, 19);
        if (randomNumber < 9)
        {
            isFertilised = true;
        }
        isFertilised = false;

        if (isFertilised)
        {
            //add to fertilised egg list in chicken manager
            
            //start timer for hatching
            StartCoroutine("HatchingTimer");
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HatchingTimer()
    {
        for (int i = 0; i < hatchTimer; i++)
        {
            yield return new WaitForSeconds(1);
        }

        HatchEgg();
    }

    void HatchEgg()
    {
        //instantiate chicken
        //add to chicken list in chicken manager
        //remove this object
    }
}
