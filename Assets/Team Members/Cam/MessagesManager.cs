using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessagesManager : ManagerBase<MessagesManager>
{
	public TextMeshProUGUI textMeshProUGUI;

	public float timeToShow = 2f;

	public Coroutine coroutine;

	public AudioSource audioSource;

	public override void Awake()
	{
		base.Awake();

		textMeshProUGUI.text = "";
	}

	public void Show(string message)
	{
		if (coroutine != null) StopCoroutine(coroutine);
		coroutine = StartCoroutine(ShowCoroutine(message));
	}

	private IEnumerator ShowCoroutine(string message)
	{
		audioSource.Play();
		textMeshProUGUI.text = message;
		textMeshProUGUI.transform.localScale = Vector3.one * 2f;
		textMeshProUGUI.transform.DOPunchScale(Vector3.one, 0.5f);
		yield return new WaitForSeconds(timeToShow * (message.Length / 15f));
		textMeshProUGUI.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutCubic);
		yield return new WaitForSeconds(0.25f);
		textMeshProUGUI.text = "";
	}
}