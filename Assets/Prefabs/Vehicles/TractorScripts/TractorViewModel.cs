using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TractorViewModel : MonoBehaviour
{
    public TractorModel tractorModel;
    public AudioSource audioSource;
    public AudioClip enterTractor;
    public AudioClip runningTractor;
    public AudioClip exitTractor;

    [Header("Turning Wheel Graphics")]
    [SerializeField]
    Transform[] wheelGraphic;
    [SerializeField]
    float turnSpeed = 0.8f;

    private Coroutine coroutine;
    
    private void Start()
    {
        tractorModel.EnterTractorEvent += () => coroutine = StartCoroutine(OnTractorEnter());
        tractorModel.ExitTractorEvent += OnTractorExit;
    }

    private void FixedUpdate()
    {
        //float lerp = Mathf.Lerp(transform.position.y, tractorModel.steeringAngle / 2, 3f);

        //Rotate each turning wheel to simulate the vehicle turning
        foreach (Transform t in wheelGraphic)
        {
            // t.DOLocalRotateQuaternion(Quaternion.Euler(0, tractorModel.steeringAngle / 2, 0), turnSpeed);
            t.localRotation = Quaternion.Slerp( t.localRotation,  Quaternion.Euler(0, tractorModel.steeringAngle / 2, 0), turnSpeed);
            //t.DORotateQuaternion(Quaternion.Euler(0, tractorModel.steeringAngle/2, 0), 1f);
            //t.localRotation = Quaternion.Euler(0, tractorModel.steeringAngle / 2, 0);
        }
    }


    IEnumerator OnTractorEnter()
    {
        audioSource.clip = enterTractor;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(enterTractor.length);
        audioSource.loop = true;
        audioSource.clip = runningTractor;
        audioSource.Play();
    }

    void OnTractorExit()
    {
        // Stops the starting sounds sequence
        StopCoroutine(coroutine);

        audioSource.clip = exitTractor;
        audioSource.loop = false;
        audioSource.Play();
        //Debug.Log("Exited Tractor");
    }
}
