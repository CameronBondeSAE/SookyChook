using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aaron;
using Anthill.AI;
using UnityEngine;

public class FoxModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;
    public Edible target;

    public float maxHunger = 10;
    public float hunger;

    public bool isHunting;
    public bool canSeeChicken;
    public bool inRange;
    public bool eatingChicken;
    public bool chickenGone;
    public bool willAttack;

    // Start is called before the first frame update
    void Start()
    {
        antAIAgent.SetGoal("Get Chicken");

        hunger = maxHunger * 0.7f;
        StartCoroutine(FoxHunger());

        GetComponent<Pathfinding>().beginningVectorPos = this.transform;
        GetComponent<Pathfinding>().finishVectorPos = this.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hunger <= 0)
        {
            hunger = 0;
        }
    }

    //cheeky pop in for now
    private IEnumerator FoxHunger()
    {
        while(hunger <= maxHunger * 2)
        {
            hunger -= 1;
            yield return new WaitForSeconds(3);
        }
    }
}
