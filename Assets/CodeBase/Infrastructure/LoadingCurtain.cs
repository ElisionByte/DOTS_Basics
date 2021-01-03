using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour, IWindow
    {
        public CanvasGroup canvasGroup;
        public Slider slider;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Open()
        {
            this.gameObject.SetActive(true);
            canvasGroup.alpha = 1;
        }
        public void Close()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (canvasGroup.alpha>0)
            {
                canvasGroup.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            this.gameObject.SetActive(false);
        }
    }
}