
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class AnimatorTransitionData : Transition
    {
        public AnimatorOverrideController overrideController;
        public Slice[] slices;
        public string triggerName;
        public AnimatorTransitionData( AnimatorOverrideController overrideController,string triggerName,Slice[] slices,float duration, SlicePanel[] slicePanels) : base(duration, slicePanels)
        {
            this.overrideController = overrideController;
            this.triggerName = triggerName;
            this.slices = slices;
        }

        public override void OnAnimationRunning(float percentage)
        {
            
        }

        public override void OnAnimationEnded()
        {
            
        }

        public override void OnAnimationStarted()
        {
            for (int indexOfSlice = 0; indexOfSlice < slices.Length; indexOfSlice++)
            {
                slices[indexOfSlice].animationTargets.UpdateAnimations(UIConstants.Default,overrideController);
                slices[indexOfSlice].animationTargets.TriggerAnimation(triggerName);
            }
        }
    }
}