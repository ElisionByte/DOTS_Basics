
using CodeBase.Services;
using UnityEngine;

public interface IInputService : IService
{
    Vector2 Axis { get; }
    Vector3 Right { get; }
    Vector3 Forward { get; }

    bool IsJumpPressed { get; }
}