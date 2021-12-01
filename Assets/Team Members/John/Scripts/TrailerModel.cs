using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerModel : MonoBehaviour, ITractorAttachment
{
    [Header("Position when placed onto tractor")]
    public Transform[] mounts;

    [Header("Attachment offset when on vehicle")]
    [SerializeField]
    [Tooltip("Offset when attaching onto tractor/vehicle")]
    Vector3 attachOffset = new Vector3(0, 0, -2f);

    [Header("Info Only: do not touch")]
    public List<GameObject> objectsInTrailer = new List<GameObject>();

    [Tooltip("Reference to the parent object of all wheels - used for turning off physics when not in use")]
    public GameObject wheels;

    private void Start()
    {
        TurnOffPhysics();
    }

    public void Attach()
    {
        wheels.SetActive(true);
    }

    public void Dettach()
    {
        TurnOffPhysics();
    }

    public Vector3 Offset()
    {
        return attachOffset;
    }

    void TurnOffPhysics()
    {
        wheels.SetActive(false);
    }
}
