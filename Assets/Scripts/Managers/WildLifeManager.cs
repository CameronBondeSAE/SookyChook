using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using Tom;
using UnityEditor;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Rob
{
    public class WildLifeManager : MonoBehaviour
    {
        [System.Serializable]
        public class WildLife
        {
            public GameObject[] animals;
            public int animalCount;
            public bool maintainNumberSpawned;
            public int timeBetweenSpawns;
            public DayNightManager.DayPhase phaseTime;
        }


        public WildLife[] wildLife;
        public bool clearList;
        public float groundOffset;
        private WildLife currentWildLife;

        // add as many as you need for the animals needed to spawn
        public Transform[] spawnPoints;

        public List<GameObject> animalsSpawned;

        private void Awake()
        {
        }

        private void Start()
        {
            FindObjectOfType<DayNightManager>().PhaseChangeEvent += ChangePhase;
        }

        public void ChangePhase(DayNightManager.DayPhase timeOfDay)
        {
            for (int i = 0; i < wildLife.Length; i++)
            {
                if (wildLife[i].phaseTime == timeOfDay)
                {
                    currentWildLife = wildLife[i];

                    for (int j = 0; j < wildLife[i].animalCount; j++)
                    {
                        Transform randomTransform = spawnPoints[Random.Range(0, spawnPoints.Length)];
                        Vector3 spawnPos = randomTransform.position;
                        spawnPos = new Vector3(spawnPos.x, spawnPos.y + 5, spawnPos.z);
                        Vector3 randomSpot = Random.insideUnitCircle * 5;
                        randomSpot.z = randomSpot.y; //hack, im sure there is an easier way to do this
                        Debug.Log(randomSpot);
                        GameObject randomWildlife =
                            currentWildLife.animals[Random.Range(0, currentWildLife.animals.Length)];
                        GameObject spawnedWildlife = Instantiate(randomWildlife, spawnPos + randomSpot,
                            randomTransform.rotation);
                        RaycastHit hit;
                        
                        if (Physics.Raycast(spawnedWildlife.transform.position, Vector3.down, out hit, 20))
                        {
                            Debug.DrawRay(spawnedWildlife.transform.position, Vector3.down * hit.distance, Color.blue);
                            Debug.Log(hit.distance);
                            spawnedWildlife.transform.position = new Vector3(spawnedWildlife.transform.position.x,
                                spawnedWildlife.transform.position.y - (hit.distance - groundOffset) ,
                                spawnedWildlife.transform.position.z);
                        }
                        animalsSpawned.Add(spawnedWildlife);
                    }
                }
            }
        }


        private void Update()
        {
            if (InputSystem.GetDevice<Keyboard>().aKey.wasPressedThisFrame)
            {
                animalsSpawned.Clear();
            }
        }

        private void OnDrawGizmos()
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                // Gizmos.color = Color.green;
                // Gizmos.DrawSphere(spawnPoint.position, 5);
#if UNITY_EDITOR || UNITY_EDITOR_64
                Handles.color = Color.green;
                Handles.DrawSolidDisc(spawnPoint.position, Vector3.up, 5);
#endif
            }
        }
    }
}