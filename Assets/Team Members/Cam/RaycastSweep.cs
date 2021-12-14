using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSweep : MonoBehaviour
{
	public float distance = 10;
	public int numberOfRays = 10;
	[SerializeField]
	public float spread = 10f;

	public Vector3 offset = new Vector3(0,0.1f,0);
	
	public List<RaycastHit> hits;

	public bool debug = true;

	public void Rescan()
	{
		RaycastHit hitInfo = new RaycastHit();

		hits.Clear();
		
		for (int i = -numberOfRays/2; i < numberOfRays/2; i++)
		{
			Vector3 sweepDirection = Quaternion.AngleAxis(i*spread, transform.up) * transform.forward;

			Ray ray = new Ray(transform.position + offset, sweepDirection);
			if (Physics.Raycast(ray, out hitInfo, distance))
			{
				hits.Add(hitInfo);
				if (debug)
				{
					Debug.DrawLine(transform.position, hitInfo.point, Color.green);
				}
			}

		}
	}

}
