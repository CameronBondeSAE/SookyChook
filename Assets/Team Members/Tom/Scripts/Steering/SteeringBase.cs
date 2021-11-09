using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class SteeringBase : MonoBehaviour
    {
        protected Rigidbody rb;

        public float neighbourRadius = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public virtual Vector3 CalculateMove(List<Transform> neighbours)
        {
            return Vector3.zero;
        }

        public List<Transform> GetNeighbours()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, neighbourRadius);
            List<Transform> neighbours = new List<Transform>();

            foreach (Collider c in cols)
            {
                if (c.GetComponent<SteeringBase>())
                {
                    neighbours.Add(c.transform);
                }
            }

            return neighbours;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, neighbourRadius);
        }
    }
}