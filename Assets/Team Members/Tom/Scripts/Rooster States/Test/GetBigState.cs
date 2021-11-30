using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class GetBigState : AntAIState
{
    public override void Enter()
    {
        base.Enter();

        //GetComponentInParent<RoosterSense>().isBig = true;
        
        transform.root.localScale = new Vector3(2, 2, 2);
        Finish();
    }

    public override void Exit()
    {
        base.Exit();
        
        //GetComponentInParent<RoosterSense>().isBig = false;
        
        transform.root.localScale = new Vector3(1,1,1);
    }
}
