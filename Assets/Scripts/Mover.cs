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
        HandleAnimationsVariables();
    }

    private void HandleAnimationsVariables()
    {
        Vector3 velocity = playerNavMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        animator.SetFloat("ForwardSpeed", localVelocity.z);
    }

    public void MoveTo(Vector3 destination)
    {
        playerNavMeshAgent.destination = destination;
    }
}
