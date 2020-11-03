using UnityEngine;

namespace Assets.Scripts.Hero 
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] protected CharacterController Controller = default;
        [SerializeField] protected Animator Animator;

        [SerializeField] protected float GRAVITY = 2f;

        [SerializeField] protected float Speed = 0f;
        [SerializeField] protected float TurnSmothTime = 0.2f;
        [SerializeField] protected float JumpSpeed = 0.5f;


    }
}

