using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent playerNavMeshAgent;
    private Animator animator;


    private void Awake()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

        HandleAnimationsVariables();
    }

    private void HandleAnimationsVariables()
    {
        Vector3 velocity = playerNavMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        animator.SetFloat("ForwardSpeed", localVelocity.z);

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
        {
            playerNavMeshAgent.destination = hit.point;
        }
    }
}
