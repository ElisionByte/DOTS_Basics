using CodeBase.Infrastructure.Factories;

using UnityEngine;

namespace CodeBase.Logic.Map
{
    public class MapPropSpawner : MonoBehaviour
    {
        public PropTypeID propTypeID;
        private string _id;

        private IMapFactory _mapFactory;

        public void Construct(IMapFactory mapFactory)
        {
            _id = GetComponent<UniqueID>().ID;
            _mapFactory = mapFactory;
        }

        public void Spawn()
        {
            _mapFactory.CreateProp(propTypeID,transform);
        }
    }
}