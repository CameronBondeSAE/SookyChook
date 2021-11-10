using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotationEditorWindow : EditorWindow
{
    //Variables
    List<float> pitches = new List<float>();
    List<float> yaws = new List<float>();
    List<int> rolls = new List<int>();
    int defaultRotation = 0;

    bool usePitch = true;
    bool useYaw = true;
    bool useRoll = true;

    int pitch;
    int yaw;
    int roll;

    int indexCounter = 0;


    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Random Rotation Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        RotationEditorWindow window = (RotationEditorWindow)EditorWindow.GetWindow(typeof(RotationEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        //Get Menu Data
        //PlayerPrefs.SetBool?

        GUILayout.Label("Options", EditorStyles.boldLabel);

        usePitch = EditorGUILayout.Toggle("Pitch", usePitch);
        if(usePitch)
        {
            foreach (Transform t in Selection.transforms)
            {
                pitches.Add(Random.Range(0, 361));
            }
        }
        else if(!usePitch)
        {
            foreach (Transform t in Selection.transforms)
            {
                pitches.Add(0);
            }
        }

        useYaw = EditorGUILayout.Toggle("Yaw", useYaw);
        if (useYaw)
        {
            foreach (Transform t in Selection.transforms)
            {
                yaws.Add(Random.Range(0, 361));
            }
        }
        else if(!useYaw)
        {
            foreach (Transform t in Selection.transforms)
            {
                yaws.Add(0);
            }

        }

        useRoll = EditorGUILayout.Toggle("Roll", useRoll);
        if (useRoll)
        {
            foreach (Transform t in Selection.transforms)
            {
                rolls.Add(Random.Range(0, 361));
            }
        }
        else if(!useRoll)
        {
            foreach (Transform t in Selection.transforms)
            {
                rolls.Add(0);
            }
        }

        //EditorGUILayout.EndToggleGroup();

        if(GUILayout.Button("Set Random Rotation"))
        {
            foreach (Transform t in Selection.transforms)
            {
                t.rotation = Quaternion.Euler(pitches[indexCounter], yaws[indexCounter], rolls[indexCounter]);
                indexCounter++;
            }
        }

        if (GUILayout.Button("Test"))
        {
            foreach (Transform t in Selection.transforms)
            {
                t.rotation = Quaternion.Euler(Random.Range(0, 361), Random.Range(0, 361), Random.Range(0, 361));
            }
        }

        if (GUILayout.Button("Reset Rotation"))
        {
            foreach (Transform t in Selection.transforms)
            {
                t.rotation = Quaternion.Euler(defaultRotation, defaultRotation, defaultRotation);
            }
        }
    }

}
