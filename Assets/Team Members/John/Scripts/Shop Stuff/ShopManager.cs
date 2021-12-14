using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public int totalMoney = 5000;
    public TMP_Text moneyUI;

    public AudioSource audioSource;
    public AudioClip buySound;

    public SeedPlanter_ShopPanel seedPlanterShop;

    // Start is called before the first frame update
    void Start()
    {
        seedPlanterShop.PlanterUpgradedEvent += OnShopBuy;
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = "$" + totalMoney;
    }

    void OnShopBuy()
    {
        audioSource.clip = buySound;
        audioSource.Play();
    }
}
