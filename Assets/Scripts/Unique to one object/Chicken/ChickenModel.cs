using System;
using System.Collections;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenModel : MonoBehaviour, IInteractable, IPickupable
{
    public float hungerLevel;
    public float hungerThreshold = 0.5f;
    public bool  isHungry;
    public bool foundFood;
    public bool atFood;
    
    //public float growth;

    public bool   isFull;
    public Edible targetEdible;

    public event Action InteractEvent;
    public event Action<bool> PickUpEvent;

    Rigidbody rb;
    [SerializeField]
    private float deathFling = 10f;

    // Start is called before the first frame update
    void Start()
    {
     
        rb = GetComponent<Rigidbody>();
        GetComponent<Health>().DeathEvent += Death;
        StartCoroutine("ReduceHungerTime");
    }

    // Update is called once per frame
    void Update()
    {
       //if eats food, increase scale size by growth until localScale == 1
    }
    
    private IEnumerator ReduceHungerTime()
    {
        while (GetComponent<Health>().GetHealth()>0)
        {
            ChangeHunger(0.02f);
            yield return new WaitForSeconds(1);
        }
    }
    
    public void ChangeHunger(float amount)
    {
        hungerLevel += amount;

        hungerLevel = Mathf.Clamp01(hungerLevel);

        if (hungerLevel > hungerThreshold)
        {
            isHungry = true;
        }
        else
        {
            isHungry = false;
        }

        // DIIIIIEEEE
        if (hungerLevel>=1f)
        {
            GetComponent<Health>().ChangeHealth(-100f);
        }
    }

    #region Interface/Overrides implementation
    
    public void Interact()
    {
        InteractEvent?.Invoke();
        Death(gameObject);
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled      = false;
        PickUpEvent?.Invoke(true);
    }

    public void PutDown()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled      = true;
        PickUpEvent?.Invoke(false);
    }

    public void Death(GameObject aGameObject)
    {
        // This takes time, so I don't want to die twice!
        GetComponent<Health>().DeathEvent -= Death;
        StartCoroutine(DeathSequence());
        // Destroy(gameObject);
    }

    public IEnumerator DeathSequence()
    {
        // Disable all chicken things. NOTE: You could just spawn a death chicken prefab
        
        rb.constraints = RigidbodyConstraints.None;
        rb.angularDrag = 0;
        rb.drag = 0;
        rb.angularVelocity = new Vector3( Random.Range(-deathFling, deathFling), Random.Range(-deathFling, deathFling),
            Random.Range(-deathFling, deathFling));
        rb.velocity = Vector3.up * deathFling/4f;
        GetComponent<Collider>().material = new PhysicMaterial();
        
        Aaron.Wander wander = GetComponent<Aaron.Wander>();
        wander.enabled = false;
        GetComponent<Tom.TurnToward>().enabled = false;
        GetComponent<ChickenModel>().enabled = false;
        
        yield return new WaitForSeconds(2f);
        // GetComponent<Edible>().
    }
    
    #endregion
}
