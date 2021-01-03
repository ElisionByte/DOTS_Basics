namespace CodeBase.Infrastructure.StateMachine
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}