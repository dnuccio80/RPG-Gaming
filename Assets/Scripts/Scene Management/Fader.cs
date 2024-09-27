using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;
        Coroutine currectActiveCoroutine;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }


        public IEnumerator FadeOut(float time)
        {
            return Fade(.5f, time);
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0, time);
        }

        public IEnumerator Fade(float target, float time) 
        {
            if (currectActiveCoroutine != null) StopCoroutine(currectActiveCoroutine);

            currectActiveCoroutine = StartCoroutine(FadeRoutine(target, time));
            yield return currectActiveCoroutine;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime/time);
                yield return null;
            }
        }

    }
}
