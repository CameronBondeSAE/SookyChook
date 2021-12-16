using System.Collections;
using System.Collections.Generic;
using Aaron;
using Anthill.AI;
using Rob;
using UnityEngine;

public class FindRoosterState : SookyAntAIState
{
    private FOV fov;
    private TassieDevilModel tassieDevilModel;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        fov = owner.GetComponent<FOV>();
        tassieDevilModel = owner.GetComponent<TassieDevilModel>();
    }

    public override void Enter()
    {
        base.Enter();
        tassieDevilModel.atTarget = false;

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        foreach (GameObject rooster in ChickenManager.Instance.roostersList)
        {
            if (fov.CanISee(rooster.transform))
            {
                tassieDevilModel.seeRooster = true;
                tassieDevilModel.prey = rooster.transform;
                tassieDevilModel.isLooking = false;
                tassieDevilModel.isMoving = true;
                Finish();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

