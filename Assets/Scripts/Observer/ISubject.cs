namespace Observer
{
    interface ISubject
    {
        void Attach(System.Collections.Generic.List<Observer> observers);
        void Detach();
        void Notify();
    }
}
