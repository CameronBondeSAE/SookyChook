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
    public class WildLifeManager : ManagerBase<WildLifeManager>
    {
        [System.Serializable]
        public class WildLife
        {
            public GameObject[] animals;
            public Transform[] spawnPoints;
            public int animalCount;
            public DayNightManager.DayPhase phaseTime;
        }

        
        public WildLife[] wildLife;
        public float groundOffset;
        private WildLife currentWildLife;

        public bool randomSpawnRotation;

        // add as many as you need for the animals needed to spawn
        

        public List<GameObject> animalsSpawned;

        private void Start()
        {
            FindObjectOfType<DayNightManager>().PhaseChangeEvent += ChangePhase;
        }

        public void ChangePhase(DayNightManager.DayPhase timeOfDay)
        {
            for (int i = 0; i < wildLife.Length; i++) //searches through all of wildLife aray
            {
                if (wildLife[i].phaseTime == timeOfDay) //if the wildlife dayPhase inside the array matches current day phase
                {
                    currentWildLife = wildLife[i];

                    for (int j = 0; j < currentWildLife.animalCount; j++)
                    {
                        Transform randomTransform = currentWildLife.spawnPoints[Random.Range(0, currentWildLife.spawnPoints.Length)];
                        Vector3 spawnPos = randomTransform.position;
                        spawnPos = new Vector3(spawnPos.x, spawnPos.y + 5, spawnPos.z);
                        Vector3 randomSpot = Random.insideUnitCircle * 5;
                        randomSpot.z = randomSpot.y; //hack, im sure there is an easier way to do this
                        Debug.Log(randomSpot);
                        GameObject randomWildlife =
                            currentWildLife.animals[Random.Range(0, currentWildLife.animals.Length)];
                        GameObject spawnedWildlife;
                        if (randomSpawnRotation)
                        {
                            spawnedWildlife = Instantiate(randomWildlife, spawnPos + randomSpot,
                                Quaternion.Euler(0,Random.Range(0,360),0));
                        }
                        else
                        {
                            spawnedWildlife = Instantiate(randomWildlife, spawnPos + randomSpot,
                                randomTransform.rotation);
                        }

                        if (Physics.Raycast(spawnedWildlife.transform.position, Vector3.down, out RaycastHit hit, 20))
                        {
                            Vector3 newSpawnPos = spawnedWildlife.transform.position;
                            Debug.DrawRay(newSpawnPos, Vector3.down * hit.distance, Color.blue);
                            Debug.Log(hit.distance);
                            newSpawnPos = new Vector3(newSpawnPos.x,
                                newSpawnPos.y - (hit.distance - groundOffset),
                                newSpawnPos.z);
                            spawnedWildlife.transform.position = newSpawnPos;
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
            if (currentWildLife != null)
            {
                foreach (Transform spawnPoint in currentWildLife.spawnPoints)
                {
#if UNITY_EDITOR || UNITY_EDITOR_64
                    Handles.color = Color.green;
                    Handles.DrawSolidDisc(spawnPoint.position, Vector3.up, 5);
#endif
                }
            }
        }
    }
}