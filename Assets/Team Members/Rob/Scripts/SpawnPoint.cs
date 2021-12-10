using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum TypeOfPoint
    {
        General,
        Forest,
        MainFarm,
        FarmEdge,
        Coop
    }

    public TypeOfPoint typeOfPointOfPoint;

    public static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    void OnEnable()
    {
        spawnPoints.Add(this);
    }

    void OnDisable()
    {
        spawnPoints.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}