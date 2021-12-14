using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Rob
{
    public class TakeChicken : SookyAntAIState
    {
        private TassieDevilModel tassieModel;
        private FollowPath followPath;

        public Transform forrestSpawnPoint;


        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            tassieModel = owner.GetComponent<TassieDevilModel>();
            followPath = owner.GetComponent<FollowPath>();
        }

        public override void Enter()
        {
            base.Enter();
            tassieModel.prey.gameObject.GetComponent<Health>().ChangeHealth(-1000);
            owner.GetComponentInChildren<Forward>().speed = 45f;
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
            owner.GetComponentInChildren<Forward>().enabled = true;
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