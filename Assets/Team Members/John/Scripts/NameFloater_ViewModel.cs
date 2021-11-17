using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameFloater_ViewModel : MonoBehaviour
{
    public TextMeshPro name;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Name());
    }

    void BadName()
    {
        name.text = "Bad Name";
        name.color = new Color(255, 0, 0);
    }

    void GoodName()
    {
        name.text = "Good Name";
        name.color = new Color(0, 255, 0);
    }

    IEnumerator Name()
    {
        BadName();

        yield return new WaitForSeconds(3f);

        GoodName();
    }
}
