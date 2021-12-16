using System;
using System.Collections;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenModel : AnimalBase, IInteractable, IPickupable, ISellable
{
	public bool foundFood;
	public bool atFood;

	//public float growth;

	public bool isFull;
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

		// REVIEW: Does it need to be global?
		GlobalEvents.OnChickenSpawned(this);
		
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

		if (GetComponent<Health>().isAlive)
		{
			// Just die
			GetComponent<Health>().ChangeHealth(-100f);
		}
	}

	public void Interact()
	{
		if (GetComponent<Health>().isAlive)
		{
			InteractEvent?.Invoke();
			Death(gameObject);
		}
	}

	public void PickUp()
	{
		GetComponent<Rigidbody>().isKinematic = true;
		foreach (Collider child in GetComponentsInChildren<Collider>())
		{
			child.enabled = false;
		}
		enabled = false;
		PickUpEvent?.Invoke(true);
	}

	public void PutDown()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		//GetComponentsInChildren<Collider>().enabled = true;
		foreach (Collider child in GetComponentsInChildren<Collider>())
		{
			child.enabled = true;
		}
		PickUpEvent?.Invoke(false);
	}
	
	public ProductType GetProductType()
	{
		return ProductType.Chicken;
	}

	public void Death(GameObject aGameObject)
	{
		if (GetComponent<Health>().isAlive)
		{
			GetComponent<Health>().ForceDie();
			GetComponent<Health>().DeathEvent -= Death;
			GlobalEvents.OnChickenDiedEvent(gameObject);
		
			// This takes time, so I don't want to die twice!
			StartCoroutine(DeathSequence());
			// Destroy(gameObject);
		}
	}

	public IEnumerator DeathSequence()
	{
		// Disable all chicken things. NOTE: You could just spawn a death chicken prefab

		rb.constraints = RigidbodyConstraints.None;
		rb.angularDrag = 0;
		rb.drag = 0;
		rb.angularVelocity = new Vector3(Random.Range(-deathFling, deathFling), Random.Range(-deathFling, deathFling),
			Random.Range(-deathFling, deathFling));
		rb.velocity = Vector3.up * deathFling / 4f;
		foreach (Collider child in GetComponentsInChildren<Collider>())
		{
			child.material = new PhysicMaterial();
		}

		Aaron.Wander wander = GetComponent<Aaron.Wander>();
		wander.enabled = false;
		GetComponent<Tom.TurnToward>().enabled = false;
		GetComponent<ChickenModel>().enabled = false;

		yield return new WaitForSeconds(2f);
		
		rb.angularDrag = 0;
		rb.drag = 1;
	}

	#endregion
}