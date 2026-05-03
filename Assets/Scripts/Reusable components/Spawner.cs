using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

#if UNITY_EDITOR || UNITY_EDITOR_64
using UnityEditor;
#endif

using Random = UnityEngine.Random;

namespace Rob
{
	public class Spawner : NetworkBehaviour
	{
		[System.Serializable]
		public class GroupInfo
		{
			public GameObject[] prefabs;

			[Tooltip("Use SpawnPoint prefab. If there's no spawnpoints, it'll use the transform position")]
			public Transform[] spawnPoints;

			public int countPerGroup;
			// public DayNightManager.DayPhase phaseTime;
		}

		public bool autoStart = false;
		
		public float radius = 5f;

		public  GroupInfo[] groupInfos;
		public  float       groundOffset;
		private GroupInfo   _currentGroupInfo;

		// add as many as you need for the animals needed to spawn


		public List<GameObject> spawned;

		public override void OnNetworkSpawn()
		{
			base.OnNetworkSpawn();
			
			// Only spawn if we're the server and autoStart is enabled
			if (IsServer && autoStart)
			{
				SpawnMultiple();
			}
		}

		// private void Start()
		// {
		// 	if (autoStart) SpawnMultiple();
		// 	
		// 	// FindObjectOfType<DayNightManager>().PhaseChangeEvent += ChangePhase;
		// }

		// public void ChangePhase(DayNightManager.DayPhase timeOfDay)
		public List<GameObject> SpawnMultiple()
		{
			// Only the server should spawn objects
			if (!IsServer)
			{
				Debug.LogWarning("SpawnMultiple called on client - only server can spawn objects");
				return spawned;
			}

			for (int i = 0; i < groupInfos.Length; i++) //searches through all of wildLife aray
			{
				// if (spawnInfos[i].phaseTime == timeOfDay) //if the wildlife dayPhase inside the array matches current day phase
				{
					_currentGroupInfo = groupInfos[i];

					Transform randomTransform;
					for (int j = 0; j < _currentGroupInfo.countPerGroup; j++)
					{
						if (_currentGroupInfo.spawnPoints.Length <= 0)
							// Use my own GO
							randomTransform = transform;
						else
							randomTransform = _currentGroupInfo.spawnPoints[Random.Range(0, _currentGroupInfo.spawnPoints.Length)];

						Vector3 spawnPos = randomTransform.position;
						spawnPos = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
						Vector3 randomSpot = Random.insideUnitCircle * radius;
						randomSpot.z = randomSpot.y; //hack, im sure there is an easier way to do this
						randomSpot.y = 0;
						// Debug.Log(randomSpot);
						GameObject randomPrefab =
							_currentGroupInfo.prefabs[Random.Range(0, _currentGroupInfo.prefabs.Length)];
						GameObject spawnedPrefab = SpawnSingle(randomPrefab, spawnPos + randomSpot, randomTransform.rotation);

						spawned.Add(spawnedPrefab);
					}
				}
			}

			return spawned;
		}

		public GameObject SpawnSingle(GameObject prefab, Vector3 pos, Quaternion rotation)
		{
			// Check if the prefab has a NetworkObject component
			NetworkObject networkObject = prefab.GetComponent<NetworkObject>();
			
			Vector3 randomSpot;
			GameObject spawnedPrefab = Instantiate(prefab, pos,
				rotation);

			// Start much higher
			Vector3 startRay = spawnedPrefab.transform.position + Vector3.up * 100f;
			if (Physics.Raycast(startRay, Vector3.down, out RaycastHit hit))
			{
				Vector3 newSpawnPos = spawnedPrefab.transform.position;
				Debug.DrawRay(startRay, Vector3.down * hit.distance, Color.blue);
				// Debug.Log(hit.distance);

				// Add offset from detected ground point
				newSpawnPos = hit.point + Vector3.up * groundOffset;
				// newSpawnPos = new Vector3(newSpawnPos.x,
					// newSpawnPos.y - (hit.distance - groundOffset),
					// newSpawnPos.z);
				spawnedPrefab.transform.position = newSpawnPos;
			}

			// If this is a networked object and we're the server/host, spawn it on the network
			if (networkObject != null && IsServer)
			{
				NetworkObject spawnedNetworkObject = spawnedPrefab.GetComponent<NetworkObject>();
				spawnedNetworkObject.Spawn();
			}

			return spawnedPrefab;
		}

		private void OnDrawGizmos()
		{
			if (_currentGroupInfo != null)
			{
				foreach (Transform spawnPoint in _currentGroupInfo.spawnPoints)
				{
#if UNITY_EDITOR || UNITY_EDITOR_64

					Handles.color = Color.green;
					Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
					Handles.DrawWireDisc(spawnPoint.position, Vector3.up, radius);
#endif
				}
			}
		}
	}
}