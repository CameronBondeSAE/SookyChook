using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CashManager : ManagerBase<CashManager>
{
    public int totalMoney = 5000;
    public TMP_Text moneyUI;
    bool canPlayEFX = true;

    //Audio
    public AudioSource audioSource;
    public AudioClip buySound;
    public AudioClip collectMoneySound;
    public AudioClip tooExpensiveSound;


    // Update is called once per frame
    void Update()
    {
        moneyUI.text = "$" + totalMoney;
    }


    public void TakeMoney(int amount)
    {
        totalMoney -= amount;
        moneyUI.transform.DOShakePosition(1f, 5f);
        moneyUI.DOColor(Color.red, 0.2f).OnComplete(ResetColor);

        //Change Sound & Play
        PlayMoneySound(buySound);

    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        moneyUI.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f, 1);
        moneyUI.DOColor(Color.green, 0.2f).OnComplete(ResetColor);

        //Play Sound
        PlayMoneySound(collectMoneySound);
    }

    public void TooExpensive()
    {
        //Only play effects after the effect is done playing to prevent UI bug
        if(canPlayEFX)
        {
            canPlayEFX = false;
            moneyUI.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f, 1).OnComplete(ResetPlayEffects);
            moneyUI.DOColor(Color.red, 0.2f).OnComplete(ResetColor);
            PlayMoneySound(tooExpensiveSound);
        }
    }

    private void ResetColor()
    {
        moneyUI.DOColor(Color.white, 0.5f);
    }

    //Prevent UI bug that stops text from returning to normal size if TooExpensive() is spammed
    void ResetPlayEffects()
    {
        canPlayEFX = true;
    }

    //Play Whatever audio is called
    void PlayMoneySound(AudioClip newSound)
    {
        audioSource.clip = newSound;
        audioSource.Play();
    }

    public void GiveMoneyDebug()
    {
        AddMoney(500);
    }
}
