using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttack = 1.5f;
        [SerializeField] private float weaponDamage = 5f;

        private Transform targetTransform;
        private Animator animator;
        private Mover mover;
        private float timeSinceLastAttack;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (targetTransform == null) return;

            if (!GetIsInRange()) mover.MoveTo(targetTransform.position);
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack >= timeBetweenAttack)
            {
                // This will trigger the Hit() Event;
                animator.SetTrigger(Dictionary.ATTACK_ANIMATOR);
                timeSinceLastAttack = 0;
            }
        }

        // Animation Event
        void Hit()
        {
            Health healthTarget = targetTransform.GetComponent<Health>();
            healthTarget.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetTransform.position) <= weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetTransform = target.transform;
        }

        public void Cancel()
        {
            targetTransform = null;
        }

  
    }
}
