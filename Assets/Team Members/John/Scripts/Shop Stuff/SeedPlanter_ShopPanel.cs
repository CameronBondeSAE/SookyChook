using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SeedPlanter_ShopPanel : MonoBehaviour
{
    [Header("Text Field References")]
    [SerializeField]
    TMP_Text price;
    [SerializeField]
    TMP_Text description;

    int upgradePrice;

    //SeedPlanterModel[] seedPlanters;
    public SeedPlanterModel seedPlanter;
    public ShopManager shopManager;

    //Event
    public event Action PlanterUpgradedEvent;
    public event Action TooExpensiveEvent;

    // Start is called before the first frame update
    void Start()
    {
        description.text = "Increase seed planting productivity";
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePrice();
    }

    void UpdatePrice()
    {
        if(seedPlanter.planterLevel == 1)
        {
            //Level 2 Upgrade Price
            upgradePrice = 500;
        }
        else if(seedPlanter.planterLevel == 2)
        {
            //Final Upgrade
            upgradePrice = 750;
            description.text = "Increase seed planting productivity X2";
        }
        else
        {
            price.text = "Max Level";
            description.text = "Seed Planter Fully Upgraded";
            return;
        }

        UpdatePriceText();

    }

    void UpdatePriceText()
    {
        price.text = "BUY: " + upgradePrice;
    }

    public void BuyUpgrade()
    {
        if (seedPlanter.planterLevel == seedPlanter.maxlevel)
        {
            Debug.Log("Max Level");
            return;
        }

        if (shopManager.totalMoney >= upgradePrice && seedPlanter.planterLevel != seedPlanter.maxlevel)
        {
            PlanterUpgradedEvent?.Invoke();
            shopManager.totalMoney -= upgradePrice;
            return;
        }

        if(shopManager.totalMoney < upgradePrice)
        {
            Debug.Log("Too Expensive");

            //Trigger EFX/SFX if item is too expensive to purcase (Player feedback)
            TooExpensiveEvent?.Invoke();
        }
    }
}
