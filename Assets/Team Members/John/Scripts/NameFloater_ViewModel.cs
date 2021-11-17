using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NameFloater_ViewModel : MonoBehaviour
{
    public TextMeshPro name;
    public AudioSource audioSource;
    public AudioClip fart;
    float defaultScale = 1f;

    //NOTE: All super hacky at the moment- was just mainly playing around with tweening effects

    public void BadName()
    {
        audioSource.clip = fart;
        audioSource.Play();
        name.text = "Bad Name";
        name.color = new Color(255, 0, 0);
    }

    public void GoodName()
    {
        //name.text = "Good Name";
        //name.color = new Color(0, 255, 0);

        Sequence goodNameSequence = DOTween.Sequence();
        goodNameSequence.Append(name.transform.DOScale(0.1f, 1f).SetEase(Ease.InBack).OnComplete(ChangeName));
        goodNameSequence.Append(name.DOColor(new Color(0, 255, 0), 0.5f));
        goodNameSequence.Append(name.transform.DOScale(1.5f, 1f).SetEase(Ease.InOutElastic));

    }

    //Using this to change the name as the DoTween shrinks (defintely a better way of doing this)
    void ChangeName()
    {
        name.text = "Good Name";
    }

    /*
    IEnumerator Name()
    {
        BadName();

        yield return new WaitForSeconds(1.5f);

        GoodName();
    }
    */
}
