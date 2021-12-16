using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aaron
{
    public class FollowPath : MonoBehaviour
    {
        public Pathfinding pathfinding;
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            pathfinding = GetComponent<Pathfinding>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void SetPath()
        {
            
        }
    }
}