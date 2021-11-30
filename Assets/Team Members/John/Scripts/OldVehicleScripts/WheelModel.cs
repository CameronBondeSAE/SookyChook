using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelModel : MonoBehaviour
{
    [Header("Vehicle Attributes - Update all values per vehicle instance")]
    public Rigidbody rb;
    public float suspensionLength = 2f;
    public float maxHeight = 1f;
    float force = 0;
    public float maxForce = 500f;
    public float frictionAmount = 50f;

    [Header("Optional - Use Anim Curve")]
    public AnimationCurve suspensionCurve;
    float suspensionValue;
    public bool useAnimCurve;

    [Header("Reference Only")]
    public Vector3 localVelocity;
    public float xVelocity;


    // Update is called once per frame
    void Update()
    {

        //This vehicles velocity
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        xVelocity = localVelocity.x;

        //Add a force to the vehicles local x velocity (left & right) so vehicle can only travel forwards
        rb.AddRelativeForce(Vector3.right * xVelocity * -frictionAmount);


        // Container for useful info coming from casting functions (note ‘out’ below)
        RaycastHit hitinfo;
        hitinfo = new RaycastHit();

        Physics.Raycast(transform.position, -transform.up, out hitinfo, suspensionLength, 255, QueryTriggerInteraction.Ignore);

        // Debug: Only draw line if we hit something
        if (hitinfo.collider)
        {
            float height = hitinfo.distance;
            force = maxHeight - height;
            force *= maxForce;
            suspensionValue = suspensionCurve.Evaluate(force);

            if (useAnimCurve)
            {
                //Using animation curve as force
                rb.AddForceAtPosition(transform.up * suspensionValue, transform.position);
            }
            else
            {
                rb.AddForceAtPosition(transform.up * force, transform.position);
            }


            Debug.DrawLine(transform.position, hitinfo.point, Color.green);
        }

        //Draw red line if nothing is being hit
        if(hitinfo.collider == null)
        {
            Debug.DrawLine(transform.position, hitinfo.point, Color.red);
        }
    }
}
