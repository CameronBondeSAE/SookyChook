using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanterModel : MonoBehaviour, ITractorAttachment
{
    //public TractorModel tractorModel;

    public GameObject seed;
    Vector3 offset = new Vector3(0, 0.5f, 0);

    [SerializeField]
    int seedAmount = 3;

    // Start is called before the first frame update
    void Start()
    {
        //tractorModel.TractorAttachableEvent += OnAttached;
    }

    /*
    void OnAttached(bool plant)
    {
        StartCoroutine(PlantSeeds(plant));
    }
    */

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
                for(int i = 0; i < seedAmount; i++)
                {
                    GameObject newSeed = Instantiate(seed, hitinfo.point, Quaternion.identity);
                }
            }

            Debug.DrawLine(transform.position + offset, hitinfo.point, Color.green);
            yield return new WaitForSeconds(1f);
        }
        while (planting == true);
	}

    public void Attach()
    {
        StartCoroutine(PlantSeeds(true));
    }

    public void Dettach()
    {
        StartCoroutine(PlantSeeds(false));

        //Update pathfinding when no longer in use
        GlobalEvents.OnLevelStaticsUpdated(gameObject);
    }
}
