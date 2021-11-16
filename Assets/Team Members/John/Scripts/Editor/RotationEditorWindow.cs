using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class RotationVariables
{
    //Data we want saved
    public bool usePitch = true;
    public bool useYaw = true;
    public bool useRoll = true;
}

[System.Serializable]
public class RotationEditorWindow : EditorWindow
{
    //Variables
    float pitch;
    float yaw;
    float roll;
    float defaultRotation = 0f;
    RotationVariables rotationVariables = new RotationVariables();
    string variableData;

    private void Awake()
    {
        string loadData = PlayerPrefs.GetString("variableData");
        JsonUtility.FromJsonOverwrite(loadData, rotationVariables);
    }

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

        rotationVariables.usePitch = EditorGUILayout.Toggle("Pitch", rotationVariables.usePitch);
        rotationVariables.useYaw = EditorGUILayout.Toggle("Yaw", rotationVariables.useYaw);
        rotationVariables.useRoll = EditorGUILayout.Toggle("Roll", rotationVariables.useRoll);

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
                if(rotationVariables.usePitch)
                {
                    pitch = Random.Range(0, 360);
                }

                if(rotationVariables.useYaw)
                {
                    yaw = Random.Range(0, 360);
                }

                if(rotationVariables.useRoll)
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

    private void OnDestroy()
    {
        PlayerPrefs.SetString("variableData", JsonUtility.ToJson(rotationVariables));
    }

}
