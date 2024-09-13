using RPG.Combat;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {

        [SerializeField] private WeaponSO weaponSO;
        [SerializeField] private float timeHiding = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                other.gameObject.GetComponent<Fighter>().EquipWeapon(weaponSO);
                StartCoroutine(DisablePickUpForTime()); 
            }
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

    }
}


