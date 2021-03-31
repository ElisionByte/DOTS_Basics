using System.Collections;

using UnityEngine;

public class ParticleHider : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    private Coroutine _hideCoroutine;

    private void OnEnable()
    {
        _hideCoroutine = StartCoroutine(Hide(ParticleSystem.main.startLifetime.constant));
    }
    private void OnDisable()
    {
        StopCoroutine(_hideCoroutine);
    }

    private IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        ParticleSystem.gameObject.SetActive(false);
    }
}
