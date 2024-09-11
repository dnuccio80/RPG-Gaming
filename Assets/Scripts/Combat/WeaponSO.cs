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


        public float WeaponRange { get { return weaponRange; } }
        public float WeaponDamage { get { return weaponDamage; } }
        public AnimatorOverrideController AnimatorOverrideController { get { return weaponOverrideController; } }
        public GameObject WeaponPrefab { get {  return weaponPrefab; } }
        public WeaponHandType HandType { get { return handType; } }
        public bool HasProjectile() { return projectile != null; }

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(weaponPrefab != null) Instantiate(weaponPrefab, handTransform);
            if(weaponOverrideController != null) animator.runtimeAnimatorController = weaponOverrideController;
        }

        public void LaunchProjectile(Transform handTransform, Transform target)
        {
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(target);
            projectileInstance.SetDamage(WeaponDamage);

        }


    }
}


