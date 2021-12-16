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

        public List<Transform> forrestSpawns;

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
            owner.GetComponentInChildren<Forward>().enabled = true;
            tassieModel.hunger = tassieModel.maxHunger;
            foreach (SpawnPoint spawnPoint in SpawnPoint.spawnPoints)
            {
                if (spawnPoint.typeOfPointOfPoint == SpawnPoint.TypeOfPoint.Forest)
                {
                    forrestSpawns.Add(spawnPoint.transform);
                    forrestSpawnPoint = forrestSpawns[Random.Range(0,forrestSpawns.Count)];
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