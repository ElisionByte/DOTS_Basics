using CodeBase.Services;

using UnityEngine;

public interface IInputService : IService
{
    Vector2 Axis { get; }
    float RightValue { get; }
    float ForwardValue { get; }

    bool IsJumpPressed { get; }
}