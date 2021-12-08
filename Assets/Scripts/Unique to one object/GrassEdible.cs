using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrassEdible : Edible
{
	public ParticleSystem particleSystem;
	
	public override void BeingEaten()
	{
		base.BeingEaten();

		// transform.localScale /= 1.5f;
		transform.DOShakeScale(0.5f, 1f);
		particleSystem.Emit(10);
	}
}
