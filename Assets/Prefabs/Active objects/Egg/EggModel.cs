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

	private Mesh EggMesh;

	public AudioSource audioSource;
	public AudioClip   eggCracking;

	public bool isFertilised;

	public  float hatchTimer;
	private float soundLength;

	// Start is called before the first frame update
	private void Start()
	{
		//EggMesh = GetComponentInChildren<Mesh>();

		// Only server determines if egg is fertilized
		if (IsServer)
		{
			int randomNumber = Random.Range(0, 19);
			if (randomNumber < 9)
			{
				isFertilised = true;
			}

			if (randomNumber >= 10)
			{
				isFertilised = false;
			}
		}

		/*if (isFertilised)
		{
		    //add to fertilised egg list in chicken manager
		    if (ChickenManager.Instance.fertilisedEggsList != null)
		        ChickenManager.Instance.fertilisedEggsList.Add(this.gameObject);
		    //start timer for hatching
		    StartCoroutine("HatchingTimer");
		}*/
		
		
		//for now all eggs start hatching open instantly 
		StartCoroutine("HatchingTimer");
		
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
		
		
		
		audioSource.Play();
		// Only server can hatch eggs (spawn chickens)
		if (!IsServer)
		{
			Debug.LogWarning("Only server can hatch eggs");
			return;
		}
		
		
		soundLength = audioSource.clip.length;

		Invoke("SpawnChicken", soundLength);
		Destroy(egg, soundLength);

		//REMOVED FOR NETWORK TESTING
		//add to chicken list in chicken manager
		//ChickenManager.Instance.chickensList.Add(copy.GetComponent<ChickenModel>());
		//remove this object
		//ChickenManager.Instance.fertilisedEggsList.Remove(this.gameObject);
	}

	public void SpawnChicken()
	{
		// Only server can spawn networked objects
		if (!IsServer)
		{
			return;
		}
		
		// Check if the chicken prefab has a NetworkObject component
		NetworkObject networkObject = chicken.GetComponent<NetworkObject>();
		
		GameObject copy = Instantiate(chicken, egg.transform.position, chicken.transform.rotation);
		
		// If this is a networked object, spawn it on the network
		if (networkObject != null)
		{
			NetworkObject spawnedNetworkObject = copy.GetComponent<NetworkObject>();
			spawnedNetworkObject.Spawn();
		}
		// else: non-networked chicken, just instantiate normally (already done above)
	}

	public ProductType GetProductType()
	{
		return ProductType.Egg;
	}
}