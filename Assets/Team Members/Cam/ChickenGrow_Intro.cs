using System;
using System.Collections;
using System.Collections.Generic;
using Rob;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenGrow_Intro : MonoBehaviour
{
	public bool skip = false;
	
	public ChickenGrowingMode chickenGrowingMode;
	public Spawner spawnerSeeds;
	public Spawner spawnerChickens;
	public ChickenModel chickenModel;
	public DayNightManager dayNightManager;
	public OrderPoint orderPoint;

	float skipTime;
	
	private void Awake()
	{
		if (skip)
		{
			skipTime = 0;
		}
		else
		{

			skipTime = 1f;
		}
		
		chickenGrowingMode.ActivateEvent += StartIntro;
	}

	public void StartIntro()
	{
		StartCoroutine(IntroCoroutine());
	}

	// NOTE: This should really use a Timeline asset for easy adjustment and non-coders to make
	private IEnumerator IntroCoroutine()
	{
		orderPoint.gameObject.SetActive(false);
		dayNightManager.enabled = false;
		dayNightManager.ChangePhase(DayNightManager.DayPhase.Morning);

		yield return new WaitForSeconds(2f*skipTime);
		MessagesManager.Instance.Show("Welcome to your farm!");
		yield return new WaitForSeconds(5f*skipTime);
		spawnerSeeds.SpawnMultiple();
		MessagesManager.Instance.Show("Hmm, those look like grass seeds. I wonder how you water them?");
		yield return new WaitForSeconds(6f*skipTime);
		MessagesManager.Instance.Show("Oh no! That chicken looks mighty hungry.. but there's no grass! Oh woe!");
		yield return new WaitForSeconds(7f*skipTime);
		// chickenModel.GetComponent<Health>().ForceDie();
		yield return new WaitForSeconds(2f*skipTime);
		MessagesManager.Instance.Show("YOU MONSTER. You WANTED that chicken DEAD. Use your guilty tears to grow the grass BASTARD");
		yield return new WaitForSeconds(10f*skipTime);
		MessagesManager.Instance.Show("Well.. seeing as you're now a chicken MURDERER, you may as well sell their INNOCENT FLESH");
		yield return new WaitForSeconds(7f*skipTime);
		spawnerChickens.SpawnMultiple();
		dayNightManager.enabled = true;

		orderPoint.gameObject.SetActive(true);

		yield return new WaitForSeconds(3f*skipTime);
		MessagesManager.Instance.Show("Plant more seeds and keep wild animals out... or whatever I don't care anymore");
		// yield return new WaitForSeconds(7f*skipTime);

		chickenGrowingMode.ActivateRestOfGameModeRules();
	}
}
