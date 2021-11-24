using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TractorViewModel : MonoBehaviour
{
    public TractorModel tractorModel;
    public AudioSource audioSource;
    public AudioClip enterTractor;
    public AudioClip exitTractor;

    [Header("Turning Wheel Graphics")]
    [SerializeField]
    Transform[] wheelGraphic;
    [SerializeField]
    float turnSpeed = 0.8f;

    private void Start()
    {
        tractorModel.EnterTractorEvent += OnTractorEnter;
        tractorModel.ExitTractorEvent += OnTractorExit;
    }

    private void FixedUpdate()
    {
        //float lerp = Mathf.Lerp(transform.position.y, tractorModel.steeringAngle / 2, 3f);

        //Rotate each turning wheel to simulate the vehicle turning
        foreach (Transform t in wheelGraphic)
        {
            t.DOLocalRotateQuaternion(Quaternion.Euler(0, tractorModel.steeringAngle / 2, 0), turnSpeed);
            //t.DORotateQuaternion(Quaternion.Euler(0, tractorModel.steeringAngle/2, 0), 1f);
            //t.localRotation = Quaternion.Euler(0, tractorModel.steeringAngle / 2, 0);
        }
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
