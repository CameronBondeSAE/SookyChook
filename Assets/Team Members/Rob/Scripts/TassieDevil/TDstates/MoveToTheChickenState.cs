using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using UnityEngine;

namespace Rob
{
    public class MoveToTheChickenState : AntAIState
    {
        private TassieDevilModel tassieModel;
        public List<WorldScan.Node> path;
        private Vector3Int preyPos;
        private Vector3Int myPos;
        public int currentIndex;
        public FollowPath followPath;


        public GameObject owner;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            followPath = GetComponent<FollowPath>();
            tassieModel = GetComponent<TassieDevilModel>();
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
            followPath.TakePath(tassieModel.prey);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}