using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : Pickup
    {

        [SerializeField] private WeaponSO weaponSO;

        protected override void PickUp(GameObject player)
        {
            Fighter fighter = player.GetComponent<Fighter>();

            fighter.EquipWeapon(weaponSO);
            StartCoroutine(DisablePickUpForTime());
        }

    }
}


