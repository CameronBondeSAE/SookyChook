using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tom
{
    [CustomEditor(typeof(DayNightManager))]
    public class DayNightEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Set Phase");
            
            if (GUILayout.Button("Morning"))
            {
                ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Morning);
            }
        
            if (GUILayout.Button("Noon"))
            {
                ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Noon);
            }
        
            if (GUILayout.Button("Evening"))
            {
                ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Evening);
            }
        
            if (GUILayout.Button("Night"))
            {
                ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Night);
            }
        
            if (GUILayout.Button("Midnight"))
            {
                ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Midnight);
            }
            
            GUILayout.EndHorizontal();
        }
    }
}