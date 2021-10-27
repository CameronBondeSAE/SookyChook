using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

namespace Aaron
{
    public class ChickenManager : MonoBehaviour
    {
        public GameObject Chicken;
        public GameObject Rooster;
        public GameObject Egg;
        
        public List<GameObject> chickensList;
        public List<GameObject> roostersList;
        public List<GameObject> eggsList;
        private string[] UnlovedNames;
        private string[] LovedNames;
        private string[] RoosterNames;
        
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
            
            SpawnChickens();
            SpawnRoosters();
            SpawnEggs();
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void SpawnChickens()
        {
            GameObject copy = Chicken;
            
            //Instantiate Chickens
            
            //assign UnlovedName, show on nametag
            
            chickensList.Add(copy);
        }

        void SpawnRoosters()
        {
            GameObject copy = Rooster;
            
            //Instantiate Roosters
            
            //assign RoosterName, show on nametag
            
            roostersList.Add(copy);
        }

        void SpawnEggs()
        {
            GameObject copy = Egg;
            
            //Instantiate Eggs

            eggsList.Add(copy);
        }
        
        //Change Name if chicken becomes attached; change nametag object and name shown
        
        
    }
}