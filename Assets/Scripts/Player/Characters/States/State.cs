namespace Player.Characters.States
{
    public abstract class State
    {
        protected Character character;

        public abstract void Tick();
        public abstract void OnStateEnter();
        public abstract void OnStateExit();

        public State(Character character)
        {
            this.character = character;
        }
    }
}