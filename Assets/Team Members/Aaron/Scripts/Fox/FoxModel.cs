using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FoxModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;
    public GameObject target;

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
