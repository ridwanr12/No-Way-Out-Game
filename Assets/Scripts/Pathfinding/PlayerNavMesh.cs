using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform targetPositionTransform;
    private NavMeshAgent brianNavMeshAgent;
    private void Awake()
    {
        brianNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        brianNavMeshAgent.destination = targetPositionTransform.position;
    }
}
