using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DayNightManager))]
public class DayNightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Change to Morning"))
        {
            ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Morning);
        }
        
        if (GUILayout.Button("Change to Noon"))
        {
            ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Noon);
        }
        
        if (GUILayout.Button("Change to Evening"))
        {
            ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Evening);
        }
        
        if (GUILayout.Button("Change to Night"))
        {
            ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Night);
        }
        
        if (GUILayout.Button("Change to Midnight"))
        {
            ((DayNightManager)target).ChangePhase(DayNightManager.DayPhase.Midnight);
        }
    }
}
