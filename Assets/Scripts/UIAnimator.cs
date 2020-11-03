using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public enum Effect
    {
        Show = 0,
        Hide = 1
    }

    public static class UIAnimator
    {
        public static void AlphaEffect(GameObject obj,float time,Effect actionType, float delay = 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Pause();
            Graphic[] graphics =  obj.GetComponents<Graphic>();

            switch (actionType)
            {
                case Effect.Show:
                    {
                        StartShow(obj);
                        foreach (var item in graphics)
                        {
                            sequence.Join(item.DOColor(GetFullAlphaColor(item.color), time)).SetDelay(delay);
                        }
                    }
                    break;
                case Effect.Hide:
                    {
                        foreach (var item in graphics)
                        {
                            sequence.Join(item.DOColor(GetNoAlphaColor(item.color), time)).SetDelay(delay);
                        }

                        sequence.OnKill(() => { CompleteHide(obj); });
                    }
                    break;
            }
            sequence.Play();
        }
        private static Color GetNoAlphaColor(Color refColor)
        {
            Color tempColor = refColor;
            tempColor.a = 0;
            return tempColor;
        }
        private static Color GetFullAlphaColor(Color refColor)
        {
            Color tempColor = refColor;
            tempColor.a = 1;
            return tempColor;
        }
        private static void CompleteHide(GameObject obj)
        {
            obj.SetActive(false);
        }
        private static void StartShow(GameObject obj)
        {
            obj.SetActive(true);
        }
    }
}
