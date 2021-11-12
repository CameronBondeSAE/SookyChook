using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotationEditorWindow : EditorWindow
{
    //Variables
    //List<int> pitches = new List<int>();
    //List<int> yaws = new List<int>();
    //List<int> rolls = new List<int>();
    float defaultRotation = 0f;

    bool usePitch = true;
    bool useYaw = true;
    bool useRoll = true;

    //bool groupEnabled = false;
    //bool keepRotation = false;

    float pitch;
    float yaw;
    float roll;


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
        useYaw = EditorGUILayout.Toggle("Yaw", useYaw);
        useRoll = EditorGUILayout.Toggle("Roll", useRoll);

        //TESTING
        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //keepRotation = EditorGUILayout.Toggle("Keep Rotation", keepRotation);
        //EditorGUILayout.EndToggleGroup();

        //Editor Buttons:

        if (GUILayout.Button("Set Random Rotation"))
        {
            //For each selected object
            foreach (Transform t in Selection.transforms)
            {
                //Set Pitch - Yaw - Roll to always equal 0 (default rotation)
                pitch = defaultRotation;
                yaw = defaultRotation;
                roll = defaultRotation;

                //Only if Pitch - Yaw - Roll are true, do we change their value to a random value
                if(usePitch)
                {
                    pitch = Random.Range(0, 360);
                }

                if(useYaw)
                {
                    yaw = Random.Range(0, 360);
                }

                if(useRoll)
                {
                    roll = Random.Range(0, 360);
                }

                //Set objects transform to a random rotation using Pitch - Yaw - Roll values
                t.rotation = Quaternion.Euler(pitch, yaw, roll);
                Debug.Log(t.name + "'s new rotation is: " + pitch + " " + yaw + " " + roll);
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
