namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    public class Transition
    {
        public float duration;
        public SlicePanel[] slicePanels;

        public Transition()
        {
            this.duration = 0f;
            this.slicePanels = new SlicePanel[0];
        }
        
        public Transition(float duration,SlicePanel[] slicePanels)
        {
            this.duration = duration;
            this.slicePanels = slicePanels;
        }
        public virtual void OnAnimationStarted()
        {
            
        }
        public virtual void OnAnimationRunning(float percentage)
        {
            
        }
        public virtual void OnAnimationEnded()
        {
            
        }
    }
}