using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomTransforms : EditorWindow
{
    public Vector3 scaleChange;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Random Transforms")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        RandomTransforms window = (RandomTransforms)EditorWindow.GetWindow(typeof(RandomTransforms));
        window.Show();
    }

    void OnGUI()
    {

        if (GUILayout.Button("Random Scale!!"))
        {
            foreach (Transform t in Selection.transforms)
            {
                scaleChange = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
                t.localScale = scaleChange;
                //(t.gameObject,t.localScale);
                
                
            }
        }

        if (GUILayout.Button("JSON Format Text"))
        {
            //Debug.Log(JsonUtility.ToJson(myWindow));
        }

    }
}