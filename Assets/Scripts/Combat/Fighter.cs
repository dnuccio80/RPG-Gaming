using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float timeBetweenAttack = 1.5f;
        [Range(0,1)]
        [SerializeField] private float attackFractionSpeed = 1f;
        [SerializeField] private Transform weaponSpawnerTransform;
        [SerializeField] private WeaponSO defaultWeaponSO;

        private Transform targetTransform;
        private Animator animator;
        private Mover mover;
        private float timeSinceLastAttack = Mathf.Infinity;
        private WeaponSO currentWeapon;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EquipWeapon(defaultWeaponSO);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (targetTransform == null || targetTransform.GetComponent<Health>().IsDead()) return;

            if (!GetIsInRange()) mover.MoveTo(targetTransform.position, attackFractionSpeed);
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }


        private void AttackBehaviour()
        {
            transform.LookAt(targetTransform);

            if (timeSinceLastAttack >= timeBetweenAttack)
            {
                // This will trigger the Hit() Event;
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger(Dictionary.STOP_ATTACK_ANIMATOR);
            animator.SetTrigger(Dictionary.ATTACK_ANIMATOR);
        }

        private void EquipWeapon(WeaponSO weaponSO)
        {
            currentWeapon = weaponSO;
            currentWeapon.Spawn(weaponSpawnerTransform, animator);
        }

        // Animation Event
        void Hit()
        {
            if(targetTransform == null) return;

            Health healthTarget = targetTransform.GetComponent<Health>();
            healthTarget.TakeDamage(currentWeapon.WeaponDamage);
        }

        private bool GetIsInRange() => Vector3.Distance(transform.position, targetTransform.position) <= currentWeapon.WeaponRange;
        public bool CanAttackTarget(GameObject target)
        {
            if(target == null) return false;
            Health targetHealth = target.transform.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetTransform = target.transform;
        }

        public void Cancel()
        {
            targetTransform = null;
            mover.Cancel();
            StopAttackTrigger();
        }

        private void StopAttackTrigger()
        {
            animator.ResetTrigger(Dictionary.ATTACK_ANIMATOR);
            animator.SetTrigger(Dictionary.STOP_ATTACK_ANIMATOR);
        }

    }
}
