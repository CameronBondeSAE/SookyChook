using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public GameObject item;
    public float fov;
    public float maxDistance;
    public float minDistance;
    public LayerMask obstacle;
    private int obs;
    

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
                
                if (hit.collider.gameObject.layer == LayerMask.GetMask("obstacle"))
                {
                    Debug.DrawRay(transform.position, item.transform.position - transform.position, Color.red, .5f);
                }
        
                if (hit.collider.gameObject.layer != 3)
                {
                    Debug.DrawRay(transform.position, item.transform.position - transform.position, Color.green,
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