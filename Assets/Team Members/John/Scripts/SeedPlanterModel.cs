using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanterModel : MonoBehaviour
{
    public TractorModel tractorModel;

    public GameObject seed;
    Vector3 offset = new Vector3(0, 0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        tractorModel.TractorAttachableEvent += OnAttachable;
    }

    void OnAttachable(bool plant)
    {
        StartCoroutine(PlantSeeds(plant));
    }
    IEnumerator PlantSeeds(bool planting)
    {
        //Wait before planting first grass for attachment to stablise onto tractor
        yield return new WaitForSeconds(1f);

        do
        {
            Debug.Log("Test");
            //Shoot raycast down & store what we hit in hitinfo
            RaycastHit hitinfo;
            hitinfo = new RaycastHit();

            //Using height offset to make sure raycase isn't shooting under ground
            Physics.Raycast(transform.position + offset, -transform.up, out hitinfo, 3, 255, QueryTriggerInteraction.Ignore);

            //if we hit something, spawn grass at that hit position (should check if dirt?)
            if (hitinfo.collider)
            {
                GameObject newSeed = Instantiate(seed, hitinfo.point, Quaternion.identity);
            }

            Debug.DrawLine(transform.position + offset, hitinfo.point, Color.green);
            yield return new WaitForSeconds(1f);
        }
        while (planting == true);
	}
}
