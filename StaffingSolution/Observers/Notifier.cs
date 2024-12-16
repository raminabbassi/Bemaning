using System;
using System.Collections.Generic;
using StaffingSolution.Interfaces;

namespace StaffingSolution.Observers
{
    public class Notifier
    {
        private readonly List<IObserver> _observers = new();

        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}
