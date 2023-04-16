using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void RegisterObserver(ref IObserver _observer);
    void RemoveObserver(ref IObserver _observer);
    void NotifyObservers();
}

public interface IObserver
{
    void Update();
}
