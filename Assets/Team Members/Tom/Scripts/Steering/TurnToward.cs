using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class TurnToward : MonoBehaviour
    {
        public float maxSpeed = 5f;
        public Vector3 target;
        public float turnSpeed = 5f;
        public bool turning = false;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            if (turning)
            {
                Vector3 direction = target - transform.position;
                Vector3 cross = Vector3.Cross(direction, transform.forward);
                cross = new Vector3(0, cross.y, 0); // Need to limit rotation to y axis

                rb.AddRelativeTorque(-cross * turnSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
                rb.AddRelativeForce(transform.forward * maxSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }
    }
}