using UnityEditor;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Navigator
    {
        public TransitionState currentStage;
        private float currentTime;
        private Transition transition;
        private INavigatorCallback[] navigatorCallbacks;
        private TransitionType transitionType;
        private float percentage;
        private bool shouldBlockInteraction;

        public Navigator(Transition transition, TransitionType transitionType, bool shouldBlockInteraction,
            float numberOfFramesToAdvance, params INavigatorCallback[] navigatorCallback)
        {
            this.currentTime = Time.fixedDeltaTime * numberOfFramesToAdvance;
            this.percentage = 0f;
            this.transition = transition;
            this.navigatorCallbacks = navigatorCallback;
            this.transitionType = transitionType;
            this.shouldBlockInteraction = shouldBlockInteraction;
            OnNavigatorReset();
            OnNavigatorStarted();
        }

        public void Navigate()
        {
            if (currentTime <= transition.duration)
            {
                currentTime += Time.fixedDeltaTime;
                percentage = Mathf.Clamp01(currentTime / transition.duration);
                transition.OnAnimationRunning(percentage);
                OnNavigatorRunning(percentage);
            }
            else
            {
                OnNavigatorCompleted();
            }
        }

        public void OnNavigatorRunning(float percentage)
        {
            currentStage = TransitionState.Running;
            for (int indexOfNavigatorCallback = 0;
                indexOfNavigatorCallback < navigatorCallbacks.Length;
                indexOfNavigatorCallback++)
            {
                navigatorCallbacks[indexOfNavigatorCallback].OnNavigationRunning(this, transitionType, percentage);
            }
        }

        public void OnNavigatorStarted()
        {
            currentStage = TransitionState.Started;
            transition.OnAnimationStarted();
            for (int indexOfNavigatorCallback = 0;
                indexOfNavigatorCallback < navigatorCallbacks.Length;
                indexOfNavigatorCallback++)
            {
                navigatorCallbacks[indexOfNavigatorCallback].OnNavigationStarted(this, transitionType);
            }
        }

        public void OnNavigatorCompleted()
        {
            currentStage = TransitionState.Completed;
            transition.OnAnimationEnded();

            for (int indexOfNavigatorCallback = 0;
                indexOfNavigatorCallback < navigatorCallbacks.Length;
                indexOfNavigatorCallback++)
            {
                navigatorCallbacks[indexOfNavigatorCallback].OnNavigationComplete(this, transitionType);
            }
        }

        public void CompleteNavigationBeforeTime()
        {
            currentTime = transition.duration;
        }

        public void OnNavigatorReset()
        {
            for (int indexOfNavigatorCallback = 0;
                indexOfNavigatorCallback < navigatorCallbacks.Length;
                indexOfNavigatorCallback++)
            {
                navigatorCallbacks[indexOfNavigatorCallback].ResetCallback();
            }
        }

        public bool ShouldBlockInteraction
        {
            get => shouldBlockInteraction;
            set => shouldBlockInteraction = value;
        }
    }
}