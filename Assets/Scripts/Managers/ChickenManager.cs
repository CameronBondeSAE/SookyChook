using System;
using System.Collections;
using System.Collections.Generic;
using Rob;
using Tanks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Aaron
{
    [Serializable]
    public class ChickenManager : ManagerBase<ChickenManager>
    {
        public Spawner spawner;
        
        public List<ChickenModel> chickensList;
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
            GlobalEvents.chickenSpawned += AddChicken;
            
            // spawner = FindObjectOfType<Spawner>();
            // spawner.SpawnMultiple();
            
            chickensList = new List<ChickenModel>();
            // chickensList = spawner.spawned;

            roostersList = new List<GameObject>();
            fertilisedEggsList = new List<GameObject>();

            names.UnlovedNames = new string[]
                                {
                                    "Farm To Table", "Two Piece Feed", "Pluckhead", "Cock", "Yolks On You", "Pie", "Bucket",
                                    "Battery", "Schnitty", "Peri-Peri",  "Chick-Fil-E",
                                    "Atilla the Hen", "Chico Roll"
                                };
            names.LovedNames = new string[]
            {
                "Bum Nuggets", "Tyrannosaurus Pecks", "Dora The Eggsplorer", "Gwyneth Poultry", "Clucky Cheese",
                "Peep", "Henny Penny"
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
            chickensList.Remove(chicken.GetComponent<ChickenModel>());
        }

        public void AddChicken(ChickenModel chicken)
        {
            chickensList.Add(chicken);
        }
        
        private void FixedUpdate()
        {
        }
    }
}