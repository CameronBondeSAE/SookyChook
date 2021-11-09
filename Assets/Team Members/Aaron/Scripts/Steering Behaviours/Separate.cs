using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aaron
{
    public class Separate : MonoBehaviour
    {
        public List<GameObject> foxNeighbours = new List<GameObject>();

        private Rigidbody rb;
        
        private Vector3 avoidMovement = Vector3.zero;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rb.AddRelativeTorque(new Vector3(avoidMovement.x, 0, avoidMovement.z), ForceMode.VelocityChange);
        }

        Vector3 AvoidFoxes()
        {
            //avoids other foxes maybe?
            if (foxNeighbours.Count > 0)
            {
                foreach (var fox in foxNeighbours)
                {
                    Vector3 direction = (this.transform.position - fox.transform.position).normalized;
                    direction = new Vector3(1f / direction.x, 0, 1f / direction.z);

                    avoidMovement += direction;
                }
            }

            return avoidMovement;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fox"))
            {
                if (!foxNeighbours.Contains(other.gameObject))
                {
                    foxNeighbours.Add(other.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (foxNeighbours.Contains(other.gameObject))
            {
                foxNeighbours.Remove(other.gameObject);
            }
        }
    }
}