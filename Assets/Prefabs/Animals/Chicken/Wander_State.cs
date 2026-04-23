using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tom;
using UnityEngine;

public class Wander_State : SookyAntAIState
{
    ChickenModel chickenModel;

    public override void Enter()
    {
        base.Enter();

        chickenModel = owner.GetComponent<ChickenModel>();

        owner.GetComponent<Tom.TurnToward>().enabled = false;
        owner.GetComponent<Aaron.Wander>().enabled = true;
        owner.GetComponent<Tom.MoveForward>().enabled = false;
    }


    public override void Exit()
    {
        base.Exit();

        owner.GetComponent<Tom.TurnToward>().enabled = false;
        owner.GetComponent<Aaron.Wander>().enabled = false;
        owner.GetComponent<Tom.MoveForward>().enabled = false;
    }
}