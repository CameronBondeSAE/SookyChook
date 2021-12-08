using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public int totalMoney = 5000;
    public TMP_Text moneyUI;


    // Start is called before the first frame update
    void Start()
    {
        //totalMoney = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = "$" + totalMoney;
    }
}
