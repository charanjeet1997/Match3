using UnityEngine.SceneManagement;

namespace ModulerUISystem
{
    using System.Collections;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class NavigationEventNotifier : MonoBehaviour, INavigatorCallback
    {
        [SerializeField] private UnityEvent onShowAnimationStart;
        [SerializeField] private UnityEvent onShowAnimationComplete;
        [SerializeField] private UnityEvent onHideAnimationStart;
        [SerializeField] private UnityEvent onHideAnimationComplete;
        [SerializeField] private FloatConditionalEvent[] onAnimationRunningDuringShow;
        [SerializeField] private FloatConditionalEvent[] onAnimationRunningDuringHide;

        public void OnNavigationStarted(Navigator navigator, TransitionType transitionType)
        {
            if (transitionType == TransitionType.Show)
            {
                onShowAnimationStart.Invoke();
            }
            else if (transitionType == TransitionType.Hide)
            {
                onHideAnimationStart.Invoke();
            }
        }

        public void OnNavigationComplete(Navigator navigator, TransitionType transitionType)
        {
            if (transitionType == TransitionType.Show)
            {
                onShowAnimationComplete.Invoke();
            }
            else if (transitionType == TransitionType.Hide)
            {
                onHideAnimationComplete.Invoke();
            }
           
        }

        public void OnNavigationRunning(Navigator navigator, TransitionType transitionType, float percentage)
        {
            if (transitionType == TransitionType.Show)
            {
                for (int indexOfEvent = 0; indexOfEvent < onAnimationRunningDuringShow.Length; indexOfEvent++)
                {
                    if (!onAnimationRunningDuringShow[indexOfEvent].isCalled)
                    {
                        onAnimationRunningDuringShow[indexOfEvent]
                            .CallEvent(onAnimationRunningDuringShow[indexOfEvent].IsValid(percentage));
                    }
                }
            }
            else if (transitionType == TransitionType.Hide)
            {
                for (int indexOfEvent = 0; indexOfEvent < onAnimationRunningDuringHide.Length; indexOfEvent++)
                {
                    if (!onAnimationRunningDuringHide[indexOfEvent].isCalled)
                    {
                        onAnimationRunningDuringHide[indexOfEvent]
                            .CallEvent(onAnimationRunningDuringHide[indexOfEvent].IsValid(percentage));
                    }
                }
            }
        }

        public void ResetCallback()
        {
            for (int indexOfEvent = 0; indexOfEvent < onAnimationRunningDuringShow.Length; indexOfEvent++)
            {
                onAnimationRunningDuringShow[indexOfEvent].ResetEvent();
            }

            for (int indexOfEvent = 0; indexOfEvent < onAnimationRunningDuringHide.Length; indexOfEvent++)
            {
                onAnimationRunningDuringHide[indexOfEvent].ResetEvent();
            }
        }

        public void PrintSomething(string printSomething)
        {
            Debug.Log(printSomething);
        }

        public int indexOfScene = 1;
        [ContextMenu("ChangeScene")]
        public void ChangeScene()
        {
            SceneManager.LoadScene(indexOfScene);
        }
    }

    [System.Serializable]
    public class ConditionalEvent<X> where X : IComparable
    {
        [HideInInspector] public bool isCalled;
        public UnityEvent conditionalEvent;

        public virtual bool IsValid(X targetValue)
        {
            return false;
        }

        public virtual void CallEvent(bool shouldCall)
        {
            if (shouldCall)
            {
                isCalled = true;
                conditionalEvent.Invoke();
            }
        }

        public virtual void ResetEvent()
        {
            isCalled = false;
        }
    }

    [System.Serializable]
    public class FloatConditionalEvent : ConditionalEvent<float>
    {
        [Range(0f, 1f)] public float percentage;

        [Dropdown("Condition", "Greater", "Lesser")]
        public int conditionType;

        public override bool IsValid(float targetValue)
        {
            switch (conditionType)
            {
                case 0:
                    if (percentage > targetValue)
                    {
                        return true;
                    }

                    break;
                case 1:
                    if (percentage < targetValue)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }
    }
}