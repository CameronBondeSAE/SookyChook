using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSystems : MonoBehaviour
{
    public string playerName;
    public float mouseSens;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void SaveData()
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetFloat("mouseSens", mouseSens);


        // Read data
        // mouseSens = PlayerPrefs.GetFloat("mouseSens");
    }
}
