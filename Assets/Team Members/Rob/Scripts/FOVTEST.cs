using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rob
{
    public class FOVTEST : MonoBehaviour
    {
        public float fov;
        public float maxDistance;
        public float minDistance;
        public GameObject prey;
        public LayerMask objectLayer;


        // Start is called before the first frame update
        void Start()
        {
            //CheckDistance();
            //preyLayer = LayerMask.GetMask("Prey");
        }

        public void Update()
        {
            Vector3 directionToEnemy = prey.transform.position - transform.position;
            float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
            //Debug.Log(angleToEnemy);
            if (angleToEnemy < fov / 2)
            {
                if (Physics.Raycast(transform.position, directionToEnemy,
                    out RaycastHit hit,
                    maxDistance))
                {
                    if (hit.transform == prey.transform)
                    {
                        Debug.DrawRay(transform.position, prey.transform.position - transform.position, Color.green,
                            .5f);
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, prey.transform.position - transform.position, Color.red, .5f);
                    }
                }


            }
        }
    }
    // else
    // {
    //     Debug.DrawLine(transform.position, prey.transform.position, Color.red, 0.5f);
    // }
}

