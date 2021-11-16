using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class MyWindow : EditorWindow
{
	public string myString = "Hello World";
	public bool   groupEnabled;
	public bool   myBool  = true;
	public float  myFloat = 1.23f;
	public double  myFloatDouble = 1.23;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Tools/Cams Window")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
		myBool       = EditorGUILayout.Toggle("Toggle", myBool);
		myFloat      = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup();

		if (GUILayout.Button("KILL!"))
		{
			foreach (Transform t in Selection.transforms)
			{
				DestroyImmediate(t.gameObject);
			}
		}
		
		if(GUILayout.Button("SAVE TEST"))
		{
			Debug.Log(JsonUtility.ToJson(this, true));
		}
	}
}