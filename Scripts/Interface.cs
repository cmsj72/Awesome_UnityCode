using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void RegisterObserver(IObserver _observer);
    void RemoveObserver(IObserver _observer);
    void NotifyObservers();
}

public interface IObserver
{
    void Update();
}
