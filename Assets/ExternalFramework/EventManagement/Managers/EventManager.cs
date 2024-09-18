using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace EventManagement
{
    public class EventManager : MonoBehaviour
    {
        
        public struct Event
        {
            public string eventName;
            public List<UnityEvent> events;

            public Event(string _eventName, UnityEvent _unityEvent)
            {
                eventName = _eventName;
                events = new List<UnityEvent>();
                events.Add(_unityEvent);
            }
            public void Register(UnityEvent thisEvent)
            {
                events.Add(thisEvent);
            }

            public void Unregister(UnityEvent thisEvent)
            {
                events.Remove(thisEvent);
            }
        }
        public static List<Event> myEvents = new List<Event>();
        public static void Raise(string eventName)
        {
            int index = GetIndex(eventName);
            if (index != -1)
            {
                for (int j = 0; j < myEvents[index].events.Count; j++)
                {
                    myEvents[index].events[j].Invoke();
                }
            }
        }

        public static void RegisterEvent(string eventName, UnityEvent unityEvent)
        {
            int index = GetIndex(eventName);
            if (index != -1)
            {
                myEvents[index].Register(unityEvent);
                return;
            }
            myEvents.Add(new Event(eventName, unityEvent));
        }
        public static void Unregister(string eventName, UnityEvent unityEvent)
        {
            int index = GetIndex(eventName);
            if (index != -1)
            {
                myEvents[index].Unregister(unityEvent);
                return;
            }
        }
        static int GetIndex(string eventName)
        {
            return myEvents.FindIndex(delegate (Event Event) { return Event.eventName.Equals(eventName); });
        }
    }
}
