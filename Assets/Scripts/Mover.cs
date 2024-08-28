using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;

    private NavMeshAgent playerNavMeshAgent;

    private void Awake()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerNavMeshAgent.destination = targetTransform.position;
    }




}
