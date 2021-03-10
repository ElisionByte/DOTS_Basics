namespace CodeBase.Logic.Hero.Animation
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState State { get; }
    }
}