using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class LachlanWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    public Vector3 scaleChange;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Lachlan Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LachlanWindow window = (LachlanWindow)EditorWindow.GetWindow(typeof(LachlanWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Random Scale!!"))
        {
            foreach (Transform t in Selection.transforms)
            {
                scaleChange = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
                t.localScale = scaleChange;
                //(t.gameObject,t.localScale);
                
                
            }
        }

    }
}
