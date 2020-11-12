using UnityEngine;

namespace aMVCc
{
    public class BaseElement : MonoBehaviour
    {
        [SerializeField] private Application _application;
        public Application MainApplication { get => _application; }
    }
}
