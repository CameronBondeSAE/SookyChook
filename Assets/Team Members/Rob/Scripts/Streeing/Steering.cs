using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public Rigidbody rb;
    public float multiplyer;
    public float torque;
    public float checkDist;
    public float checkDistLeft;
    public float checkDistRight;
    


    private void Start()
    {
    }

    void Update()
    {
        Ray rayForward = new Ray(transform.position, transform.forward);
        Ray rayLeft = new Ray(transform.position, transform.forward - transform.right);
        Ray rayRight = new Ray(transform.position, transform.forward + transform.right);

        RaycastHit hit;

        if (Physics.Raycast(rayForward, out hit, checkDist))
        {
            float distanceTo = (checkDist - hit.distance) * multiplyer;
            rb.AddRelativeTorque(Vector3.up * torque * checkDist * multiplyer, ForceMode.VelocityChange);
        }

        if (Physics.Raycast(rayRight, out hit, checkDistRight))
        {
            float distanceTo = (checkDistRight - hit.distance) * multiplyer;
            rb.AddRelativeTorque(Vector3.up * torque * (checkDistRight) * multiplyer, ForceMode.VelocityChange);
        }

        if (Physics.Raycast(rayLeft, out hit, checkDistLeft))
        {
            float distanceTo = (checkDistLeft - hit.distance) * multiplyer;
            rb.AddRelativeTorque(Vector3.up * torque * (checkDistLeft) * multiplyer, ForceMode.VelocityChange);
            
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * checkDist, Color.black);
        Debug.DrawRay(transform.position, (transform.forward + transform.right) * checkDistRight, Color.red);
        Debug.DrawRay(transform.position, (transform.forward - transform.right) * checkDistLeft, Color.red);
    }
}