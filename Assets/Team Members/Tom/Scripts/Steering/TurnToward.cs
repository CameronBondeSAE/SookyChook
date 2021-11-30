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

            rb.AddTorque(Vector3.up * turnDirection * turnSpeed * Time.fixedDeltaTime,
                ForceMode.VelocityChange);
        }
    }
}