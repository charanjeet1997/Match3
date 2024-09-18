namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    [RequireComponent(typeof(Animator))]
    public class AnimationPropagator : MonoBehaviour
    {
        public Animator animator;
        public SlicePanel[] associatedPanels;
        public Propogrator[] propagators;


        public void OnPropogationStart()
        {
            for (int indexOfPropagator = 0; indexOfPropagator < propagators.Length; indexOfPropagator++)
            {
                propagators[indexOfPropagator].OnPropogationStart();
            }
        }

        public void Propogate()
        {
            for (int indexOfPropagator = 0; indexOfPropagator < propagators.Length; indexOfPropagator++)
            {
                propagators[indexOfPropagator].Propogate();
            }
        }

        public void OnPropogationEnd()
        {
            for (int indexOfPropagator = 0; indexOfPropagator < propagators.Length; indexOfPropagator++)
            {
                propagators[indexOfPropagator].OnPropogationEnd();
            }
        }

        public void UpdateAnimations(AnimatorOverrideController overrideController)
        {
            animator.runtimeAnimatorController = overrideController;
        }

        public void TriggerAnimation(string parameterName)
        {
            animator.SetTrigger(parameterName);
        }
        #if UNITY_EDITOR
        private void AssignPropogatorMembers()
        {
            for (int indexOfSlicePanel = 0; indexOfSlicePanel < propagators.Length; indexOfSlicePanel++)
            {
                propagators[indexOfSlicePanel].AssignMembers(associatedPanels);
            }
        }
        #endif
    }
}