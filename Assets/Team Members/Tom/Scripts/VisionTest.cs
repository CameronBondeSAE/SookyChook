using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTest : MonoBehaviour
{
    [Serializable]
    public class Target
    {
        public Transform target;
        public float angleToTarget;
        public float distanceToTarget;
        public bool canSeeTarget;
    }
    
    public List<Target> targets = new List<Target>();
    public float canSeeAngle = 45f;
    public float canSeeDistance = 10f;
    
    public void Update()
    {
        if (targets != null)
        {
            foreach (Target t in targets)
            {
                // Calculates angle and distance
                t.angleToTarget = Vector3.Angle(transform.forward, t.target.position);
                t.distanceToTarget = Vector3.Distance(transform.position, t.target.position);
                
                // Can see target if withing vision angle and distance
                if (t.angleToTarget < canSeeAngle && t.distanceToTarget < canSeeDistance)
                {
                    t.canSeeTarget = true;
                }
                else
                {
                    t.canSeeTarget = false;
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (targets != null)
        {
            foreach (Target t in targets)
            {
                if (t.canSeeTarget)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
            
                Gizmos.DrawLine(transform.position, t.target.position);
            }
        }
    }
}
