using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rob
{
    public class FOV : MonoBehaviour
    {
        public float fov;
        public float maxDistance;
        //public GameObject prey;


        // Start is called before the first frame update
        void Start()
        {
            //CheckDistance();
        }

        public bool CanISee(Transform target)
        {
            //target = prey.transform;
            Vector3 directionToEnemy = target.position - transform.position;
            float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
            //Debug.Log(angleToEnemy);
            if (angleToEnemy < fov / 2)
            {
                if (Physics.Raycast(transform.position, directionToEnemy, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.transform == target)
                    {
                        Debug.DrawRay(transform.position, target.position - transform.position, Color.green, .5f);
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, target.position - transform.position, Color.red,
                            .5f);
                        return false;
                    }
                }
            }

            return false;
        }
    }
}