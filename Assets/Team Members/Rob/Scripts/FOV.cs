using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rob
{
    public class FOV : MonoBehaviour
    {
        public float fov;
        public float maxDistance;

        [Tooltip("Must set layerMask to object")]
        // public LayerMask objectLayer;
        [HideInInspector]
        public bool canSeeTarget;


        // Start is called before the first frame update
        void Start()
        {
            //CheckDistance();
        }

        public void CanISee(Transform target)
        {
            Vector3 directionToEnemy = target.position - transform.position;
            float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
            //Debug.Log(angleToEnemy);
            if (angleToEnemy < fov / 2)
            {
                if (Physics.Linecast(transform.position, target.position, out RaycastHit hit))
                {
                    if (hit.transform == target)
                    {
                        Debug.DrawRay(transform.position, target.position - transform.position, Color.green, .5f);
                        canSeeTarget = true;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, target.position - transform.position, Color.red,
                            .5f);
                        canSeeTarget = false;
                    }
                }
            }
        }
    }
}