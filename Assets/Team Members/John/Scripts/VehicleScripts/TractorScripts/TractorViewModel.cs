using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorViewModel : MonoBehaviour
{
    public TractorModel tractorModel;
    public AudioSource audioSource;
    public AudioClip enterTractor;
    public AudioClip exitTractor;

    private void Start()
    {
        tractorModel.EnterTractorEvent += OnTractorEnter;
        tractorModel.ExitTractorEvent += OnTractorExit;
    }

    void OnTractorEnter()
    {
        audioSource.clip = enterTractor;
        audioSource.Play();
        //Debug.Log("Entered Tractor");
    }

    void OnTractorExit()
    {
        audioSource.clip = exitTractor;
        audioSource.Play();
        //Debug.Log("Exited Tractor");
    }
}
