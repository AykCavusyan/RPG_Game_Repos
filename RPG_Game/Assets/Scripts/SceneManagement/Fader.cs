using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            Debug.Log(canvasGroup.alpha);
            StartCoroutine(FadeOutIn(2f));
        }

        public IEnumerator FadeOutIn(float time)
        {
            yield return FadeOut(time);
            print("fadeed out");
            yield return FadeIn(time);
            print("faded in ");
        }


        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1) //
            {
                canvasGroup.alpha += Time.deltaTime / time;

                yield return null;
            }
                
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0) //
            {
                canvasGroup.alpha -= Time.deltaTime / time;

                yield return null;
            }

        }
    }
}
