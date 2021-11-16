using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class GraveyardManager : MonoBehaviour
{
    public List<GameObject> ghostChickens = new List<GameObject>();

    public GameObject ghost;

    //TODO link to day/night cycle
    
    //TODO sub to chicken murder event from somewhere; instantiate (to be enabled/disabled per relevant time cycle)
    private void spawnGhosts()
    {
        GameObject copy = ghost;
        Instantiate(copy, new Vector3(), copy.transform.rotation);
    }

    //On night cycle begin
    void releaseGhosts()
    {
        foreach (var ghost in ghostChickens)
        {
            enabled = true;
        }   
    }

    //On morning/dawn cycle begin
    void clearGhostChickens()
    {
        enabled = false;
    }
}
