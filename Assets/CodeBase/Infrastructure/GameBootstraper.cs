using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.StateMachine;

using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstraper : MonoBehaviour,ICoroutineRunner
    {
        private Game game;

        private void Start()
        {
            game = new Game(this, Instantiate(Resources.Load<LoadingCurtain>(AssetPaths.loadingCurtainPath)));
            game.gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}