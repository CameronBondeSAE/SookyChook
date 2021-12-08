using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

namespace Aaron
{
    public class FindChickenState : AntAIState
    {
        //private ChickenManager chickenManager = FindObjectOfType<ChickenManager>();
        public GameObject owner;
        public GameObject target;

        public List<GameObject> chickensInWorld = new List<GameObject>();

        public float fov = 45;
        public float distance = 20;

        public bool canSeeTarget;
        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            
            chickensInWorld = ChickenManager.Instance.chickensList;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            foreach (var chicken in chickensInWorld)
            {
                Vector3 directionToTarget = chicken.transform.position - transform.position;
            
                float angleToEnemy = Vector3.Angle(transform.forward, directionToTarget);
                float distanceToTarget = Vector3.Distance(transform.position, chicken.transform.position);
            
                if (angleToEnemy < fov && distanceToTarget < distance)
                {
                    canSeeTarget = true;
                    Debug.DrawLine(transform.position, chicken.transform.position, Color.green);

                }
                else
                {
                    canSeeTarget = false;
                    Debug.DrawLine(transform.position, chicken.transform.position, Color.red);
                }
                
                if (canSeeTarget)
                {
                    target = chicken;
                    owner.GetComponent<FoxModel>().target = target;
                    owner.GetComponent<FoxModel>().canSeeChicken = true;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}

