using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NameFloater_ViewModel : MonoBehaviour
{
    public TextMeshPro nameText;
    public AudioSource audioSource;
    public AudioClip fart;
    float defaultScale = 1f;

    string newGoodName;

    //NOTE: All super hacky at the moment- was just mainly playing around with tweening effects

    public void BadName(string name)
    {
        audioSource.clip = fart;
        audioSource.Play();
        nameText.text = name;
        nameText.color = new Color(255, 0, 0);
    }

    public void GoodName(string newName)
    {
        //Reference for name as OnComplete doesn't take methods
        newGoodName = newName;

        Sequence goodNameSequence = DOTween.Sequence();
        goodNameSequence.Append(nameText.transform.DOScale(0.1f, 1f).SetEase(Ease.InBack).OnComplete(ChangeName));
        goodNameSequence.Append(nameText.DOColor(new Color(0, 255, 0), 0.5f));
        goodNameSequence.Append(nameText.transform.DOScale(1.5f, 1f).SetEase(Ease.InOutElastic));

    }

    //Using this to change the name as the DoTween shrinks (defintely a better way of doing this)
    void ChangeName()
    {
        nameText.text = newGoodName;
    }

    public void BadNameButton()
    {
        BadName("A Bad Name");
    }

    public void GoodNameButton()
    {
        GoodName("A Good Name");
    }
}
