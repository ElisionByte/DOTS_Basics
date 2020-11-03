using Assets.Scripts.Hero;
using UnityEngine;

public class ReturnToCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IHero>().ToCheckPoint();
    }
}
