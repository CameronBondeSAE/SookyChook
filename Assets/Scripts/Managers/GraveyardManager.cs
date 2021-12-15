using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks;
using UnityEngine;

public class GraveyardManager : MonoBehaviour
{
    public List<GameObject> ghostChickens = new List<GameObject>();

    public GameObject ghost;

    //TODO sub to chicken murder event from somewhere; instantiate (to be enabled/disabled per relevant time cycle)

    private void Awake()
    {
        DayNightManager.Instance.PhaseChangeEvent += InstanceOnPhaseChange;
    }

    void InstanceOnPhaseChange(DayNightManager.DayPhase obj)
    {
        if (obj == DayNightManager.DayPhase.Night || obj == DayNightManager.DayPhase.Midnight)
        {
            ReleaseGhosts();
        }
        
        if (obj == DayNightManager.DayPhase.Morning)
        {
            ClearGhostChickens();
        }
    }
    private void SpawnGhosts()
    {
        GameObject copy = ghost;
        //TODO set spawn point
        Instantiate(copy, new Vector3(), copy.transform.rotation);
        ghostChickens.Add(copy);
    }

    //On night cycle begin
    void ReleaseGhosts()
    {
        foreach (var ghost in ghostChickens)
        {
            ghost.SetActive(true);
        }
    }

    //On morning/dawn cycle begin
    void ClearGhostChickens()
    {
        foreach (var ghost in ghostChickens)
        {
            ghost.SetActive(false);
        }
    }
}
