namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class NormalizePositionPropagator : Propogrator
    {   
        public NormalizePositionController[] members;
        [SerializeField]
        [VectorRange(-1f,1f)]
        private Vector2 normalizedPosition;

        public override void Propogate()
        {

            for (int indexOfMembers = 0; indexOfMembers < members.Length; indexOfMembers++)
            {
                members[indexOfMembers].OnPropogationReceiveStart(normalizedPosition);
            }
        }

        public override void OnPropogationStart()
        {

            for (int indexOfMembers = 0; indexOfMembers < members.Length; indexOfMembers++)
            {
                members[indexOfMembers].OnPropogationReceiveStart(normalizedPosition);
            }
        }

        public override void OnPropogationEnd()
        {
            for (int indexOfMembers = 0; indexOfMembers < members.Length; indexOfMembers++)
            {
                members[indexOfMembers].OnPropogationReceiveStart(normalizedPosition);
            }
        }
#if UNITY_EDITOR
        public override void AssignMembers(SlicePanel[] slicePanels)
        {
            List<NormalizePositionController> propogationReceivers = new List<NormalizePositionController>();
            for (int indexOfSlicePanel = 0; indexOfSlicePanel < slicePanels.Length; indexOfSlicePanel++)
            {
                propogationReceivers.Add(slicePanels[indexOfSlicePanel].GetComponent<NormalizePositionController>()); 
            }
            members = propogationReceivers.ToArray();
        }
#endif
    }
}