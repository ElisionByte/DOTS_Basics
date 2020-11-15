using Observers;
using System.Collections.Generic;

namespace aMVCc.Models
{
    public class MapModel:BaseElement
    {
        public List<Observer> _observers = default;
        private Subject _subject;
        public bool IsLevelLoaded;
        public Subject Sublect { get => _subject; }

        public void Initialise()
        {
            IsLevelLoaded = true;
            _subject = new Subject();
            _subject.Attach(_observers);
        }
    }
}