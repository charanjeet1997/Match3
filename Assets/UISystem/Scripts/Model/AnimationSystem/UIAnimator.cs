using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ModulerUISystem
{
    [System.Serializable]
    public class AnimationEvent
    {
        public float time;
        public UnityEvent Event;
        public bool isCalled=false;
    }
    public enum UIAnimationType
    {
        Show,
        Hide
    }
    public class UIAnimator : MonoBehaviour
    {    
        public List<Animatable> showAnimatableUI = new List<Animatable>();
        public List<Animatable> hideAnimatableUI = new List<Animatable>();
        public virtual void RegisterAnimatable(Animatable animatable, UIAnimationType animationType)
        {
            if (animationType == UIAnimationType.Show && !showAnimatableUI.Contains(animatable))
            {
                showAnimatableUI.Add(animatable);
            }
            if(animationType == UIAnimationType.Hide && !hideAnimatableUI.Contains(animatable))
            {
                hideAnimatableUI.Add(animatable);
            }
        }
        public virtual void UnRegisterAnimatable(Animatable animatable, UIAnimationType animationType)
        {
            if (animationType == UIAnimationType.Show && showAnimatableUI.Contains(animatable))
            {
                showAnimatableUI.Remove(animatable);
            }
            if(animationType == UIAnimationType.Hide && hideAnimatableUI.Contains(animatable))
            {
                hideAnimatableUI.Remove(animatable);
            }
        }

        private void OnDisable()
        {
            StopHide();
            StopShow();
        }

        public void StartShow()
        {
            StopAllCoroutines();
            for (int i = 0; i < showAnimatableUI.Count; i++)
            {
                showAnimatableUI[i].StartAnimate();
            }
        }
   
        public void StartHide()
        {
            for (int i = 0; i < hideAnimatableUI.Count; i++)
            {
                hideAnimatableUI[i].StartAnimate();
            }
        }
        
        public void StopShow()
        {
            for (int counter = 0; counter < showAnimatableUI.Count; counter++)
            {
                showAnimatableUI[counter].StopAnimate();
            }
        }
        
        public void StopHide()
        {
            for (int counter = 0; counter < hideAnimatableUI.Count; counter++)
            {
                hideAnimatableUI[counter].StopAnimate();
            }
        }
    }
}