using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aaron;
using Anthill.AI;
using UnityEngine;

namespace Rob
{
    public class MoveToTheChickenState : SookyAntAIState
    {
        private TassieDevilModel tassieModel;
        private Transform pathTowardsTransform;
        
        [SerializeField]private Rigidbody rb;

        public List<WorldScan.Node> path;
        public int currentIndex;
        public FollowPath followPath;


        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            followPath = owner.GetComponent<FollowPath>();
            tassieModel = owner.GetComponent<TassieDevilModel>();
            rb = owner.GetComponent<Rigidbody>();

            //currentIndex = 0;
        }

        public override void Enter()
        {
            base.Enter();
            //followPath.SetPath(tassieModel.prey);
            tassieModel.isAtFarm = false;
            owner.GetComponentInChildren<Wander>().enabled = false;

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            //followPath.TakePath();
            
            Vector3 targetWorldPosition = transform.InverseTransformPoint(tassieModel.prey.position);
            float turningDirection = targetWorldPosition.x;
            rb.AddTorque(Vector3.up * 100 * turningDirection, ForceMode.Acceleration);
            
            //rb.AddForce(owner.transform.forward * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            float dist = Vector3.Distance(owner.transform.position, tassieModel.prey.position);
            if (dist <= 2f)
            {
                tassieModel.atPrey = true;
                tassieModel.isMoving = false;
                owner.GetComponentInChildren<Forward>().enabled = false;
                Finish();
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}