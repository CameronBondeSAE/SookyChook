using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using Anthill.AI;
using Tanks;
using UnityEditor.MPE;
using UnityEngine;

namespace Aaron
{
    public class MoveToChickenState : AntAIState
    {
        public ScanningGrid.Node currentNodeTarget;
        public GameObject owner;
        public GameObject chickenTarget;
        private TurnTowards turnTowards;
        private Wander wander;
        private MoveForward moveForward;

        public Rigidbody rb;

        private bool rotateTowards = false;
        private bool moveTowards = false;

        private Vector3 targetPosition;

        public float speed = 20;
        public float turnSpeed = 10;
        private float distance;

        private int currentNodeIndex;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();

            //owner.GetComponent<Pathfinding>().enabled = true;

            //Steering Behaviour changes
            wander = owner.GetComponent<Wander>();
            wander.enabled = false;
            turnTowards = owner.GetComponent<TurnTowards>();
            turnTowards.enabled = true;
            moveForward = owner.GetComponent<MoveForward>();
            moveForward.enabled = true;

            chickenTarget = owner.GetComponent<FoxModel>().target;
            rb = owner.GetComponent<Rigidbody>();
            
            turnTowards.target = chickenTarget.transform.position;

            //Pathfinding info
            Vector3Int foxLocation = new Vector3Int(Mathf.RoundToInt(owner.transform.position.x),
                Mathf.RoundToInt(owner.transform.position.y), Mathf.RoundToInt(owner.transform.position.z));
            Vector3Int targetLocation = new Vector3Int(Mathf.RoundToInt(chickenTarget.transform.position.x),
                Mathf.RoundToInt(chickenTarget.transform.position.y),
                Mathf.RoundToInt(chickenTarget.transform.position.z));
            owner.GetComponent<Pathfinding>().FindPath(foxLocation, targetLocation);

            //rotateTowards = true;
            //moveTowards = true;

            //As in Cams tbh
            /*List<ScanningGrid.Node> path = owner.GetComponent<Pathfinding>().path;

            if (path.Count == 0)
            {
                Finish();
            }

            else
            {
                currentNodeTarget = path[currentNodeIndex];
                
                TurnTowards(chickenTarget.transform.position);
                moveTowards = true;
            }*/
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            //As in Cams again
            /*if (currentNodeTarget != null && Vector3.Distance(transform.position,
                owner.GetComponent<Pathfinding>().VectorToInt(currentNodeTarget.coords)) < 1f)
            {
                if (currentNodeIndex < GetComponent<Pathfinding>().path.Count - 1)
                {
                    owner.GetComponent<FoxModel>().inRange = true;
                    Finish();
                }

                else
                {
                    currentNodeIndex++;

                    currentNodeTarget = owner.GetComponent<Pathfinding>().path[currentNodeIndex];
                    
                    TurnTowards(currentNodeTarget.coords);
                }
            }*/

            //Probs Hacks?
            distance = Vector3.Distance(chickenTarget.transform.position, owner.transform.position);
            CheckDistance();
        }

        public override void Exit()
        {
            base.Exit();
        }

        void CheckDistance()
        {
            if (distance <= 0.5f)
            {
                rotateTowards = false;
                moveTowards = false;
            }

            if (distance <= 1)
            {
                owner.GetComponent<FoxModel>().inRange = true;
            }

            if (distance > 1)
            {
                owner.GetComponent<FoxModel>().inRange = false;
            }
        }
    }
}

