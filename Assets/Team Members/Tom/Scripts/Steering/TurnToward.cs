using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class TurnToward : MonoBehaviour
    {
        public Vector3 target;
        public float turnSpeed = 5f;

        public Rigidbody rb;
        private Vector3 cross;
        private Vector3 targetLocalPosition;

        public void FixedUpdate()
        {
            targetLocalPosition = transform.InverseTransformPoint(target);
            float turnDirection = targetLocalPosition.x;
            
            // Prevents object going straight when facing the exact opposite direction
            // by checking if the target is directly behind it in the local Z axis
            if (turnDirection < 0.01f && turnDirection > -0.01f && targetLocalPosition.z < 0)
            {
                turnDirection = 1f;
            }

            rb.AddTorque(Vector3.up * turnDirection * turnSpeed * Time.fixedDeltaTime,
                ForceMode.VelocityChange);
        }
    }
}