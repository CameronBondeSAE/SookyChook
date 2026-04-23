using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Rob
{
    public class RunAwayState : SookyAntAIState
    {
        private FollowPath followPath;
        private Transform forrestSpawnPoint;
        private TassieDevilModel tassieModel;


        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            followPath = owner.GetComponent<FollowPath>();
            tassieModel = owner.GetComponent<TassieDevilModel>();

        }

        public override void Enter()
        {
            base.Enter();
            foreach (SpawnPoint spawnPoint in SpawnPoint.spawnPoints)
            {
                if (spawnPoint.typeOfPointOfPoint == SpawnPoint.TypeOfPoint.Forest)
                {
                    forrestSpawnPoint = spawnPoint.transform;
                }
            }
            followPath.SetPath(forrestSpawnPoint);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            followPath.TakePath();
            if (tassieModel.atTarget)
            {
                tassieModel.isLooking = true;
                tassieModel.atTarget = false;
                tassieModel.atPrey = false;
                tassieModel.seeChicken = false;
                tassieModel.isHungry = false;
                owner.GetComponentInChildren<Wander>().enabled = true;
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        } 
    }
}