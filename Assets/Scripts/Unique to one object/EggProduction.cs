using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WOULD HAVE LOVED TO USE INHERITANCE BUT IT WAS PRETTY MESSY?
//DIDN'T REALLY WANT TO HARD RIP TO A CHICKEN
public class EggProduction : MonoBehaviour
{
    private ChickenModel chicken;
    public GameObject egg;
    
    public int layTime = 10;
    
    private float layTimeMulitplier;
    
    // Start is called before the first frame update
    void Start()
    {
        chicken = GetComponent<ChickenModel>();
        StartCoroutine("LayTimer");

        layTimeMulitplier = (chicken.maxHunger - chicken.hungerLevel) / 10;
    }

    private IEnumerator LayTimer()
    {
        for (int i = 0; i < layTime; i++)
        {
            yield return new WaitForSeconds(1 + layTimeMulitplier);
        }

        LayEgg();
        //restarts the coroutine/timer thing. Need a nicer way to do this
        StartCoroutine("LayTimer");
    }

    void LayEgg()
    {
        GameObject copy = egg;
        Vector3 chickenLocation = this.transform.position;
        //need lay offset

        //Instantiate Eggs
        Instantiate(copy, new Vector3(chickenLocation.x, chickenLocation.y, chickenLocation.z), copy.transform.rotation);

        //Instantiate Egg
        Debug.Log("PLOOP!");
        //laying an egg uses food reserves
        chicken.ReduceHunger(0.25f);
    }
}
