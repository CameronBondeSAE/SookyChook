using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aaron;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class EggModel : NetworkBehaviour, ISellable
{
	public GameObject chicken;
    public GameObject egg;

    //added for testing without sound
    public ParticleSystem eggHatchParticles;

    public AudioSource audioSource;
    public AudioClip eggCracking;

    public bool isFertilised;

    public float hatchTimer;
    private float soundLength;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            return;
        }

        int randomNumber = Random.Range(0, 19);
        if (randomNumber < 9)
        {
            isFertilised = true;
        }
        if (randomNumber >= 10)
        {
            isFertilised = false; 
        }

        /*if (isFertilised)
        {
            //add to fertilised egg list in chicken manager
            if (ChickenManager.Instance.fertilisedEggsList != null)
                ChickenManager.Instance.fertilisedEggsList.Add(this.gameObject);
            //start timer for hatching
            StartCoroutine("HatchingTimer");
        }*/
    }

    private IEnumerator HatchingTimer()
    {
        for (int i = 0; i < hatchTimer; i++)
        {
            yield return new WaitForSeconds(1);
        }

        HatchEgg();
    }


    [Button]
    public void HatchEgg()
    {
        if (NetworkManager.Singleton.IsServer)
        {
	        audioSource.Play();
	        //Particles just for testing without sounds
	        eggHatchParticles.Play();
	        
	        //instantiate chicken
	        GameObject copy = chicken;
	        Instantiate(copy, transform.position, copy.transform.rotation);
	        
	        //This is an issue - "Object already spawned"
	        copy.GetComponent<NetworkObject>().Spawn();
	        
	        soundLength = audioSource.clip.length;

	        Destroy(egg, soundLength);

			//REMOVED FOR NETWORK TESTING
            //add to chicken list in chicken manager
            //ChickenManager.Instance.chickensList.Add(copy.GetComponent<ChickenModel>());
            //remove this object
            //ChickenManager.Instance.fertilisedEggsList.Remove(this.gameObject);
        }
    }

    public ProductType GetProductType()
    {
        return ProductType.Egg;
    }
}
