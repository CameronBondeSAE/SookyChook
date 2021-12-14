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

		List<Edible> imInterestedInThese = new List<Edible>();

		// Hack. Pick a random edible
		foreach (Edible edible in Edible.edibles)
		{
			if (!edible.GetComponent<ChickenModel>())
			{
				imInterestedInThese.Add(edible);
			}
		}


		if (imInterestedInThese != null)
			owner.GetComponent<ChickenModel>().targetEdible =
				imInterestedInThese[Random.Range(0, imInterestedInThese.Count)];
		owner.GetComponent<ChickenModel>().foundFood    = true;
		// Debug.Log($"Found food {owner.GetComponent<ChickenModel>().targetEdible}");

		
		Finish();
	}
}
