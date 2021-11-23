using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_ViewModel : MonoBehaviour
{
    public FenceModel fenceModel;
    public AudioSource audioSource;
    public ParticleSystem particleSystem;
    public AudioClip pickUpFence;
    public AudioClip placeFence;

    private void Start()
    {
        fenceModel.PickedUpEvent += FencePickedUp;
    }

    void FencePickedUp(bool pickUp)
    {
        if(pickUp)
        {
            audioSource.clip = pickUpFence;
            audioSource.Play();
        }
        else
        {
            //Hack to allow particles to play once the fence has been placed on the ground
            StartCoroutine(PlaceFence());
        }
    }

    IEnumerator PlaceFence()
    {
        audioSource.clip = placeFence;
        audioSource.Play();

        yield return new WaitForSeconds(0.15f);

        particleSystem.Emit(10);
    }
}
