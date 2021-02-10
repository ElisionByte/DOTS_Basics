using CodeBase.Services;

using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IHeroFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
    }
}