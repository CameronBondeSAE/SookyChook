using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating_State : SookyAntAIState
{
	ChickenModel chickenModel;
	
	public override void Enter()
	{
		base.Enter();

		chickenModel = owner.GetComponent<ChickenModel>();
		
		StartCoroutine(EatCoroutine());
	}

	private IEnumerator EatCoroutine()
	{
		yield return new WaitForSeconds(3f);
		
		chickenModel.ChangeHunger(-chickenModel.targetEdible.foodAmount);
		DestroyImmediate(chickenModel.targetEdible.gameObject);
		// DestroyImmediate(owner);
		
		chickenModel.atFood = false;
		chickenModel.foundFood = false;
		chickenModel.targetEdible = null;
		Finish();
	}
}