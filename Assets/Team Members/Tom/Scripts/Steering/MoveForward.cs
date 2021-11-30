using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class MoveForward : MonoBehaviour
    {
        public Rigidbody rb;
        public float speed = 20f;

        private void FixedUpdate()
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}