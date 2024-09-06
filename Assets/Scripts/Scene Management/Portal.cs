using RPG.Control;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C , D
        }


        [SerializeField] private Dictionary.Scenes targetScene;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                StartCoroutine(TransitionScene());
            }
        }

        IEnumerator TransitionScene()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindAnyObjectByType<Fader>();
            GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG).GetComponent<PlayerController>().enabled = false;
            yield return fader.FadeOut(0.5f);
            yield return SceneManager.LoadSceneAsync(targetScene.ToString());
            Portal scenePortal = GetScenePortal();
            UpdatePlayer(scenePortal);
            yield return fader.FadeIn(0.5f);
            GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG).GetComponent<PlayerController>().enabled = true;
            Destroy(gameObject);
        }

        private Portal GetScenePortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();

            foreach (Portal portal in portals)
            {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;
                return portal;  
            }
            return null;
        }

        private void UpdatePlayer(Portal scenePortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG);

            player.GetComponent<NavMeshAgent>().Warp(scenePortal.spawnTransform.position);
            player.transform.rotation = scenePortal.spawnTransform.rotation;
        }



    }
}
