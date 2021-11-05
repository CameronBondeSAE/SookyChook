using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(CamTest))]
public class HandlesTestEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        var t   = target as CamTest;
        var tr  = t.transform;
        var pos = tr.position;
        // display an orange disc where the object is
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;
        Handles.DrawWireDisc(pos, tr.up, 1.0f);
        // display object "value" in scene
        GUI.color = color;
        Handles.Label(pos, t.name);
    }
}