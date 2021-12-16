using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_ViewModel : MonoBehaviour
{
	public ChickenModel chickenModel;
	public Material deathMaterial;
	public AudioSource  audioSource;
	public AudioClip    interactedWith;
	public AudioClip    pickedUp;
	public AudioClip    putDown;

	public List<AudioClip> dyingSounds;

	public Animator animator;
	
	// Start is called before the first frame update
	void Start()
	{
		chickenModel.InteractEvent += ChickenModelOnInteractEvent;
		chickenModel.PickUpEvent += ChickenModelOnPickUpEvent;
		
		chickenModel.GetComponent<Health>().DeathEvent += OnDeathEvent;
	}

	private void OnDeathEvent(GameObject gameobject)
	{
		audioSource.clip = dyingSounds[Random.Range(0, dyingSounds.Count)];
		audioSource.Play();
	}

	void ChickenModelOnPickUpEvent(bool pickingUp)
	{
		if (!GetComponentInParent<Health>().isAlive)
		{
			return;
		}

		if (pickingUp)
		{
			audioSource.clip = pickedUp;
			animator.SetTrigger("Flap");
		}
		else
		{
			audioSource.clip = putDown;
			animator.SetTrigger("Flap");
		}
		audioSource.Play();
	}

	void ChickenModelOnInteractEvent()
	{
		if (GetComponentInParent<Health>().isAlive)
		{
			audioSource.clip = interactedWith;
			audioSource.Play();

			if (deathMaterial != null) GetComponentInParent<Renderer>().material = deathMaterial;
		}
	}
}