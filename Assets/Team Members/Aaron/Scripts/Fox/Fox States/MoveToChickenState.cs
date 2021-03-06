using System;
using System.Buffers;
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
        public Edible chickenTarget;
        private TurnTowards turnTowards;
        private Wander wander;
        private MoveForward moveForward;
        private Rigidbody rb;
        private Vector3 targetPosition;

        private bool rotateTowards = false;
        private bool moveTowards = false;
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

            //Steering Behaviour changes
            wander = owner.GetComponent<Wander>();
            wander.enabled = false;
            turnTowards = owner.GetComponent<TurnTowards>();
            turnTowards.enabled = true;
            
            /*moveForward = owner.GetComponent<MoveForward>();
            moveForward.enabled = true;*/

            chickenTarget = owner.GetComponent<FoxModel>().target;
            rb = owner.GetComponent<Rigidbody>();

            //turnTowards.target = chickenTarget.transform.position;

            //Pathfinding info
            if (owner.GetComponent<FoxModel>().target != null)
            {
                Vector3Int foxLocation = new Vector3Int(Mathf.RoundToInt(owner.transform.position.x),
                    Mathf.RoundToInt(owner.transform.position.y), Mathf.RoundToInt(owner.transform.position.z));
                Vector3Int targetLocation = new Vector3Int(Mathf.RoundToInt(chickenTarget.transform.position.x),
                    Mathf.RoundToInt(chickenTarget.transform.position.y),
                    Mathf.RoundToInt(chickenTarget.transform.position.z));

                owner.GetComponent<Pathfinding>().beginningVectorPos = this.transform;
                owner.GetComponent<Pathfinding>().finishVectorPos = chickenTarget.transform;
                owner.GetComponent<Pathfinding>().FindPath(foxLocation, targetLocation);
            }

            //A la Cam
            List<ScanningGrid.Node> path = owner.GetComponent<Pathfinding>().path;

            if (path.Count <= 0)
            {
                Finish();
            }

            else
            {
                currentNodeTarget = path[currentNodeIndex];

                turnTowards.target = owner.GetComponent<Pathfinding>().VectorToInt(currentNodeTarget.coords);

                owner.GetComponent<MoveForward>().enabled = true;
            }
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            //Probs Hacks?
            distance = Vector3.Distance(chickenTarget.transform.position, owner.transform.position);
            CheckDistance();

            //Also a la Cam
            if (Vector3.Distance(owner.transform.position,
                owner.GetComponent<Pathfinding>().VectorToInt(currentNodeTarget.coords)) < 0.5f)
            {
                currentNodeIndex++;
                currentNodeTarget = owner.GetComponent<Pathfinding>().path[currentNodeIndex];
                //Debug.Log("CURRENT TARGET NODE " + currentNodeTarget.coords);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        void CheckDistance()
        {
            if (distance <= 0.8f)
            {
                rotateTowards = false;
                moveTowards = false;
            }

            if (distance <= 1)
            {
                owner.GetComponent<FoxModel>().inRange = true;
                if (!chickenTarget.GetComponent<Health>().isAlive)
                {
                    owner.GetComponent<FoxModel>().willAttack = false;
                    owner.GetComponent<FoxModel>().eatingChicken = true;
                }
                else
                {
                    owner.GetComponent<FoxModel>().willAttack = true;
                }
            }

            if (distance > 1)
            {
                owner.GetComponent<FoxModel>().inRange = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (currentNodeTarget != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube( new Vector3(currentNodeTarget.coords.x, 0, currentNodeTarget.coords.z), Vector3.one);                
            }
            
        }
    }
    
    
    

}

