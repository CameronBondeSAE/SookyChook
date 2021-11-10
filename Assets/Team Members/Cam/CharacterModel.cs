using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 5f;
    public Vector2 movementDirection;

    public Rigidbody rb;
    [SerializeField]
    private float turnSpeed = 15f;

    public Vector3 lookMovementDirection;

    public event Action JumpEvent;
    public event Action LandedEvent;
    public event Action<bool> OnGroundEvent;
    
    // Start is called before the first frame update
    public void Jump()
    {
        rb.AddForce(0,jumpHeight,0, ForceMode.VelocityChange);
        JumpEvent?.Invoke();
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
            // Don't insta look using transform.LookAt, instead Lerp slowlyish so it looks better
            // This calculates the WORLD SPACE rotation to look at a point FROM 0,0,0
            Quaternion toRotation = Quaternion.LookRotation(lookMovementDirection);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, turnSpeed * Time.deltaTime );
            
            // Old line, don't use, too crappy looking
            // transform.LookAt(transform.position + lookMovementDirection);
        }
    }

    public void Interact()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        LandedEvent?.Invoke();
    }

    void OnCollisionExit(Collision other)
    {
        OnGroundEvent?.Invoke(false);
    }

    void OnCollisionStay(Collision other)
    {
        OnGroundEvent?.Invoke(true);
    }
}
