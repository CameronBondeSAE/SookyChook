using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTurn : MonoBehaviour
{
    public Rigidbody rb;

    public float turn;
    public float rayDistance;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, rayDistance))
        {
            rb.AddRelativeTorque(new Vector3(0, turn, 0), ForceMode.Force);
        }

        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.yellow);
    }
}
