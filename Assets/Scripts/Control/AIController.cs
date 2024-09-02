using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;

        GameObject player;

        private Health health;
        private Fighter fighter;
        private ActionScheduler actionScheduler;
        private Mover mover;
        private Vector3 guardianPosition;
        private float timeSinceLastSawTarget;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG);
            fighter = GetComponent<Fighter>();
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            health.OnDead += Health_OnDead;
            guardianPosition = transform.position;
        }

        private void Health_OnDead(object sender, System.EventArgs e)
        {
            fighter.Cancel();
            GetComponent<NavMeshAgent>().enabled = false;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange(player) && fighter.CanAttackTarget(player))
            {
                timeSinceLastSawTarget = 0;
                AttackBehaviour();

            }
            else if (timeSinceLastSawTarget < suspicionTime)
            {
                SuspicionState();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawTarget += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardianPosition);
        }

        private void SuspicionState()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRange(GameObject target)
        {
            return Vector3.Distance(transform.position, target.transform.position) <= chaseDistance;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }



    }
}
