namespace Assets.Scripts.Hero
{
    public interface IHero
    {
        void Walk();
        void Rotate();
        void Run();
        void JumpCheck();
        void ToCheckPoint();
    }
}
