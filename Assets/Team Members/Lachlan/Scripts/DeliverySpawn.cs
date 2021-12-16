using System.Collections;
using System.Collections.Generic;
using Rob;
using UnityEngine;


public class DeliverySpawn : MonoBehaviour
{

    public Spawner spawner;
    public float spawnFenceEvent;

    // Start is called before the first frame update
    void Start()
    {
        DayNightManager.Instance.PhaseChangeEvent += InstanceOnPhaseChangeEvent;
    }

    private void InstanceOnPhaseChangeEvent(DayNightManager.DayPhase obj)
    {
        if (obj == DayNightManager.DayPhase.Noon)
        {
            StartCoroutine(FenceSpawn());
        }
    }

    private IEnumerator FenceSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        spawner.SpawnMultiple();
        StopCoroutine(FenceSpawn());
    }
}
