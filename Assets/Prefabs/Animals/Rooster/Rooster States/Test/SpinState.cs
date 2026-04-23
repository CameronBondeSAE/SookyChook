using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class SpinState : AntAIState
{
    public float timer = 3f;
    
    public override void Enter()
    {
        base.Enter();

        timer = 3f;

        
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        timer -= aDeltaTime;
        
        transform.root.Rotate(Vector3.up, 360 * Time.deltaTime);

        if (timer <= 0)
        {
            //GetComponentInParent<RoosterSense>().spinning = true;
            //Finish();
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        //GetComponentInParent<RoosterSense>().spinning = true;
    }
}
