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
		textMeshProUGUI.text = message;
		textMeshProUGUI.transform.localScale = Vector3.one * 2f;
		textMeshProUGUI.transform.DOPunchScale(Vector3.one, 0.5f);
		yield return new WaitForSeconds(timeToShow * (message.Length / 15f));
		textMeshProUGUI.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutFlash);
		yield return new WaitForSeconds(0.5f);
		textMeshProUGUI.text = "";
	}
}