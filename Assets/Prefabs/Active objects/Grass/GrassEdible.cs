using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;

public class GrassEdible : Edible
{
	public ParticleSystem particleSystem;
	
	public AudioSource audioSource;
	public AudioClip grassClip;

	private void Start()
	{
		// Network spawned from server, don't need ClientRpc
		audioSource.clip = grassClip;
		audioSource.Play();
		transform.DOShakeRotation(0.5f);
	}

	public override void BeingEaten(float amount)
	{
		base.BeingEaten(amount);

		// transform.localScale /= 1.5f;
		transform.DOShakeScale(0.5f, 1f);
		particleSystem.Emit(10);
	}

	public override void Eaten()
	{
		base.Eaten();
		
		Debug.Log("Do something cool");
	}
}
