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
        
        private float fov = 45;
        
        void Update()
        {
            //CheckingForward();
            CheckFOV();
        }

        void CheckFOV()
        {
            Vector3 directionToEnemy = target.transform.position - transform.position;
            float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
            
            if (angleToEnemy < fov)
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.green);
                //checking for obstruction
                if (Physics.Raycast(transform.position, target.transform.position))
                {
                    Debug.DrawLine(transform.position, target.transform.position, Color.cyan);
                }
            }
            else
            {
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