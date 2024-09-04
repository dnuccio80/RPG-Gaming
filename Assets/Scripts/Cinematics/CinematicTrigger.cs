using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyPlayed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG) && !alreadyPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                alreadyPlayed = true;
            }
        }


    }
}
