using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tom;
using UnityEngine;

public class GoToFood_State : SookyAntAIState
{
    public PathfindingGrid.Node currentNodeTarget;
    public int currentNodeIndex;
    
    ChickenModel chickenModel;
    TurnToward turnToward;

    public override void Enter()
    {
        base.Enter();
        currentNodeIndex = 0;

        turnToward = owner.GetComponent<TurnToward>();
        turnToward.enabled = true;

        chickenModel = owner.GetComponent<ChickenModel>();
        Vector3 destination = chickenModel.targetEdible.transform.position;
        owner.GetComponent<PathfindingAgent>().FindPath(chickenModel.transform.position, destination);

        List<PathfindingGrid.Node> path = owner.GetComponent<PathfindingAgent>().path;

        if (path.Count <= 0)
        {
            Finish();
        }
        else
        {
            currentNodeTarget = path[currentNodeIndex];

            turnToward.target = owner.GetComponent<PathfindingAgent>()
                .ConvertNodeCoordinatesToPosition(currentNodeTarget.coordinates);

            owner.GetComponent<Tom.MoveForward>().enabled = true;
            // turnToward.target =  currentNodeTarget.coordinates
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (currentNodeTarget != null && Vector3.Distance(transform.position,
	        owner.GetComponent<PathfindingAgent>()
		        .ConvertNodeCoordinatesToPosition(currentNodeTarget.coordinates)) < 1f)
        {
            // At end of path?
            if (currentNodeIndex >= owner.GetComponent<PathfindingAgent>().path.Count - 1)
            {
                chickenModel.atFood = true;
                Finish();
            }
            else
            {
                // Simpler keeping track of current path node with an int
                currentNodeIndex++;
                currentNodeTarget = owner.GetComponent<PathfindingAgent>().path[currentNodeIndex];

                turnToward.target = owner.GetComponent<PathfindingAgent>()
                    .ConvertNodeCoordinatesToPosition(currentNodeTarget.coordinates);

                // Fancy just ask the list itself what the index of the current node is and add 1
                // currentNodeTarget = owner.GetComponent<PathfindingAgent>()
                // .path[owner.GetComponent<PathfindingAgent>().path.IndexOf(currentNodeTarget) + 1];
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        owner.GetComponent<Tom.TurnToward>().enabled = false;
        owner.GetComponent<Aaron.Wander>().enabled = false;
        owner.GetComponent<Tom.MoveForward>().enabled = false;
    }
}