using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerModel : MonoBehaviour, ITractorAttachment
{
    [Header("Object position when placed onto tractor")]
    public Transform[] mounts;

    [Header("Attachment offset when on vehicle")]
    [SerializeField]
    [Tooltip("Offset when attaching onto tractor/vehicle")]
    Vector3 attachOffset = new Vector3(0, 0, -2f);

    [Header("Info Only: do not touch")]
    public List<GameObject> objectsInTrailer = new List<GameObject>();

    [Tooltip("Reference to the parent object of all wheels - used for turning off physics when not in use")]
    public GameObject wheels;

    public ConfigurableJoint configurableJoint;

    public void Attach(TractorModel aTractorModel)
    {
        transform.parent = aTractorModel.attachmentMount.transform;
        transform.localPosition = attachOffset;
        transform.rotation = aTractorModel.transform.rotation;

        configurableJoint.connectedBody = aTractorModel.attachmentMount.GetComponent<Rigidbody>();
        //hingeJoint.autoConfigureConnectedAnchor = false;
        //hingeJoint.connectedAnchor = transform.parent.position;

        // wheels.SetActive(true);
    }

    public void Detach()
    {
        configurableJoint.connectedBody = null;
    }

    private void OnTriggerExit(Collider other)
    {
        //Remove objects from list when they fall out of trailer
        if(objectsInTrailer.Contains(other.gameObject))
        {
            objectsInTrailer.Remove(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //HACK: Hardcoded to chickens only
        if(other.GetComponent<ChickenModel>() != null)
        {
            //if this chicken is not already in the list add it to the list
            if(!objectsInTrailer.Contains(other.gameObject))
            {
                other.gameObject.transform.position = mounts[0].transform.position;
                objectsInTrailer.Add(other.gameObject);
            }
        }
    }

    //Old Interface
    /*
    public Vector3 Offset()
    {
        return attachOffset;
    }
    */
}
