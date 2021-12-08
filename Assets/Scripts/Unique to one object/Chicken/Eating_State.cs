using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating_State : SookyAntAIState
{
	public override void Enter()
	{
		base.Enter();

		ChickenModel chickenModel = owner.GetComponent<ChickenModel>();
		
		Destroy(chickenModel.targetEdible.gameObject);
		chickenModel.ReduceHunger(chickenModel.hungerLevel);
	}
}