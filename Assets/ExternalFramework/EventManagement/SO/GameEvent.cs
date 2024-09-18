
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EventManagement
{

    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        public delegate void EventDelegate<T>(T e) where T : GameData;

        private Dictionary<System.Type, System.Delegate> delegates = new Dictionary<System.Type, System.Delegate>();

        public void Add<T>(EventDelegate<T> del) where T : GameData
        {
            if (delegates.ContainsKey(typeof(T)))
            {
                System.Delegate tempDel = delegates[typeof(T)];
                delegates[typeof(T)] = System.Delegate.Combine(tempDel, del);
            }
            else
            {
                delegates[typeof(T)] = del;
            }
        }

        public void Remove<T>(EventDelegate<T> del) where T : GameData
        {
            if (delegates.ContainsKey(typeof(T)))
            {
                var currentDel = System.Delegate.Remove(delegates[typeof(T)], del);

                if (currentDel == null)
                {
                    delegates.Remove(typeof(T));
                }
                else
                {
                    delegates[typeof(T)] = currentDel;
                }
            }
        }

        public void Invoke(GameData e)
        {
            if (e == null)
            {
                Debug.Log("Invalid event argument: " + e.GetType().ToString());
                return;
            }

            if (delegates.ContainsKey(e.GetType()))
            {
                delegates[e.GetType()].DynamicInvoke(e);
            }
        }
        public GameData gameData;
        [ContextMenu("Raise")]
        public void Raise()
        {
            Invoke(gameData);
        }
    }
}
