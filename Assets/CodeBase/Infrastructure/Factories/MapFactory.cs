using System.Collections.Generic;

using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.JobSystems.Map;
using CodeBase.Logic.Map;
using CodeBase.Services.Map;

using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public class MapFactory : IMapFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IMapJobsSystemService _mapJobsSystemService;

        private List<UpAndDownCube> _upAndDownCubes;

        public MapFactory(IAssetProvider assetProvider, IMapJobsSystemService mapJobsSystemService)
        {
            _mapJobsSystemService = mapJobsSystemService;
            _assetProvider = assetProvider;

            _upAndDownCubes = new List<UpAndDownCube>();
        }

        public void CreateProp(PropTypeID propTypeID, Transform transform)
        {
            UpAndDownCube tempObj = _assetProvider.Instantiate(AssetPaths.upAndDownCubePath, transform.position).GetComponent<UpAndDownCube>();
            tempObj.Construct(Random.Range(10f, 20f), Random.Range(0.5f, 2f), 0.01f);
            _upAndDownCubes.Add(tempObj);
        }

        public void CreatePropNotificator() =>
            _assetProvider.Instantiate(AssetPaths.mapObserverNotificator)
                          .GetComponent<MapObserverNotificator>()
                          .Construct(_mapJobsSystemService, _upAndDownCubes);
    }
}