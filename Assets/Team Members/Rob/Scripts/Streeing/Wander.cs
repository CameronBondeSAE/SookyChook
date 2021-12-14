using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : MonoBehaviour
{
    public Rigidbody rb;
    public float turningAmount;

    private float randomOffset;
    private float perlin;
    

    // Start is called before the first frame update
    void Start()
    {
        randomOffset = Random.Range(1f, 100f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        perlin = Mathf.PerlinNoise(Time.time + randomOffset, 0f) * 2 - 1;
        rb.AddRelativeTorque(0f, perlin * turningAmount, 0f, ForceMode.VelocityChange);

    }
}