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
        DayNightManager.Instance.PhaseChangeEvent += OrderMenu2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OrderMenu(ChickenGrowingMode.Order amount)
    {
        Debug.Log("test");
        orderText.text = amount.ToString();
        Debug.Log(amount);
        Debug.Log("Check");
    }

    void OrderMenu2(DayNightManager.DayPhase order)
    {
        Debug.Log("test");
        orderText.text = order.ToString();
        Debug.Log(order);
    }
}
