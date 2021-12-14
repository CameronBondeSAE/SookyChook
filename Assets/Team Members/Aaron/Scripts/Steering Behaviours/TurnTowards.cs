using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aaron
{
    public class TurnTowards : MonoBehaviour
    {
        private Rigidbody rb;
        public Vector3 target;
        private Vector3 targetPos;

        public int turnSpeed = 25;
        private float turnValue;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            targetPos = transform.InverseTransformPoint(target);
            turnValue = targetPos.x;

            rb.AddTorque(Vector3.up * turnValue * turnSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}