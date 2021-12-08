using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindFood_State : SookyAntAIState
{
	public override void Enter()
	{
		base.Enter();

		StartCoroutine(FindFoodCoroutine());
	}

	public IEnumerator FindFoodCoroutine()
	{
		yield return new WaitForSeconds(2f);

		// Hack. Pick a random edible
		foreach (Edible edible in Edible.edibles)
		{
			if (!edible.GetComponent<ChickenModel>())
			{
				owner.GetComponent<ChickenModel>().targetEdible = edible;
				owner.GetComponent<ChickenModel>().foundFood = true;
				Debug.Log($"Found food {owner.GetComponent<ChickenModel>().targetEdible}");
			}
		}
		
		Finish();
	}
}
