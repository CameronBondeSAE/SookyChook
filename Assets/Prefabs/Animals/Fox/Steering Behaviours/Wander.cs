using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aaron
{
    public class Wander : MonoBehaviour
    {
        public Rigidbody rb;

        public float speed = 10f;
        public float turn;
        public float turnMultiplier;
        private float startOffset;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            startOffset = Random.Range(0, 100000);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
            turn = Mathf.PerlinNoise(0, Time.time+startOffset) * 2 - 1;

            rb.AddRelativeTorque(0, turn * turnMultiplier, 0);
        }
    }
}