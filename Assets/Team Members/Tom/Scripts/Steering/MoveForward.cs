using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveForward : MonoBehaviour
    {
        private Rigidbody rb;
        public float speed = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}