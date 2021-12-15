using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NameFloater_ViewModel))]

public class NameTagEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Bad Name"))
		{
			(target as NameFloater_ViewModel)?.BadNameButton();
		}

		if (GUILayout.Button("Good Name"))
		{
			(target as NameFloater_ViewModel)?.GoodNameButton();
		}
	}
}
