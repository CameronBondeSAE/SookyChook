using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aaron
{
    public class MoveForward : MonoBehaviour
    {
        private Rigidbody rb;
        public float speed = 25f;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}