using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceSelectionEditor : EditorWindow
{
    //Variables
    bool keepScale = true;
    bool keepRotation = true;
    bool groupEnabled = false;

    public Object prefab;
    GameObject newObject;
    GameObject oldObject;

    //List<GameObject> oldObjects = new List<GameObject>();
    //int counter = 0;


    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Replace Selection")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ReplaceSelectionEditor window = (ReplaceSelectionEditor)EditorWindow.GetWindow(typeof(ReplaceSelectionEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Prefab", EditorStyles.boldLabel);

        groupEnabled = EditorGUILayout.BeginToggleGroup("More Options", groupEnabled);
        keepRotation = EditorGUILayout.Toggle("Keep Rotation", keepRotation);
        keepScale = EditorGUILayout.Toggle("Keep Scale", keepScale);
        EditorGUILayout.EndToggleGroup();

        if (prefab != null)
        {
            prefab = EditorGUI.ObjectField(Rect.MinMaxRect(50, 0, 300, 16), ((GameObject)prefab).name, prefab, typeof(GameObject), true);
        }
        else
        {
            prefab = EditorGUI.ObjectField(Rect.MinMaxRect(50, 0, 300, 16), prefab, typeof(GameObject), true);
        }


        //Editor Buttons:


        if (GUILayout.Button("Replace Selected Objects"))
        {
            if (prefab == null)
            {
                Debug.Log("No Prefab");
                return;
            }

            foreach (GameObject obj in Selection.gameObjects)
            {
                //Store old object in a variable
                oldObject = obj;
                //oldObjects.Insert(counter, obj);
                //counter+=1;

                //Instaniate selected prefab
                newObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                //Set the new prefab to the old object's position/scale/rotation
                newObject.transform.position = obj.transform.position;

                //Only set scale & rotation if set to true
                if (keepScale)
                {
                    newObject.transform.localScale = obj.transform.localScale;
                }

                if(keepRotation)
                {
                    newObject.transform.rotation = obj.transform.rotation;
                }

                obj.SetActive(false);
            }
        }

        if(GUILayout.Button("Replace & Delete Selected Objects"))
        {
            if (prefab == null)
            {
                Debug.Log("No Prefab");
                    return;
            }

            foreach (GameObject obj in Selection.gameObjects)
            {
                newObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                newObject.transform.position = obj.transform.position;

                if (keepScale)
                {
                    newObject.transform.localScale = obj.transform.localScale;
                }

                if (keepRotation)
                {
                    newObject.transform.rotation = obj.transform.rotation;
                }

                DestroyImmediate(obj, true);
            }
        }

        if (GUILayout.Button("Undo"))
        {
            Debug.Log("Undo");
        }
    }
}
