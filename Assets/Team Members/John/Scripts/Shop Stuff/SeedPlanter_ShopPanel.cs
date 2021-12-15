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
    TMP_Text upgradePriceText;
    [SerializeField]
    TMP_Text refillPriceText;
    [SerializeField]
    TMP_Text description;

    int upgradePrice;
    public int seedRefillPrice = 250;


    SeedPlanterModel seedPlanter;

    //Event
    public event Action PlanterUpgradedEvent;
    public event Action TooExpensiveEvent;

    // Start is called before the first frame update
    void Start()
    {
        description.text = "Increase seed planting productivity";

        //Find the seedPlanter in the scene
        seedPlanter = FindObjectOfType<SeedPlanterModel>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePrice();
    }

    void UpdatePrice()
    {
        if(seedPlanter.seedsAvailable == seedPlanter.maxSeeds)
        {
            refillPriceText.text = "Full";
        }
        else if(seedPlanter.seedsAvailable < seedPlanter.maxSeeds)
        {
            refillPriceText.text = "BUY: " + seedRefillPrice;
        }

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
            upgradePriceText.text = "Max Level";
            description.text = "Seed Planter Fully Upgraded";
            return;
        }

        UpdatePriceText();

    }

    void UpdatePriceText()
    {
        upgradePriceText.text = "BUY: " + upgradePrice;
    }

    //Buying Planter Upgrades
    public void BuyUpgrade()
    {
        //Do not upgrade if already max level
        if (seedPlanter.planterLevel == seedPlanter.maxlevel)
        {
            Debug.Log("Max Level");
            return;
        }

        //If can afford & seed planter is not max level upgrade the seed planter
        if (CashManager.Instance.totalMoney >= upgradePrice && seedPlanter.planterLevel != seedPlanter.maxlevel)
        {
            seedPlanter.Upgrade();
            CashManager.Instance.TakeMoney(upgradePrice);
            return;
        }

        //If cannot afford
        if(CashManager.Instance.totalMoney < upgradePrice)
        {
            CashManager.Instance.TooExpensive();
        }
    }

    //Buying Seed Refills
    public void BuySeedRefill()
    {
        //Too Expensive don't buy
        if(CashManager.Instance.totalMoney < seedRefillPrice)
        {
            CashManager.Instance.TooExpensive();
            return;
        }

        //If can afford & seed planter not already full - refill seed planter
        if(seedPlanter.seedsAvailable < seedPlanter.maxSeeds && CashManager.Instance.totalMoney > seedRefillPrice)
        {
            seedPlanter.seedsAvailable = seedPlanter.maxSeeds;
            CashManager.Instance.TakeMoney(seedRefillPrice);
        }
    }
}
