using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OrderPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Menu Properties")]
    public GameObject orderMenu;

    public float orderLateTime = 10.0f;

    [SerializeField]
    private GameObject orderIcon;

    [SerializeField]
    private Transform orderIconPos;
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
        //Playing around with colors and tweening
        //TODO: Need to stack the list for the order. Probably requires instantiating new text.
        GameObject orderMenu = Instantiate(orderIcon, orderIconPos);
        GetComponentInChildren<TextMeshProUGUI>().text = amount.productType.ToString();
        transform.DOMove(Vector3.one, 2, false);
        GetComponentInChildren<TextMeshProUGUI>().DOColor(Color.white, 0);
        StartCoroutine(OrderUI());
        //Debug for Order
        Debug.Log(amount.productType.ToString());

    }

    public IEnumerator OrderUI()
    {
        yield return new WaitForSeconds(orderLateTime);
        GetComponentInChildren<TextMeshProUGUI>().DOColor(Color.red, 2.0f);
        StopCoroutine(OrderUI());
    }
    
}
