using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public enum WeaponHandType
        {
            leftHanded,
            rightHanded,
        }

        [SerializeField] private float weaponRange;
        [SerializeField] private float weaponDamage;
        [SerializeField] private AnimatorOverrideController weaponOverrideController;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private WeaponHandType handType;
        [SerializeField] private Projectile projectile;
        [SerializeField] private AudioClip weaponSound;

        public float WeaponRange { get { return weaponRange; } }
        public float WeaponDamage { get { return weaponDamage; } }
        public AnimatorOverrideController AnimatorOverrideController { get { return weaponOverrideController; } }
        public GameObject WeaponPrefab { get {  return weaponPrefab; } }
        public WeaponHandType HandType { get { return handType; } }
        public AudioClip WeaponSound {  get { return weaponSound; } }
        public bool HasProjectile() { return projectile != null; }


        public void Spawn(Transform handTransform, Animator animator)
        {
            if(weaponPrefab != null) Instantiate(weaponPrefab, handTransform);

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if(weaponOverrideController != null) animator.runtimeAnimatorController = weaponOverrideController;
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

        }

        public void DestroyOldWeapon(Transform handTransform)
        {

            if (handTransform.childCount == 0) return; 
                
            Transform oldWeapon = handTransform.GetChild(0);

            if (oldWeapon == null) return;

            Destroy(oldWeapon.gameObject);

        }

        public void LaunchProjectile(Transform handTransform, GameObject instigator, Transform target, float damageMultiplier)
        {
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            SetprojectileVariables(projectileInstance, target, instigator, damageMultiplier);
        }

        private void SetprojectileVariables(Projectile projectile, Transform target, GameObject instigator, float damageMultiplier)
        {
            projectile.SetTarget(target);
            projectile.SetInstigator(instigator);
            float damage = WeaponDamage * damageMultiplier;
            projectile.SetDamage(damage);
        }


    }
}


