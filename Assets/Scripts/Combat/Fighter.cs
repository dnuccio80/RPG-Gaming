using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] private float timeBetweenAttack = 1.5f;
        [Range(0,1)]
        [SerializeField] private float attackFractionSpeed = 1f;
        [SerializeField] private Transform rightHandTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private WeaponSO defaultWeaponSO;

        private Transform targetTransform;
        private Animator animator;
        private Mover mover;
        private float timeSinceLastAttack = Mathf.Infinity;
        private WeaponSO currentWeapon;
        private AudioSource audioSource;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            //WeaponSO weapon = Resources.Load<WeaponSO>("Unarmed");
            //EquipWeapon(weapon);
            EquipWeapon(defaultWeaponSO);

        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (targetTransform == null || targetTransform.GetComponent<Health>().IsDead()) return;

            if (!GetIsInRange(targetTransform)) mover.MoveTo(targetTransform.position, attackFractionSpeed);
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

        public void EquipWeapon(WeaponSO weaponSO)
        {
            if(currentWeapon != null)
            {
                currentWeapon.DestroyOldWeapon(GetHandTransform(currentWeapon));
            }

            currentWeapon = weaponSO;
            currentWeapon.Spawn(GetHandTransform(weaponSO), animator);
        }

        private Transform GetHandTransform(WeaponSO weaponSO)
        {
            Transform handTransform = null;

            switch (weaponSO.HandType)
            {
                case WeaponSO.WeaponHandType.leftHanded:
                    handTransform = leftHandTransform; break;
                case WeaponSO.WeaponHandType.rightHanded:
                    handTransform = rightHandTransform; break;
            }

            return handTransform;
        }

        // Animation Event
        void Hit()
        {
            if(targetTransform == null) return;

            float damageMultiplier = GetComponent<BaseStats>().GetStat(Stat.DamageMultiplier);

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(GetHandTransform(currentWeapon), gameObject, targetTransform, damageMultiplier);
                audioSource.PlayOneShot(currentWeapon.WeaponSound);
            } else
            {
                Health healthTarget = targetTransform.GetComponent<Health>();
                audioSource.PlayOneShot(currentWeapon.WeaponSound);
                float damage = currentWeapon.WeaponDamage * damageMultiplier;
                healthTarget.TakeDamage(gameObject, damage);
            }            
        }

        // Animation Event
        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange(Transform targetTransform) => Vector3.Distance(transform.position, targetTransform.position) <= currentWeapon.WeaponRange;
        public bool CanAttackTarget(GameObject target)
        {
            if(target == null) return false;
            if(!mover.CanMoveTo(target.transform.position) && !GetIsInRange(target.transform)) return false;

            Health targetHealth = target.transform.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetTransform = target.transform;

            if(target.TryGetComponent(out CombatTarget combatTarget))
            {
                combatTarget.EnemyTargeted();
            }

        }

        public void Cancel()
        {
            if(targetTransform != null && targetTransform.TryGetComponent(out CombatTarget combatTarget)) combatTarget.EnemyNoTargeted();
            targetTransform = null;
            mover.Cancel();
            StopAttackTrigger();
        }

        private void StopAttackTrigger()
        {
            animator.ResetTrigger(Dictionary.ATTACK_ANIMATOR);
            animator.SetTrigger(Dictionary.STOP_ATTACK_ANIMATOR);
        }

        public bool HasTarget() => targetTransform != null;
        public Health GetTargetHealth() => targetTransform.GetComponent<Health>();
        public WeaponSO GetCurrentWeapon() => currentWeapon;

    }
}
