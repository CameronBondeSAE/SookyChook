using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster_Model : MonoBehaviour
{
    public float sightRange = 10f;
    public Transform targetEnemy;
    public Transform targetChicken;

    public List<T> FindObjects<T>(float range)
    {
        List<T> objects = new List<T>();

        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in nearbyObjects)
        {
            if (col.GetComponentInParent<T>() != null)
            {
                objects.Add(col.GetComponentInParent<T>());
            }
        }

        return objects;
    }
}
