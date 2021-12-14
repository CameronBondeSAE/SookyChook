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

        public event Action ChickenDeathEvent;

        [Serializable]
        public class Names
        {
            public string[] UnlovedNames;
            public string[] LovedNames;
            public string[] RoosterNames;
        }

        public Names names = new Names();

        void Start()
        {

            //spawner = GetComponent<Spawner>();
            // spawner.SpawnMultiple();
            
            chickensList = new List<GameObject>();
            chickensList = spawner.spawned;
            

            
            roostersList = new List<GameObject>();
            fertilisedEggsList = new List<GameObject>();

            names.UnlovedNames = new string[]
                                {
                                    "Farm To Table", "Two Piece Feed", "Pluckhead", "Cock", "Yolks On You", "Pie", "Bucket",
                                    "Battery", "Nicolas In A Cage", "Schnitty", "Peri-Peri",  "Chick-Fil-E",
                                    "Atilla the Hen"
                                };
            names.LovedNames = new string[]
            {
                "Bum Nuggets", "Chico Roll","Tyrannosaurus Pecks", "Dora The Eggsplorer", "Gwyneth Poultry", "Clucky Cheese",
                "Peep"
            };
            names.RoosterNames = new string[]
                                 {"Il Jefe", "Henedict Cluckerbatch", "Cluck Norris", "Chickolas Cage", "The Colonel", "Henlord"};

            
            //TODO: Try and fix this up (Lachlan Stuff)
            // JSON SAVE AND LOAD
            ChickenManager chickenManager = this;
            string json = JsonUtility.ToJson(this);
            chickenManager = JsonUtility.FromJson<ChickenManager>(json);
            //string chickenNames = this.ToString();
            //JsonUtility.FromJson<ChickenManager>(chickenNames);

            Debug.Log(JsonUtility.ToJson(this));
        }

        private void OnEnable()
        {
            foreach (var chicken in chickensList)
            {
                chicken.GetComponent<Health>().DeathEvent += RemoveChicken;
            }
        }

        public void RemoveChicken(GameObject chicken)
        {
            ChickenDeathEvent?.Invoke();
            chickensList.Remove(chicken);
        }
        
        private void FixedUpdate()
        {
        }
    }
}