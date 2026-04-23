using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;


namespace Rob
{
    public class MoveToRoosterState : SookyAntAIState
    {
        private TassieDevilModel tassieModel;
        private Transform pathTowardsTransform;
        private TurnTowards turnTowards;
        


        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            tassieModel = owner.GetComponent<TassieDevilModel>();
            turnTowards = GetComponentInChildren<TurnTowards>();


            //currentIndex = 0;
        }

        public override void Enter()
        {
            base.Enter();
            //followPath.SetPath(tassieModel.prey);
            tassieModel.isAtFarm = false;
            owner.GetComponentInChildren<Wander>().enabled = false;
            turnTowards.target = tassieModel.prey;
            turnTowards.turnTowardsActive = true;

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            //followPath.TakePath();

            //rb.AddForce(owner.transform.forward * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            float dist = Vector3.Distance(owner.transform.position, tassieModel.prey.position);
            if (dist <= 2f)
            {
                tassieModel.atPrey = true;
                tassieModel.isMoving = false;
                owner.GetComponentInChildren<Forward>().enabled = false;
                turnTowards.turnTowardsActive = false;
                Finish();
            }
            
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}