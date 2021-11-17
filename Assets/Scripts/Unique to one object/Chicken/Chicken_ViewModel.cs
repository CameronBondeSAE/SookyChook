using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_ViewModel : MonoBehaviour
{
	public ChickenModel chickenModel;
	public AudioSource  audioSource;
	public AudioClip    interactedWith;
	public AudioClip    pickedUp;
	public AudioClip    putDown;

	public Animator animator;
	
	// Start is called before the first frame update
	void Start()
	{
		chickenModel.InteractEvent += ChickenModelOnInteractEvent;
		chickenModel.PickUpEvent += ChickenModelOnPickUpEvent;
	}

	void ChickenModelOnPickUpEvent(bool pickingUp)
	{
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
		audioSource.clip = interactedWith;
		audioSource.Play();
	}
}