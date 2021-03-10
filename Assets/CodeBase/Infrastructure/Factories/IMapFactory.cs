using CodeBase.Logic.Map;
using CodeBase.Services;

using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IMapFactory : IService
    {
        void CreateProp(PropTypeID propTypeID, Transform transform);
        void CreatePropNotificator();
    }
}