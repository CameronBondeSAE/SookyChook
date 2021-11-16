using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ReplaceSelectionVariables
{
    //Variables
    public bool keepScale = true;
    public bool keepRotation = true;
    public bool groupEnabled = false;
}
public class ReplaceSelectionEditor : EditorWindow
{
    ReplaceSelectionVariables selectionVariables = new ReplaceSelectionVariables();
    string variableData;

    public Object prefab;
    GameObject newObject;
    GameObject oldObject;

    List<GameObject> oldObjects = new List<GameObject>();
    List<GameObject> newObjects = new List<GameObject>();

    private void Awake()
    {
        string load = PlayerPrefs.GetString("variableData");
        JsonUtility.FromJsonOverwrite(load, selectionVariables);
    }

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

        selectionVariables.groupEnabled = EditorGUILayout.BeginToggleGroup("More Options", selectionVariables.groupEnabled);
        selectionVariables.keepRotation = EditorGUILayout.Toggle("Keep Rotation", selectionVariables.keepRotation);
        selectionVariables.keepScale = EditorGUILayout.Toggle("Keep Scale", selectionVariables.keepScale);
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

        //Replace Selected Objects Without Destroying Them (Only Turning GO's off) - this allows you to undo
        GUILayout.Label("Replace Selection By Turning Off GO's (Can be Undone)", EditorStyles.boldLabel);
        if (GUILayout.Button("Replace Selected Objects"))
        {
            if (prefab == null)
            {
                Debug.Log("No Prefab");
                return;
            }

            foreach (GameObject obj in Selection.gameObjects)
            {
                //Store all old objects in a list
                oldObjects.Add(obj);

                //Instaniate selected prefab
                newObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                //Store each new object instance in a list
                newObjects.Add(newObject);

                //Set the new prefab to the old object's position/scale/rotation
                newObject.transform.position = obj.transform.position;

                //Only set scale & rotation if set to true
                if (selectionVariables.keepScale)
                {
                    newObject.transform.localScale = obj.transform.localScale;
                }

                if(selectionVariables.keepRotation)
                {
                    newObject.transform.rotation = obj.transform.rotation;
                }

                obj.SetActive(false);
            }
        }

        //Revert GO's Back to Old GO's (Only Works if GO's havent been destroyed)
        if (GUILayout.Button("Undo"))
        {
            foreach (GameObject obj in oldObjects)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in newObjects)
            {
                Destroy(obj);
            }
        }

        //Destroy & Replace All GO's With New GO's
        GUILayout.Label("Cannot Be Undone!", EditorStyles.boldLabel);
        if (GUILayout.Button("Replace & Delete Selected Objects"))
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

                if (selectionVariables.keepScale)
                {
                    newObject.transform.localScale = obj.transform.localScale;
                }

                if (selectionVariables.keepRotation)
                {
                    newObject.transform.rotation = obj.transform.rotation;
                }

                DestroyImmediate(obj, true);
            }
        }
    }

    private void OnDestroy()
    {
        //JsonUtility.ToJson(selectionVariables);
        PlayerPrefs.SetString("variableData", JsonUtility.ToJson(selectionVariables));
    }
}
