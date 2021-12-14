using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MessagesManager : ManagerBase<MessagesManager>
{
	public TextMeshProUGUI textMeshProUGUI;

	public float timeToShow = 2f;

	public override void Awake()
	{
		base.Awake();

		textMeshProUGUI.text = "";
	}

	public void Show(string message)
	{
		StartCoroutine(ShowCoroutine(message));
	}

	private IEnumerator ShowCoroutine(string message)
	{
		textMeshProUGUI.text = message;
		textMeshProUGUI.transform.localScale = Vector3.one * 2f;
		textMeshProUGUI.transform.DOPunchScale(Vector3.one, 0.5f);
		yield return new WaitForSeconds(timeToShow * (message.Length/20f));
		textMeshProUGUI.text = "";
	}
}