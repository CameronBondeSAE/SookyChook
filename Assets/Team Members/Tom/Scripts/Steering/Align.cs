using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Align : SteeringBase
    {
        public override Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return transform.forward;
            }
        
            Vector3 alignmentMove = Vector3.zero;

            foreach (Transform n in neighbours)
            {
                alignmentMove += n.transform.forward;
            }

            alignmentMove /= neighbours.Count;

            return alignmentMove;
        }

        private void FixedUpdate()
        {
            rb.AddTorque(CalculateMove(GetNeighbours()));
        }
    }
}