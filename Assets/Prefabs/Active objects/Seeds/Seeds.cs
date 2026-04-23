using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Seeds : MonoBehaviour, IWaterable
{
	[Header("Seed Properties")]
	public GameObject target;

	public GameObject grassSpawn;

	public bool Crying = false;

	public AudioSource audioSource;
	public AudioClip   grassClip;

	// Start is called before the first frame update

	void Growth(bool isCrying)
	{
		if (isCrying)
		{
			Crying = true;
		}
		else
		{
			Crying = false;
		}
	}


	//Interfaces
	[Button]
	public void BeingWatered(float amount)
	{
		//Destroy Seeds and Grow Grass
		GameObject.Destroy(gameObject);
		GameObject newGrass = GameObject.Instantiate(grassSpawn, transform.localPosition, Quaternion.identity);
		//PlayGrassEffectsClient(newGrass);
	}

	void PlayGrassEffectsClient(GameObject newGrass)
	{
		newGrass.transform.DOShakeRotation(0.5f);
	}
}