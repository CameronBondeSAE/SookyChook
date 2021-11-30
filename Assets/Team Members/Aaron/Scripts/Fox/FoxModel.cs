using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FoxModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        antAIAgent.SetGoal("Get Chicken");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
