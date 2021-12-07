using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Edible : MonoBehaviour
{
	// Global list of edibles for AI to check
	public static List<Edible> edibles = new List<Edible>();

	void OnEnable()
	{
		edibles.Add(this);
	}

	void OnDisable()
	{
		edibles.Remove(this);
	}
}
