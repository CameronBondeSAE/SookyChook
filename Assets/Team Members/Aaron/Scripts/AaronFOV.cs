using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks;
using Unity.VisualScripting;
using UnityEngine;

namespace Aaron
{
    public class AaronFOV : MonoBehaviour
    {
        public GameObject target;

        public bool canSeeTarget;

        private float fov = 45;
        public float distance = 10;
        
        void Update()
        {
            //CheckingForward();
            CheckFOV();
        }

        void CheckFOV()
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            
            float angleToEnemy = Vector3.Angle(transform.forward, directionToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            
            if (angleToEnemy < fov && distanceToTarget < distance)
            {
                if (Physics.Raycast(transform.position, directionToTarget, distance))
                {
                    canSeeTarget = true;
                    Debug.DrawRay(transform.position, directionToTarget, Color.cyan);
                }
            }
            else
            {
                canSeeTarget = false;
                Debug.DrawLine(transform.position, target.transform.position, Color.red);
            }
        }

        /*void CheckingForward()
        {
            Vector3 enemyLocalOffset = transform.InverseTransformPoint(target.transform.position);

            if (enemyLocalOffset.z > 0)
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.magenta);
            }
        }*/
    }
}