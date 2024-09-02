using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;

        GameObject player;


        private Health health;
        private Fighter fighter;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG);
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            health.OnDead += Health_OnDead;
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
                fighter.Attack(player);
            } else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRange(GameObject target)
        {
            return Vector3.Distance(transform.position, target.transform.position) <= chaseDistance;
        }




    }
}
