using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingWithEars : MonoBehaviour
{
    EarModel earModel;

    private void Awake()
    {
        earModel = GetComponent<EarModel>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        earModel.NoiseReactEvent += myReaction;
    }

    void myReaction()
    {
        Debug.Log(name + " Run Away");
    }
}
