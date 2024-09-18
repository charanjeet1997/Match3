namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class AlphaPropagator : Propogrator
    {
        #region PUBLIC_VARS
        public List<IPropogationReceiver<float>> propogationReceivers;
        [Range(0f,1f)]
        public float alpha;
        
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region PUBLIC_METHODS

        public override void OnPropogationStart()
        {
        }

        public override void OnPropogationEnd()
        {
        }

        public override void Propogate()
        {
               
        }
#if UNITY_EDITOR
        public override void AssignMembers(SlicePanel[] slicePanels)
        {
            
        }
        
#endif
        #endregion

        #region PRIVATE_METHODS

        #endregion
    }
}