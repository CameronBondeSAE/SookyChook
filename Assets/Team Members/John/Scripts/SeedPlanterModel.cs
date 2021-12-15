using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanterModel : MonoBehaviour, ITractorAttachment, IUpgradeable
{
    //public TractorModel tractorModel;
    public GameObject seed;
    public float planterSpeed = 3f;
    public Transform[] plantPositions;

    [Header("Seeds Variables")]
    public int seedsAvailable = 50;
    public int maxSeeds = 50;
    public Vector3 seedSpawnOffset = new Vector3(0, 1f, 0);


    [Header("Attachment offset when on vehicle")]
    [SerializeField]
    [Tooltip("Offset when attaching onto tractor/vehicle")]
    Vector3 attachOffset = new Vector3(0, 0, -3f);


    [Header("Info Only:")]
    public int planterLevel = 1;
    int seedAmountPerPlant = 1;
    [SerializeField]
    Vector3 raycastOffset = new Vector3(0, 0.5f, 0);
    public float tractorVelocity;
    public int maxlevel = 3;

    bool isAttached = false;
    bool tractorMoving;
    TractorModel tractor;

    //Events
    public event System.Action LevelUpEvent;
    public event System.Action MaxLevelEvent;
    public event System.Action<bool> IsAttachedEvent;

    //List of plant positions used for planting corresponding amount of seeds (upgrades per level)
    List<Transform> currentPlantPositions = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        //if (shop is { }) shop.PlanterUpgradedEvent += Upgrade;

        //Default (lvl 1) plant positions used for only planting 2 seeds
        currentPlantPositions.Add(plantPositions[2]);
        currentPlantPositions.Add(plantPositions[3]);

        seedsAvailable = maxSeeds;
    }

    IEnumerator PlantSeeds()
    {
        //Wait before planting first grass for attachment to stablise onto tractor
        yield return new WaitForSeconds(1f);

        do
        {
            //Shoot raycast down & store what we hit in hitinfo
            List<RaycastHit> hits = new List<RaycastHit>();
            RaycastHit hit = new RaycastHit();
            //hits = new RaycastHit();
                
            foreach(Transform plantPos in currentPlantPositions)
            {
                Debug.Log("Shooting Ray");
                Physics.Raycast(plantPos.position, -transform.up, out hit, 3, 255, QueryTriggerInteraction.Ignore);
                hits.Add(hit);

                //Minus a seed for each planterPos planting a seed
                seedsAvailable -= 1;
            }

            //Using height offset to make sure raycase isn't shooting under ground
            foreach(RaycastHit newHit in hits)
            {
                if(newHit.collider)
                {
                    Debug.Log("PLanting Seed");
                    GameObject newSeed = Instantiate(seed, newHit.point + seedSpawnOffset, Quaternion.identity);
                }

                //hits.Remove(newHit);
            }

            //hits.RemoveAll();
            hits.Clear();
            Debug.Log(seedsAvailable);

            //if we hit something, spawn grass at that hit position (should check if dirt?)
            //if (hits.collider)
            {
                //plant the desired amount of seeds per plant cycle
                //for(int i = 0; i < seedAmountPerPlant; i++)
                {
                }
            }

            //Debug.DrawLine(transform.position + offset, hitinfo.point, Color.green);

            //Only run this while tractor is moving - otherwise wait to continue planting 
            yield return tractorMoving;

            //Planting is based on tractor velocity - clamp this speed so planting doesn't plant 100 in 1 second
            Mathf.Clamp(tractorVelocity, 1, planterSpeed + 1.5f);
            yield return new WaitForSeconds(planterSpeed/tractorVelocity);
        }            
        while(isAttached && seedsAvailable > 0);
	}

    private void FixedUpdate()
    {
        if(tractor != null)
        {
            tractorVelocity = tractor.GetComponent<Rigidbody>().velocity.magnitude;

            if(tractorVelocity > 0.5)
            {
                tractorMoving = true;
            }
            else
            {
                tractorMoving = false;
            }
        }
    }

    public void Attach(TractorModel aTractorModel)
    {
        isAttached = true;
        tractor = aTractorModel;
        
        transform.parent = aTractorModel.transform;
        transform.localPosition = attachOffset;
        transform.rotation = aTractorModel.transform.rotation;
        
        
        StartCoroutine(PlantSeeds());
        IsAttachedEvent?.Invoke(true);
    }

    public void Detach()
    {
        isAttached = false;
        tractor = null;
        StopCoroutine(PlantSeeds());
        IsAttachedEvent?.Invoke(false);

        //Update pathfinding when no longer in use
        GlobalEvents.OnLevelStaticsUpdated(gameObject);
    }

    public void Upgrade()
    {
        //Upgrade to next level
        if(planterLevel < maxlevel)
        {
            planterLevel += 1;
            PlanterUpgrades(planterLevel);
        }

        if(planterLevel == maxlevel)
        {
            planterLevel = maxlevel;
            MaxLevelEvent?.Invoke();
        }
    }

    void PlanterUpgrades(int newLevel)
    {
        //Event for whatever EFX/SFX we want to happen on upgrading
        LevelUpEvent?.Invoke();

        if(newLevel == 2)
        {
            //seedAmountPerPlant = 2;
            currentPlantPositions.Add(plantPositions[1]);
            currentPlantPositions.Add(plantPositions[4]);
            return;
        }

        if(newLevel == maxlevel)
        {
            //seedAmountPerPlant = 4;
            currentPlantPositions.Add(plantPositions[0]);
            currentPlantPositions.Add(plantPositions[5]);
        }
    }

    //Old interface
    /*
    public Vector3 Offset()
    {
        return attachOffset;
    }
    */
}
