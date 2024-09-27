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
            gameObject.transform.SetParent(null);

            DontDestroyOnLoad(gameObject);
            
            Fader fader = FindObjectOfType<Fader>();
            DisablePlayerController();
            yield return fader.FadeOut(0.5f);
            // Save player Data
            yield return SceneManager.LoadSceneAsync(targetScene.ToString());
            // Load Player Data
            DisablePlayerController();
            Portal scenePortal = GetScenePortal();
            UpdatePlayer(scenePortal);
            yield return fader.FadeIn(0.5f);
            EnablePlayerController();
            Destroy(gameObject);
        }

        private static void EnablePlayerController()
        {
            GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG).GetComponent<PlayerController>().enabled = true;
        }

        private static void DisablePlayerController()
        {
            GameObject.FindGameObjectWithTag(Dictionary.PLAYER_TAG).GetComponent<PlayerController>().enabled = false;
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
