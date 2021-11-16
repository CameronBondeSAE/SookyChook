using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class GraveyardManager : MonoBehaviour
{
    public List<GameObject> ghostChickens = new List<GameObject>();

    public GameObject ghost;

    //TODO sub to chicken murder event from somewhere; instantiate (to be enabled/disabled per relevant time cycle)
    private void spawnGhosts()
    {
        GameObject copy = ghost;
        Instantiate(copy, new Vector3(), copy.transform.rotation);
        ghostChickens.Add(copy);
    }

    private void Update()
    {
        if (DayNightManager.Instance.currentPhase == DayNightManager.DayPhase.Night || DayNightManager.Instance.currentPhase == DayNightManager.DayPhase.Midnight)
        {
            releaseGhosts();
        }

        if (DayNightManager.Instance.currentPhase == DayNightManager.DayPhase.Morning)
        {
            clearGhostChickens();
        }
    }

    //On night cycle begin
    void releaseGhosts()
    {
        Debug.Log("Release Ghosts");
        foreach (var ghost in ghostChickens)
        {
            ghost.SetActive(true);
        }
    }

    //On morning/dawn cycle begin
    void clearGhostChickens()
    {
        Debug.Log("Clear Ghosts");
        foreach (var ghost in ghostChickens)
        {
            ghost.SetActive(false);
        }
    }
}
