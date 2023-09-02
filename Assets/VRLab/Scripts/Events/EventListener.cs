using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public Event wydarzenie;
    public UnityEvent<int> responseInt;
    private void OnEnable()
    {
        wydarzenie.RegisterListener(this);
    }
    private void OnDisable()
    {
        wydarzenie.UnRegisterListener(this);
    }
    public void OnEventRaised(int przelsane)
    {
        responseInt.Invoke(przelsane);
    }
}