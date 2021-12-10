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
        
        public List<WorldScan.Node> path;
        public int currentIndex;
        public FollowPath followPath;
        

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            followPath = owner.GetComponent<FollowPath>();
            tassieModel = owner.GetComponent<TassieDevilModel>();
            
            currentIndex = 0;
        }

        public override void Enter()
        {
            base.Enter();
            
            followPath.SetPath(tassieModel.prey);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            followPath.TakePath();
            if (tassieModel.atTarget)
            {
                tassieModel.isMoving = false;
                
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}