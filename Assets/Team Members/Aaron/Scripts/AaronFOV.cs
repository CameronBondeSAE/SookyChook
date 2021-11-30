using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

namespace Aaron
{
    public class AaronFOV : MonoBehaviour
    {
        public GameObject target;

        [FormerlySerializedAs("targets")]
        public List<GameObject> chickensInWorld = new List<GameObject>();

        public bool canSeeTarget;

        private float fov = 45;
        public float distance = 10;

        private void Start()
        {
            chickensInWorld = ChickenManager.Instance.chickensList;
        }

        void Update()
        {
            foreach (var chicken in chickensInWorld)
            {
                Vector3 directionToTarget = chicken.transform.position - transform.position;
            
                float angleToEnemy = Vector3.Angle(transform.forward, directionToTarget);
                float distanceToTarget = Vector3.Distance(transform.position, chicken.transform.position);
            
                if (angleToEnemy < fov && distanceToTarget < distance)
                {
                    canSeeTarget = true;
                    Debug.DrawLine(transform.position, chicken.transform.position, Color.green);

                }
                else
                {
                    canSeeTarget = false;
                    Debug.DrawLine(transform.position, chicken.transform.position, Color.red);
                }
                
                if (canSeeTarget)
                {
                    target = chicken;
                }
            }
        }
        
        
        
        
        
        

        /*void CheckFOV()
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            
            float angleToEnemy = Vector3.Angle(transform.forward, directionToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            
            if (angleToEnemy < fov && distanceToTarget < distance)
            {
                canSeeTarget = true;
            }
            else
            {
                canSeeTarget = false;
                Debug.DrawLine(transform.position, target.transform.position, Color.red);
            }
        }*/
    }
}