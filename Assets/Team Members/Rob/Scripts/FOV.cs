using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public GameObject item;
    public float fov;
    public float maxDistance;
    public float minDistance;
    public GameObject prey;


    // Start is called before the first frame update
    void Start()
    {
        //CheckDistance();
    }

    private void Update()
    {
        Vector3 directionToEnemy = item.transform.position - transform.position;
        float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
        //Debug.Log(angleToEnemy);
        if (angleToEnemy < fov / 2)
        {
            if (Physics.Raycast(transform.position, item.transform.position - transform.position, out RaycastHit hit,
                maxDistance))
            {
                Debug.Log(hit.collider.gameObject.layer);
                if (hit.collider.gameObject == prey)
                {
                    Debug.DrawRay(transform.position, item.transform.position - transform.position, Color.green, .5f);
                }
                else
                {
                    Debug.DrawRay(transform.position, item.transform.position - transform.position, Color.red,
                        .5f);
                }
            }
        }
        else
        {
            Debug.DrawLine(transform.position, item.transform.position, Color.red, 0.5f);
        }
    }
}