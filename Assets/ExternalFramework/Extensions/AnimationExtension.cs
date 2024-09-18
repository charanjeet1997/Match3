/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class AnimationExtension
{
    public static void AnimationToggle (this Animation animation, AnimationClip clip1, AnimationClip clip2) {
        if (animation.clip == clip1) {
            animation.clip = clip2;
        } else {
            animation.clip = clip1;
        }
        animation.Play ();
    }
    
    public static void ResetAllTriggers(this Animator animator)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }
}