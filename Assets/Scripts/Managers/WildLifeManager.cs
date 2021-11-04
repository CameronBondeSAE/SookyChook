using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using Tom;
using Random = UnityEngine.Random;

namespace Rob
{
    public class WildLifeManager : MonoBehaviour
    {
        [System.Serializable]
        public class WildLife
        {
            public GameObject[] animals;
            public int count;
            public bool maintainNumberSpawned;
            public int timeBetweenSpawns;
            public DayNightManager.DayPhase phaseTime;
        }


        public WildLife[] wildLife;

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

                    for (int j = 0; j < wildLife[i].count; j++)
                    {
                        Transform randomTransform = spawnPoints[Random.Range(0, spawnPoints.Length)];
                        GameObject randomWildlife =
                            currentWildLife.animals[Random.Range(0, currentWildLife.animals.Length)];
                        GameObject spawnedWildlife = Instantiate(randomWildlife, randomTransform.position,
                            randomTransform.rotation);
                        animalsSpawned.Add(spawnedWildlife);
                    }
                }
            }
        }
    }
}