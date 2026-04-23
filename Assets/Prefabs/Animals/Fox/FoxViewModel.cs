using System;
using System.Collections;
using System.Collections.Generic;
using Aaron;
using UnityEngine;

public class FoxViewModel : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip eatingSound;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EatChickenState.EatChickenEvent += EatChicken;
    }

    void EatChicken()
    {
        audioSource.clip = eatingSound;
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
}
