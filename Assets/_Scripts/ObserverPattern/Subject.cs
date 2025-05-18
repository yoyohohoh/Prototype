using System.Collections.Generic;

public abstract class Subject : PersistentSingleton<PlayerController>
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(PlayerData playerData)
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(playerData);
        });
    }
}