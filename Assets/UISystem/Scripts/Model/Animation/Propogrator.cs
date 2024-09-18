namespace ModulerUISystem
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Propogrator : MonoBehaviour
    {
        public abstract void OnPropogationStart();
        public abstract void Propogate();
        public abstract void OnPropogationEnd();

#if UNITY_EDITOR
        public abstract void AssignMembers(SlicePanel[] slicePanels);
#endif
    }
}