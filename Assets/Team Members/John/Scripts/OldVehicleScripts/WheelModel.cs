using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelModel : MonoBehaviour
{
    [Header("Vehicle Attributes - Update all values per vehicle instance")]
    public Rigidbody rb;
    [Tooltip("Distance the Raycast will shoot")]
    public float suspensionLength = 2f;
    [Tooltip("Height of vehicle before forces are applied")]
    public float maxHeight = 1f;
    public float maxForce = 500f;
    public float frictionAmount = 50f;

    [Header("Optional Settings - Use Anim Curve")]
    public AnimationCurve suspensionCurve;
    float suspensionValue;
    public bool useAnimCurve;

    [Header("Reference Only")]
    public Vector3 localVelocity;
    public float xVelocity;
    [SerializeField]
    float force = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Container for useful info coming from casting functions (note ‘out’ below)
        RaycastHit hitinfo;
        hitinfo = new RaycastHit();

        Physics.Raycast(transform.position, -transform.up, out hitinfo, suspensionLength, 255, QueryTriggerInteraction.Ignore);

        //Only run this code if we hit a collider
        if (hitinfo.collider)
        {
            //Wheel Force Logic
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

            //Applying lateral forces - Get vehicles velocity
            localVelocity = transform.InverseTransformDirection(rb.velocity);
            xVelocity = localVelocity.x;

            //Add a force to the vehicles local x velocity (left & right) so vehicle can only travel forwards
            rb.AddRelativeForce(Vector3.right * xVelocity * -frictionAmount);


            //Draw line when we hit a collider for debugging
            Debug.DrawLine(transform.position, hitinfo.point, Color.green);
        }

        //Draw red line if nothing is being hit
        if(hitinfo.collider == null)
        {
            Debug.DrawLine(transform.position, hitinfo.point, Color.red);
        }
    }
}
