using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tom;

namespace Rob
{
    public class WildLifeManager : MonoBehaviour
    {
        [System.Serializable]
        public class WildLife
        {
            public GameObject[] wildlife;
            public int count;
            public float timeBetweenSpawns;
            public bool maintainSpawnedNumber;
        }

        public WildLife[] animalsToSpawn;
        public Transform[] spawnPoints;
        public List<GameObject> animalsSpawned;

         

        

        private void Awake()
        {
            
        }

        private void Start()
        {
            FindObjectOfType<DayNightManager>().PhaseChangeEvent += ChangePhase;
        }

        private void ChangePhase(DayNightManager.DayPhase timeOfDay)
        {
             if (timeOfDay == DayNightManager.DayPhase.Morning)
             {
                 //insert spawn logic here?
                 foreach (WildLife animal in animalsToSpawn)
                 {
                     
                 }
                 Debug.Log(" its " + timeOfDay);
             }
            
             if (timeOfDay == DayNightManager.DayPhase.Noon)
             {
                 //insert spawn logic here?
                 Debug.Log(" its " + timeOfDay);
             }
            
             if (timeOfDay == DayNightManager.DayPhase.Evening)
             {
                 //insert spawn logic here?
                 Debug.Log(" its " + timeOfDay);
             }
            
             if (timeOfDay == DayNightManager.DayPhase.Night)
             {
                 //insert spawn logic here?
                 Debug.Log(" its " + timeOfDay);
             }
        }
    }
}