using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aaron;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class EggModel : MonoBehaviour
{
    private ChickenManager chickenManager;
    public GameObject chicken;

    public bool isFertilised;

    public float hatchTimer;
    
    // Start is called before the first frame update
    private void Awake()
    {
        chickenManager = FindObjectOfType<ChickenManager>();
        
        int randomNumber = Random.Range(0, 19);
        if (randomNumber < 9)
        {
            isFertilised = true;
        }
        if (randomNumber >= 10)
        {
            isFertilised = false; 
        }

        if (isFertilised)
        {
            //add to fertilised egg list in chicken manager
            if (ChickenManager.Instance.fertilisedEggsList != null)
                ChickenManager.Instance.fertilisedEggsList.Add(this.gameObject);
            //start timer for hatching
            StartCoroutine("HatchingTimer");
        }
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
        GameObject copy = chicken;
        //copy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Instantiate(copy, this.transform.position, copy.transform.rotation);

        //add to chicken list in chicken manager
        chickenManager.chickensList.Add(copy);
        
        //remove this object
        chickenManager.fertilisedEggsList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
