using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Separate : SteeringBase
    {
        public float moveForce = 1f;
    
        public override Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return transform.forward;
            }

            Vector3 separateMove = Vector3.zero;

            foreach (Transform n in neighbours)
            {
                if (n != transform)
                {
                    Vector3 vectorToAdd = transform.position - n.transform.position;
                    vectorToAdd = new Vector3(1f / vectorToAdd.x, 0f, 1f / vectorToAdd.z);
                    separateMove += vectorToAdd;
                }
            }

            separateMove /= neighbours.Count;

            return separateMove;
        }

        private void FixedUpdate()
        {
            rb.AddForce(CalculateMove(GetNeighbours()) * moveForce, ForceMode.Acceleration);
        }
    }
}