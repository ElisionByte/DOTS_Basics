using DG.Tweening;
using UnityEngine;

namespace AnimationHandlers
{
    public static class ObjectAnimator
    {
        public static void UpScale(GameObject obj,float time)
        {
            Transform objTransform = obj.transform;
            objTransform.localScale = Vector3.zero;
            objTransform.DOScale(Vector3.one, time);
        }
    }
}
