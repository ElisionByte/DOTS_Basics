using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Observer
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private List<Observer> _observers=default;

        private Subject _subject;

        private bool IsLevelLoaded = true;

        private void Awake()
        {
            _subject = new Subject();
        }

        private void Start()
        {
            _subject.Attach(_observers);
        }

        private void FixedUpdate()
        {
            if(IsLevelLoaded)
                _subject.Notify();
        }

        private void OnDestroy()
        {
            _subject.Detach();
        }
    }
}
