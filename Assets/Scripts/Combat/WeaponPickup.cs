using RPG.Control;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {

        [SerializeField] private WeaponSO weaponSO;
        [SerializeField] private float timeHiding = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                PickUp(other.gameObject.GetComponent<Fighter>());
            }
        }

        private void PickUp(Fighter fighter)
        {
            fighter.EquipWeapon(weaponSO);
            StartCoroutine(DisablePickUpForTime());
        }

        IEnumerator DisablePickUpForTime()
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(timeHiding);
            ShowPickUp(true);
        }

        private void ShowPickUp(bool mustShow)
        {
            GetComponent<Collider>().enabled = mustShow;
            transform.GetChild(0).gameObject.SetActive(mustShow);
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(Input.GetMouseButtonDown(0))
            {
                PickUp(callingController.GetComponent<Fighter>());
            }

            return true;
        }
    }
}


