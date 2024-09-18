using System;
using UnityEngine;

namespace StateStuff
{
    // [Serializable]
    public class StateMachine<T>
    {
        public string currentStateName;
        public State<T> currentState { get; private set; }
        public State<T> prevState { get; private set; }
        public T Owner;

        public StateMachine(T _o)
        {
            Owner = _o;
            currentState = null;
        }

        public void ChangeState(State<T> _newstate)
        {
            if (currentState != null)
            {
                prevState = currentState;
                currentState.ExitState(Owner);
            }
            currentState = _newstate;
            currentStateName = _newstate.ToString();
            currentState.EnterState(Owner);   
        }

        public void Update()
        {
            if (currentState != null)
            {
                // Debug.Log("Current State : "+currentState.ToString());
                currentState.UpdateState(Owner);
            }
        }

        public void Lateupdate()
        {
            if (currentState != null)
            {
                currentState.LateUpdateState(Owner);
            }
        }
    }
    
    public abstract class State<T>
    {
        public abstract void EnterState(T owner);
        public abstract void ExitState(T owner);
        public abstract void UpdateState(T owner);
        public abstract void LateUpdateState(T owner);
    }
}