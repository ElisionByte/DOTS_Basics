using CodeBase.Services;
using CodeBase.Infrastructure.StateMachine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine gameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            this.gameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, loadingCurtain);
        }
    }
}