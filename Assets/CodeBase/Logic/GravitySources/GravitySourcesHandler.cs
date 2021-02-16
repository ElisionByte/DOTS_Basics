using UnityEngine;

namespace CodeBase.Logic.GravitySources
{
    public class GravitySourcesHandler : MonoBehaviour
    {
        [SerializeField]
        private GravitySourse[] _allGravitySources;

        public GravitySourse[] GravitySources => _allGravitySources;
    }
}