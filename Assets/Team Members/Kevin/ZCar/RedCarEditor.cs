using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(RedCar))]
public class RedCarEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Clicked!"))
        {
            RedCar redCar = target as RedCar; 
            Debug.Log("You clicked me!!!");
            
            redCar.transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }
    
    
}
