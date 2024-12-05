using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasgroup;
        void Start()
        {
            canvasgroup = GetComponent<CanvasGroup>();
        }
        public IEnumerator FadeOut(float time)
        {
            while (canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += Time.deltaTime/time;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            while (canvasgroup.alpha > 0)
            {
                canvasgroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
