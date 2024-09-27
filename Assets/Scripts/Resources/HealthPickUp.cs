using RPG.Control;
using RPG.Resources;
using UnityEngine;

public class HealthPickUp : Pickup
{
    [SerializeField] private float healthPoints;

    protected override void PickUp(GameObject player)
    {
        Health health = player.GetComponent<Health>();
        health.IncrementHealth(healthPoints);
        StartCoroutine(DisablePickUpForTime());

    }

}
