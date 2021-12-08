using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Object = UnityEngine.Object;

public class Seeds : MonoBehaviour, IWaterable
{
    [Header("Seed Properties")]
    public Object seedObject;
    public GameObject target;
    public GameObject grassSpawn;

    public bool Crying = false;

    public AudioSource audioSource;
    public AudioClip grassClip;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<CharacterModel>().CryingEvent += Growth;
        //FindObjectOfType<CharacterModel>().CryingEvent -= Growth;
    }
    void Growth(bool isCrying)
    {
        if (isCrying)
        {
            Crying = true;
            
        }
        else
        {
            Crying = false;
        }
    }


    //Interfaces

    public void BeingWatered(float amount)
    {
        //Audio Source is Quiet
        audioSource.clip = grassClip;
        audioSource.Play();
            
        //Destroy Seeds and Grow Grass
        GameObject.Destroy(seedObject);
        GameObject.Instantiate(grassSpawn, transform.localPosition, Quaternion.identity);
    }
}
