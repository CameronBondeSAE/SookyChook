using System;
using UnityEngine;

public class GlobalEvents
{
	public static event Action<GameObject> levelStaticsUpdated;
	public static event Action<GameObject> chickenDiedEvent;
	public static event Action<ChickenModel> chickenSpawned;



	public static void OnLevelStaticsUpdated(GameObject callerGO)
	{
		levelStaticsUpdated?.Invoke(callerGO);
	}

	public static void OnChickenDiedEvent(GameObject obj)
	{
		chickenDiedEvent?.Invoke(obj);
	}

	public static void OnChickenSpawned(ChickenModel obj)
	{
		chickenSpawned?.Invoke(obj);
	}
}
