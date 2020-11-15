using UnityEngine;

namespace aMVCc
{
    public class BaseElement : MonoBehaviour
    {
        [SerializeField] private Mediator _mediator = default;
        public Mediator Mediator { get => _mediator; }

        //private void Awake()
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //}
    }
}