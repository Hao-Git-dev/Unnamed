using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{

}
public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Dictionary<EventType, UnityEvent> events = new Dictionary<EventType, UnityEvent>();
    public void Subscribed(EventType type, UnityAction e)
    {
        if (!events.ContainsKey(type)) events.Add(type, new());
        events[type].AddListener(e);
    }
    public void RemoveEvent(EventType type, UnityAction e)
    {
        if (!events.ContainsKey(type)) return;
        events[type].RemoveListener(e);
    }

    public void Distribute(EventType type)
    {
        if (!events.ContainsKey(type)) return;
        events[type].Invoke();
    }
}
