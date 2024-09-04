using RPG.Control;
using RPG.Core;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        public event Action onFinish;

        GameObject player;


        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag(Dictionary.PLAYER_TAG);
        }


        private void DisableControl(PlayableDirector director)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;

        }
        private void EnableControl(PlayableDirector director)
        {
            player.GetComponent<PlayerController>().enabled = true;

        }
    }
}
