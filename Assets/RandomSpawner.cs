using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject key;
    // public GameObject canSoda;

    List<Vector3> keyLocation = new List<Vector3>();
    // List<Vector3> canSodaLocation = new List<Vector3>();


    void Start()
    {
        Debug.Log(key.tag);
        // Debug.Log(canSoda.tag);
        keyLocation.Add(new Vector3((float)-13.9, (float)0.5, (float)9.5));
        keyLocation.Add(new Vector3((float)7.9,(float)0.6,5));
        keyLocation.Add(new Vector3(21,(float)0.6,(float)-17.9));
        keyLocation.Add(new Vector3((float)6.4,(float)2.4,(float)22.5));

        // Instantiate(key, keyLocation[0], Quaternion.Euler(90,0,0));
        // Instantiate(key, keyLocation[1], Quaternion.Euler(90,0,0));
        // Instantiate(key, keyLocation[2], Quaternion.Euler(90,0,0));
        // Instantiate(key, keyLocation[3], Quaternion.Euler(90,0,0));

        // canSodaLocation.Add(new Vector3((float)17.81, (float)1, (float)-6.83));
        // canSodaLocation.Add(new Vector3((float)7.6, (float)1, (float)-12.7));
        // canSodaLocation.Add(new Vector3((float)-3.64, (float)1, (float)-7.33));
        // canSodaLocation.Add(new Vector3((float)-12.4, (float)1, (float)1.73));

        Instantiate(key, keyLocation[Random.Range(0, keyLocation.Count)], Quaternion.Euler(90,0,0));

        // for(int i = 0; i < 3; i++)
        // {
        //     Instantiate(canSoda, canSodaLocation[Random.Range(0, canSodaLocation.Count)], Quaternion.Euler(90, 0, 0));
        // }
        
    }
}