using CodeBase.Logic.GravitySources;
using CodeBase.Services;

namespace CodeBase.Infrastructure.Factory
{
    public interface IMapFactory : IService
    {
        void InitialiseMapComponents(GravitySourse[] sourses);
    }
}