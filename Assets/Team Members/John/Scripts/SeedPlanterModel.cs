using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanterModel : MonoBehaviour, ITractorAttachment, IUpgradeable
{
    //public TractorModel tractorModel;
    public GameObject seed;
    public SeedPlanter_ShopPanel shop;
    public float planterSpeed = 3f;

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

    bool isAttached = false;
    bool tractorMoving;
    int maxlevel = 3;
    TractorModel tractor;

    //Events
    public event System.Action LevelUpEvent;


    // Start is called before the first frame update
    void Start()
    {
        if (shop is { }) shop.PlanterUpgradedEvent += Upgrade;
        //tractorModel.TractorAttachableEvent += OnAttached;
    }

    /*
    void OnAttached(bool plant)
    {
        StartCoroutine(PlantSeeds(plant));
    }
    */

    IEnumerator PlantSeeds()
    {
        //Wait before planting first grass for attachment to stablise onto tractor
        yield return new WaitForSeconds(1f);

        do
        {
            //Shoot raycast down & store what we hit in hitinfo
            RaycastHit hitinfo;
            hitinfo = new RaycastHit();

            //Using height offset to make sure raycase isn't shooting under ground
            Physics.Raycast(transform.position + raycastOffset, -transform.up, out hitinfo, 3, 255, QueryTriggerInteraction.Ignore);

            //if we hit something, spawn grass at that hit position (should check if dirt?)
            if (hitinfo.collider)
            {
                //plant the desired amount of seeds per plant cycle
                for(int i = 0; i < seedAmountPerPlant; i++)
                {
                    GameObject newSeed = Instantiate(seed, hitinfo.point, Quaternion.identity);
                }
            }

            //Debug.DrawLine(transform.position + offset, hitinfo.point, Color.green);

            //Only run this while tractor is moving - otherwise wait to continue planting 
            yield return tractorMoving;

            //Planting is based on tractor velocity - clamp this speed so planting doesn't plant 100 in 1 second
            Mathf.Clamp(tractorVelocity, 1, planterSpeed + 1.5f);
            yield return new WaitForSeconds(planterSpeed/tractorVelocity);
        }            

        while(isAttached);
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
    }

    public void Detach()
    {
        isAttached = false;
        tractor = null;
        StopCoroutine(PlantSeeds());

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
            //Max Level Event ?
        }
    }

    void PlanterUpgrades(int newLevel)
    {
        //Event for whatever EFX/SFX we want to happen on upgrading
        LevelUpEvent?.Invoke();

        if(newLevel == 2)
        {
            seedAmountPerPlant = 2;
            return;
        }

        if(newLevel == maxlevel)
        {
            seedAmountPerPlant = 4;
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
