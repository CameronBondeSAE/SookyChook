using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Coroutines : SerializedMonoBehaviour
{
	[SerializeField]
	public List<Coroutine> mainCoroutines = new List<Coroutine>();
 
	[Button]
	void AddAnother()
	{
		// WARNING if you add more than one here, it won't remember them. If you really want to add more than one, keep a List<Coroutine>
		mainCoroutines.Add(StartCoroutine(TestCoroutine()));
	}

	public Coroutine coroutine;
	
	[Button]
	void ResetAndAddOne()
	{
		// Just for a single coroutine. No good for lots
		if (coroutine != null) StopCoroutine(coroutine);

		List<Coroutine> removeCoroutines = new List<Coroutine>();

		// You can keep your own list of coroutines, if you need to reset them all in one go
		foreach (Coroutine co in mainCoroutines)
		{
			StopCoroutine(co);
			removeCoroutines.Add(co);
		}

		foreach (Coroutine removeCoroutine in removeCoroutines)
		{
			mainCoroutines.Remove(removeCoroutine);
		}
	}

	private IEnumerator TestCoroutine()
	{
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(2f);
			Debug.Log($"Tick {i}");
		}
	}
}