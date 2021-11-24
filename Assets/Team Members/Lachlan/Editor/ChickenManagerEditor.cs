using System;
using System.Collections;
using System.Collections.Generic;
using Aaron;
using UnityEditor;
using UnityEngine;

namespace Lachlan
{
    [Serializable]
    
    [CustomEditor(typeof(ChickenManager))]
    public class ChickenManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("JSON Format Data");
            
            if (GUILayout.Button("JSON TEXT"))
            {
                Debug.Log(JsonUtility.ToJson(FindObjectOfType<ChickenManager>()));
            }
            
            if (GUILayout.Button("Save Names"))
            {
                //string json = JsonUtility.ToJson(ChickenManager);
                string json = JsonUtility.ToJson(FindObjectOfType<ChickenManager>());
            }
        
            // TODO: Still need to work on fixing this code...
            if (GUILayout.Button("Load Names"))
            {
                //string chickenNames = this.chickensList.ToString();
                string chickenNames = FindObjectOfType<ChickenManager>().ToString();
                JsonUtility.FromJson<ChickenManager>(chickenNames);
            }

            GUILayout.EndHorizontal();
        }
    }
}