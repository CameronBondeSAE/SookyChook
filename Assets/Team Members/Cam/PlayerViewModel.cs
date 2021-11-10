using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewmodel : MonoBehaviour
{
    public CharacterModel characterModel;
    public Animator       animator;
    public ParticleSystem particleSystem;
    public AudioSource    audioSource;
    public AudioClip      jumpClip;
    
    // Start is called before the first frame update
    void Start()
    {
        characterModel.JumpEvent += OnCharacterModelOnJumpEvent;
        characterModel.OnGroundEvent += CharacterModelOnOnGroundEvent;
        characterModel.LandedEvent += CharacterModelOnLandedEvent;
    }

    void CharacterModelOnLandedEvent()
    {
        audioSource.clip = jumpClip;
        audioSource.Play();
        particleSystem.Emit(100);
    }

    void CharacterModelOnOnGroundEvent(bool onGround)
    {
        animator.SetBool("OnGround", onGround);
    }

    void OnCharacterModelOnJumpEvent()
    {
        animator.SetTrigger("Jump");
        //animator.SetBool("OnGround", false); // CHECK
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("LookDirectionActive", characterModel.lookMovementDirection.magnitude);
        animator.SetFloat("Velocity",            characterModel.rb.velocity.magnitude);
        
        particleSystem.Emit((int)(characterModel.rb.velocity.magnitude * Time.deltaTime));
    }
}
