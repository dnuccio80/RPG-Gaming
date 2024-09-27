using RPG.Control;
using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour, IRaycastable
{
    [SerializeField] private float timeHiding = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG))
        {
            PickUp(other.gameObject);
        }
    }

    protected virtual void PickUp(GameObject player) { }

    protected IEnumerator DisablePickUpForTime()
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
        if (Input.GetMouseButtonDown(0))
        {
            PickUp(callingController.gameObject);
        }

        return true;
    }

    public CursorType GetCursorType()
    {
        return CursorType.PickUp;
    }
}
