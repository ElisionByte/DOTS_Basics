namespace CodeBase.Services
{
    public class AllServices
    {
        private static AllServices instance = new AllServices();
        public static AllServices Container => instance;

        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;
        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private class Implementation<TService> where TService : IService
        {
            private static TService serviceInstance;
            public static TService ServiceInstance
            {
                get => serviceInstance;
                set => serviceInstance = value;
            }
        }
    }
}