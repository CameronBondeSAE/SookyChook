using System;
using System.Collections;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenModel : AnimalBase, IInteractable, IPickupable
{
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
    public override void Start()
    {
        base.Start();
     
        rb = GetComponent<Rigidbody>();
        GetComponent<Health>().DeathEvent += Death;
    }

    // Update is called once per frame
    void Update()
    {
       //if eats food, increase scale size by growth until localScale == 1
    }
    
    #region Interface/Overrides implementation

    public override void ReachedMaxHungry()
    {
        base.ReachedMaxHungry();

        // Just die
        GetComponent<Health>().ChangeHealth(-100f);
    }

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
