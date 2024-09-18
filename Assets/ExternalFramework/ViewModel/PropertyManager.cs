using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommanTickManager;
using nameSpaceName;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DataBindingFramework
{
    //We will have to implement interface where we check if the observer still have some subscribers or not
    //If not then we can remove the observer from the dictionary
    public interface IObserverSubscribersStatus
    {
        bool HasEnoughSubscribers();
    }

    // Interface for property management
    public interface IProperty<T> : IObserverSubscribersStatus
    {
        T Value { get; set; }
    }

    // Interface for property observers
    public interface IObserver : IObserverSubscribersStatus
    {
        void Bind(UnityEngine.Object owner, Action observer);
        void Unbind(Action observer);
        void Notify();
    }

    // Interface for property observers with one generic parameter
    public interface IObserver<T> : IObserverSubscribersStatus
    {
        void Bind(UnityEngine.Object owner, Action<T> observer);
        void Unbind(Action<T> observer);
        void Notify(T value);
    }


    // Implementation of the Observer class with one generic parameter
    public class Observer : IObserver
    {
        private Dictionary<Action, UnityEngine.Object> _observers;

        public Observer()
        {
            _observers = new Dictionary<Action, UnityEngine.Object>();
        }

        public void Bind(UnityEngine.Object owner, Action action)
        {
            if(_observers.ContainsKey(action))
            {
                Debug.LogError("Observer already contains the action");
            }
            else
            {
                _observers.Add(action, owner);
            }
        }

        public void Unbind(Action action)
        {
            Cleanup();
            if (_observers.ContainsKey(action))
            {
                _observers.Remove(action);
            }
        }

        private void Cleanup()
        {
            var actionsToRemove = new List<Action>();

            foreach (var pair in _observers)
            {
                if (pair.Value == null)
                {
                    actionsToRemove.Add(pair.Key);
                }
            }

            foreach (var tempAction in actionsToRemove)
            {
                _observers.Remove(tempAction);
            }
        }

        public void Notify()
        {
            var actionsToRemove = new List<Action>();

            foreach (var pair in _observers)
            {
                if (pair.Value != null)
                {
                    pair.Key();
                }
                else
                {
                    actionsToRemove.Add(pair.Key);
                }
            }

            foreach (var action in actionsToRemove)
            {
                _observers.Remove(action);
            }
        }

        public bool HasEnoughSubscribers()
        {
            Cleanup();

            return _observers.Count > 0;
        }
    }

    // Implementation of the Observer class with one generic parameter
    public class Observer<T> : IObserver<T>
    {
        private Dictionary<Action<T>, UnityEngine.Object> _observers;

        public Observer()
        {
            _observers = new Dictionary<Action<T>, UnityEngine.Object>();
        }

        public void Bind(UnityEngine.Object owner, Action<T> action)
        {
            if(_observers.ContainsKey(action))
            {
                Debug.LogError("Observer already contains the action");
            }
            else
            {
                _observers.Add(action, owner);
            }
        }

        public void Unbind(Action<T> action)
        {
            Cleanup();
            if (_observers.ContainsKey(action))
            {
                _observers.Remove(action);
            }
        }

        public void Notify(T value)
        {
            var actionsToRemove = new List<Action<T>>();

            foreach (var pair in _observers)
            {
                if (pair.Value != null)
                {
                    pair.Key(value);
                }
                else
                {
                    actionsToRemove.Add(pair.Key);
                }
            }

            foreach (var action in actionsToRemove)
            {
                _observers.Remove(action);
            }
        }

        private void Cleanup()
        {
            var actionsToRemove = new List<Action<T>>();

            foreach (var pair in _observers)
            {
                if (pair.Value == null)
                {
                    actionsToRemove.Add(pair.Key);
                }
            }

            foreach (var action in actionsToRemove)
            {
                _observers.Remove(action);
            }
        }
        
        public bool HasEnoughSubscribers()
        {
            Cleanup();
            return _observers.Count > 0;
        }
    }

    // Implementation of the Property class with optional observer
    public class Property<T> : IProperty<T>
    {
        private T _value;
        private Observer<T> _observer;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChanged();
            }
        }


        public Property(Observer<T> observer = null)
        {
            _observer = observer;
        }

        public void Bind(UnityEngine.Object owner,Action<T> observer)
        {
            EnsureObserver();
            _observer.Bind(owner,observer);
        }

        public void Unbind(Action<T> observer, string message = null)
        {
            EnsureObserver();
            _observer.Unbind(observer);
        }

        public void NotifyPropertyChanged()
        {
            EnsureObserver();
            _observer.Notify(_value);
        }

        private void EnsureObserver()
        {
            if (_observer == null)
            {
                _observer = new Observer<T>();
            }
        }

        // Factory method to create a property with an optional observer
        public static Property<T> CreateWithObserver(Observer<T> observer = null)
        {
            return new Property<T>(observer);
        }

        public bool HasEnoughSubscribers()
        {
            EnsureObserver();
            return _observer.HasEnoughSubscribers();
        }
    }


    // Interface for property managers
    public interface IPropertyManager
    {
        Property<T> CreateProperty<T>(string key, Observer<T> observer = null);
        Property<T> GetOrCreateProperty<T>(string key);
        void RemoveProperty<T>(string key);
    }

    // Implementation of the Property Manager
    public class PropertyManager : IPropertyManager
    {
        private List<string> notifiers = new List<string>();
        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        private int cleanupTime = 5000;
        private Thread cleanupThread;

        public PropertyManager(int cleanupTimeInSeconds = 5)
        {
            this.cleanupTime = cleanupTimeInSeconds * 1000;
            cleanupThread = new Thread(CleanupLoop);
            cleanupThread.Start();
        }

        ~PropertyManager()
        {
            cleanupThread.Abort();
        }

        public Property<T> CreateProperty<T>(string key, Observer<T> observer = null)
        {
            var property = Property<T>.CreateWithObserver(observer);
            _properties[key] = property;
            notifiers.Add(key);
            return property;
        }

        public Property<T> GetOrCreateProperty<T>(string key)
        {
            if (_properties.TryGetValue(key, out var propertyObj))
            {
                return (Property<T>)propertyObj;
            }

            return CreateProperty<T>(key);
        }

        public void RemoveProperty<T>(string key)
        {
            notifiers.Remove(key);
        }

        public void CleanUnusedProperties()
        {
            foreach (var tempPropertyKey in _properties.Keys.ToList())
            {
                if (!notifiers.Contains(tempPropertyKey) &&
                    _properties.TryGetValue(tempPropertyKey, out var observerObj))
                {
                    var property = (IObserverSubscribersStatus)observerObj;
                    if (!property.HasEnoughSubscribers())
                    {
                        _properties.Remove(tempPropertyKey);
                    }
                }
            }
        }

        public void CleanupLoop()
        {
            while (true)
            {
                Thread.Sleep(cleanupTime);
                CleanUnusedProperties();
            }
        }
    }

    //Interface for observer manager
    public interface IObserverManager
    {
        IObserver CreateObserver(string key);
        IObserver GetOrCreateObserver(string key);
        IObserver<T> CreateObserver<T>(string key);
        IObserver<T> GetOrCreateObserver<T>(string key);
        void RemoveObserver(string key);
    }

    //Implementation of observer manager
    public class ObserverManager : IObserverManager
    {
        private List<string> notifiers = new List<string>();
        private Dictionary<string, object> _observers = new Dictionary<string, object>();
        private int cleanupTime = 5000;

        private Thread cleanupThread;

        public ObserverManager(int cleanupTimeInSeconds = 5)
        {
            this.cleanupTime = cleanupTimeInSeconds * 1000;
            cleanupThread = new Thread(CleanupLoop);
            cleanupThread.Start();
        }

        ~ObserverManager()
        {
            cleanupThread.Abort();
        }

        public IObserver CreateObserver(string key)
        {
            var observer = new Observer();
            _observers[key] = observer;
            notifiers.Add(key);
            return observer;
        }

        public IObserver<T> CreateObserver<T>(string key)
        {
            var observer = new Observer<T>();
            _observers[key] = observer;
            notifiers.Add(key);
            return observer;
        }

        public IObserver GetOrCreateObserver(string key)
        {
            if (_observers.TryGetValue(key, out var observerObj))
            {
                return (IObserver)observerObj;
            }

            return CreateObserver(key);
        }

        public IObserver<T> GetOrCreateObserver<T>(string key)
        {
            if (_observers.TryGetValue(key, out var observerObj))
            {
                return (IObserver<T>)observerObj;
            }

            return CreateObserver<T>(key);
        }

        public void RemoveObserver(string key)
        {
            notifiers.Remove(key);
        }

        public void CleanUnusedObservers()
        {
            foreach (var key in _observers.Keys.ToList())
            {
                if (!notifiers.Contains(key) && _observers.TryGetValue(key, out var observerObj))
                {
                    var observer = (IObserverSubscribersStatus)observerObj;
                    if (!observer.HasEnoughSubscribers())
                    {
                        _observers.Remove(key);
                    }
                }
            }
        }

        public void CleanupLoop()
        {
            while (true)
            {
                Thread.Sleep(cleanupTime);
                CleanUnusedObservers();
            }
        }
    }
}