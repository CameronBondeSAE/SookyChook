using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseManager : MonoBehaviour
{
    GameObject myRef;

    private void Start()
    {
        myRef = gameObject;
    }
    //Noise Event
    public delegate void NoiseSignature (GameObject gameObject);
    public static event NoiseSignature NoiseEvent;

    public void Noise()
    {
        NoiseEvent?.Invoke(myRef);
    }
}
