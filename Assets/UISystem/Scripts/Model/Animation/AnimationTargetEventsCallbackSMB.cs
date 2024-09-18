using UnityEngine.Animations;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AnimationTargetEventsCallbackSMB : StateMachineBehaviour
    {
        private AnimationTargetEvents targetEvents;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
            targetEvents = animator.GetComponent<AnimationTargetEvents>();
            targetEvents?.OnAnimationStarted();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex, controller);
            targetEvents?.OnAnimationRunning();

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            base.OnStateExit(animator, stateInfo, layerIndex, controller);
            targetEvents?.OnAnimationEnded();
        }
    }
}