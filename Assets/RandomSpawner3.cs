using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner3 : MonoBehaviour
{
    public GameObject key;
    // public GameObject canSoda;

    List<Vector3> keyLocation = new List<Vector3>();
    // List<Vector3> canSodaLocation = new List<Vector3>();

    void Start()
    {
        keyLocation.Add(new Vector3((float)16.7, (float)0.6, (float)26.5));
        keyLocation.Add(new Vector3((float)-4.2, (float)0.6, (float)-21.9));
        keyLocation.Add(new Vector3((float)-21, (float)0.6, -(float)16.5));
        keyLocation.Add(new Vector3((float)5.7, (float)0.6, (float)0.3));

        // Instantiate(key, keyLocation[0], Quaternion.Euler(90,0,0));
        // Instantiate(key, keyLocation[1], Quaternion.Euler(90,0,0));
        // Instantiate(key, keyLocation[2], Quaternion.Euler(90,0,0));
        // Instantiate(key, keyLocation[3], Quaternion.Euler(90,0,0));

        // canSodaLocation.Add(new Vector3((float)18.25, (float)0.5, (float)-12.57));
        // canSodaLocation.Add(new Vector3((float)-0.85, (float)0.5, (float)1.67));
        // canSodaLocation.Add(new Vector3((float)-3.27, (float)0.5, (float)-7.07));
        // canSodaLocation.Add(new Vector3((float)3.47, (float)0.5, (float)12.91));
        // canSodaLocation.Add(new Vector3((float)-6.27, (float)0.5, (float)13));
        // canSodaLocation.Add(new Vector3((float)6.68, (float)0.5, (float)-5.04));

        Instantiate(key, keyLocation[Random.Range(0, keyLocation.Count)], Quaternion.Euler(90, 0, 0));

        // for (int i = 0; i < 3; i++)
        // {
        //     Instantiate(canSoda, canSodaLocation[Random.Range(0, canSodaLocation.Count)], Quaternion.Euler(90, 0, 0));
        // }
    }
}