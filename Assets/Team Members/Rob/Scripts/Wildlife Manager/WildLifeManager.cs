using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        
        public WildLife[] animalsToSpawn;
        public Transform[] spawnPoints;
        public List<GameObject> animalsSpawned;






    }
}
