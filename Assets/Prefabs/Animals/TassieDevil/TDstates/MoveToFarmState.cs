using System.Collections;
using System.Collections.Generic;
using Rob;
using UnityEngine;

public class MoveToFarmState : SookyAntAIState
{
    public Transform farmSpawnPoint;
    public List<Transform> mainFarmSpawn;

    private FollowPath followPath;
    private TassieDevilModel tassieDevilModel;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        followPath = owner.GetComponent<FollowPath>();
        tassieDevilModel = owner.GetComponent<TassieDevilModel>();
        
        
    }

    public override void Enter()
    {
        base.Enter();
        foreach (SpawnPoint spawnPoint in SpawnPoint.spawnPoints)
        {
            if (spawnPoint.typeOfPointOfPoint == SpawnPoint.TypeOfPoint.MainFarm)
            {
                mainFarmSpawn.Add(spawnPoint.transform);
                farmSpawnPoint = mainFarmSpawn[Random.Range(0, mainFarmSpawn.Count)];
            }
        }
        followPath.SetPath(farmSpawnPoint);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        followPath.TakePath();
        if (tassieDevilModel.atTarget)
        {
            tassieDevilModel.isAtFarm = true;
            tassieDevilModel.GetComponentInChildren<Wander>().enabled = true;
            Finish();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}