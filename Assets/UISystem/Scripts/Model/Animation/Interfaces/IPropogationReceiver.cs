namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public interface IPropogationReceiver<T>
    {
        void OnPropogationReceiveStart(T val);
        void OnPropogationReceive(T val);
        void OnPropogationReceiveEnd(T val);
    }
}
