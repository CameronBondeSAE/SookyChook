using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarModel : MonoBehaviour
{
    [SerializeField]
    float maxHearDistance;

    //React to noise (Local Event)
    public delegate void NoiseReactionSignature();
    public event NoiseReactionSignature NoiseReactEvent;

    // Start is called before the first frame update
    void Awake()
    {
        NoiseManager.NoiseEvent += HearNoise;
    }

    void HearNoise(GameObject objectRef)
    {
        float noiseDistance = Vector3.Distance(transform.position, objectRef.transform.position);
        //If Noise Within Hear Radius
        if(noiseDistance < maxHearDistance)
        {
            NoiseReactEvent?.Invoke();
        }


        //Testing Event
        Debug.Log(name + " heard " + objectRef.name);
    }
}
