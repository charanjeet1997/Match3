using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModulerUISystem
{
    public class Animatable : MonoBehaviour
    {
        public bool shouldAnimateWhenScreenIsShowed = true;
        public bool shouldAnimateOnEnable=false;
        public UIAnimationType animationType;
        public AnimationCycleType cycleType;
        public AnimationCurve curve;
        [HideInInspector] public bool isAnimationRunning = false;
        protected RectTransform rectTransform;
        public float cycleDelay;
        public float delay = 0;
        public float duration = 0.4f;
        private IEnumerator animationRoutine;
        WaitForSeconds waitForSeconds;
        WaitForSeconds waitForCycleDelay;
        public List<AnimationEvent> animationEvents;

        public virtual void Awake()
        {
            CacheComponents();
        }

        public void OnEnable()
        {
            if (shouldAnimateOnEnable)
            {
                StartAnimate();
            }
        }

        void CacheComponents()
        {
            waitForSeconds = new WaitForSeconds(delay);
            waitForCycleDelay = new WaitForSeconds(cycleDelay);
            rectTransform = GetComponent<RectTransform>();
        }

        public void Validate()
        {
            waitForSeconds = new WaitForSeconds(delay);
            waitForCycleDelay = new WaitForSeconds(cycleDelay);
        }

        public void StartAnimate()
        {
            if (gameObject.activeInHierarchy)
            {
                StopAllCoroutines();
                animationRoutine = AnimateShow();
                StartCoroutine(animationRoutine);
            }
        }

        public void StopAnimate()
        {
            if (animationRoutine != null)
            {
                StopCoroutine(animationRoutine);
                OnAnimationEnded();
            }
        }

        public IEnumerator AnimateShow()
        {
            float elapsed = 0;
            float perc = 0;
            OnAnimationStarted();
            CheckForAnimationEvent(elapsed);
            yield return waitForSeconds;
            while (elapsed <= duration)
            {
                perc = elapsed / duration;
                CheckForAnimationEvent(elapsed);
                OnAnimationRunning(curve.Evaluate(perc));
                elapsed += Time.fixedDeltaTime;
                if (cycleType == AnimationCycleType.Continuous && elapsed > duration)
                {
                    elapsed = 0;
                    ResetAnimationEvent();
                    yield return waitForCycleDelay;
                }

                yield return null;
            }
            CheckForAnimationEvent(elapsed);
            OnAnimationEnded();
            yield return null;
        }

        public virtual void OnAnimationStarted()
        {
            isAnimationRunning = false;
            foreach (AnimationEvent animEvent in animationEvents)
            {
                animEvent.isCalled = false;
            }
        }

        public virtual void OnAnimationEnded()
        {
            isAnimationRunning = false;
        }

        public virtual void OnAnimationRunning(float percentage)
        {
            isAnimationRunning = true;
        }

        public GameObject FindBaseUI(GameObject childObject)
        {
            Transform t = childObject.transform;
            while (t.parent != null)
            {
                if (t.parent.GetComponent<UIAnimator>() != null)
                {
                    return t.parent.gameObject;
                }

                t = t.parent.transform;
            }

            return null;
        }

        int counter = 0;

        public void CheckForAnimationEvent(float elapsed)
        {
            for (counter = animationEvents.Count - 1; counter >= 0; counter--)
            {
                if (animationEvents[counter].time <= elapsed && !animationEvents[counter].isCalled)
                {
                    animationEvents[counter].Event.Invoke();
                    animationEvents[counter].isCalled = true;
                }
            }
        }

        public void ResetAnimationEvent()
        {
            for (int indexOfAnimation = 0; indexOfAnimation < animationEvents.Count; indexOfAnimation++)
            {
                animationEvents[indexOfAnimation].isCalled = false;
            }
        }

        private void OnValidate()
        {
            if (shouldAnimateWhenScreenIsShowed)
            {
                if (FindBaseUI(this.gameObject) != null)
                {
                    FindBaseUI(this.gameObject).GetComponent<UIAnimator>().RegisterAnimatable(this, animationType);
                }
            }
            else
            {
                if (FindBaseUI(this.gameObject) != null)
                {
                    FindBaseUI(this.gameObject).GetComponent<UIAnimator>().UnRegisterAnimatable(this, animationType);
                }
            }

        }
    }
}