using System;
using UnityEngine;

namespace Game.Factories
{
    public interface IManagedLifecycle<T> where T : Component
    {
        void Activate(); // Called when the object is created/activated
        void Deactivate(); // Called when the object is returned/deactivated
        void Initialize(IFactory<T> factory=null); // Called to reset the object to its initial state
    }
}