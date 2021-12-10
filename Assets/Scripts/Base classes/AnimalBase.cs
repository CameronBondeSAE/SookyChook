using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBase : MonoBehaviour
{
	public float hungerLevel;
	public float hungerThreshold = 0.5f;
	public bool  isHungry;
	public float hungerIncreaseInterval = 1f;

	public virtual void Start()
	{
		InvokeRepeating("ReduceHungerTime", hungerIncreaseInterval, hungerIncreaseInterval);
	}

	// private IEnumerator ReduceHungerTime()
	private void ReduceHungerTime()
	{
		ChangeHunger(0.02f);
		// yield return new WaitForSeconds(1);
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
		if (hungerLevel >= 1f)
		{
			ReachedMaxHungry();
		}
	}

	public virtual void ReachedMaxHungry()
	{
	}
}