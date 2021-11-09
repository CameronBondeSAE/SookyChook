using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotationEditorWindow : EditorWindow
{
    //Variables
    int pitch;
    int yaw;
    int roll;
    int defaultRotation = 0;

    bool usePitch = true;
    bool useYaw = true;
    bool useRoll = true;


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
        GUILayout.Label("Options", EditorStyles.boldLabel);

        usePitch = EditorGUILayout.Toggle("Pitch", usePitch);
        if(usePitch)
        {
            pitch = Random.Range(0, 361);
        }
        else
        {
            pitch = defaultRotation;
        }

        useYaw = EditorGUILayout.Toggle("Yaw", useYaw);
        if (useYaw)
        {
            yaw = Random.Range(0, 361);
        }
        else
        {
            yaw = defaultRotation;
        }

        useRoll = EditorGUILayout.Toggle("Roll", useRoll);
        if (useRoll)
        {
            roll = Random.Range(0, 361);
        }
        else
        {
            roll = defaultRotation;
        }

        //EditorGUILayout.EndToggleGroup();

        if(GUILayout.Button("Set Random Rotation"))
        {
            foreach (Transform t in Selection.transforms)
            {
                t.rotation = Quaternion.Euler(pitch, yaw, roll);
            }
        }
    }

}
