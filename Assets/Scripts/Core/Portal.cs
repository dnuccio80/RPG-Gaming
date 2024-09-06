using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        [SerializeField] private Dictionary.Scenes targetScene;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                SceneManager.LoadScene(targetScene.ToString());
            }
        }



    }
}
