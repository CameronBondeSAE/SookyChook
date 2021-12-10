using System.Collections;
using System.Collections.Generic;
using Rob;
using UnityEngine;

public class MoveToFarmState : SookyAntAIState
{
    public Transform farmSpawnPoint;

    private FollowPath followPath;
    private TassieDevilModel tassieDevilModel;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        followPath = owner.GetComponent<FollowPath>();
        tassieDevilModel = owner.GetComponent<TassieDevilModel>();

        foreach (SpawnPoint spawnPoint in SpawnPoint.spawnPoints)
        {
            if (spawnPoint.typeOfPointOfPoint == SpawnPoint.TypeOfPoint.MainFarm)
            {
                farmSpawnPoint = spawnPoint.transform;
            }
        }
    }

    public override void Enter()
    {
        base.Enter();
        followPath.SetPath(farmSpawnPoint);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        followPath.TakePath();
        if (tassieDevilModel.atTarget)
        {
            
            Finish();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}