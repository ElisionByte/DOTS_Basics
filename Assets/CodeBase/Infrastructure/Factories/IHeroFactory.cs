using CodeBase.Services;

using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IHeroFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
    }
}