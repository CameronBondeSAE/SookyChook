using System;
using System.Collections;
using System.Collections.Generic;
using Rob;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenGrow_Intro : MonoBehaviour
{
	public ChickenGrowingMode chickenGrowingMode;
	public Spawner spawnerSeeds;
	public Spawner spawnerChickens;
	public ChickenModel chickenModel;

	private void Awake()
	{
		chickenGrowingMode.ActivateEvent += StartIntro;
	}

	public void StartIntro()
	{
		StartCoroutine(IntroCoroutine());
	}

	// NOTE: This should really use a Timeline asset for easy adjustment and non-coders to make
	private IEnumerator IntroCoroutine()
	{
		yield return new WaitForSeconds(2f);
		MessagesManager.Instance.Show("Welcome to your farm!");
		yield return new WaitForSeconds(5f);
		spawnerSeeds.SpawnMultiple();
		MessagesManager.Instance.Show("Hmm, those look like grass seeds. I wonder how you water them?");
		yield return new WaitForSeconds(6f);
		MessagesManager.Instance.Show("Oh no! That chicken looks mighty hungry.. but there's no grass! Oh woe!");
		yield return new WaitForSeconds(7f);
		chickenModel.GetComponent<Health>().ForceDie();
		MessagesManager.Instance.Show("YOU MONSTER. You WANTED that chicken DEAD. Use your guilty tears to grow the grass BASTARD");
		yield return new WaitForSeconds(7f);
		spawnerChickens.SpawnMultiple();

	}
}
