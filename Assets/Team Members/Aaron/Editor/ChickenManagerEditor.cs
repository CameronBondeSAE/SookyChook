using System.Collections;
using System.Collections.Generic;
using Aaron;
using Codice.CM.SEIDInfo;
using UnityEditor;
using UnityEngine;
using XDiffGui;

namespace Aaron
{
    [CustomEditor(typeof(ChickenManager))]
        public class ChickenManagerEditor : Editor
        {
            /*public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Spawn Chicken"))
                {
                    ((ChickenManager)target).SpawnChickens();
                }

                if (GUILayout.Button("Spawn Rooster"))
                {
                    ((ChickenManager)target).SpawnRoosters();
                }
            }*/

            
        }
}