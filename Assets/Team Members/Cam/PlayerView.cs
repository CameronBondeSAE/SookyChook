using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerModel playerModel;
    public Animator animator;
    public ParticleSystem particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("LookDirectionActive", playerModel.lookMovementDirection.magnitude);
        animator.SetFloat("Velocity", playerModel.rb.velocity.magnitude);
        particleSystem.Emit((int)playerModel.rb.velocity.magnitude);

    }
}
