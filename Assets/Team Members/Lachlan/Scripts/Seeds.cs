using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Netcode;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Object = UnityEngine.Object;

public class Seeds : MonoBehaviour, IWaterable
{
    [Header("Seed Properties")] public GameObject target;
    public GameObject grassSpawn;

    public bool Crying = false;

    public AudioSource audioSource;
    public AudioClip grassClip;

    // Start is called before the first frame update

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
    [Button]
    public void BeingWatered(float amount)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            //Destroy Seeds and Grow Grass
            GameObject.Destroy(gameObject);
            GameObject newGrass = GameObject.Instantiate(grassSpawn, transform.localPosition, Quaternion.identity);
            newGrass.GetComponent<NetworkObject>().Spawn();

            //PlayGrassEffectsClientRpc(newGrass);
        }
    }

    [ClientRpc]
    void PlayGrassEffectsClientRpc(GameObject newGrass)
    {
        newGrass.transform.DOShakeRotation(0.5f);
    }
}
