using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Steer : MonoBehaviour
    {
        public Rigidbody rb;
    
        public float detectRange = 3f;
        public float turnForce = 5f;
        public float proximityMultiplier = 10f;
    
        public enum Direction
        {
            Right = 1,
            Left = -1
        }

        public Direction direction;

        void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, detectRange))
            {
                float proximity = (detectRange - hit.distance) * proximityMultiplier;
                rb.AddTorque(new Vector3(0f, turnForce * (float)direction * proximity, 0f), ForceMode.Acceleration);
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward * detectRange, Color.red);
        }
    }
}