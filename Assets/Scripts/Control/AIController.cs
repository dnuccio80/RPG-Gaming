using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using System;

using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float followNearbyTeammateDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointDwellTime = 3f;
        [SerializeField] private LayerMask enemyLayer;
        [Range(0,1)]
        [SerializeField] private float patrolSpeedFraction = 0.2f;

        GameObject player;

        private Health health;
        private Fighter fighter;
        private ActionScheduler actionScheduler;
        private Mover mover;
        private Vector3 guardianPosition;
        private Vector3 currentWaypoint;
        private float timeSinceLastSawTarget;
        private float waypointTolerance = 1f;
        private float timeInWaypoint;
        private bool AtPlace = false;
        private float followPlayerDistance;
        private float chaseDistanceExpanded;

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
            health.OnDamageTaken += Health_OnDamageTaken;
            guardianPosition = transform.position;
            if(fighter.GetCurrentWeapon() != null) chaseDistance = fighter.GetCurrentWeapon().WeaponRange;
            chaseDistanceExpanded = chaseDistance * 4;
            followPlayerDistance = chaseDistance;
        }

        private void Health_OnDamageTaken(object sender, Health.OnDamageTakenEventArgs e)
        {
            ExpandFollowDistance();
            LookForNearbyTeammates();
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
                PatrolBehaviour();
            }

            timeSinceLastSawTarget += Time.deltaTime;
        
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardianPosition;
            if (patrolPath != null)
            {

                if (AtWaypoint())
                {
                    timeInWaypoint += Time.deltaTime;
                    if(timeInWaypoint > waypointDwellTime) CycleWaypoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }

        private Vector3 GetCurrentWayPoint()
        {
            if (currentWaypoint == Vector3.zero) CycleWaypoint();
            return currentWaypoint;
        }

        private void CycleWaypoint()
        {
            currentWaypoint = patrolPath.GetNextWaypoint();
            timeInWaypoint = 0;
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionState()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        public void ExpandFollowDistance()
        {
            if (followPlayerDistance == chaseDistanceExpanded) return;
            float timeToExpand = 2f;
            followPlayerDistance = chaseDistanceExpanded;
            Invoke("ResetFollowDistance", timeToExpand);
        }

        private void LookForNearbyTeammates()
        {
            Vector3 origin = transform.position;

            RaycastHit[] hits = Physics.SphereCastAll(origin, followNearbyTeammateDistance, Vector3.up, followNearbyTeammateDistance, enemyLayer);

            foreach(RaycastHit hit in hits)
            {
                hit.transform.GetComponent<AIController>().ExpandFollowDistance();
            }
        }

        private void ResetFollowDistance()
        {
            followPlayerDistance = chaseDistance;
        }

        private bool InAttackRange(GameObject target)
        {
            return Vector3.Distance(transform.position, target.transform.position) <= followPlayerDistance;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }



    }
}
