using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Tanks;
using TMPro;
using UnityEngine;

public class PopupInfo : MonoBehaviour
{
	[SerializeField]
	private float distanceThreshold = 5f;

	private bool poppedUp = false;


	public string message = "";

	public bool onlyShowOnce = false;

	[Header("Setup")]
	public TextMeshProUGUI textMesh;

	public SpriteRenderer spriteRenderer;

	private bool shownOnce = false;

	private void Start()
	{
		textMesh.text = message;
		transform.localScale = Vector3.zero;

		spriteRenderer.size = new Vector2(2f + message.Length / 3f, 1);
		// message.Length
	}

	void Update()
	{
		if (onlyShowOnce && shownOnce)
		{
			return;
		}

		// Only pay attention to the closest player
		float distance = float.MaxValue;
		CharacterModel characterModel = null;
		foreach (CharacterModel _characterModel in GameManager.Instance.players)
		{
			float checkDistance = Vector3.Distance(_characterModel.transform.position, transform.position);
			if (checkDistance < distance)
			{
				characterModel = _characterModel;
				distance = checkDistance;
			}
		}

		if (!poppedUp && distance < distanceThreshold)
		{
			Popup();
		}
		else if (poppedUp && distance > distanceThreshold * 1.5f)
		{
			PullDown();
		}
	}


	private void Popup()
	{
		poppedUp = true;
		transform.localScale = Vector3.one;
		transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), .5f);
		transform.DOShakeRotation(0.5f, 10f);
	}

	private void PullDown()
	{
		poppedUp = false;
		transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutExpo);
		shownOnce = true;
	}
}