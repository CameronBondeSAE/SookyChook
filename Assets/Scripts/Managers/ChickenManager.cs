using System;
using System.Collections;
using System.Collections.Generic;
using Rob;
using Tanks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Aaron
{
    [Serializable]
    public class ChickenManager : ManagerBase<ChickenManager>
    {

        public Spawner spawner;
        
        public List<GameObject> chickensList;
        public List<GameObject> roostersList;
        public List<GameObject> fertilisedEggsList;

        public string[] UnlovedNames;
        public string[] LovedNames;
        public string[] RoosterNames;
        
        void Start()
        {

            ChickenManager chickenData = new ChickenManager();
            
            spawner = GetComponent<Spawner>();
            spawner.Spawn();
            
            chickensList = new List<GameObject>();
            chickensList = spawner.spawned;
            
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
                {"Il Jefe", "Henedict Cluckerbatch", "Cluck Norris", "Chickolas Cage", "The Colonel", "Henlord"};

            
            //TODO: Try and fix this up (Lachlan Stuff)
            // JSON SAVE AND LOAD
            string json = JsonUtility.ToJson(chickenData);
            
            
            chickenData = JsonUtility.FromJson<ChickenManager>(json);
            //string chickenNames = this.ToString();
            //JsonUtility.FromJson<ChickenManager>(chickenNames);

            Debug.Log(JsonUtility.ToJson(this));



        }
        
    }
}