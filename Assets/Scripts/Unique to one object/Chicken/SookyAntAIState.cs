using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

public class SookyAntAIState : AntAIState
{
	[Header("Debug")]
	[ReadOnly]
	public GameObject owner;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);

		owner = aGameObject;
	}
}