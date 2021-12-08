using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aaron;
using Anthill.AI;
using Unity.XR.OpenVR;
using UnityEngine;

public class IdleState : AntAIState
{
    public GameObject owner;
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();

        owner.GetComponent<FoxModel>().target = null;
        
        //reset relevant bools
        owner.GetComponent<FoxModel>().isHunting = false;
        owner.GetComponent<FoxModel>().chickenGone = false;
        owner.GetComponent<FoxModel>().inRange = false;
        owner.GetComponent<FoxModel>().canSeeChicken = false;
        owner.GetComponent<FoxModel>().eatingChicken = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (owner.GetComponent<FoxModel>().hunger <= owner.GetComponent<FoxModel>().maxHunger / 2)
        {
            owner.GetComponent<FoxModel>().isHunting = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
