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

        public float speed = 1;
        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Moving to Chicken State");

            chickenTarget = owner.GetComponent<FoxModel>().target;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            //TODO rotate towards target
            //move towards chicken
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, chickenTarget.transform.position, speed*Time.deltaTime);
            
            owner.GetComponent<Wander>().enabled = false;

            if (Vector3.Distance(owner.transform.position, chickenTarget.transform.position) < 0.5f)
            {
                owner.GetComponent<FoxModel>().inRange = true;
            }
            else
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

