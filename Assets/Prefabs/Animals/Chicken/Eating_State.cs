using System;
using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating_State : SookyAntAIState
{
	ChickenModel chickenModel;

	Coroutine coroutine;
	
	public override void Enter()
	{
		base.Enter();

		chickenModel = owner.GetComponent<ChickenModel>();

		if (coroutine!=null)
		{
			StopCoroutine(coroutine);
		}
		coroutine = StartCoroutine(EatCoroutine());
	}

	private void OnDestroy()
	{
		if (coroutine!=null)
		{
			StopCoroutine(coroutine);
		}
	}

	private IEnumerator EatCoroutine()
	{
		for (int i = 0; i < 3; i++)
		{
			chickenModel.targetEdible.GetComponent<Edible>().BeingEaten(0.3f);
			yield return new WaitForSeconds(1f);
		}

		chickenModel.ChangeHunger(-chickenModel.targetEdible.foodAmount);
		DestroyImmediate(chickenModel.targetEdible.gameObject);
		
		chickenModel.atFood = false;
		chickenModel.foundFood = false;
		chickenModel.targetEdible = null;
		Finish();
	}
}