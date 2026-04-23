using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rob
{


    public class TurnTowards : MonoBehaviour
    {
        public Rigidbody rb;
        public Transform target;
        public float torque;
        public bool turnTowardsActive = false;
        
        
        
        private void FixedUpdate()
        {
            if (turnTowardsActive)
            {
                Vector3 targetWorldPosition = transform.InverseTransformPoint(target.position);
                float turningDirection = targetWorldPosition.x;
                rb.AddTorque(Vector3.up * torque * turningDirection, ForceMode.Acceleration);
            }
            
        }
    }
}
