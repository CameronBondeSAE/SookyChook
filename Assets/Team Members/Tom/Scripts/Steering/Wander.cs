using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Wander : MonoBehaviour
    {
        public Rigidbody rb;
        public float turnForce = 1f;
        
        private void FixedUpdate()
        {
            float perlin = Mathf.PerlinNoise(Time.time, 0f) - 0.5f;
            rb.AddRelativeTorque(0f, perlin * turnForce, 0f);
        }
    }
}