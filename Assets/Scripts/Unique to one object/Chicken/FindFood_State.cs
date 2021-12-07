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
		
		// Hack. Pick a random edible
		owner.GetComponent<ChickenModel>().targetEdible = Edible.edibles[Random.Range(0,Edible.edibles.Count)];
		// Edible lookFor = vision.LookForEdible();
		// Edible lookFor = vision.LookFor<Edible>();
	}
}
