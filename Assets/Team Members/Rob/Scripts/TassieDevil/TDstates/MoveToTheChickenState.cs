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
        private Rigidbody rb;

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
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            //followPath.TakePath();


            if (Vector3.Distance(transform.position, tassieModel.prey.position) >= 1f)
            {
                transform.LookAt(tassieModel.prey);
                //transform.Translate(0, 0, Time.deltaTime * 5, Space.Self);
                Vector3.MoveTowards(transform.position, tassieModel.prey.position, .5f);
            }
            else
            {
                tassieModel.isMoving = false;
                tassieModel.atPrey = true;

                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}