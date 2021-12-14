using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Edible : MonoBehaviour
{
	public float foodAmount = 0f;
	
	// Global list of edibles for AI to check
	public static List<Edible> edibles = new List<Edible>();

	public event Action<float> BeingEatenEvent;
	public event Action EatenEvent;
	
	/// <summary>
	/// Override this to do custom fx for being eaten! Or sub to the event
	/// </summary>
	/// <param name="amount">amount is 0-1f. 1 should insta-eat the whole thing</param>
	public virtual void BeingEaten(float amount)
	{
		BeingEatenEvent?.Invoke(amount);
		
		foodAmount -= amount;
		
		if (foodAmount <= 0)
		{
			Eaten();
		}
		
	}

	public virtual void Eaten()
	{
		EatenEvent?.Invoke();
		Destroy(gameObject);
	}

	void OnEnable()
	{
		edibles.Add(this);
	}

	void OnDisable()
	{
		edibles.Remove(this);
	}
}
