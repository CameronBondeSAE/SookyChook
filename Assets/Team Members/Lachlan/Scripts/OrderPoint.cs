using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrderPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Text Properties")]
    public Text orderText;
    
    void Start()
    {
        FindObjectOfType<ChickenGrowingMode>().NewOrderEvent += OrderMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OrderMenu(ChickenGrowingMode.Order amount)
    {
        orderText.text = amount.ToString();
    }
}
