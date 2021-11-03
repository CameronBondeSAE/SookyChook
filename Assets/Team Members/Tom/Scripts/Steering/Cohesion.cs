using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Cohesion : SteeringBase
    {
        public float moveForce = 0.5f;
    
        public override Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return transform.forward;
            }

            Vector3 centreMove = Vector3.zero;

            foreach (Transform n in neighbours)
            {
                centreMove += n.transform.position - transform.position;
            }

            centreMove /= neighbours.Count;

            return centreMove;
        }

        private void FixedUpdate()
        {
            rb.AddForce(CalculateMove(GetNeighbours()) * moveForce, ForceMode.Acceleration);
        }
    }
}