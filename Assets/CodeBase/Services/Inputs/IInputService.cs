using CodeBase.Services;

public interface IInputService : IService
{
    UnityEngine.Vector2 Axis { get; }

    bool IsJumpPressed { get; }
}