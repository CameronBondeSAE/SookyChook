using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tanks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class OrderPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Menu Properties")]
    public GameObject orderMenuParent;
    GameObject newOrder;
    public GameObject gameModeGO;
    
    public float orderLateTime = 25.0f;

    [SerializeField]
    private GameObject orderIcon;
    
    [SerializeField]
    private Transform orderIconPos;

    public List<OrderUIText> orderUITexts = new List<OrderUIText>();

    public class OrderUIText
    {
        public ChickenGrowingMode.Order order;
        public GameObject uiTextGameObject;
    }
    
    //events
    public event Action<CharacterModel> OrderPointEvent;

    void Start()
    {
        gameModeGO.GetComponent<ChickenGrowingMode>().NewOrderEvent += OrderMenu;
        gameModeGO.GetComponent<ChickenGrowingMode>().OrderCompleteEvent += RemoveOrderMenu;
    }

    void RemoveOrderMenu(ChickenGrowingMode.Order order)
    {
        OrderUIText foundOrderUIText = null;
        
        foreach (OrderUIText orderUIText in orderUITexts)
        {
            if (order == orderUIText.order)
            {
	            Destroy(orderUIText.uiTextGameObject);
                orderUITexts.Remove(orderUIText);
            }
        }

        //orderUITexts.Remove(orderUITexts.Find(foundOrderUIText.uiTextGameObject);


        //if(orderUITexts == order.productType.ToString())
        //{
        // orderUITexts.Remove(foundOrderUIText.uiTextGameObject(order));
        //}

    }

    public void OrderMenu(ChickenGrowingMode.Order order)
    {
        //Playing around with colors and tweening
        //Spawns Texts and Saves it in a List
        newOrder = Instantiate(orderIcon);
        newOrder.transform.parent = orderMenuParent.transform;

        OrderUIText orderUIText = new OrderUIText();
        orderUIText.uiTextGameObject = newOrder;
        orderUIText.order = order;
        orderUITexts.Add(orderUIText);

        GetComponentInChildren<TextMeshProUGUI>().text = order.productType.ToString();
        // transform.DOMove(Vector3.one, 2, false);
        GetComponentInChildren<TextMeshProUGUI>().DOColor(Color.white, 0);
        StartCoroutine(OrderUI());
        //Debug for Order
        Debug.Log(order.productType.ToString());

    }

    public IEnumerator OrderUI()
    {
        transform.DOPunchScale(Vector3.one, 1, 50);
        yield return new WaitForSeconds(orderLateTime);
        GetComponentInChildren<TextMeshProUGUI>().DOColor(Color.red, 2.0f);
        StopCoroutine(OrderUI());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterModel>())
        {
            OrderPointEvent?.Invoke(other.GetComponent<CharacterModel>());
        }
    }
}
