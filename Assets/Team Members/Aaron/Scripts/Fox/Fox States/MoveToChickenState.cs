using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using Tanks;
using UnityEditor.MPE;
using UnityEngine;

namespace Aaron
{
    public class MoveToChickenState : AntAIState
    {
        public GameObject owner;
        public GameObject chickenTarget;

        public Rigidbody rb;

        private bool rotateTowards = false;
        private bool moveTowards = false;

        private Vector3 targetPosition;

        public float speed = 20;
        public float turnSpeed = 10;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();

            chickenTarget = owner.GetComponent<FoxModel>().target;
            rb = owner.GetComponent<Rigidbody>();

            rotateTowards = true;
            moveTowards = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            //rotate towards target
            if(rotateTowards)
            {
                targetPosition = transform.InverseTransformPoint(chickenTarget.transform.position);
                
                float turnDirection = targetPosition.x;
                
                rb.AddTorque(Vector3.up * turnDirection * turnSpeed * Time.deltaTime,
                    ForceMode.VelocityChange);
            }

            //TODO change to addForce
            if(moveTowards)
            {
                //having issues with addForce
                //rb.AddForce(owner.transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

                owner.GetComponent<Wander>().enabled = false;

                owner.transform.position = Vector3.MoveTowards(owner.transform.position, chickenTarget.transform.position, (speed*Time.deltaTime)/2);
            }

            //Probs Hacks?
            float distance = Vector3.Distance(chickenTarget.transform.position, owner.transform.position);
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

        public override void Exit()
        {
            base.Exit();
        }
    }
}

