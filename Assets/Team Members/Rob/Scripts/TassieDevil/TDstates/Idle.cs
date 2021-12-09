using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;


namespace Rob
{

    public class Idle : AntAIState
    {
        public GameObject owner;
        private TassieDevilModel tassieDevilModel;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            StartCoroutine(TestFindChicken());
            
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        IEnumerator TestFindChicken()
        {
            yield return new WaitForSeconds(3);
            Finish();
        }
    }
}
