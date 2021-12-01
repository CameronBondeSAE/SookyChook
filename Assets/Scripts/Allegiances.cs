using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allegiances : MonoBehaviour
{
	// Check: This break modularity a bit
	public enum Types
	{
		Fox,
		TassieDevil,
		Rooster,
		Player,
		Chicken,
		Vehicle,
		LightCone
	}
	
	// What am I
	public Types whatAmI;

	[Serializable]
	public class Entry
	{
		public Types type;
		[Range(-1f,1f)]
		[Tooltip("0 = Neutral, -1 = Full hate, 1 = Full like. Each animal does what it wants though with the number")]
		public float amount;
	}
	
	public List<Entry> allegiance = new List<Entry>();
}
