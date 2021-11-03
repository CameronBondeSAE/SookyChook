using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Steer : MonoBehaviour
    {
        private Rigidbody rb;
    
        public float detectRange = 3f;
        public float turnForce = 5f;
    
        public enum Direction
        {
            Right = 1,
            Left = -1
        }

        public Direction direction;

        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, detectRange))
            {
                rb.AddRelativeTorque(new Vector3(0f, turnForce * (float)direction, 0f), ForceMode.Acceleration);
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward * detectRange, Color.red);
        }
    }
}