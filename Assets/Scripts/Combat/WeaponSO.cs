using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon")]
    public class WeaponSO : ScriptableObject
    {

        [SerializeField] private float weaponRange;
        [SerializeField] private float weaponDamage;
        [SerializeField] private AnimatorOverrideController weaponOverrideController;
        [SerializeField] private GameObject weaponPrefab;


        public float WeaponRange { get { return weaponRange; } }
        public float WeaponDamage { get { return weaponDamage; } }
        public AnimatorOverrideController AnimatorOverrideController { get { return weaponOverrideController; } }
        public GameObject WeaponPrefab { get {  return weaponPrefab; } }

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(weaponPrefab, handTransform);
            animator.runtimeAnimatorController = weaponOverrideController;
        }
    }
}


