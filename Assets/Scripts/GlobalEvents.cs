using System;
using UnityEngine;

public class GlobalEvents
{
	public static event Action<GameObject> levelStaticsUpdated;

	public static void OnLevelStaticsUpdated(GameObject callerGO)
	{
		levelStaticsUpdated?.Invoke(callerGO);
	}
}
