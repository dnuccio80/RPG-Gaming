using RPG.Combat;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {

        [SerializeField] private WeaponSO weaponSO;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                other.gameObject.GetComponent<Fighter>().EquipWeapon(weaponSO);
                Destroy(gameObject);
            }
        }
    }
}


