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

        public float turn;
        public float turnMultiplier;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            turn = Mathf.PerlinNoise(0, Time.time) * 2 - 1;

            rb.AddRelativeTorque(0, turn * turnMultiplier, 0);
        }
    }
}