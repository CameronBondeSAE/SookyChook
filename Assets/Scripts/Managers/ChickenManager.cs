using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Aaron
{
    public class ChickenManager : ManagerBase<ChickenManager>
    {
        public GameObject chicken;
        public GameObject rooster;
        public GameObject egg;
        
        public List<GameObject> chickensList;
        public List<GameObject> roostersList;
        public List<GameObject> fertilisedEggsList;

        private string[] UnlovedNames;
        private string[] LovedNames;
        private string[] RoosterNames;

        float spawnRangeX;
        float spawnRangeZ;

        // Start is called before the first frame update
        void Start()
        {
            chickensList = new List<GameObject>();
            roostersList = new List<GameObject>();
            fertilisedEggsList = new List<GameObject>();

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
        }
        
    }
}