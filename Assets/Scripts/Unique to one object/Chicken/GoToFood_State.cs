using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tom;
using UnityEngine;

public class GoToFood_State : SookyAntAIState
{
	public PathfindingGrid.Node currentNodeTarget;

	public override void Enter()
	{
		base.Enter();

		ChickenModel chickenModel = owner.GetComponent<ChickenModel>();
		owner.GetComponent<PathfindingAgent>().FindPath(chickenModel.transform.position, chickenModel.targetEdible.transform.position);

		List<PathfindingGrid.Node> path = owner.GetComponent<PathfindingAgent>().path;

		if (path.Count <= 0)
		{
			Finish();
		}
		else
		{
			currentNodeTarget = path[0];

			TurnToward turnToward = owner.GetComponent<TurnToward>();
			turnToward.enabled = true;
			// turnToward.target =  currentNodeTarget.coordinates
		}
		
	}

	public override void Exit()
	{
		base.Exit();
		
		owner.GetComponent<TurnToward>().enabled = false;
	}
}
