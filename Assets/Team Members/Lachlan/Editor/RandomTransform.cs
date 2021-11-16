using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomTransform : EditorWindow
{
    public Vector3 scaleChange;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Random Transform")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        RandomTransform window = (RandomTransform)EditorWindow.GetWindow(typeof(RandomTransform));
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
            Debug.Log(JsonUtility.ToJson(this));
        }

    }
}