using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public Rigidbody rb;
    public float torque;
    public float checkDist;
    public float checkDistLeft;
    public float checkDistRight;


    private void Start()
    {
    }

    void Update()
    {
        Ray rayForward = new Ray(this.transform.position, this.transform.forward);
        Ray rayLeft = new Ray(this.transform.position, transform.forward - transform.right);
        Ray rayRight = new Ray(this.transform.position, transform.forward + transform.right);

        RaycastHit hit;

        if (Physics.Raycast(rayForward, out hit, checkDist))
        {
            rb.AddRelativeTorque(Vector3.up * torque);
        }

        if (Physics.Raycast(rayRight, out hit, checkDistRight))
        {
            rb.AddRelativeTorque(Vector3.up * torque);
        }

        if (Physics.Raycast(rayLeft, out hit, checkDistLeft))
        {
            rb.AddRelativeTorque(Vector3.up * torque);
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * checkDist, Color.black);
        Debug.DrawRay(transform.position, (transform.forward + transform.right) * checkDistRight, Color.red);
        Debug.DrawRay(transform.position, (transform.forward - transform.right) * checkDistLeft, Color.red);
    }
}