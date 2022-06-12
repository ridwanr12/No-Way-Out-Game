using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingCanSoda : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    Vector3 collisionLocation;

    // Start is called before the first frame update
    void Start()
    {
        collisionLocation = player.transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = collisionLocation;
    }
}
