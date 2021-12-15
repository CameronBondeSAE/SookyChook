using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Vision : MonoBehaviour
    {
        public float canSeeAngle = 45f;

        public LayerMask obstacleLayer;

        public bool CanSeeTarget(Transform target)
        {
            // Calculates angle and distance
            float angleToTarget = Vector3.Angle(transform.forward, target.position);
                
            // Can see target if withing vision angle and distance
            if (angleToTarget < canSeeAngle)
            {
                if (!Physics.Raycast(transform.position, target.position,
                    Vector3.Distance(transform.position, target.position), obstacleLayer))
                {
                    return true;
                }
            }

            return false;
        }
    }
}