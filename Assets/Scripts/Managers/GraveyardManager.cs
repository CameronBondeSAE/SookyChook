using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks;
using UnityEngine;

public class GraveyardManager : MonoBehaviour
{
    public List<GameObject> ghostChickens = new List<GameObject>();

    public GameObject ghostPrefab;

    //TODO sub to chicken murder event from somewhere; instantiate (to be enabled/disabled per relevant time cycle)

    private void Start()
    {
        DayNightManager.Instance.PhaseChangeEvent += InstanceOnPhaseChange;
        
        GlobalEvents.chickenDiedEvent += GlobalEventsOnchickenDiedEvent;
    }

    private void GlobalEventsOnchickenDiedEvent(GameObject obj)
    {
	    SpawnGhost(obj.transform.position, obj.transform.rotation);
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
    private void SpawnGhost(Vector3 position, Quaternion rotation)
    {
        //TODO set spawn point
        ghostChickens.Add(Instantiate(ghostPrefab, position, rotation));
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
