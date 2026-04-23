using System.Collections;
using System.Collections.Generic;
using Aaron;
using UnityEngine;

public class GhostChicken : MonoBehaviour
{
	public Vector3 playerLocation;
	public Rigidbody rb;
	private TurnTowards turnTowards;
	private CharacterModel characterModel;

	public List<AudioClip> clips;
	public AudioSource audioSource;

	// Start is called before the first frame update
	void Start()
	{
		characterModel = FindObjectOfType<CharacterModel>();
		rb = GetComponent<Rigidbody>();
		turnTowards = GetComponent<TurnTowards>();
		playerLocation = characterModel.transform.position;
		turnTowards.target = playerLocation;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (clips.Count > 0)
		{
			if (Random.Range(0, 100) == 1)
			{
				audioSource.clip = clips[Random.Range(0, clips.Count)];
				audioSource.Play();
			}
		}
	}
}