using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent NavMeshAgent;
        private Animator animator;


        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            HandleAnimationsVariables();
        }

        private void HandleAnimationsVariables()
        {
            Vector3 velocity = NavMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            animator.SetFloat("ForwardSpeed", localVelocity.z);
        }

        public void MoveTo(Vector3 destination)
        {
            NavMeshAgent.destination = destination;
            NavMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            NavMeshAgent.isStopped = true;
        }
    }
}
