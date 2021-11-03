using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Aaron
{
    public class ChickenManager : MonoBehaviour
    {
        public GameObject chicken;
        public GameObject rooster;
        public GameObject egg;
        
        public List<GameObject> chickensList;
        public List<GameObject> roostersList;
        public List<GameObject> eggsList;
        
        private string[] UnlovedNames;
        private string[] LovedNames;
        private string[] RoosterNames;
        
        public int spawnRangeXMin;
        public int spawnRangeXMax;
        public int spawnRangeZMin;
        public int spawnRangeZMax;
        public int spawnHeight;
        
        float spawnRangeX;
        float spawnRangeZ;

        // Start is called before the first frame update
        void Start()
        {
            chickensList = new List<GameObject>();
            roostersList = new List<GameObject>();
            eggsList = new List<GameObject>();
            
            UnlovedNames = new string[]
            {
                "Farm To Table", "Two Piece Feed", "Pluckhead", "Cock", "Yolks On You", "Pie", "Bucket",
                "Battery", "Nicolas In A Cage", "Schnitty", "Peri-Peri",  "Chick-Fil-E",
                "Atilla the Hen"
            };
            LovedNames = new string[]
            {
                "Bum Nuggets", "Chico Roll","Tyrannosaurus Pecks", "Dora The Eggsplorer", "Gwyneth Poultry", "Clucky Cheese",
                "Peep"
            };
            RoosterNames = new string[]
                {"Il Jefe", "Henedict Cluckerbatch", "Cluck Norris", "Chickolas Cage", "The Colonel"};
            
            
            //On Game Setup
            /*SpawnChickens();
            SpawnRoosters();
            SpawnEggs();*/
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SpawnChickens()
        {
            GameObject copy = chicken;
            spawnRangeX = Random.Range(spawnRangeXMin, spawnRangeXMax);
            spawnRangeZ = Random.Range(spawnRangeZMin, spawnRangeZMax);
            
            //Instantiate Chickens
            Instantiate(copy, new Vector3(spawnRangeX,spawnHeight,spawnRangeZ), copy.transform.rotation);
            
            //assign UnlovedName, show on nametag

            chickensList.Add(copy);
        }

        public void SpawnRoosters()
        {
            GameObject copy = rooster;
            spawnRangeX = Random.Range(spawnRangeXMin, spawnRangeXMax);
            spawnRangeZ = Random.Range(spawnRangeZMin, spawnRangeZMax);
            
            //Instantiate Roosters
            Instantiate(copy, new Vector3(spawnRangeX, spawnHeight, spawnRangeZ), copy.transform.rotation);
            
            //assign RoosterName, show on nametag
            
            roostersList.Add(copy);
        }

        public void SpawnEggs()
        {
            GameObject copy = egg;
            spawnRangeX = Random.Range(spawnRangeXMin, spawnRangeXMax);
            spawnRangeZ = Random.Range(spawnRangeZMin, spawnRangeZMax);
            
            //Instantiate Eggs
            Instantiate(copy, new Vector3(spawnRangeX, spawnHeight, spawnRangeZ), copy.transform.rotation);

            eggsList.Add(copy);
        }
        
        //Change Name if chicken becomes attached; change nametag object and name shown
        
        
    }
}