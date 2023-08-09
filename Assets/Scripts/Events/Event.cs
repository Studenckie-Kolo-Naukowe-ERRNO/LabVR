using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nowe wydarzenie", menuName = "Eventy/Nowy event")]
public class Event : ScriptableObject
{
    [SerializeField] private int eventValue;
    private List<EventListener> listeners = new List<EventListener>();
    public void Raise() 
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(eventValue);
        }
    }
    public void RegisterListener(EventListener listener) 
    {
        listeners.Add(listener);
    }
    public void UnRegisterListener(EventListener listener)
    {
        listeners.Remove(listener);
    }

}
