using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 5f;
    public Vector2 movementDirection;

    public Rigidbody rb;
    [SerializeField]
    private float turnSpeed = 15f;

    public Vector3 lookMovementDirection;

    // Start is called before the first frame update
    public void Jump()
    {
        rb.AddForce(0,jumpHeight,0, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movementDirectionFinal = new Vector3(movementDirection.x, 0, movementDirection.y);
        rb.AddForce(movementDirectionFinal * speed, ForceMode.Acceleration);
        lookMovementDirection = movementDirectionFinal;
        lookMovementDirection.y = 0; // Don't look up and down
        
        // Don't go crazy if you're not moving at all
        if (lookMovementDirection.magnitude > 0.01f)
        {
            Vector3 relativePos = transform.position + lookMovementDirection - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, turnSpeed * Time.deltaTime );
            
            // transform.LookAt(transform.position + lookMovementDirection);
        }
    }

    public void Interact()
    {
        
    }
}
